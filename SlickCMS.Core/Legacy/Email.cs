using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace SlickCMS.Core.Legacy
{
    public class Email
    {
        /// <summary>
        /// Initiliases Email
        /// </summary>
        public Email()
        {
            // instance level
            this.FromEmail = "";
            this.ToEmail = "";
            this.ToEmails = null;
            this.Subject = "";
            this.Message = "";

            // application level - retrieve from Properties?
            // TODO: move the below properties to settings that can be changed from within the Web Application itself
            this.IsHTML = true;
            this.SMTP = "";
            this.Port = 25;

            // TODO: load these from a Settings table within the DB
            /*if (SlickCMS.Properties.Settings.Default.Email_UserName != "")
            {
                this.UserName = SlickCMS.Properties.Settings.Default.Email_UserName;
                this.Password = SlickCMS.Properties.Settings.Default.Email_Password;
            }
            else*/
            {
                this.UserName = "";
                this.Password = "";
            }
        }

        #region Properties
        /// <summary>
        /// Email address of the sender
        /// </summary>
        public string FromEmail { get; set; }

        /// <summary>
        /// Email address of the recipient
        /// </summary>
        public string ToEmail { get; set; }

        /// <summary>
        /// List of email addresses to send the email to
        /// </summary>
        public List<string> ToEmails { get; set; }

        /// <summary>
        /// Subject of the Email
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Message to appear within Email body
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Specifies whether to send HTML (true) or Plain-Text (false) Email
        /// </summary>
        public bool IsHTML { get; set; }

        /// <summary>
        /// Specifies the Name/IP Address of the SMTP Server, e.g. "127.0.0.1"
        /// </summary>
        public string SMTP { get; set; }

        /// <summary>
        /// Port of SMTP Server
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// UserName for SMTP Server (optional)
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password for SMTP Server (optional)
        /// </summary>
        public string Password { get; set; }
        #endregion

        /// <summary>
        /// Sends an Email
        /// </summary>
        /// <returns>True if sent successfully, else False</returns>
        public bool Send()
        {
            try
            {
                // create the mail message
                MailMessage mail = new MailMessage();

                // set the addresses
                mail.From = new MailAddress(this.FromEmail);

                if (ToEmails != null)
                {
                    foreach (string email in this.ToEmails)
                    {
                        mail.To.Add(email);
                    }
                }
                else
                {
                    mail.To.Add(this.ToEmail);
                }

                // set the content
                mail.Subject = this.Subject;
                mail.Body = this.Message;
                mail.IsBodyHtml = this.IsHTML;

                // set smtp properties
                SmtpClient smtp = new SmtpClient(this.SMTP);
                smtp.Port = this.Port;

                if (this.UserName != "")
                {
                    smtp.Credentials = new NetworkCredential(this.UserName, this.Password);
                }

                // send a copy to developer
                /*string developerEmail = "matt@slickhouse.com";
                if (!this.ToEmail.Contains(developerEmail))
                {
                    if (this.ToEmails == null || this.ToEmails.Count == 0 || !this.ToEmails.Contains(developerEmail))
                    {
                        mail.Bcc.Add(developerEmail);
                    }
                }*/

                // send the message
                smtp.Send(mail);

                // clean up
                mail.Dispose();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
