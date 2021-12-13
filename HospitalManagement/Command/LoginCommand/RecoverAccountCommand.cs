using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.View;
using HospitalManagement.View.Login;
using HospitalManagement.ViewModel;
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
        private RecoverAccountViewModel recoverAccountViewModel;

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
        public RecoverAccountCommand(RecoverAccountViewModel recoverAccountViewModel)
        {
            this.recoverAccountViewModel = recoverAccountViewModel;
        }

        public void Execute(object parameter)
        {
            RecoverAccountWindow rw = parameter as RecoverAccountWindow;
            if (Check(rw))
            {
               List<USER> users = recoverAccountViewModel.db.DB.USERs.ToList();
               foreach (USER user in users)
                {
                    if (user.USERNAME == EmailProcessing.emailUserName)
                    {
                        user.PASSWORD = Encryptor.Hash(rw.txbMatKhau.Password);
                        recoverAccountViewModel.db.DB.SaveChanges();
                        NotifyWindow notifyWindow = new NotifyWindow("Success", "Khôi phục tài khoản thành công!");
                        notifyWindow.ShowDialog();
                        break;
                    }
                }
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

            if (rw.txbNhapLaiMatKhau.Password != rw.txbMatKhau.Password)
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Mật khẩu đã nhập không khớp");
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
