using System.Security.Cryptography;
using System.Text;

namespace Sharpchain.Helpers
{
    public static class StringHelper
    {
        public static string ToSHA256HashString(this string str)
        {
            string hash = string.Empty;
            byte[] crypto = new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(str));
            foreach (byte theByte in crypto)
                hash += theByte.ToString("x2");

            return hash;
        }
    }
}