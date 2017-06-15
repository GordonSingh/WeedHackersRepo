using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using DS32017_WeedHackers.Models.ApplicationModels;
using WeedHackers_Data;
using WeedHackers_Data.Entities;

namespace DS32017_WeedHackers.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Your contact page.";
            var baseModel = new BaseModel();

            SetUpNavItems(baseModel);

            return View(baseModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your contact page.";
            var baseModel = new BaseModel();

            SetUpNavItems(baseModel);

            return View(baseModel);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            var baseModel=new BaseModel();

            SetUpNavItems(baseModel);

            return View(baseModel);
        }

        public ActionResult ForgotPassword([FromUri]string email,string oldPassword,string newPassword)
        {
            var cryptionHelper=new FrostAura.Dynamics.Core.Helpers.FaCryptographyHelper();

            var user =
                WeedHackersContext.Users.ToList()
                    .Find(u => u.Email == email && u.Password == cryptionHelper.HashString(oldPassword));
            if (user!=null)
            {
                user.Password = newPassword;
            }
            WeedHackersContext.Users.AddOrUpdate(user);

            return View();
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