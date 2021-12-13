using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HospitalManagement.Utils
{
    public class EmailProcessing
    {
        private string emailToAddress;
        private string emailFromAddress;
        private string passEmail; 
        private string emailSubject;
        private string emailBody;
        public static string emailName;
        public static int vertificedNumber;
        public static string emailUserName;

        public EmailProcessing(string emailTo, string emailFrom, string pass,string subject, string body ) 
        { 
            this.emailToAddress = emailTo;
            this.emailFromAddress = emailFrom;
            this.passEmail = pass;
            this.emailSubject = subject;
            this.emailBody = body;
        }
        public void sendEmail()
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 0;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(this.emailFromAddress,this.passEmail);
                MailMessage msg = new MailMessage();
                msg.To.Add(this.emailToAddress);
                msg.From = new MailAddress(this.emailFromAddress);
                msg.Subject = this.emailSubject;
                string bodyEmail = this.emailBody;
                msg.Body = bodyEmail;
                var send = client.SendMailAsync(msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
