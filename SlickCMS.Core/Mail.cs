using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Mail;

namespace SlickCMS.Core
{
    class Mail
    {
        public string FromName { get; set; }
        public string FromEmail { get; set; }

        public string ToName { get; set; }
        public string ToEmail { get; set; }

        public string Subject { get; set; }
        public string Message { get; set; }

        public Mail()
        {
            this.FromName = "";
            this.FromEmail = "";

            this.ToName = "";
            this.ToEmail = "";

            this.Subject = "";
            this.Message = "";
        }

        // Option 1 - using SmtpClient to send the email
        // http://www.aspheute.com/english/20000918.asp
        // http://weblogs.asp.net/scottgu/archive/2005/12/16/432854.aspx
        // https://community.rackspace.com/products/f/18/t/4229 - via Mailgun
        public bool SendUsingSmtpClient()
        {
            try
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(this.FromEmail, this.FromName, Encoding.UTF8),
                    Subject = this.Subject,
                    IsBodyHtml = true,
                    Body = this.Message,
                    Priority = MailPriority.Normal,
                };

                mailMessage.To.Add(new MailAddress(this.ToEmail, this.ToName, Encoding.UTF8));

                using (var client = new SmtpClient())
                {
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Host = "smtp.mailgun.org";
                    client.Credentials = new System.Net.NetworkCredential("postmaster@mailgun.slickhouse.com", "2ba1e5daa482474bd6a2e6cd5216426e");

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
        public bool SendUsingMailgunAPI()
        {
            try
            {
                // TODO
                return false;

                return true;
            }
            catch (Exception ex)
            {
                // TODO: log exception
                return false;
            }
        }
    }
}
