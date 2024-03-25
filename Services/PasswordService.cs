using System.Text;
using XSystem.Security.Cryptography;

namespace ServiceUser.Services
{
    public class PasswordService
    {
        public static string GetHash(string? pass)
        {
            var sha = new SHA1Managed();
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(pass));
            return Convert.ToBase64String(hash);
        }

        public static bool DifficultyCheck(string pass)
        {
            if (pass.Length >= 8
                && pass.Length <= 30
                && pass.Any(x => char.IsLower(x))
                && pass.Any(x => char.IsUpper(x))
                && pass.Any(x => char.IsLetter(x))
                && pass.Any(x => char.IsDigit(x)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
