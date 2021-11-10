using System;
using System.Security.Cryptography;
using System.Text;

namespace Identity.Infastructure.Application.Utilities.Extentions
{
    public static class StringExtentions
    {
        public static string GenerateHash(this string password)
        {
            var crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(password));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }
    }
}
