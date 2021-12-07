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
        private static QUANLYBENHVIENEntities db = new QUANLYBENHVIENEntities();
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
            try
            {
                var addNewAccountForm = parameter as AddNewAccountForm;
                if (Check(addNewAccountForm))
                {
                    string roleAccount = addNewAccountForm.txbVaiTro.SelectedValue.ToString();
                    string password = CreatePassword(10);
                    var userInput = new USER
                    {
                        USERNAME = string.Empty,
                        PASSWORD = Encryptor.Hash(password),
                    };

                    if (addNewAccountForm.txbVaiTro.SelectedIndex == 0)
                    {
                        if (checkUsername(userInput.USERNAME))
                        {
                            NotifyWindow notifyWindowUsername = new NotifyWindow("Warning", "Tên đăng nhập này đã tồn tại!");
                            notifyWindowUsername.ShowDialog();
                            addNewAccountForm.txbGroup.Focus();
                            return;
                        }
                        var adminUser = new ADMIN
                        {
                            ID = addNewAccountForm.txbID.Text,
                            IDUSER = userInput.ID,
                            EMAIL = addNewAccountForm.txbEmail.Text,
                        };
                        userInput.USERNAME = adminUser.ID;
                        userInput.ROLE = "admin";
                        db.ADMINs.Add(adminUser);
                    }

                    else if (addNewAccountForm.txbVaiTro.SelectedIndex == 1)
                    {
                        string groupName = addNewAccountForm.txbGroup.SelectedItem.ToString();
                        var leaderUser = new BACSI
                        {
                            CMND_CCCD = addNewAccountForm.txbID.Text,
                            IDTO = int.Parse(groupName),
                            IDUSER = userInput.ID
                        };
                        userInput.USERNAME = leaderUser.CMND_CCCD;
                        userInput.ROLE = "leader";
                        db.BACSIs.Add(leaderUser);
                    }

                    else if (addNewAccountForm.txbVaiTro.SelectedIndex == 2)
                    {
                        string groupName = addNewAccountForm.txbGroup.SelectedItem.ToString();
                        userInput.USERNAME = CreateUsername(groupName);
                        var doctorUser = new BACSI
                        {
                            CMND_CCCD = addNewAccountForm.txbID.Text,
                            IDTO = int.Parse(groupName),
                            IDUSER = userInput.ID
                        };
                        userInput.USERNAME = doctorUser.CMND_CCCD;
                        userInput.ROLE = "doctor";
                        db.BACSIs.Add(doctorUser);
                    }
                    else
                    {
                        string groupName = addNewAccountForm.txbGroup.SelectedItem.ToString();
                        var nurseUser = new YTA
                        {
                            CMND_CCCD = addNewAccountForm.txbID.Text,
                            IDTO = int.Parse(groupName),
                            IDUSER = userInput.ID
                        };
                        userInput.USERNAME = nurseUser.CMND_CCCD;
                        userInput.ROLE = "nurse";
                        db.YTAs.Add(nurseUser);
                    }

                    db.USERs.Add(userInput);
                    db.SaveChanges();
                    SendEmailAccount(addNewAccountForm, password, userInput);
                    NotifyWindow notifyWindow = new NotifyWindow("Success", "Đăng ký mới thành công!");
                    NotifyWindow notifyWindow1 = new NotifyWindow("Success", "Đã gửi tên đăng nhập, mật khẩu qua\t email đăng ký.");
                    notifyWindow.ShowDialog();
                    notifyWindow1.ShowDialog();
                }
            }
            catch
            {
                NotifyWindow notifyWindow = new NotifyWindow("Success", "Vui lòng kiểm tra thông tin");
                notifyWindow.ShowDialog();
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

        public bool checkUsername(string usernameStaff)
        {
            return db.USERs.Any(x=> x.USERNAME == usernameStaff);
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
    }
}
