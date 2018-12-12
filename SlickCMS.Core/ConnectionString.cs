using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Runtime.Caching;

namespace SlickCMS.Core
{
    /// <summary>
    /// This class allows the full source-code to be stored in a repository, with the sensitive connection string held outside of the environment.
    /// 
    /// Currently used in Startup.cs and SlickCMSContext.cs
    /// 
    /// Notes below from Microsoft:
    /// 
    /// In a real application you would typically put the connection string in a configuration file or environment variable.
    /// For the sake of simplicity, this tutorial has you define it in code.
    /// For more information, see Connection Strings (https://docs.microsoft.com/en-gb/ef/core/miscellaneous/connection-strings).
    /// 
    /// To protect potentially sensitive information in your connection string, you should move it out of source code.
    /// See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
    /// </summary>
    public class ConnectionString
    {
        public string Path = @"C:\ConnectionStrings\";// default path for storing the connection string
        public string FileName = @"slickcms.txt";

        private string boilerplateConnectionString = @"Server=<server>;Initial Catalog=slickcms;Persist Security Info=False;User ID=slickcms;Password=<password>;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private string cacheKey = "ConnectionString";

        public string Get(string environmentPath = "")
        {
            var cachedConnectionString = SlickCMS.Core.Caching.MemoryCache.Get(cacheKey);
            if (cachedConnectionString != null)
                return cachedConnectionString.ToString();

            if (environmentPath != "")
                this.Path = environmentPath + @"\ConnectionStrings\";

            if (!FileExists())
                Create();

            string connectionString = System.IO.File.ReadAllText(this.Path + this.FileName);
            Caching.MemoryCache.Add(cacheKey, connectionString);

            return connectionString;
        }

        private bool FileExists()
        {
            if (System.IO.Directory.Exists(this.Path))
            {
                if (System.IO.File.Exists(this.Path + this.FileName))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void Create()
        {
            if (!System.IO.Directory.Exists(this.Path))
                System.IO.Directory.CreateDirectory(this.Path);

            //System.IO.File.Create(this.Path + this.FileName);

            System.IO.File.WriteAllText(this.Path + this.FileName, this.boilerplateConnectionString);
        }
    }
}
