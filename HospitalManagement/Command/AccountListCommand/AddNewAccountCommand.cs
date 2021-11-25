using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.View;
using HospitalManagement.View.AddStaff;
using HospitalManagement.ViewModel;
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
                string roleAccount = addNewAccountForm.txbVaiTro.SelectedValue.ToString();
                string password = CreatePassword(10);
                var userInput = new USER
                {
                    USERNAME = string.Empty,
                    PASSWORD = Encryptor.Hash(password),
                    ROLE = roleAccount,
                };
                
                if (addNewAccountForm.txbVaiTro.SelectedIndex == 0)
                {
                    userInput.USERNAME = addNewAccountForm.txbTenDangNhap.Text;
                    var adminUser = new ADMIN
                    {
                        ID = addNewAccountForm.txbID.Text,
                        IDUSER = userInput.ID,
                        EMAIL = addNewAccountForm.txbEmail.Text,
                    };
                    DataProvider.Ins.DB.ADMINs.Add(adminUser);
                }

                if (addNewAccountForm.txbVaiTro.SelectedIndex == 1)
                {
                    string groupName = addNewAccountForm.txbGroup.SelectedItem.ToString();
                    userInput.USERNAME = CreateUsername(groupName);
                    if(checkUsernameStaff(userInput.USERNAME))
                    {
                        NotifyWindow notifyWindowUsername = new NotifyWindow("Warning", "Tài khoản cho tổ này đã tồn tại!");
                        notifyWindowUsername.ShowDialog();
                        addNewAccountForm.txbGroup.Focus();
                        return;
                    }

                    DataProvider.Ins.DB.TOes.ToList().ForEach(x =>
                        {
                            if (x.ID == int.Parse(groupName))
                                x.IDUSER = userInput.ID;
                        });
                }

                DataProvider.Ins.DB.USERs.Add(userInput);
                DataProvider.Ins.DB.SaveChanges();
                SendEmailAccount(addNewAccountForm, password, userInput);
                SendEmailAccountToFHMS(addNewAccountForm, userInput);
                NotifyWindow notifyWindow = new NotifyWindow("Success", "Đăng ký mới thành công!");
                NotifyWindow notifyWindow1 = new NotifyWindow("Success", "Đã gửi tên đăng nhập, mật khẩu qua\t email đăng ký.");
                notifyWindow.ShowDialog();
                notifyWindow1.ShowDialog();
                
            }
        }
        
        public bool Check(AddNewAccountForm addNewAccountForm)
        {
            if (addNewAccountForm == null) return false;
            List<USER> users = DataProvider.Ins.DB.USERs.ToList();
            if (string.IsNullOrWhiteSpace(addNewAccountForm.txbEmail.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập email");
                notifyWindow.ShowDialog();
                addNewAccountForm.txbEmail.Focus();
                return false;
            }

            if (addNewAccountForm.txbVaiTro.SelectedIndex == 0)
            {
                if(DataProvider.Ins.DB.BACSIs.Find(addNewAccountForm.txbID.Text) != null ||
                    DataProvider.Ins.DB.YTAs.Find(addNewAccountForm.txbID.Text) != null ||
                    DataProvider.Ins.DB.ADMINs.Find(addNewAccountForm.txbID.Text) != null ||
                    DataProvider.Ins.DB.BENHNHANs.Find(addNewAccountForm.txbID.Text) != null)
                {
                    NotifyWindow notifyWindow = new NotifyWindow("Warning", "CMND/CCCD đã tồn tại");
                    notifyWindow.ShowDialog();
                    addNewAccountForm.txbID.Focus();
                    return false;
                }

                if (string.IsNullOrWhiteSpace(addNewAccountForm.txbTenDangNhap.Text))
                {
                    NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập tên đăng nhập");
                    notifyWindow.ShowDialog();
                    addNewAccountForm.txbTenDangNhap.Focus();
                    return false;
                }

                if (string.IsNullOrWhiteSpace(addNewAccountForm.txbID.Text))
                {
                    NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập CMND/CCCD");
                    notifyWindow.ShowDialog();
                    addNewAccountForm.txbID.Focus();
                    return false;
                }
                foreach (USER user in users)
                {
                    if (user.USERNAME == addNewAccountForm.txbTenDangNhap.Text)
                    {
                        NotifyWindow notifyWindow = new NotifyWindow("Warning", "Tên đăng nhập này đã tồn tại");
                        notifyWindow.ShowDialog();
                        addNewAccountForm.txbTenDangNhap.Focus();
                        return false;
                    }
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(addNewAccountForm.txbGroup.Text))
                {
                    NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng chọn tổ công tác");
                    notifyWindow.ShowDialog();
                    addNewAccountForm.txbGroup.Focus();
                    return false;
                }
            }
            

            if (!addNewAccountForm.txbEmail.Text.Contains('@'))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Email không hợp lệ!");
                notifyWindow.ShowDialog();
                addNewAccountForm.txbEmail.Focus();
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
        public string CreateUsername(string groupName)
        {
            return "fhmsto" + groupName;
        }

        public bool checkUsernameStaff(string usernameStaff)
        {
            return DataProvider.Ins.DB.USERs.Any(x=> x.USERNAME == usernameStaff);
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

        public void SendEmailAccount(AddNewAccountForm addNewAccountForm, string password, USER user)
        {
            string emailSubject = "FHMS New Account";
            string emailBody = "Vui lòng không tiết lộ thông tin này ra ngoài!" +
                                $"\n Tên đăng nhập: {user.USERNAME}" +
                                $"\n Mật khẩu: {password}";
            string emailAddress = ConfigurationManager.AppSettings.Get("EmailAddress");
            string emailPassword = ConfigurationManager.AppSettings.Get("EmailPassword");
            EmailProcessing emailProcessing = new EmailProcessing(addNewAccountForm.txbEmail.Text, emailAddress, 
                                                                    emailPassword, emailSubject, emailBody);
            emailProcessing.sendEmail();
        }

        public string findIdUserAdmin()
        {
            string idAdmin = string.Empty;
            DataProvider.Ins.DB.ADMINs.ToList().ForEach(ad =>
                {
                    if (ad.IDUSER == MainWindowViewModel.User.ID)
                        idAdmin = ad.ID;
                });
            return idAdmin;
        }

        public void SendEmailAccountToFHMS(AddNewAccountForm addNewAccountForm, USER user)
        {
            string idAdmin = findIdUserAdmin();
            string emailSubject = "FHMS Create Account";
            string emailBody = $"ID Admin: {MainWindowViewModel.User.ID} đã tạo một tài khoản mới: " +
                                $"\n Tên đăng nhập đã tạo mới: {user.USERNAME}";
            string emailAddress = ConfigurationManager.AppSettings.Get("EmailAddress");
            string emailPassword = ConfigurationManager.AppSettings.Get("EmailPassword");
            EmailProcessing emailProcessing = new EmailProcessing(emailAddress, emailAddress,
                                                                    emailPassword, emailSubject, emailBody);
            emailProcessing.sendEmail();
        }
    }
}
