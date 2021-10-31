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
    class SignUpValidationCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return parameter == null ? false : true;
        }

        public void Execute(object parameter)
        {
            if (Check(parameter as SignUpWindow))
            {
                MessageBox.Show("Đăng ký thành công!");
            }
        }

        public bool Check(SignUpWindow mw)
        {
            if (mw == null) return false;
            if (string.IsNullOrWhiteSpace(mw.txbHo.Text))
            {
                MessageBox.Show("Vui lòng nhập họ");
                mw.txbHo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(mw.txbTen.Text))
            {
                MessageBox.Show("Vui lòng nhập tên");
                mw.txbTen.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(mw.txbTenDangNhap.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập");
                mw.txbTenDangNhap.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(mw.txbEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập email");
                mw.txbEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(mw.txbMatKhau.Password))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu");
                mw.txbMatKhau.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(mw.txbNhapLaiMatKhau.Password))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu");
                mw.txbNhapLaiMatKhau.Focus();
                return false;
            }

            if (mw.txbMatKhau.Password != mw.txbNhapLaiMatKhau.Password)
            {
                MessageBox.Show("Mật khẩu đã nhập không khớp");
                mw.txbNhapLaiMatKhau.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(mw.txbNgaySinh.Text))
            {
                MessageBox.Show("Vui lòng nhập ngày sinh");
                mw.txbNgaySinh.Focus();
                return false;
            }

            return true;
        }
    }
}
