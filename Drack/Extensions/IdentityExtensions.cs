using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace Drack.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetFullName(this IIdentity identity)
        {
            return ((ClaimsIdentity)identity).FindFirst("MyApp:FullName").Value;
        }
        public static string GetId(this IIdentity identity)
        {
            return ((ClaimsIdentity)identity).FindFirst("MyApp:Id").Value;
        }
    }
}