using System.Net.Mail;

namespace ServiceUser.Services
{
    public class EmailService
    {
        public static bool CheckEmailFormat(string emailAddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailAddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
