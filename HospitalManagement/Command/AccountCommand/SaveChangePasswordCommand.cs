using HospitalManagement.View;
using HospitalManagement.View.Others;
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
            throw new NotImplementedException();
        }

        public bool Check(ChangePasswordWindow changePasswordWindow)
        {
            if (changePasswordWindow == null) return false;
            //List<USER> users = signUpFormViewModel?.db?.DB?.USERs?.ToList();
            //foreach (USER user in users)
            //{
            //    if (user.USERNAME == mw.txbTenDangNhap.Text)
            //    {
            //        NotifyWindow notifyWindow = new NotifyWindow("Warning", "Tên đăng nhập này đã tồn tại");
            //        notifyWindow.ShowDialog();
            //        mw.txbTenDangNhap.Focus();
            //        return false;
            //    }
            //}
            if (string.IsNullOrWhiteSpace(changePasswordWindow.txbPasswordCurent.Password))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập mật khẩu hiện tại");
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
