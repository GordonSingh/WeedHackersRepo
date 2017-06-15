using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DS32017_WeedHackers.Models.ApplicationModels;
using DS32017_WeedHackers.Models.Authentication;
using FrostAura.Dynamics.Core.Helpers;
using Vereyon.Web;

namespace DS32017_WeedHackers.Controllers
{
    public class SecurityController : BaseController
    {
        // GET: Security
        public ActionResult Index()
        {
            var baseModel = new LoginModel();

            SetUpNavItems(baseModel);

            return View(baseModel);
        }

        [HttpPost]
        public async Task<ActionResult> ForgotPassword()
        {

            return RedirectToAction("Index", "Security");
        }


        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {
            //async non blocking call, allows many users to use a function or for instance many users at different
            //computers can log in...sync- users can log in one at a time
            if (ModelState.IsValid)
            {
                var enteredPassword = (new FaCryptographyHelper()).HashString(model.Password);
                var user =
                    WeedHackersContext.Users.ToList().Find(u => u.Password == enteredPassword && u.Email == model.Email);
                var userContext = await WeedHackersContext
                    .Users
                    .Include(u=>u.Employee.EmployeeType)
                    .Include(u=>u.Customer)
                    .SingleOrDefaultAsync(u => u.Password == enteredPassword && u.Email == model.Email&& !u.Deleted);

                if (userContext == null)
                {
                    
                    ModelState.AddModelError("Password","Incorrect Credentials!");
                    FlashMessage.Danger("User Not Found","Please ensure you have entered the correct login details and try again");
                    return RedirectToAction("Index");
                }

              

                // Create the session
                var session = new SessionModel
                {
                    Identifier = Guid.NewGuid(), // Session unique identifier (This gets sent to the client)
                    User = userContext, // The mandatory user object a session belongs to
                    ExpiryTime = DateTime.Now.AddMinutes(20) // Session valid for 20 minutes
                };
                // Store the session on the server (As opposed to the database)
                MvcApplication.Sessions[session.Identifier.ToString()] = session;


                // Pass the session to the client via cookies (like before)
                var sessionCookie = new HttpCookie("WeedHackersSession")
                {
                    Value = session.Identifier.ToString(),
                    Expires = session.ExpiryTime,
                    HttpOnly = true
                };

                Response.Cookies.Add(sessionCookie);
                if (userContext.SuperAdmin)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (userContext.Employee != null)
                {
                    if (userContext.Employee.EmployeeType.Name == "Service Advisor")
                    {
                        return RedirectToAction("Index", "Employee");
                    }
                    if (userContext.Employee.EmployeeType.Name == "Manager")
                    {
                        return RedirectToAction("ManagerHome", "Employee");
                    }
                    if (userContext.Employee.EmployeeType.Name == "Employee")
                    {
                        return RedirectToAction("EmployeeHome", "Employee");
                    }

                }
                else
                {
                    return RedirectToAction("Index", "Customer");
                }
            }
            ModelState.AddModelError("Password","Please fill in all fields and try again");
            FlashMessage.Danger("Log in failed","Please fill in all fields and try again");
            return View("Index", model);

        }

        public ActionResult Logout()
        {
            Response.Cookies.Add(new HttpCookie("WeedHackersSession", "")
            {
                //expires yesterday
                Expires = DateTime.Now.AddDays(-1)
            });
            FlashMessage.Confirmation("You have successfully logged out.");
            return RedirectToAction("Index");

        }

        private void SetUpNavItems(BaseModel baseModel)
        {
            baseModel.NavigationModels.Add(new NavigationModel
            {
                Link = Url.Action("Index", "Home"),
                Title = "Home"
            });
            baseModel.NavigationModels.Add(new NavigationModel
            {
                Link = Url.Action("About", "Home"),
                Title = "About"
            });
            baseModel.NavigationModels.Add(new NavigationModel
            {
                Link = Url.Action("Contact", "Home"),
                Title = "Contact"
            });
            baseModel.NavigationModels.Add(new NavigationModel
            {
                Link = Url.Action("Index", "Security"),
                Title = "Sign In"
            });
            baseModel.NavigationModels.Add(new NavigationModel
            {
                Link = Url.Action("Index", "Registration"),
                Title = "Register"
            });
        }
    }
}