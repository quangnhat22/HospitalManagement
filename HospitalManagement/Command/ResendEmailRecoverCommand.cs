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

        private string emailAddress;
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public ResendEmailRecoverCommand(string emailToAddress)
        {
            this.emailAddress = emailToAddress;
        }
        public void Execute(object parameter)
        {
            try
            {
                string emailSubject = "Mã xác thực FHMS";
                Random rd = new Random();
                string emailBody = "Mã xác thực của bạn là: " + rd.Next(0, 999999);
                EmailProcessing emailProcessing = new EmailProcessing(emailAddress, "hotrofhms@gmail.com", "supportfhms719", emailSubject, emailBody);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
