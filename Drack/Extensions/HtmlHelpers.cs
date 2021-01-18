using Drack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Drack.Extensions
{
    public static class HtmlHelpers
    {
        public static string Truncate(this HtmlHelper helper, string input, int length)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            else if (input.Length <= length)
            {
                return input;
            }
            else
            {
                return input.Substring(0, length) + "...";
            }
        }
        public static string StripHTML(this HtmlHelper helper, string input)
        {
            return Regex.Replace(input, "<[^>]*(>|$)", String.Empty);
        }

        public static string GetUserFullName(this HtmlHelper helper, string id)
        {
            ApplicationDbContext AspDb;
            AspDb = new ApplicationDbContext();

            if (!string.IsNullOrEmpty(id) || !string.IsNullOrWhiteSpace(id))
            {
                ApplicationUser user = AspDb.Users.Where(x => x.Id == id).FirstOrDefault();
                return user.FullName;
            }
            else
            {
                return null;
            }
        }
    }
}