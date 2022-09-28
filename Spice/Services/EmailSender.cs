using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace Spice.Services
{
    public class EmailSendere : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            throw new System.NotImplementedException();
        }
    }
}
