using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Common.Utils.Helpers
{
    public static class Utils
    {
        public static bool ValidateEmail(string email)
        {
            bool result = false;
            string expresion = "^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                    result = true;
                else
                    result = false;
            }
            else
                result = false;
            return result;
        }

        public static string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
    }
}
