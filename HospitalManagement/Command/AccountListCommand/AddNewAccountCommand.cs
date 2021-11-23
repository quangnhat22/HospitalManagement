using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.View;
using HospitalManagement.View.AddStaff;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace HospitalManagement.Command.AccountListCommand
{
    public class AddNewAccountCommand : ICommand
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
            var addNewAccountForm = parameter as AddNewAccountForm;
            if(Check(addNewAccountForm))
            {
                string roleAccount = addNewAccountForm.txbVaiTro.SelectedValue as string;
                string password = CreatePassword(16);
                var userInput = new USER
                {
                    USERNAME = addNewAccountForm.txbTenDangNhap.Text,
                    PASSWORD = Encryptor.Hash(password),
                    ROLE = roleAccount
                };
                DataProvider.Ins.DB.USERs.Add(userInput);
                DataProvider.Ins.DB.SaveChanges();
                SendEmailAccount(addNewAccountForm, password);
                NotifyWindow notifyWindow = new NotifyWindow("Success", "Đăng ký mới thành công!");
                notifyWindow.ShowDialog();

            }
        }
        public bool Check(AddNewAccountForm addNewAccountForm)
        {
            if (addNewAccountForm == null) return false;
            List<USER> users = DataProvider.Ins.DB.USERs.ToList();
            foreach(USER user in users)
            {
                if(user.USERNAME == addNewAccountForm.txbTenDangNhap.Text)
                {
                    NotifyWindow notifyWindow = new NotifyWindow("Warning", "Tên đăng nhập này đã tồn tại");
                    notifyWindow.ShowDialog();
                    addNewAccountForm.txbTenDangNhap.Focus();
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(addNewAccountForm.txbEmail.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập email");
                notifyWindow.ShowDialog();
                addNewAccountForm.txbEmail.Focus();
                return false;
            }

            if(!addNewAccountForm.txbEmail.Text.Contains('@'))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Email không hợp lệ!");
                notifyWindow.ShowDialog();
                addNewAccountForm.txbEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(addNewAccountForm.txbTenDangNhap.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập tên đăng nhập");
                notifyWindow.ShowDialog();
                addNewAccountForm.txbTenDangNhap.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(addNewAccountForm.txbVaiTro.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng chọn vai trò");
                notifyWindow.ShowDialog();
                addNewAccountForm.txbVaiTro.Focus();
                return false;
            }
            return true;
        }
        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public void SendEmailAccount(AddNewAccountForm addNewAccountForm, string password)
        {
            string emailSubject = "FHMS New Account";
            string emailBody = "Vui lòng không tiết lộ thông tin này ra ngoài!" +
                                $"\n Tên đăng nhập: {addNewAccountForm.txbTenDangNhap.Text}" +
                                $"\n Mật khẩu: {password}";
            string emailAddress = ConfigurationManager.AppSettings.Get("EmailAddress");
            string emailPassword = ConfigurationManager.AppSettings.Get("EmailPassword");
            EmailProcessing emailProcessing = new EmailProcessing(addNewAccountForm.txbEmail.Text, emailAddress, 
                                                                    emailPassword, emailSubject, emailBody);
            emailProcessing.sendEmail();
        }
    }
}
