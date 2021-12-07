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
using HospitalManagement.Utils;

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
                if (Check(rpf))
                {
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool Check(ReportForm reportForm)
        {
            if (string.IsNullOrWhiteSpace(reportForm.txbEmail.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập email");
                notifyWindow.ShowDialog();
                reportForm.txbEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(reportForm.txbSubject.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập chủ đề");
                notifyWindow.ShowDialog();
                reportForm.txbSubject.Focus();
                return false;
            }

            if (!ValidatorEmail.EmailIsValid(reportForm.txbEmail.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Địa chỉ email không hợp lệ");
                notifyWindow.ShowDialog();
                reportForm.txbSubject.Focus();
                return false;
            }
           
            return true;
        }

        private string[] spliter(string text) => text.Split(';');
    }
}
