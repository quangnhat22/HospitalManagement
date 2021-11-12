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
using HospitalManagement.Model;
using HospitalManagement.Utils;
using System.Windows.Controls;

namespace HospitalManagement.Command
{
    class SignUpValidationCommand : ICommand
    {
        private SignUpFormViewModel signUpFormViewModel;
        public SignUpValidationCommand(SignUpFormViewModel signUpFormViewModel)
        {
            this.signUpFormViewModel = signUpFormViewModel;
        }
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
            SignUpWindow signUpWindow = parameter as SignUpWindow;
            if (Check(signUpWindow))
            {
                string gioiTinh = signUpWindow.txbGioiTinh.SelectedValue as string;
                var userInput = new USER
                {
                    HO = signUpWindow.txbHo.Text ,
                    TEN = signUpWindow.txbTen.Text,
                    USERNAME = signUpWindow.txbTenDangNhap.Text,
                    PASSWORD = Encryptor.Hash(signUpWindow.txbMatKhau.Password),
                    EMAIL = signUpWindow.txbEmail.Text,
                    NGSINH = signUpWindow.txbNgaySinh.DisplayDate,
                    GIOITINH = (gioiTinh != "Nam"),
                };
                signUpFormViewModel?.db?.DB?.USERs?.Add(userInput);
                signUpFormViewModel?.db?.DB?.SaveChanges();
                NotifyWindow notifyWindow = new NotifyWindow("Success", "Đăng ký thành công!");
                notifyWindow.ShowDialog();
            }
        }

        public bool Check(SignUpWindow mw)
        {
            if (mw == null) return false;
            List<USER> users = signUpFormViewModel?.db?.DB?.USERs?.ToList();
            foreach (USER user in users)
            {
                if (user.USERNAME == mw.txbTenDangNhap.Text)
                {
                    NotifyWindow notifyWindow = new NotifyWindow("Warning", "Tên đăng nhập này đã tồn tại");
                    notifyWindow.ShowDialog();
                    mw.txbTenDangNhap.Focus();
                    return false;
                }
            }
            if (string.IsNullOrWhiteSpace(mw.txbHo.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập họ");
                notifyWindow.ShowDialog();
                mw.txbHo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(mw.txbTen.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập tên");
                notifyWindow.ShowDialog();
                mw.txbTen.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(mw.txbTenDangNhap.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập tên đăng nhập");
                notifyWindow.ShowDialog();
                mw.txbTenDangNhap.Focus();
                return false;
            }

            

            if (string.IsNullOrWhiteSpace(mw.txbEmail.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập email");
                notifyWindow.ShowDialog();
                mw.txbEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(mw.txbMatKhau.Password))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập mật khẩu");
                notifyWindow.ShowDialog();
                mw.txbMatKhau.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(mw.txbNhapLaiMatKhau.Password))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập lại mật khẩu");
                notifyWindow.ShowDialog();
                mw.txbNhapLaiMatKhau.Focus();
                return false;
            }

            if (mw.txbMatKhau.Password != mw.txbNhapLaiMatKhau.Password)
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Mật khẩu đã nhập không khớp");
                notifyWindow.ShowDialog();
                mw.txbNhapLaiMatKhau.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(mw.txbNgaySinh.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập ngày sinh");
                notifyWindow.ShowDialog();
                mw.txbNgaySinh.Focus();
                return false;
            }

            return true;
        }
    }
}
