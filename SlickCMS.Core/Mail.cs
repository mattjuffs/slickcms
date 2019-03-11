using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;

namespace SlickCMS.Core
{
    public class Mail
    {
        // Option 1 - using SmtpClient to send the email
        // http://www.aspheute.com/english/20000918.asp
        // http://weblogs.asp.net/scottgu/archive/2005/12/16/432854.aspx
        // https://community.rackspace.com/products/f/18/t/4229 - via Mailgun
        public bool SendUsingSmtpClient(Entities.Email email)
        {
            try
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(email.FromEmail, email.FromName, Encoding.UTF8),
                    Subject = email.Subject,
                    IsBodyHtml = true,
                    Body = email.Message,
                    Priority = MailPriority.Normal,
                };

                mailMessage.To.Add(new MailAddress(email.ToEmail, email.ToName, Encoding.UTF8));

                using (var client = new SmtpClient())
                {
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Host = "smtp.mailgun.org";
                    client.Credentials = new System.Net.NetworkCredential("username@mailserver.example.com", "password-goes-here");

                    client.Send(mailMessage);
                }

                return true;
            }
            catch (Exception ex)
            {
                // TODO: log exception
                return false;
            }
        }

        // Option 2 - using Mailgun's API (preferred)
        // https://documentation.mailgun.com/wrappers.html#c
        public bool SendUsingMailgunAPI(Entities.Email email)
        {
            try
            {
                // TODO
                return false;
            }
            catch (Exception ex)
            {
                // TODO: log exception
                return false;
            }
        }
    }
}
