using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ServiceForTutorContracts.BusinessLogicContracts;
using ServiceForTutorContracts.BindingModels;

namespace ServiceForTutorBusinessLogic.MailWorker
{
    public class MailKitWorker : AbstractMailWorker
    {
        public MailKitWorker(IUserLogic organizerLogic) : base(organizerLogic) { }

        protected override async Task SendMailAsync(MailSendInfoBindingModel info)
        {
            using var objMailMessage = new MailMessage();
            using var objSmtpClient = new SmtpClient(_smtpClientHost, _smtpClientPort);

            try
            {
                objMailMessage.From = new MailAddress(_mailLogin);
                objMailMessage.To.Add(new MailAddress(info.MailAddress));
                objMailMessage.Subject = info.Subject;
                objMailMessage.Body = info.Text;
                objMailMessage.SubjectEncoding = Encoding.UTF8;
                objMailMessage.BodyEncoding = Encoding.UTF8;
                //Attachment attachment = new Attachment("E:\\ReportsCourseWork\\pdffile.pdf", new ContentType(MediaTypeNames.Application.Pdf));
                // objMailMessage.Attachments.Add(attachment);

                objSmtpClient.UseDefaultCredentials = false;
                objSmtpClient.EnableSsl = true;
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtpClient.Credentials = new NetworkCredential(_mailLogin, _mailPassword);

                await Task.Run(() => objSmtpClient.Send(objMailMessage));
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
