using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DS32017_WeedHackers.Helpers;
using DS32017_WeedHackers.Models.HelperModels;
using Vereyon.Web;

namespace DS32017_WeedHackers.Controllers.SystemFunctions
{
    public class ForgotPasswordController : BaseController
    {
        // GET: ForgotPassword
        public ActionResult Index(EmailPasswordModel model)
        {
            model=new EmailPasswordModel();
            return View(model);
        }

        public async Task<ActionResult> ResetPassword(EmailPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserContext = WeedHackersContext.Users.Single(u => u.Email == model.Email);
                string base64GuidPassword = Convert.ToBase64String(Guid.NewGuid().ToByteArray());//New Password assigned, User can later go and change it
                model.UserContext.Password = base64GuidPassword;
                WeedHackersContext.Users.AddOrUpdate(model.UserContext);
                await WeedHackersContext.SaveChangesAsync();
                var SendNewPassword = new EmailHelper();
                await SendNewPassword.SendEmail(new EmailFormModel
                {
                    Message =
                        "Your New Default password is  " + model.UserContext.Password +
                        "  please use this to login and then update your password to your prefered new password",
                    Recipient = model.UserContext.Email
                });

                FlashMessage.Confirmation("Forgot Password","Your password has been reset. An email was sent to you containing your new password.");
                return RedirectToAction("Index", "Security");
            }
         
            return RedirectToAction("Index", "ForgotPassword");
        }
    }
}