using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SlickCMS.Utility;

namespace SlickCMS
{
    public partial class Role
    {
        /// <summary>
        /// Retrieves a Role corresponding to the specified ID or the lowest privilege role
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public static Role Get(int? roleID)
        {
            if (roleID == null) return LowestRole();

            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                return db.Roles.Where(r => r.RoleID == Convert.ToInt32(roleID)).FirstOrDefault();
            }

            //return GetAll().Where(r => r.RoleID == (roleID ?? 0)).FirstOrDefault();
        }

        public static List<Role> GetAll()
        {
            string bufferName = "Roles";

            if (HttpContext.Current.Application[bufferName] == null)
            {
                using (SlickCMSDataContext db = SlickCMSDataContext.Create())
                {
                    var query = (
                        from r in db.Roles
                        orderby r.Rank descending
                        select r
                    );

                    HttpContext.Current.Application[bufferName] = query.ToList();
                }
            }

            return (List<Role>)HttpContext.Current.Application[bufferName];
        }

        private static Role Get(int roleID)
        {
            string bufferName = "Role" + roleID.ToString();

            if (HttpContext.Current.Application[bufferName] == null)
            {
                Role role;

                using (SlickCMSDataContext db = SlickCMSDataContext.Create())
                {
                    role = db.Roles.Where(p => p.RoleID == roleID).FirstOrDefault();
                }

                HttpContext.Current.Application[bufferName] = role;
            }

            return (Role)HttpContext.Current.Application[bufferName];
        }

        /// <summary>
        /// Retrieves the lowest privilege role
        /// </summary>
        /// <returns></returns>
        private static Role LowestRole()
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var query = (
                    from r in db.Roles
                    orderby r.Rank descending
                    select r
                );

                return query.FirstOrDefault();
            }
        }
    }
}
