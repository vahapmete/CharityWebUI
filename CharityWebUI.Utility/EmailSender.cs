using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AWJ.EmailProviders;
using Myvas.AspNetCore.Email;

namespace CharityWebUI.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task<bool> SendEmailAsync(string recipient, string subject, string htmlBody)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendEmailAsync(string recipients, string subject, string plainBody, bool isBodyHtml)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendEmailAsync(EmailDto input)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendEmailHtmlAsync(string recipient, string subject, string htmlBody)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendEmailPlainAsync(string recipient, string subject, string plainBody)
        {
            throw new NotImplementedException();
        }
    }
}
