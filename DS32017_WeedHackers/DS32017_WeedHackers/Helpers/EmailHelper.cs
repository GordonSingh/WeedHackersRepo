using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DS32017_WeedHackers.Models.HelperModels;

namespace DS32017_WeedHackers.Helpers
{
    public class EmailHelper
    {
        //method available as a service to be used whenever needed
        public async Task<bool> SendEmail(EmailFormModel model, MailMessage message = null)
        {
            if (message == null)
            {
                message = new MailMessage();
                message.To.Add(new MailAddress(model.Recipient));
                message.From = new MailAddress("weedhackerss@gmail.com");
                message.Subject = "WeedHackers";
                message.Body = model.Message;
                message.IsBodyHtml = true;
            }

            using (var smtp = new SmtpClient())
            {
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential("weedhackerss@gmail.com", "TheAlphas1");
                smtp.Timeout = 20000;

                //---
                smtp.Send(message);
                //---
                await smtp.SendMailAsync(message);
                return true;
            }
        }

        public async Task<bool> SendQuotationEmail(EmailQuoteModel model)
        {
            var message = new MailMessage();

            message.To.Add(new MailAddress(model.EmailFormModel.Recipient));
            message.From = new MailAddress("weedhackerss@gmail.com");
            message.Subject = "WeedHackers";
            message.Body = model.EmailFormModel.Message;
            message.IsBodyHtml = true;

            using (var reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Content/Templates/quotation_template.html")))
            {
                string quotationTemplate = reader.ReadToEnd();
                var logoId = Guid.NewGuid().ToString();

                quotationTemplate = quotationTemplate
                     .Replace("<%QUOTATION_NUMBER%>", model.QuotationNumber)
                     .Replace("<%UNIT_SUBTOTAL%>", model.UnitSubtotal)
                     .Replace("<%UNIT_TOTAL%>", model.UnitTotal)
                     .Replace("<%EMAIL%>", model.Email)
                     .Replace("<%TELL%>", model.Tell)
                     .Replace("<%UNIT_QTY%>", model.UnitQuantity)
                     .Replace("<%UNIT_PRICE%>", model.UnitPrice)
                     .Replace("<%LOGO_ID%>", logoId)
                     .Replace("<%SERVICE_TYPE%>", model.ServiceType);

                var alternateViewFromString = AlternateView.CreateAlternateViewFromString(quotationTemplate, Encoding.UTF8, "text/html");
                var inline =
                    new LinkedResource(HttpContext.Current.Server.MapPath("~/Content/Images/logo.png"),
                        MediaTypeNames.Image.Jpeg) {ContentId = logoId};
                alternateViewFromString.LinkedResources.Add(inline);

                message.AlternateViews.Add(alternateViewFromString);

                return await SendEmail(model.EmailFormModel,message );
            }
        }
    }
}