using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using DS32017_WeedHackers.Attributes;
using DS32017_WeedHackers.Enums;
using DS32017_WeedHackers.Helpers;
using DS32017_WeedHackers.Models.ApplicationModels;
using DS32017_WeedHackers.Models.CustomerModels;
using DS32017_WeedHackers.Models.HelperModels;
using DS32017_WeedHackers.Models.ServiceModels;
using Vereyon.Web;
using WeedHackers_Data.Entities;
using WeedHackers_Data.ServiceProcess;

namespace DS32017_WeedHackers.Controllers.Customer
{
    [WeedHackersAuth(UserAuthType.Customer)]
    public class CustomerController : BaseController
    {
        // GET: Customer
        public ActionResult Index()
        {
            var WeedHackSesh = System.Web.HttpContext.Current.Request.Cookies["WeedHackersSession"].Value;
            var sessionModel = MvcApplication.Sessions[WeedHackSesh];
            var model = new CustomerInformationModel();

            model.CurrentCustUser = sessionModel.User;

            model.ServiceRequestModel = new ServiceRequestModel();
            model.ServiceRequestModel.Services = WeedHackersContext.Services.ToList();
            model.Address = sessionModel.User.Customer.Address;



            //model.CurrentCustomer =
            //    WeedHackersContext.Customers.ToList().Find(u => u.UserId == model.CurrentCustUser.Id);
            //model.CurreCustomerType =
            //    WeedHackersContext.CustomerTypes.ToList().Find(ct => ct.Id == model.CurrentCustomer.CustomerTypeId);
            #region NavigationsForCustomer
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("CustomerProfile", "Customer"),
                Title = "Profile Information",
                Icon = "fa fa-user-circle-o",
                Class = ""
            });
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("Index", "Customer"),
                Title = "New Service Request",
                Icon = "fa fa-plus",
                Class = "active"
            });
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("QuotesReceived", "Customer"),
                Title = "View Quotes",
                Icon = "fa fa-plus",
                Class = ""
            });
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("Logout", "Security"),
                Icon = "fa fa-sign-out",
                Title = "Sign Out"
            });

            #endregion

            return View(model);
        }

        public async Task<ActionResult> CustomerProfile()
        {
            var WeedHackSesh = System.Web.HttpContext.Current.Request.Cookies["WeedHackersSession"].Value;
            var model = new CustomerInformationModel();
            var sessionModel = MvcApplication.Sessions[WeedHackSesh];

            model.CurrentCustUser = sessionModel.User;
            model.CurrentCustomer = sessionModel.User.Customer;
            model.CurrentCustomer.CustomerType = WeedHackersContext.CustomerTypes.Single(c => c.Id == sessionModel.User.Customer.CustomerTypeId);

            #region NavigationsForCustomer
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("CustomerProfile", "Customer"),
                Title = "Profile Information",
                Icon = "fa fa-user-circle-o",
                Class = "active"
            });
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("Index", "Customer"),
                Title = "New Request Service",
                Icon = "fa fa-plus",
                Class = ""
            });
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("QuotesReceived", "Customer"),
                Title = "View Quotes",
                Icon = "fa fa-file-text",
                Class = ""
            });
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("Logout", "Security"),
                Icon = "fa fa-sign-out",
                Title = "Sign Out"
            });

            #endregion


            return View(model);
        }

        //=================================
        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> UpdateDetails(CustomerInformationModel model)
        {
            HttpCookie WeedHackSesh = System.Web.HttpContext.Current.Request.Cookies["WeedHackersSession"];
            var UserDetails = MvcApplication.Sessions[WeedHackSesh.Value].User;
            var CustomerDetails = WeedHackersContext.Customers.ToList().Find(u => u.Id == UserDetails.Id);

            if (ModelState.IsValid)
            {
                var cryptionHelper = new FrostAura.Dynamics.Core.Helpers.FaCryptographyHelper();
                UserDetails.Email = model.email;
                if (model.password == "")
                {
                    UserDetails.Password = UserDetails.Password;
                }
                UserDetails.Password = cryptionHelper.HashString(model.password);
                UserDetails.PhoneNumber = model.phonenumber;
                CustomerDetails.Address = model.Address;

                WeedHackersContext.Users.AddOrUpdate(u => u.Id, UserDetails);
                WeedHackersContext.Customers.AddOrUpdate(c => c.Id, CustomerDetails);

                await WeedHackersContext.SaveChangesAsync();
                FlashMessage.Confirmation("Profile Information", "Your information has been updated.");
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("Email", "Could not update details!");
            FlashMessage.Danger("Update Unsuccessful", "We could not update your profile. Please ensure you have filled out your new details correctly and try again.");
            return View("CustomerProfile", model);
            //===============================================================
        }

        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> RequestService(CustomerInformationModel model)
        {
            var WeedHackSesh = System.Web.HttpContext.Current.Request.Cookies["WeedHackersSession"].Value;
            var UserDetails = MvcApplication.Sessions[WeedHackSesh].User;
            var CustomerDetails = WeedHackersContext.Customers.ToList().Find(u => u.Id == UserDetails.Id);
            ServiceRequestStatusUpdate ServiceRequestStatus;
            CustomerDetails.Address = model.Address;

            WeedHackersContext.Customers.AddOrUpdate(c => c.Id, CustomerDetails);

            if (ModelState.IsValid)
            {
                model.ServiceRequestModel.ServiceRequest.CustomerId = CustomerDetails.Id;

                WeedHackersContext.ServiceRequests.Add(model.ServiceRequestModel.ServiceRequest);
                ServiceRequestStatus = new ServiceRequestStatusUpdate
                {
                    ServiceRequestId = model.ServiceRequestModel.ServiceRequest.Id,
                    ServiceStatusId = WeedHackersContext.ServiceStatuses.Single(s => s.Name == "Created").Id,
                    Message = "Service Requested has been created",

                };
                WeedHackersContext.ServiceRequestStatusUpdates.Add(ServiceRequestStatus);
                // Magic
                WeedHackers_Data.Entities.Service service =
                    WeedHackersContext
                    .Services
                    .Single(s => s.Id == model.ServiceRequestModel.ServiceRequest.ServiceId);

                var assigningEmployee = WeedHackersContext
                    .Employees
                    .Include(e => e.EmployeeType)
                    .Where(e => e.DepartmentId == service.DepartmentId && e.EmployeeType.Name == "Service Advisor")
                    .Include(e => e.AssignedServiceRequests)
                    .OrderByDescending(e => e.AssignedServiceRequests.Count)
                    .First();

                model.ServiceRequestModel.ServiceRequest.ServiceAdvisorId = assigningEmployee.Id;

                await WeedHackersContext.SaveChangesAsync();
            }
            FlashMessage.Confirmation("Service Requested", "Your service has successfully been requested and is awaiting inspection.");
            return RedirectToAction("Index", "Customer");
        }

        public async Task<ActionResult> QuotesReceived(CustomerQuoteModel model)
        {
            var WeedHackSesh = System.Web.HttpContext.Current.Request.Cookies["WeedHackersSession"].Value;
            var sessionModel = MvcApplication.Sessions[WeedHackSesh];

            model.CustomerQuotes = WeedHackersContext
                  .ServiceRequests
                  .Include(s => s.Service)
                  .Include(em => em.ServiceAdvisor)
                  .Include(ss => ss.ServiceRequestStatusUpdates.Select(srsu => srsu.ServiceStatus))
                  .Where(sr => sr.Customer.User.Id == sessionModel.User.Id && sr.ServiceRequestStatusUpdates.Any() && !sr.Deleted)
                  .ToList();

            foreach (var Service in model.CustomerQuotes)
            {
                Service.Total = (Service.UnitQuantity * Service.Service.PricePerUnit) * 1.14;
            }

            #region NavigationsForCustomer
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("CustomerProfile", "Customer"),
                Title = "Profile Information",
                Icon = "fa fa-user-circle-o",
                Class = ""
            });
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("Index", "Customer"),
                Title = "New Service Request",
                Icon = "fa fa-plus",
                Class = ""
            });
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("QuotesReceived", "Customer"),
                Title = "View Quotes",
                Icon = "fa fa-file-text",
                Class = "active"
            });

            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("Logout", "Security"),
                Icon = "fa fa-sign-out",
                Title = "Sign Out"
            });

            #endregion

            return View(model);
        }

        public async Task<ActionResult> QuoteApproval(int Id)
        {
            var WeedHackSesh = System.Web.HttpContext.Current.Request.Cookies["WeedHackersSession"].Value;
            var UserDetails = MvcApplication.Sessions[WeedHackSesh].User;
            var serviceRequest = WeedHackersContext.ServiceRequests
                .Include(sa => sa.ServiceAdvisor.User)
                .Include(md=>md.Service.Department.ManagingEmployee.User)
                .Single(sr => sr.Id == Id);
            

            var emailmodel = new EmailFormModel
            {
                Message = UserDetails.Name + " " + UserDetails.Surname + " has Accepted Quote #" + serviceRequest.Id + " <br/>",
                Recipient = serviceRequest.ServiceAdvisor.User.Email
            };
            var emailmodelManager = new EmailFormModel
            {
                Message = UserDetails.Name + " " + UserDetails.Surname + " has Accepted Quote #" + serviceRequest.Id + " <br/>",
                Recipient = serviceRequest.Service.Department.ManagingEmployee.User.Email
            };


            var ServiceRequestStatus = new ServiceRequestStatusUpdate
            {
                ServiceRequestId = serviceRequest.Id,
                ServiceStatusId = WeedHackersContext.ServiceStatuses.Single(s => s.Name == "Accepted").Id,
                Message = "Service Requested has been Accepted",
            };

            WeedHackersContext.ServiceRequests.AddOrUpdate(sr => sr.Id, serviceRequest);
            WeedHackersContext.ServiceRequestStatusUpdates.Add(ServiceRequestStatus);

            await WeedHackersContext.SaveChangesAsync();
            var email = new EmailHelper();
            await email.SendEmail(emailmodel);
            FlashMessage.Confirmation("Quote Rejected", "");
            return RedirectToAction("QuotesReceived", "Customer");

          
        }
        public async Task<ActionResult> QuoteDecline(int Id)
        {
            var WeedHackSesh = System.Web.HttpContext.Current.Request.Cookies["WeedHackersSession"].Value;
            var UserDetails = MvcApplication.Sessions[WeedHackSesh].User;
            var serviceRequest = WeedHackersContext.ServiceRequests
                .Include(sa => sa.ServiceAdvisor.User)
                .Single(sr => sr.Id == Id);

            var emailmodel = new EmailFormModel
            {
                Message = UserDetails.Name + " " + UserDetails.Surname + " has declined Quote #" + serviceRequest.Id + " issued to him <br/>",
                Recipient = serviceRequest.ServiceAdvisor.User.Email
            };

            var ServiceRequestStatus = new ServiceRequestStatusUpdate
            {
                ServiceRequestId = serviceRequest.Id,
                ServiceStatusId = WeedHackersContext.ServiceStatuses.Single(s => s.Name == "Rejected").Id,
                Message = "Service Requested has been rejected",
            };
            WeedHackersContext.ServiceRequests.AddOrUpdate(sr => sr.Id, serviceRequest);
            WeedHackersContext.ServiceRequestStatusUpdates.Add(ServiceRequestStatus);

            await WeedHackersContext.SaveChangesAsync();
            var email = new EmailHelper();
            await email.SendEmail(emailmodel);
            FlashMessage.Confirmation("Quote Rejected", "");
            return RedirectToAction("QuotesReceived", "Customer");
        }
    }
}