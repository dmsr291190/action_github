using Microsoft.Extensions.Configuration;
using Minem.Tupa.IApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Minem.Tupa.Application
{
    public class EmailApplication(IConfiguration configuration) : IEmailApplication
    {
        private readonly IConfiguration _configuration = configuration;
        public async Task<bool> SendMail(string correoremitente, string correodestino, string asunto, string bodycorreo, bool EnableHTML, bool EnableSSL, string rutaarhivoadjunto = null)
        {
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(correoremitente);

            if (correodestino.Contains(","))
            {
                string[] destinos = correodestino.Split(',');
                foreach (string destino in destinos)
                {
                    string correo = destino.Trim();
                    mail.To.Add(new MailAddress(correo));
                }
            }
            else
            {
                mail.To.Add(new MailAddress(correodestino));
            }
            mail.Subject = asunto;
            mail.Body = bodycorreo;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            Attachment data = null;
            if (EnableHTML)
            {
                mail.IsBodyHtml = true;
                mail.BodyEncoding = Encoding.Default;
                mail.SubjectEncoding = Encoding.Default;
            }
            else
            {
                mail.IsBodyHtml = false;
                mail.BodyEncoding = Encoding.Unicode;
                mail.SubjectEncoding = Encoding.Unicode;
            }

            if (!String.IsNullOrEmpty(rutaarhivoadjunto))
            {
                data = new Attachment(rutaarhivoadjunto);
                mail.Attachments.Add(data);
                //data.Dispose();
            }

            string smtpserver = Convert.ToString(_configuration.GetSection("ConfigurationSmtp:SmtpClient").Value.ToString());
            string email = _configuration.GetSection("ConfigurationSmtp:SmtpFromAddress").Value.ToString();
            string password = _configuration.GetSection("ConfigurationSmtp:SmtpPassword").Value.ToString();
            string strport = _configuration.GetSection("ConfigurationSmtp:SmtpPort").Value.ToString();

            SmtpClient smtp = new SmtpClient();
            smtp.Host = smtpserver;
            smtp.Credentials = new System.Net.NetworkCredential(email, password);

            smtp.EnableSsl = EnableSSL;

            if (!string.IsNullOrEmpty(strport))
            {
                int port = Convert.ToInt32(strport);
                smtp.Port = port;
            }
            try
            {
                smtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                smtp.Dispose();
            }
        }

    }
}
