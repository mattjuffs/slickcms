using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlickCMS.Web
{
    public static class Global
    {
        public static bool SendEmail(Models.ContactModel contactModel)
        {
            var email = new SlickCMS.Core.Entities.Email
            {
                FromName = contactModel.Name,
                FromEmail = contactModel.Email,

                ToName = "SlickCMS Developer",
                ToEmail = "your.name@domain.com",

                Subject = "Subject goes here",
                Message = contactModel.Message,
            };

            var mail = new SlickCMS.Core.Mail();
            return mail.SendUsingSmtpClient(email);
        }
    }
}
