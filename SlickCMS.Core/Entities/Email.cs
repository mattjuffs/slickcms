using System;
using System.Collections.Generic;
using System.Text;

namespace SlickCMS.Core.Entities
{
    public class Email
    {
        public string FromName { get; set; }
        public string FromEmail { get; set; }

        public string ToName { get; set; }
        public string ToEmail { get; set; }

        public string Subject { get; set; }
        public string Message { get; set; }

        public Email()
        {
            this.FromName = "";
            this.FromEmail = "";

            this.ToName = "";
            this.ToEmail = "";

            this.Subject = "";
            this.Message = "";
        }
    }
}
