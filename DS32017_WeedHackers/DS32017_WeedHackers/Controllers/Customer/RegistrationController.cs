using System;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DS32017_WeedHackers.Models.ApplicationModels;
using DS32017_WeedHackers.Models.Authentication;
using Vereyon.Web;
using WeedHackers_Data.Entities;

namespace DS32017_WeedHackers.Controllers.Customer
{
    public class RegistrationController : BaseController
    {
       
        // GET: Registration
        public ActionResult Index()
        { 
            var baseModel = new CustomerRegistrationModel();

            SetUpNavItems(baseModel);

            return View(baseModel);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Register(CustomerRegistrationModel model)
        {
            var cryptionHelper = new FrostAura.Dynamics.Core.Helpers.FaCryptographyHelper();
           
            if (ModelState.IsValid)
            {
                var existingUser = WeedHackersContext.Users.FirstOrDefault(u => u.Email == model.RegistringUser.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Email already registered");
                    FlashMessage.Danger("{0} is already registered. Please use a different valid email address to register",model.RegistringUser.Email);
                }

                var User = new User
                {
                    Name = model.RegistringUser.Name,
                    Surname = model.RegistringUser.Surname,
                    Email = model.RegistringUser.Email,
                    Password = cryptionHelper.HashString(model.RegistringUser.Password),
                    PhoneNumber = model.RegistringUser.PhoneNumber,
                    Deleted = false,
                    Timestamp = DateTime.Now,
                    SuperAdmin = false
                };
                WeedHackersContext.Users.Add(User);

                var Customer = new WeedHackers_Data.Entities.Customer
                {
                    Id = User.Id,
                    Address = model.RegistringCustomer.Address,
                    CustomerTypeId = model.RegistringCustomer.CustomerTypeId,
                    EmailVerified = false

                };
                WeedHackersContext.Customers.Add(Customer);

                await WeedHackersContext.SaveChangesAsync();
                var userContext = await WeedHackersContext
                   .Users
                   .Include(u => u.Customer.CustomerType)
                   .Include(u => u.Customer)
                   .SingleOrDefaultAsync(u => u.Id==model.RegistringUser.Id);

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

                //var userContext = (User)ViewBag.UserContext;
                FlashMessage.Confirmation("Registration Successful!","Welcome to WeedHackers {0}",model.RegistringUser.Name);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("","Please fill in all fields and try again");
                FlashMessage.Danger("Error", "Please fill in all fields and try again");
                return View("Index", model);
            }
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