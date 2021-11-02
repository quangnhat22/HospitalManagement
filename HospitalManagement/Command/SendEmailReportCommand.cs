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

namespace HospitalManagement.Command
{
    class SendEmailReportCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            try
            {
                ReportForm rpf = parameter as ReportForm;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("hotrofhms@gmail.com", "supportfhms719");
                MailMessage msg = new MailMessage();
                msg.To.Add("hotrofhms@gmail.com");
                msg.From = new MailAddress("hotrofhms@gmail.com");
                msg.Subject = rpf.txbSubject.Text;
                string bodyEmail = rpf.txbEmail.Text + " đã gửi: \n" + rpf.txbBody.Text;
                msg.Body = bodyEmail;
                client.Send(msg);
                
                MessageBox.Show("Đã gửi thành công. Cảm ơn đã phản hồi, chúng tôi sẽ phàn hồi bạn sớm nhất có thể.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
