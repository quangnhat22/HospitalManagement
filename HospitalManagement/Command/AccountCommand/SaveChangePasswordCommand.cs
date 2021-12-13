using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.View;
using HospitalManagement.View.Others;
using HospitalManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.Command.AccountCommand
{
    internal class SaveChangePasswordCommand : ICommand
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
            ChangePasswordWindow changePasswordWindow = parameter as ChangePasswordWindow;  
            if(Check(changePasswordWindow))
            {
                List<USER> users = DataProvider.Ins?.DB?.USERs?.ToList();
                foreach (USER user in users)
                {
                    if (user.USERNAME == MainWindowViewModel.User.USERNAME && user.PASSWORD == MainWindowViewModel.User.PASSWORD)
                    {
                        user.PASSWORD = Encryptor.Hash(changePasswordWindow.txbNewPassword.Password);
                        DataProvider.Ins?.DB?.SaveChanges();
                        break;
                    }    
                }
                NotifyWindow notifyWindow = new NotifyWindow("Success", "Đã cập nhập thành công");
                notifyWindow.ShowDialog();
            }
        }

        public bool Check(ChangePasswordWindow changePasswordWindow)
        {
            if (string.IsNullOrWhiteSpace(changePasswordWindow.txbPasswordCurent.Password))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập mật khẩu hiện tại");
                notifyWindow.ShowDialog();
                changePasswordWindow.txbPasswordCurent.Focus();
                return false;
            }

            if (Encryptor.Hash(changePasswordWindow.txbPasswordCurent.Password) != MainWindowViewModel.User.PASSWORD)
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Mật khẩu hiện tại không đúng");
                notifyWindow.ShowDialog();
                changePasswordWindow.txbPasswordCurent.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(changePasswordWindow.txbNewPassword.Password))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập mật khẩu mới");
                notifyWindow.ShowDialog();
                changePasswordWindow.txbNewPassword.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(changePasswordWindow.txbNewRepassword.Password))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập lại mật khẩu mới");
                notifyWindow.ShowDialog();
                changePasswordWindow.txbNewRepassword.Focus();
                return false;
            }

            if (changePasswordWindow.txbNewPassword.Password != changePasswordWindow.txbNewRepassword.Password)
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Mật khẩu đã nhập không khớp vui lòng nhập lại");
                notifyWindow.ShowDialog();
                changePasswordWindow.txbNewRepassword.Focus();
                return false;
            }
            return true;
        }
    }
}
