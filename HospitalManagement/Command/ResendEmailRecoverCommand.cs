using HospitalManagement.Utils;
using HospitalManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HospitalManagement.Command
{
    internal class ResendEmailRecoverCommand : ICommand
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
        public void Execute(object parameter)
        {
            try
            {
                string emailSubject = "Mã xác thực FHMS";
                Random rd = new Random();
                string emailBody = "Mã xác thực của bạn là: " + rd.Next(0, 999999);
                EmailProcessing emailProcessing = new EmailProcessing(EmailProcessing.emailName, "hotrofhms@gmail.com", "supportfhms719", emailSubject, emailBody);
                emailProcessing.sendEmail();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
