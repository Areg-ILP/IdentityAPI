using System;
using System.Security.Cryptography;
using System.Text;

namespace Identity.Infastructure.Application.Utilities.Extentions
{
    public static class HashHelper
    {
        public static string GetSoltedHash(string password,string solt)
        {
            var passHash = GenerateHash(password);
            var soltHash = GenerateHash(solt);
            
            return soltHash + passHash;
        }

        private static string GenerateHash(string toHash)
        {
            var crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(toHash));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }
    }
}
