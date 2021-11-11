using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net;
using System.Net.Mail;
using HospitalManagement.View;
using System.Windows;
using Microsoft.Win32;
using System.Configuration;
using HospitalManagement.View.Others;

namespace HospitalManagement.Command
{
    class SendEmailReportCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            try
            {
                ReportForm rpf = parameter as ReportForm;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                //client.Timeout = 100000;
                client.Timeout = 0;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;

                string emailAddress = ConfigurationManager.AppSettings.Get("EmailAddress");
                string emailPassword = ConfigurationManager.AppSettings.Get("EmailPassword");

                client.Credentials = new NetworkCredential(emailAddress, emailPassword);
                MailMessage msg = new MailMessage();
                msg.To.Add(emailAddress);
                msg.From = new MailAddress(emailAddress);
                msg.Subject = rpf.txbSubject.Text;
                string bodyEmail = rpf.txbEmail.Text + " đã gửi: \n" + rpf.txbBody.Text;
                msg.Body = bodyEmail;

                if (rpf.btnAttachment.Content.ToString().Contains(".jpg") ||
                    rpf.btnAttachment.Content.ToString().Contains(".png") ||
                    rpf.btnAttachment.Content.ToString().Contains(".pdf"))
                {
                    foreach (string path in spliter(rpf.btnAttachment.Content.ToString()))
                    {
                        Attachment atc = new Attachment(path);
                        msg.Attachments.Add(atc);
                    }
                }
                var send = client.SendMailAsync(msg);
                ThankyouWindow thankyouWindow = new ThankyouWindow();
                thankyouWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string[] spliter(string text) => text.Split(';');
    }
}
