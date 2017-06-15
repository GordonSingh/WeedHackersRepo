using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
using DS32017_WeedHackers.Models.EmployeeModels.Employee;
using DS32017_WeedHackers.Models.EmployeeModels.Manager;
using DS32017_WeedHackers.Models.HelperModels;
using DS32017_WeedHackers.Models.ServiceModels;
using Vereyon.Web;
using WeedHackers_Data;
using WeedHackers_Data.Entities;
using WeedHackers_Data.ServiceProcess;

namespace DS32017_WeedHackers.Controllers.Employee
{
    [WeedHackersAuth(UserAuthType.Employee)]
    public class EmployeeController : BaseController
    {
        // GET: Employee
        public async Task<ActionResult> Index()
        {
            var WeedHackSesh = System.Web.HttpContext.Current.Request.Cookies["WeedHackersSession"].Value;

            var model = new EmployeeInformationModel();

            var currUser = MvcApplication.Sessions[WeedHackSesh].User;
            model.CurrentEmpUser = currUser;
            model.CurrentEmployee = currUser.Employee;


            model.EmpServiceRequests = await WeedHackersContext
                .ServiceRequests
                .Include(s => s.Service)
                .Include(s => s.Customer)
                .Include(ss => ss.ServiceRequestStatusUpdates.Select(srsu => srsu.ServiceStatus))//select in this context is a nested include
                .Where(s => s.ServiceAdvisorId == currUser.Employee.Id && !s.Deleted && !s.IsResolved && s.ServiceRequestStatusUpdates.Any())//.Any() to cater for service requests without any status
                .ToListAsync();

            #region NavigationsForServiceAdvisorEmployee

            if (model.CurrentEmployee.EmployeeType.Name == "Employee")
            {
                model.SideNavModels.Add(new NavigationModel
                {
                    Link = Url.Action("EmployeeHome", "Employee"),
                    Title = "Profile Information",
                    Icon = "fa fa-user",
                    Class = "active"
                });
            }
            if (model.CurrentEmployee.EmployeeType.Name == "Manager")
            {
                model.SideNavModels.Add(new NavigationModel
                {
                    Link = Url.Action("EmployeeHome", "Employee"),
                    Title = "Profile Information",
                    Icon = "fa fa-user",
                    Class = "active"
                });
            }
            if (model.CurrentEmployee.EmployeeType.Name == "Service Advisor")
            {
                model.SideNavModels.Add(new NavigationModel
                {
                    Link = Url.Action("ManagerHome", "Employee"),
                    Title = "Profile Information",
                    Icon = "fa fa-user",
                    Class = "active"
                });
            }
            //Navigation for an Employee
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("Index", "Employee"),
                Title = "Open Request",
                Icon = "fa fa-plus",
                Class = "active"
            });

      

            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("Logout", "Security"),
                Title = "Sign Out",
                Icon = "fa fa-plus",
                Class = "active"
            });
            #endregion

            return View(model);
        }

        public async Task<ActionResult> EmployeeHome()
        {
            HttpCookie WeedHackSesh = System.Web.HttpContext.Current.Request.Cookies["WeedHackersSession"];
            var model = new EmployeeInformationModel();
            var currUser = MvcApplication.Sessions[WeedHackSesh.Value].User;
            model.CurrentEmpUser = currUser;
            model.CurrentEmployee = currUser.Employee;
            model.CurrentEmployee.Department = WeedHackersContext.Departments.Single(d => d.Id == currUser.Employee.DepartmentId);

            #region NavigationsForDepartmentEmployee

            //Navigation for an Employee
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("EmployeeHome", "Employee"),
                Title = "Profile Information",
                Icon = "fa fa-user-circle-o",
                Class = "active"
            });
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("EmployeeJobs", "Employee"),
                Title = "View Jobs",
                Icon = "fa fa-file-text",
                Class = ""
            });
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("Logout", "Security"),
                Title = "Sign Out",
                Icon = "fa fa-plus",
                Class = ""
            });
            #endregion
            return View(model);
        }

        public async Task<ActionResult> EmployeeJobs(EmployeeInformationModel model)
        {
            HttpCookie WeedHackSesh = System.Web.HttpContext.Current.Request.Cookies["WeedHackersSession"];
          
            var currUser = MvcApplication.Sessions[WeedHackSesh.Value].User;

            model.EmpServiceRequests = WeedHackersContext
               .ServiceRequests
               .Include(srsu => srsu.ServiceRequestStatusUpdates.Select(sv => sv.ServiceStatus))
               .Include(s => s.Service)
               .Include(e => e.LeadEmployee.User)
               .Include(ea => ea.AssignedEmployees)
               .Include(c => c.Customer)
               .Where(sr => sr.ServiceRequestStatusUpdates.Any(srsu => srsu.ServiceStatus.Name == "In Progress") && sr.Service.Department.Id == currUser.Employee.DepartmentId)
               .ToList();

            #region NavigationsForDepartmentEmployee

            //Navigation for an Employee
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("EmployeeHome", "Employee"),
                Title = "Profile Information",
                Icon = "fa fa-user-circle-o",
                Class = ""
            });
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("EmployeeJobs", "Employee"),
                Title = "View Jobs",
                Icon = "fa fa-file-text",
                Class = "active"
            });
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("Logout", "Security"),
                Title = "Sign Out",
                Icon = "fa fa-plus",
                Class = ""
            });
            #endregion

            return View(model);
        }


        public async Task<ActionResult> ManagerHome()
        {
            HttpCookie WeedHackSesh = System.Web.HttpContext.Current.Request.Cookies["WeedHackersSession"];
            var model = new EmployeeInformationModel();
            var currUser = MvcApplication.Sessions[WeedHackSesh.Value].User;

            model.CurrentEmpUser = currUser;
            model.CurrentEmployee = currUser.Employee;
            model.CurrentEmployee.Department = WeedHackersContext.Departments.Single(d => d.Id == currUser.Employee.DepartmentId);




            #region NavigationsForDepartmentManager

            //Navigation for an Employee
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("ManagerHome", "Employee"),
                Title = "Profile Information",
                Icon = "fa fa-user-circle-o",
                Class = "active"
            });
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("QuotesForManager", "Employee"),
                Title = "Accepted Quotes",
                Icon = "fa fa-file-text",
                Class = ""
            });
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("Logout", "Security"),
                Title = "Sign Out",
                Icon = "fa fa-plus",
                Class = ""
            });
            #endregion
            return View(model);
        }
        public async Task<ActionResult> QuotesForManager()
        {
            HttpCookie WeedHackSesh = System.Web.HttpContext.Current.Request.Cookies["WeedHackersSession"];
            var model = new EmployeeInformationModel();
            var currUser = MvcApplication.Sessions[WeedHackSesh.Value].User;

            model.EmpServiceRequests = WeedHackersContext
                .ServiceRequests
                .Include(srsu=>srsu.ServiceRequestStatusUpdates.Select(sv=>sv.ServiceStatus))
                .Include(s=>s.Service)
                .Include(e=>e.LeadEmployee)
                .Include(ea=>ea.AssignedEmployees)
                .Include(c=>c.Customer)
                .Where(sr => sr.ServiceRequestStatusUpdates.Any(srsu => srsu.ServiceStatus.Name == "Accepted") && sr.Service.Department.ManagerId==currUser.Id)
                .ToList();

            #region NavigationsForDepartmentManager

            //Navigation for an Employee
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("ManagerHome", "Employee"),
                Title = "Profile Information",
                Icon = "fa fa-user-circle-o",
                Class = ""
            });
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("QuotesForManager", "Employee"),
                Title = "Accepted Quotes",
                Icon = "fa fa-file-text",
                Class = "active"
            });
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("Logout", "Security"),
                Title = "Sign Out",
                Icon = "fa fa-plus",
                Class = ""
            });
            #endregion
            return View(model);
        }

        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> QuotesManagement(int Id)
        {
            ManagerQuoteManagmentModel model=new ManagerQuoteManagmentModel();

            var WeedHackSesh = System.Web.HttpContext.Current.Request.Cookies["WeedHackersSession"].Value;
            var UserDetails = MvcApplication.Sessions[WeedHackSesh].User;
            var updateQuote = await WeedHackersContext
                .ServiceRequests
                .Include(srsu => srsu.ServiceRequestStatusUpdates.Select(sv => sv.ServiceStatus))
                .Include(s => s.Service)
                .Include(e => e.LeadEmployee)
                .Include(ea => ea.AssignedEmployees)
                .Include(c => c.Customer)
                .SingleAsync(sr => sr.Id == Id);

            model.ServiceRequest = updateQuote;
            model.Employees =
                await WeedHackersContext.Employees.Where(
                    e => !e.Deleted && e.DepartmentId == updateQuote.Service.DepartmentId && e.EmployeeType.Name!="Manager" && e.EmployeeType.Name!="Service Advisor").ToListAsync();
           
            #region NavigationsForDepartmentManager

            //Navigation for an Employee
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("ManagerHome", "Employee"),
                Title = "Profile Information",
                Icon = "fa fa-user-circle-o",
                Class = ""
            });
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("QuotesForManager", "Employee"),
                Title = "Accepted Quotes",
                Icon = "fa fa-file-text",
                Class = "active"
            });
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("Logout", "Security"),
                Title = "Sign Out",
                Icon = "fa fa-plus",
                Class = ""
            });
            #endregion

            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> UpdateDetails(EmployeeInformationModel model)
        {
            HttpCookie WeedHackSesh = System.Web.HttpContext.Current.Request.Cookies["WeedHackersSession"];
            var UserDetails = MvcApplication.Sessions[WeedHackSesh.Value].User;
            var EmployeeDetails = WeedHackersContext.Employees.ToList().Find(u => u.Id == UserDetails.Id);

            if (ModelState.IsValid)
            {
                var cryptionHelper = new FrostAura.Dynamics.Core.Helpers.FaCryptographyHelper();

                if (model.email == null)
                {
                    model.email = UserDetails.Email;
                }
                UserDetails.Email = model.email;

                if (model.password == null)
                {
                    model.password = UserDetails.Password;
                }
                UserDetails.Password = cryptionHelper.HashString(model.password);

                if (model.phonenumber == null)
                {
                    model.phonenumber = UserDetails.PhoneNumber;
                }
                UserDetails.PhoneNumber = model.phonenumber;


                WeedHackersContext.Users.AddOrUpdate(u => u.Id, UserDetails);
                WeedHackersContext.Employees.AddOrUpdate(c => c.Id, EmployeeDetails);

                await WeedHackersContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(model.phonenumber, "Could not add!");
            FlashMessage.Danger("Update Unsuccessful", "We could not update your profile. Please ensure you have filled out your new details correctly and try again.");
            if (EmployeeDetails.EmployeeType.Name == "Employee")
            {
                return RedirectToAction("EmployeeHome");
            }
            else if (EmployeeDetails.EmployeeType.Name == "Manager")
            {
                return RedirectToAction("ManagerHome");
            }
            else
            {
                return RedirectToAction("Index");
            }

            //===============================================================
        }

        [System.Web.Mvc.HttpGet]
        public async Task<ActionResult> EditInspection(InspectionModel model)
        {
            model = new InspectionModel();
            model.ServiceRequest = new ServiceRequestModel();
            model.SideNavModels = new List<NavigationModel>();

            #region NavigationsForCustomer

            //Navigation for an Employee
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("Index", "Employee"),
                Title = "Dashboard",
                Icon = "fa fa-plus",
                Class = "active"
            });
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("Logout", "Security"),
                Title = "Sign Out",
                Icon = "fa fa-plus",
                Class = "active"
            });
            #endregion

            var serviceId = Request
                .Params
                .GetValues("id")[0];
            var requestId = int.Parse(serviceId);
            var dbServiceRequest = await WeedHackersContext
                .ServiceRequests
                .Include(ct => ct.Customer.CustomerType)
                .Include(s => s.Service)
                .Include(d => d.Service.Department)
                .SingleAsync(s => s.Id == requestId);

            dbServiceRequest.UnitQuantity = model.ServiceRequest.ServiceRequest.UnitQuantity;

            model.ServiceRequest.ServiceRequest = dbServiceRequest;

            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> SendQuote(InspectionModel model)
        {
            EmailHelper send = new EmailHelper();

            try
            {
                var dbServiceRequest = await WeedHackersContext
                     .ServiceRequests
                     .Include(ct => ct.Customer.CustomerType)
                     .Include(ct => ct.Customer.User)
                     .Include(s => s.Service)
                     .Include(d => d.Service.Department)
                     .Include(d => d.ServiceAdvisor.User)
                     .Include(ss => ss.ServiceRequestStatusUpdates.Select(srsu => srsu.ServiceStatus))
                     .SingleAsync(s => s.Id == model.ServiceRequest.ServiceRequest.Id);


                double subtotal = dbServiceRequest.Service.PricePerUnit *
                               (double)model.ServiceRequest.ServiceRequest.UnitQuantity;
                double Total = subtotal * 1.14;
                dbServiceRequest.UnitQuantity = model.ServiceRequest.ServiceRequest.UnitQuantity;

                var serviceStatus = await WeedHackersContext.ServiceStatuses.SingleAsync(ss => ss.Name == "Inspected");
                ServiceRequestStatusUpdate serviceRequestStatusUpdate = new ServiceRequestStatusUpdate
                {
                    ServiceRequestId = dbServiceRequest.Id,
                    ServiceStatusId = serviceStatus.Id,
                    Message = "Service Request has been Inspected."
                };


                WeedHackersContext.ServiceRequestStatusUpdates.Add(serviceRequestStatusUpdate);
                await WeedHackersContext.SaveChangesAsync();

                var emailRequest = new EmailQuoteModel();

                emailRequest.Email = dbServiceRequest.ServiceAdvisor.User.Email;
                emailRequest.QuotationNumber = dbServiceRequest.Id.ToString();
                emailRequest.ServiceType = dbServiceRequest.Service.ServiceName;
                emailRequest.Tell = dbServiceRequest.ServiceAdvisor.User.PhoneNumber;
                emailRequest.UnitQuantity = dbServiceRequest.UnitQuantity.ToString();
                emailRequest.UnitPrice = dbServiceRequest.Service.PricePerUnit.ToString("R0.00");
                emailRequest.UnitSubtotal = subtotal.ToString("R0.00");
                emailRequest.UnitTotal = Total.ToString("R0.00");
                emailRequest.EmailFormModel = new EmailFormModel
                {
                    Recipient = dbServiceRequest.Customer.User.Email,
                    Message = ""
                };

                await send.SendQuotationEmail(emailRequest);
                FlashMessage.Confirmation("Inspection Complete", "Quote of inspection has been sent to the customer.");
                return RedirectToAction("Index", "Employee");
            }
            catch (Exception)
            {

                FlashMessage.Danger("Inspection Error", "There was a problem processing the inspection.");
                return View("Index");
            }
        }

        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> AssignEmployeedToQuote(ManagerQuoteManagmentModel request)
        {
            var serviceRequest = await WeedHackersContext
                .ServiceRequests
                .SingleAsync(sr => sr.Id == request.ServiceRequest.Id);

            serviceRequest.LeadEmployeeId = request.ServiceRequest.LeadEmployeeId;

            foreach (int requestAssignedEmployeeId in request.AssignedEmployeeIds)
            {
                serviceRequest.AssignedEmployees.Add(await WeedHackersContext.Employees.SingleAsync(e => e.Id == requestAssignedEmployeeId));
            }

            WeedHackersContext.ServiceRequests.AddOrUpdate(sr => sr.Id, serviceRequest);
            var serviceStatus = await WeedHackersContext.ServiceStatuses.SingleAsync(ss => ss.Name == "In Progress");
            var serviceStatusUpdate=new ServiceRequestStatusUpdate
            {
                ServiceRequestId = serviceRequest.Id,
                Message = "The Job is assigned and is in progress",
                ServiceStatusId = serviceStatus.Id
            };


            WeedHackersContext.ServiceRequestStatusUpdates.Add(serviceStatusUpdate); 
            await WeedHackersContext.SaveChangesAsync();
            return RedirectToAction("QuotesManagement", new {Id = request.ServiceRequest.Id});
        }
    }
}