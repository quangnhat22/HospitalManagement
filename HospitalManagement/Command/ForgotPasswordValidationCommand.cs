using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HospitalManagement.ViewModel;
using HospitalManagement.View;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace HospitalManagement.Command
{
    class ForgotPasswordValidationCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public bool CanExecute(object parameter)
        {
            return parameter == null ? false : true;
        }

        public void Execute(object parameter)
        {
            ForgotPasswordWindow mw = parameter as ForgotPasswordWindow;
            if (Check(mw))
            {
                sendEmail(mw);
                NotifyWindow notifyWindow = new NotifyWindow("Success", "Mã xác thực đã được gửi về email!");
                notifyWindow.ShowDialog();
            }
        }

        public bool Check(ForgotPasswordWindow mw)
        {
            if (mw == null) return false;
            if (string.IsNullOrWhiteSpace(mw.tbMailAddress.Text) || mw.tbMailAddress.Text.Contains('@') == false)
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập địa chỉ email");
                notifyWindow.ShowDialog();
                mw.tbMailAddress.Focus();
                return false;
            }
            return true;
        }
        public async void sendEmail(ForgotPasswordWindow mw)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 0;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;


                string emailAddress = ConfigurationManager.AppSettings.Get("EmailAddress");
                string emailPassword = ConfigurationManager.AppSettings.Get("EmailPassword");
                client.Credentials = new NetworkCredential(emailAddress, emailPassword);
                MailMessage msg = new MailMessage();
                msg.To.Add(mw.tbMailAddress.Text);
                msg.From = new MailAddress(emailAddress);
                msg.Subject = "Mã xác thực FHMS";
                Random rd = new Random();
                string bodyEmail = "Mã xác thực của bạn là: " + rd.Next(0, 999999);
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
