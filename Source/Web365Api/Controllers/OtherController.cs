using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web.Http;

namespace Web365Api.Controllers
{
    public class OtherController : BaseController
    {
        [HttpPost]
        public object SendMail(JObject data)
        {
            var title = data["title"] != null ? data["title"].ToString() : string.Empty;
            var email = data["email"] != null ? data["email"].ToString() : string.Empty;
            var content = data["content"] != null ? data["content"].ToString() : string.Empty;
            var username = data["username"] != null ? data["username"].ToString() : string.Empty;
            var password = data["password"] != null ? data["password"].ToString() : string.Empty;

            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com", // smtp server address here…
                Port = 25,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new System.Net.NetworkCredential(username, password),
                Timeout = 30000,
            };

            MailMessage message = new MailMessage(username, email, title, content);

            message.IsBodyHtml = true;

            smtp.Send(message);

            return new { 
                error = false
            };
        }
    }
}
