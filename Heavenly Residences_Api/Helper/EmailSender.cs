using System;
using System.IO;
using System.Threading.Tasks;
using Mailjet.Client;
using Mailjet.Client.Resources;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using Newtonsoft.Json.Linq;

namespace Heavenly_Residences_Api.Helper
{
    public class EmailSender : IEmailSender
    {
        private readonly GmailSettings _gmailSetting;
       

        public EmailSender(IOptions<GmailSettings> gmailSetting)
        {
            _gmailSetting = gmailSetting.Value;
            
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var emailObj = new MimeMessage();
                emailObj.Sender = MailboxAddress.Parse(_gmailSetting.SMTPServer);
                emailObj.To.Add(MailboxAddress.Parse(email));
                emailObj.Subject = subject;
                var builder = new BodyBuilder();

                #region SendEmailFile

                //if (mailRequest.Attachments != null)
                //{
                //    byte[] fileBytes;
                //    foreach (var file in mailRequest.Attachments)
                //    {
                //        if (file.Length > 0)
                //        {
                //            using (var ms = new MemoryStream())
                //            {
                //                file.CopyTo(ms);
                //                fileBytes = ms.ToArray();
                //            }
                //            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                //        }
                //    }
                //}

                #endregion
                builder.HtmlBody = htmlMessage;
                emailObj.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_gmailSetting.SMTPServer, _gmailSetting.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_gmailSetting.UserName, _gmailSetting.Password);
                await smtp.SendAsync(emailObj);
                smtp.Disconnect(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
            }
           
        }


    }
}
