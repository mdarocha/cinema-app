using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.Services.Description;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Net;

namespace CinemaApp
{
    public interface IMailSender
    {
        Task SendIdentityMessage(IdentityMessage msg);
        void SendMail(MailMessage msg);
        void SendMail(string To, string Subject, string Message);
    }

    public class MailSender : IMailSender
    {
        public Task SendIdentityMessage(IdentityMessage message)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(message.Destination);
            msg.Subject = message.Subject;
            msg.Body = message.Body;

            SendMail(msg);
            return Task.FromResult(0);
        }

        public void SendMail(MailMessage message)
        {
            var config = ConfigurationManager.AppSettings;

            message.From = new MailAddress(config["SmtpEmail"]);

            SmtpClient client = new SmtpClient(config["SmtpHost"], int.Parse(config["SmtpPort"]));
            NetworkCredential credential = new NetworkCredential(config["SmtpUser"], config["SmtpPassword"]);
            client.Credentials = credential;
            client.EnableSsl = false;
            client.Send(message);
        }

        public void SendMail(string To, string Subject, string Message)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(To);
            msg.Subject = Subject;
            msg.Body = Message;

            SendMail(msg);
        }
    }
}