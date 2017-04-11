using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace BankLite.Web.Extensions
{
    public static class IdentityExtensions
    {
        //PRIMJER
        //public static string GetProperty(this IIdentity identity)
        //{
        //    var claim = ((ClaimsIdentity)identity).FindFirst("Property");
        //    // Test for null to avoid issues during local testing
        //    return (claim != null) ? claim.Value : string.Empty;
        //}

        public static int GetUser_ID(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("User_ID");
            // Test for null to avoid issues during local testing
            return (claim != null) ? Convert.ToInt32(claim.Value) : 0;
        }
    }
}