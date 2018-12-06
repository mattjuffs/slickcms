using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlickCMS.Core.Legacy
{
    /// <summary>
    /// Error Class is used to specify a list of known possible user friendly errors within SlickCMS and to handle errors
    /// </summary>
    public class Error// NOTE: used on Post currently
    {
        /// <summary>
        /// Optional code associated with the error
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Description of the error
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Property that the error corresponds to
        /// </summary>
        public string Property { get; private set; }

        /// <summary>
        /// Used to specify a new known error
        /// </summary>
        /// <param name="code"></param>
        /// <param name="description"></param>
        public Error(int code, string description)
        {
            this.Code = code;
            this.Description = description;
        }

        /// <summary>
        /// Used to specify a new error that affects a property
        /// </summary>
        /// <param name="description">Description of the error</param>
        /// <param name="property">Property that the error affects/relates to</param>
        public Error(string description, string property)
        {
            Description = description;
            Property = property;
        }

        public static List<Error> GetErrors()
        {
            // see: http://stackoverflow.com/questions/3994288/if-statement-vs-oo-design/3994458#3994458
            List<Error> errors = new List<Error>()
            {
                new Error(300,"Description for a 300 error"),
                new Error(301,"Description for a 301 error")
            };

            return errors;
        }

        public static string GetErrorDescription(int errorCode)
        {
            List<Error> errors = Error.GetErrors();

            string errorDescription = (
                from e in errors
                where e.Code == errorCode
                select e.Description
            ).FirstOrDefault();

            return errorDescription;
        }

        /// <summary>
        /// Retrieves a list of Errors in a renderable string
        /// </summary>
        /// <param name="violations">Any violations as a result of validating an object</param>
        /// <returns>String of formatted errors</returns>
        public static string GetErrors(IEnumerable<Error> violations)
        {
            StringBuilder s = new StringBuilder();

            foreach (Error violation in violations)
            {
                s.Append(violation.Description);
                // do something with: Property;
            }

            return s.ToString();
        }
    }
}
