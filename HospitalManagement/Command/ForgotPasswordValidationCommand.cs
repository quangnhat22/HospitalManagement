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
            if (Check(parameter as ForgotPasswordWindow))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Success", "Mã xác thực đã được gửi về email!");
                notifyWindow.Show();
            }
        }

        public bool Check(ForgotPasswordWindow mw)
        {
            if (mw == null) return false;
            if (string.IsNullOrWhiteSpace(mw.tbMailAddress.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập địa chỉ email");
                notifyWindow.Show();
                mw.tbMailAddress.Focus();
                return false;
            }
            return true;
        }
    }
}
