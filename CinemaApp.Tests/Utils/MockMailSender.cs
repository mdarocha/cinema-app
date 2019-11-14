using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Tests
{
    class MockMailSender : IMailSender
    {
        public Task SendIdentityMessage(IdentityMessage msg)
        {
            return Task.FromResult(0);
        }

        public void SendMail(MailMessage msg)
        {
        }

        public void SendMail(string To, string Subject, string Message)
        {

        }
    }
}
