using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DS32017_WeedHackers.Attributes;
using DS32017_WeedHackers.Enums;
using DS32017_WeedHackers.Models.ApplicationModels;
using DS32017_WeedHackers.Models.EmployeeModels.Admin;
using DS32017_WeedHackers.Models.EmployeeModels.Employee;
using Vereyon.Web;
using WeedHackers_Data;
using WeedHackers_Data.Entities;
using WeedHackers_Data.UserTypes;

namespace DS32017_WeedHackers.Controllers.Admin
{
    [WeedHackersAuth(UserAuthType.Admin)]
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            HttpCookie WeedHackSesh = System.Web.HttpContext.Current.Request.Cookies["WeedHackersSession"];

            var model = new AdminModel();

            model.AdminUser = MvcApplication.Sessions[WeedHackSesh.Value].User;
            model.AdminEmployee = WeedHackersContext.Employees.ToList().Find(u => u.Id == model.AdminUser.Id);

            #region NavigationsForCustomer
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("Index", "Admin"),
                Title = "Profile Information",
                Icon = "fa fa-user-circle-o",
                Class = "active"
            });
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("AddEmployee", "Admin"),
                Title = "Add Employee",
                Icon = "fa fa-user-plus",
                Class = ""
            });
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("AllEmployees", "Admin"),
                Title = "Remove Employees",
                Icon = "fa fa-user-times",
                Class = ""
            });
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("AllCustomers", "Admin"),
                Title = "Remove Customers",
                Icon = "fa fa-user-times",
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

        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> UpdateDetails(AdminModel model)
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
            return RedirectToAction("Index");
        }

        //public async Task<ActionResult> AddEmployee() { }
        public async Task<ActionResult> AllEmployees(AdminModel model)
        {
            model.AllEmployees = await WeedHackersContext
                .Employees
                .Include(et => et.EmployeeType)
                .Include(d => d.Department)
                .Include(u => u.User)
                .Where(et => et.EmployeeType.Name != "Admin" && !et.Deleted)
                .ToListAsync();



            #region NavigationsForCustomer

            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("Index", "Admin"),
                Title = "Profile Information",
                Icon = "fa fa-user-circle-o",
                Class = "active"
            });
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("AddEmployee", "Admin"),
                Title = "Add Employee",
                Icon = "fa fa-user-plus",
                Class = ""
            });
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("AllEmployees", "Admin"),
                Title = "Remove Employees",
                Icon = "fa fa-user-times",
                Class = ""
            });
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("AllCustomers", "Admin"),
                Title = "Remove Customers",
                Icon = "fa fa-user-times",
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
        public async Task<ActionResult> AllCustomers(AdminModel model)
        {
            model.AllCustomers = await WeedHackersContext
                .Customers
                .Include(ct => ct.CustomerType)
                .Include(u => u.User)
                .Where(u => !u.Deleted)
                .ToListAsync();

            #region NavigationsForCustomer

            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("Index", "Admin"),
                Title = "Profile Information",
                Icon = "fa fa-user-circle-o",
                Class = "active"
            });
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("AddEmployee", "Admin"),
                Title = "Add Employee",
                Icon = "fa fa-user-plus",
                Class = ""
            });
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("AllEmployees", "Admin"),
                Title = "Remove Employees",
                Icon = "fa fa-user-times",
                Class = ""
            });
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("AllCustomers", "Admin"),
                Title = "Remove Customers",
                Icon = "fa fa-user-times",
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

        [HttpGet]
        public ActionResult DeleteEmployee(int Id, int uId)
        {
            WeedHackersContext
                .Users
                .Single(u => u.Id == Id)
                .Deleted = true;
            WeedHackersContext.SaveChanges();
            WeedHackersContext
              .Users
              .Single(u => u.Id == uId)
              .Deleted = true;
            return RedirectToAction("AllEmployees", "Admin");
        }
        [HttpGet]
        public ActionResult DeleteCustomer(int cId, int uId)
        {
            WeedHackersContext
                .Customers
                .Single(u => u.Id == cId)
                .Deleted = true;

            WeedHackersContext
               .Users
               .Single(u => u.Id == uId)
               .Deleted = true;

            WeedHackersContext.SaveChanges();
            return RedirectToAction("AllCustomers", "Admin");
        }

        public ActionResult AddEmployee(AdminModel model)
        {
            HttpCookie WeedHackSesh = System.Web.HttpContext.Current.Request.Cookies["WeedHackersSession"];

            model.AdminUser = MvcApplication.Sessions[WeedHackSesh.Value].User;
            model.AllDepartments = WeedHackersContext.Departments.ToList();
            model.AllEmployeeTypes = WeedHackersContext.EmployeeTypes.ToList();


            #region NavigationsForCustomer

            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("Index", "Admin"),
                Title = "Profile Information",
                Icon = "fa fa-user-circle-o",
                Class = "active"
            });
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("AddEmployee", "Admin"),
                Title = "Add Employee",
                Icon = "fa fa-user-plus",
                Class = ""
            });
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("AllEmployees", "Admin"),
                Title = "Remove Employees",
                Icon = "fa fa-user-times",
                Class = ""
            });
            model.SideNavModels.Add(new NavigationModel
            {
                Link = Url.Action("AllCustomers", "Admin"),
                Title = "Remove Customers",
                Icon = "fa fa-user-times",
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
        [HttpPost]
        public async Task<ActionResult> NewEmployee(AdminModel model)
        {
            model.AllDepartments = WeedHackersContext.Departments.ToList();
            model.AllEmployeeTypes = WeedHackersContext.EmployeeTypes.ToList();
            var cryptionHelper = new FrostAura.Dynamics.Core.Helpers.FaCryptographyHelper();

            var userCheck = WeedHackersContext.Users.ToList().Find(u => u.Email == model.NewUser.Email);

            if (ModelState.IsValid)
            {
                if (userCheck != null)
                {
                    ModelState.AddModelError("Email", "Email already exists! Please use a different email address");
                    FlashMessage.Danger("Invalid Email", "Email already exists! Please use a different email address");
                    return View("AddEmployee", model);
                }
                var UserEmp = new User
                {
                    Name = model.NewUser.Name,
                    Surname = model.NewUser.Surname,
                    Email = model.NewUser.Email,
                    Password = cryptionHelper.HashString(model.NewUser.Password),
                    PhoneNumber = model.NewUser.PhoneNumber,
                    SuperAdmin = false,
                    Deleted = false
                };
                var newEmployee = new WeedHackers_Data.Entities.Employee
                {
                    Id = UserEmp.Id,
                    EmployeeTypeId = model.NewEmployee.EmployeeTypeId,
                    DepartmentId = model.NewEmployee.DepartmentId,
                    Deleted = false,
                    Timestamp = DateTime.Now
                };

                WeedHackersContext.Users.Add(UserEmp);
                WeedHackersContext.Employees.Add(newEmployee);
                await WeedHackersContext.SaveChangesAsync();
                return RedirectToAction("AllEmployees");
            }
            ModelState.AddModelError("Name", "The Registration process could not be completed! Please ensure you have filled out the form correctly and try again");
            FlashMessage.Danger("Name", "The Registration process could not be completed! Please ensure you have filled out the form correctly and try again");
            return View("AddEmployee", model);
        }
    }
}