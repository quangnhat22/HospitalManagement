using HospitalManagement.Utils;
using HospitalManagement.View;
using HospitalManagement.View.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.Command
{
    internal class RecoverAccountCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }
        public string MyProperty { get; set; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            RecoverAccountWindow rw = parameter as RecoverAccountWindow;
            if (Check(rw))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Success", "Khôi phục tài khoản thành công!");
                notifyWindow.ShowDialog();
            }
        }
        public bool Check(RecoverAccountWindow rw)
        {
            if (rw == null) return false;
            if (string.IsNullOrWhiteSpace(rw.txbMaXacThuc.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập mã xác thực");
                notifyWindow.ShowDialog();
                rw.txbMaXacThuc.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(rw.txbMatKhau.Password))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập mật khẩu");
                notifyWindow.ShowDialog();
                rw.txbMatKhau.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(rw.txbNhapLaiMatKhau.Password))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập lại mật khẩu");
                notifyWindow.ShowDialog();
                rw.txbNhapLaiMatKhau.Focus();
                return false;
            }

            if (Convert.ToInt32(rw.txbMaXacThuc.Text) != EmailProcessing.vertificedNumber)
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Mã xác thực không chính xác");
                notifyWindow.ShowDialog();
                rw.txbNhapLaiMatKhau.Focus();
                return false;
            }
            return true;
        }
    }
}
