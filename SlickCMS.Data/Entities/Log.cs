using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Web;

namespace SlickCMS
{
    public partial class Log
    {
        /// <summary>
        /// Logs an exception to database (or file) and emails the developer
        /// </summary>
        /// <param name="ex">The Exception to log</param>
        public static void LogException(Exception ex)
        {
            Log log = new Log();
            log.Setup();
            log.Type = "Exception";
            log.Message = ex.Message;
            log.StackTrace = ex.StackTrace;

            try
            {
                HttpException httpException = ex as HttpException;
                log.HtmlMessage = httpException.GetHtmlErrorMessage();
            }
            catch { }

            try
            {
                log.LogToDatabase();
            }
            catch
            {
                // if unable to log to database, then log to file
                log.LogToFile();
            }

            // as this is an error, also send an email to the developer
            // 2013-10-07 removed as it clogs up inbox without being meaningful
            //log.LogToEmail();
        }

        /// <summary>
        /// Logs a message to database (or file)
        /// </summary>
        /// <param name="message"></param>
        public static void LogMessage(string message)
        {
            Log log = new Log();
            log.Setup();
            log.Type = "Message";
            log.Message = message;

            try
            {
                log.LogToDatabase();
            }
            catch
            {
                // if unable to log to database, then log to file
                log.LogToFile();
            }
        }

        private string GetUrl()
        {
            try
            {
                return HttpContext.Current.Request.Url.ToString().ToLower();
            }
            catch
            {
                return "N/A";
            }
        }

        private string GetReferrer()
        {
            try
            {
                return HttpContext.Current.Request.UrlReferrer.OriginalString;
            }
            catch
            {
                return "N/A";
            }
        }

        private string GetUserAgent()
        {
            try
            {
                return HttpContext.Current.Request.UserAgent;
            }
            catch
            {
                return "N/A";
            }
        }

        /// <summary>
        /// Pre populates all of the properties
        /// </summary>
        private void Setup()
        {
            this.Type = "";
            this.DateRecorded = DateTime.Now;
            this.Url = GetUrl();
            this.Message = "";
            this.StackTrace = "";
            this.Referrer = GetReferrer();
            this.UserAgent = GetUserAgent();
        }

        private void LogToDatabase()
        {
            SlickCMSDataContext db = SlickCMSDataContext.Create();
            db.Logs.InsertOnSubmit(this);
            db.SubmitChanges();
            db.Dispose();
        }

        private void LogToFile()
        {
            try
            {
                string filename = DateTime.Now.ToString("yyyy-MM-dd") + ".log";
                string path = HttpContext.Current.Server.MapPath("/logs");// TODO: override via config

                // build up line to log as a pipe separated string
                string line = String.Format(
                    "{0}|{1}|{2}|{3}|{4}|{5}|{6}",
                    this.Type,
                    this.DateRecorded,
                    this.Url,
                    this.Message,
                    this.StackTrace,
                    this.Referrer,
                    this.UserAgent
                );

                // clear line breaks to ensure message appears as a single line in the file
                line = line.Replace(Environment.NewLine, "");

                FileStream fs = new FileStream(path + "/" + filename, FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);

                sw.WriteLine(line);
                sw.Close();
                sw.Dispose();

                fs.Close();
                fs.Dispose();
            }
            catch { }
        }

        private string BuildEmail()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("<strong>Type:</strong> {0}<br />", this.Type);
            sb.AppendFormat("<strong>DateRecorded:</strong> {0}<br />", this.DateRecorded);
            sb.AppendFormat("<strong>Url:</strong> {0}<br />", this.Url);
            sb.AppendFormat("<strong>Message:</strong> {0}<br />", this.Message);
            sb.AppendFormat("<strong>StackTrace:</strong> {0}<br />", this.StackTrace);
            sb.AppendFormat("<strong>Referrer:</strong> {0}<br />", this.Referrer);
            sb.AppendFormat("<strong>UserAgent:</strong> {0}<br />", this.UserAgent);

            return sb.ToString();
        }

        private void LogToEmail()
        {
            Email email = new Email();

            // TODO: override via config
            //email.FromEmail = "webmaster@slickcms.slickhouse.com";
            email.FromEmail = "website@courthousejunior.co.uk";
            email.ToEmail = "matt@slickhouse.com";

            email.Subject = "Error on Courthouse";// TODO: override via config
            email.Message = BuildEmail();
            email.IsHTML = true;

            // TODO: override via config
            //email.SMTP = "mail.slickhouse.com";
            //email.UserName = "slickcms@slickhouse.com";
            //email.Password = "tYPb6FErq4S57SI47Gpc64fy8Ttp8Aim";

            email.SMTP = "s393544820.onlinehome.info";
            email.UserName = "website@courthousejunior.co.uk";
            email.Password = "41ced85a91af4d3a8fc21b9939334dd4";

            email.Send();
        }

        /// <summary>
        /// Writes a Message to the Event Log (Application > SlickCMS)
        /// </summary>
        private void LogToEventlog()
        {
            // TODO: override via config
            if (!EventLog.SourceExists("SlickCMS"))
            {
                EventLog.CreateEventSource("SlickCMS", "Application");
            }

            EventLog el = new EventLog();
            el.Source = "SlickCMS";
            el.WriteEntry(this.Message);
            el.Dispose();
        }
    }
}
