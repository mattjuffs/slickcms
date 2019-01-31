using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SlickCMS.Utility;

namespace SlickCMS
{
    public partial class User : IData<User>
    {
        /// <summary>
        /// Retrieves the corresponding Role associated with this User
        /// </summary>
        public Role Role
        {
            get
            {
                return SlickCMS.Role.Get(this.RoleID);
            }
        }

        /// <summary>
        /// Inserts a new User
        /// </summary>
        public void Insert()
        {
            //set system generated items:
            this.UUID = System.Guid.NewGuid();
            this.DateCreated = DateTime.Now;
            this.DateModified = DateTime.Now;
            this.Password = Hash.GenerateHash(this.Password, Hash.HashType.MD5);

            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            dc.Users.InsertOnSubmit(this);
            dc.SubmitChanges();
            dc.Dispose();
        }

        public static User Get(int userID)
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                return db.Users.Where(u => u.UserID == userID).FirstOrDefault();
            }
        }

        /// <summary>
        /// Selects an existing User
        /// </summary>
        /// <param name="id">Unique ID of User</param>
        /// <returns>A User, if found</returns>
        public User Select(int id)
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            return dc.Users.SingleOrDefault(u => u.UserID == id);
        }

        /// <summary>
        /// Selects a List of Users
        /// </summary>
        /// <param name="skip">Number of Users to skip</param>
        /// <param name="take">Number of Users to select</param>
        /// <returns>A List of Users</returns>
        public List<User> SelectMultiple(int skip, int take)
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();

            var query = (
                from u in dc.Users
                orderby u.UserID
                select u
            );

            //TODO: implement search

            return query.Skip(skip).Take(take).ToList();
        }

        /// <summary>
        /// Updates an existing User
        /// </summary>
        public void Update()
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();

            User user = dc.Users.Single(u => u.UserID == this.UserID);

            this.Password = Hash.GenerateHash(this.Password, Hash.HashType.MD5);

            user.Name = this.Name;
            user.Email = this.Email;
            user.Password = this.Password;
            user.URL = this.URL;
            user.IP = this.IP;
            user.Biography = this.Biography;
            user.DateModified = DateTime.Now;
            user.Active = this.Active;
            user.LoginFails = this.LoginFails;
            user.RoleID = this.RoleID;

            dc.SubmitChanges();
            dc.Dispose();
        }

        /// <summary>
        /// Deletes an existing User
        /// </summary>
        public void Delete()
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            dc.Users.DeleteOnSubmit(this);
            dc.SubmitChanges();
            dc.Dispose();
        }

        /// <summary>
        /// Logs in a User if their credentials are valid
        /// </summary>
        /// <param name="email">User's Email</param>
        /// <param name="password">User's raw password (it is hashed prior to checking)</param>
        /// <returns>Result of login attempt</returns>
        public bool Login(string email, string password)
        {
            //MD5 password, as it is stored as a hash within the database:
            password = Hash.GenerateHash(password, Hash.HashType.MD5);

            SlickCMSDataContext dc = SlickCMSDataContext.Create();

            User user = (
                from u in dc.Users
                where
                    u.Email == email
                    && u.Active == 1
                    //TODO: need a more reliable way, as MD5 may be upper/lower?
                    && u.Password.ToUpper() == password.ToUpper()
                select u
            ).FirstOrDefault();

            if (user != null)
            {
                //we have a user, so name/password is valid

                //persist to Session, for caching
                HttpContext.Current.Session["User"] = user;
                HttpContext.Current.Session["LoggedIn"] = true;

                //use for retrieving User at a later stage:
                //user = (User)HttpContext.Current.Session["User"];

                return true;
            }
            else
            {
                //username and/or password was invalid
                return false;
            }
        }

        /// <summary>
        /// Logs out a User
        /// </summary>
        /// <returns>Result</returns>
        public bool Logout()
        {
            //destroy user in Session if it exists
            if (HttpContext.Current.Session["User"] != null)
            {
                HttpContext.Current.Session["User"] = null;
            }

            HttpContext.Current.Session["LoggedIn"] = null;

            return true;
        }

        /// <summary>
        /// Checks if a User is logged in
        /// </summary>
        public static bool LoggedIn()
        {
            bool loggedIn;

            if (HttpContext.Current.Session["LoggedIn"] == null)
            {
                loggedIn = false;
            }
            else
            {
                if ((bool)HttpContext.Current.Session["LoggedIn"] == true)
                {
                    loggedIn = true;
                }
                else
                {
                    loggedIn = false;
                }
            }

            return loggedIn;
        }

        public static User GetLoggedIn()
        {
            if (LoggedIn())
                return (User)HttpContext.Current.Session["User"];
            else
                return null;
        }

        /// <summary>
        /// "Deletes" a User by marking them as inactive
        /// </summary>
        /// <param name="userID"></param>
        public static void DeleteUser(int userID)
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                User user = (
                    from u in db.Users
                    where u.UserID == userID
                    select u
                ).FirstOrDefault();

                if (user != null)
                {
                    user.Active = 0;
                    db.SubmitChanges();
                }
            }
        }

        public static List<User> GetAll()
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var query = (
                    from u in db.Users
                    where u.Active == 1
                    select u
                );

                return query.ToList();
            }
        }
    }
}
