using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace Tennis.UI.Common
{
    public class UserPrincipal : GenericPrincipal
    {
    /// <summary>
        /// Defines the constant User ID the identifies an invalid user for the blank principal.
        /// </summary>
        public const int InvalidUserId = 0;

        private const string UserPrincipalSessionKey = "UserPrincipal";

        /// <summary>
        /// Defines a default blank principal to be returned when none is available in the current session.
        /// By not specifying anything else (no id, no roles), this means this principal is never 
        /// authenticated/authorized to do anything.
        /// </summary>
        private static readonly UserPrincipal BlankPrincipal = new UserPrincipal(new ClaimsIdentity(), null, InvalidUserId);

        /// <summary>
        /// Initialises a new instance of the <see cref="UserPrincipal"/> class.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="roles">The roles.</param>
        /// <param name="userId">The user Id.</param>
        public UserPrincipal(IIdentity identity, string[] roles, int userId)
            : base(identity, roles)
        {
            UserId = userId;
        }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int UserId { get; set; }
        //public int UserId { get; private set; }

        /// <summary>
        /// Gets the current user principal, or an empty default one if none available.
        /// </summary>
        /// <remarks>Hiding base reference to <c>Current</c>.</remarks>
        public static new UserPrincipal Current
        {
            get
            {
                if (HttpContext.Current.Session[UserPrincipalSessionKey] != null)
                {
                    //HttpContext.Current.Request.conte
                    return (UserPrincipal)HttpContext.Current.Session[UserPrincipalSessionKey];
                }

                return BlankPrincipal;
            }
        }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Gets or sets the UserLevelId.
        /// </summary>
        public byte? UserLevelId { get; set; }

        /// <summary>
        /// Gets or sets the UserLevelStatus.
        /// </summary>
        public string UserLevelStatus { get; set; }

        /// <summary>
        /// Gets or sets the PheCenterId.
        /// </summary>
        public int? PheCenterId { get; set; }

        /// <summary>
        /// Log a user off in the UI session.
        /// </summary>
        public static void LogOff()
        {
            HttpContext.Current.Session[UserPrincipalSessionKey] = null;
        }
    }
}