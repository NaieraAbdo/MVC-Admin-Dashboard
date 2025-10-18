using Mvc.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Mvc.PAL.Helpers
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            //Google Email Server
            var Client = new SmtpClient("smtp.gmail.com",587);
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("aliaaTarek.route@gmail.com","1234");
            Client.Send("aliaaTarek.route@gmail.com",email.To,email.Subject,email.Body);
        }
    }
}
