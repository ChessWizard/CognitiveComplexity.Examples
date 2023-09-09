using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service.Common
{
    public static class PasswordGenerator
    {
        public static string Hash(string password)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(password);
            
            return Convert.ToBase64String(sha.ComputeHash(asByteArray));
        }
    }
}
