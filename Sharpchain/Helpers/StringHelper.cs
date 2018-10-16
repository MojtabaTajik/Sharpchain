using System.Text;
using System.Security.Cryptography;

namespace Sharpchain.Helpers
{
    public static class StringHelper
    {
        public static byte[] ToSha256Hash(this string str)
        {
            string hash = string.Empty;
            return new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(str));
        }

        public static string ToSha256HashString(this string str)
        {
            string hash = string.Empty;

            byte[] sha256Hash = ToSha256Hash(str);
            foreach (byte theByte in sha256Hash)
                hash += theByte.ToString("x2");

            return hash;
        }

        public static bool IsValidHashDifficulty(this string hash, int difficulty)
        {
            string difficultyZeroString = new string('0', difficulty);
            return hash.StartsWith(difficultyZeroString);
        }
    }
}