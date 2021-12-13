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
        private string groupName;
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
                if(MainWindowViewModel.User.ROLE == "sudo")
                {
                    if (checkSuperAdmin(addNewAccountForm))
                    {
                        string roleAccount = addNewAccountForm.txbVaiTro.SelectedValue.ToString();
                        string password = createPassword(10);

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
                                GIOITINH = false as bool?
                            };
                            userInput.USERNAME = adminUser.ID;
                            userInput.ROLE = "admin";
                            db.ADMINs.Add(adminUser);
                        }

                        else if (addNewAccountForm.txbVaiTro.SelectedIndex == 1)
                        {
                            groupName = addNewAccountForm.txbGroup.SelectedItem.ToString();

                            if (checkLeaderTeam(addNewAccountForm))
                            {
                                NotifyWindow ntf = new NotifyWindow("Warning", "Tổ này đã có nhóm trưởng");
                                ntf.ShowDialog();
                                addNewAccountForm.txbEmail.Focus();
                                return;
                            }

                            var leaderUser = new BACSI
                            {
                                CMND_CCCD = addNewAccountForm.txbID.Text,
                                IDTO = int.Parse(groupName),
                                IDUSER = userInput.ID,
                                GIOITINH = false as bool?
                            };
                            userInput.USERNAME = leaderUser.CMND_CCCD;
                            userInput.ROLE = "leader";
                            db.BACSIs.Add(leaderUser);
                        }

                        else if (addNewAccountForm.txbVaiTro.SelectedIndex == 2)
                        {

                            string groupName = addNewAccountForm.txbGroup.SelectedItem.ToString();
                            groupName = addNewAccountForm.txbGroup.SelectedItem.ToString();
                            var doctorUser = new BACSI
                            {
                                CMND_CCCD = addNewAccountForm.txbID.Text,
                                IDTO = int.Parse(groupName),
                                IDUSER = userInput.ID,
                                GIOITINH = false as bool?
                            };
                            userInput.USERNAME = doctorUser.CMND_CCCD;
                            userInput.ROLE = "doctor";
                            db.BACSIs.Add(doctorUser);
                        }
                        else
                        {
                            groupName = addNewAccountForm.txbGroup.SelectedItem.ToString();
                            var nurseUser = new YTA
                            {
                                CMND_CCCD = addNewAccountForm.txbID.Text,
                                IDTO = int.Parse(groupName),
                                IDUSER = userInput.ID,
                                GIOITINH = false as bool?
                            };
                            userInput.USERNAME = nurseUser.CMND_CCCD;
                            userInput.ROLE = "nurse";
                            db.YTAs.Add(nurseUser);
                        }

                        db.USERs.Add(userInput);
                        db.SaveChanges();
                        SendEmailAccount(addNewAccountForm, password, userInput);
                        SendEmailAccountToFHMS(addNewAccountForm, userInput);
                        NotifyWindow notifyWindow = new NotifyWindow("Success", "Đăng ký mới thành công!");
                        NotifyWindow notifyWindow1 = new NotifyWindow("Success", "Đã gửi tên đăng nhập, mật khẩu qua\t email đăng ký.");
                        notifyWindow.ShowDialog();
                        notifyWindow1.ShowDialog();
                    }
                }
                else
                {
                    if (checkAdmin(addNewAccountForm))
                    {
                        string roleAccount = addNewAccountForm.txbVaiTro.SelectedValue.ToString();
                        string password = createPassword(10);

                        var userInput = new USER
                        {
                            USERNAME = string.Empty,
                            PASSWORD = Encryptor.Hash(password),
                        };

                        if (addNewAccountForm.txbVaiTro.SelectedIndex == 0)
                        {
                            groupName = addNewAccountForm.txbGroup.SelectedItem.ToString();

                            if (checkLeaderTeam(addNewAccountForm))
                            {
                                NotifyWindow ntf = new NotifyWindow("Warning", "Tổ này đã có nhóm trưởng");
                                ntf.ShowDialog();
                                addNewAccountForm.txbEmail.Focus();
                                return;
                            }

                            var leaderUser = new BACSI
                            {
                                CMND_CCCD = addNewAccountForm.txbID.Text,
                                IDTO = int.Parse(groupName),
                                IDUSER = userInput.ID,
                                GIOITINH = false as bool?
                            };
                            userInput.USERNAME = leaderUser.CMND_CCCD;
                            userInput.ROLE = "leader";
                            db.BACSIs.Add(leaderUser);
                        }

                        else if (addNewAccountForm.txbVaiTro.SelectedIndex == 1)
                        {

                            string groupName = addNewAccountForm.txbGroup.SelectedItem.ToString();
                            groupName = addNewAccountForm.txbGroup.SelectedItem.ToString();
                            var doctorUser = new BACSI
                            {
                                CMND_CCCD = addNewAccountForm.txbID.Text,
                                IDTO = int.Parse(groupName),
                                IDUSER = userInput.ID,
                                GIOITINH = false as bool?
                            };
                            userInput.USERNAME = doctorUser.CMND_CCCD;
                            userInput.ROLE = "doctor";
                            db.BACSIs.Add(doctorUser);
                        }
                        else
                        {
                            groupName = addNewAccountForm.txbGroup.SelectedItem.ToString();
                            var nurseUser = new YTA
                            {
                                CMND_CCCD = addNewAccountForm.txbID.Text,
                                IDTO = int.Parse(groupName),
                                IDUSER = userInput.ID,
                                GIOITINH = false as bool?
                            };
                            userInput.USERNAME = nurseUser.CMND_CCCD;
                            userInput.ROLE = "nurse";
                            db.YTAs.Add(nurseUser);
                        }

                        db.USERs.Add(userInput);
                        db.SaveChanges();
                        SendEmailAccount(addNewAccountForm, password, userInput);
                        SendEmailAccountToFHMS(addNewAccountForm, userInput);
                        NotifyWindow notifyWindow = new NotifyWindow("Success", "Đăng ký mới thành công!");
                        NotifyWindow notifyWindow1 = new NotifyWindow("Success", "Đã gửi tên đăng nhập, mật khẩu qua\t email đăng ký.");
                        notifyWindow.ShowDialog();
                        notifyWindow1.ShowDialog();
                    }
                }
                
            }
            catch
            {
                NotifyWindow notifyWindow = new NotifyWindow("Error", "Vui lòng kiểm tra thông tin");
                notifyWindow.ShowDialog();
            }
        }
        
        public bool checkSuperAdmin(AddNewAccountForm addNewAccountForm)
        {
            if (addNewAccountForm == null) return false;
            List<USER> users = DataProvider.Ins.DB.USERs.ToList();

            if (string.IsNullOrWhiteSpace(addNewAccountForm.txbVaiTro.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng chọn vai trò");
                notifyWindow.ShowDialog();
                addNewAccountForm.txbVaiTro.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(addNewAccountForm.txbID.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập CMND/CCCD");
                notifyWindow.ShowDialog();
                addNewAccountForm.txbID.Focus();
                return false;
            }

            if (db.BACSIs.Find(addNewAccountForm.txbID.Text) != null ||
                    db.YTAs.Find(addNewAccountForm.txbID.Text) != null ||
                    db.ADMINs.Find(addNewAccountForm.txbID.Text) != null ||
                    db.BENHNHANs.Find(addNewAccountForm.txbID.Text) != null)
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "CMND/CCCD đã tồn tại");
                notifyWindow.ShowDialog();
                addNewAccountForm.txbID.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(addNewAccountForm.txbEmail.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập email");
                notifyWindow.ShowDialog();
                addNewAccountForm.txbEmail.Focus();
                return false;
            }

            if (!ValidatorEmail.EmailIsValid(addNewAccountForm.txbEmail.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Địa chỉ email không hợp lệ");
                notifyWindow.ShowDialog();
                addNewAccountForm.txbEmail.Focus();
                return false;
            }

            if (addNewAccountForm.txbVaiTro.SelectedIndex != 0)
            {
                if (string.IsNullOrWhiteSpace(addNewAccountForm.txbGroup.Text))
                {
                    NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng chọn tổ công tác");
                    notifyWindow.ShowDialog();
                    addNewAccountForm.txbGroup.Focus();
                    return false;
                }
            }

            return true;
        }

        public bool checkAdmin(AddNewAccountForm addNewAccountForm)
        {
            if (addNewAccountForm == null) return false;
            List<USER> users = DataProvider.Ins.DB.USERs.ToList();

            if (string.IsNullOrWhiteSpace(addNewAccountForm.txbVaiTro.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng chọn vai trò");
                notifyWindow.ShowDialog();
                addNewAccountForm.txbVaiTro.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(addNewAccountForm.txbID.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập CMND/CCCD");
                notifyWindow.ShowDialog();
                addNewAccountForm.txbID.Focus();
                return false;
            }

            if (db.BACSIs.Find(addNewAccountForm.txbID.Text) != null ||
                    db.YTAs.Find(addNewAccountForm.txbID.Text) != null ||
                    db.ADMINs.Find(addNewAccountForm.txbID.Text) != null ||
                    db.BENHNHANs.Find(addNewAccountForm.txbID.Text) != null)
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "CMND/CCCD đã tồn tại");
                notifyWindow.ShowDialog();
                addNewAccountForm.txbID.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(addNewAccountForm.txbEmail.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập email");
                notifyWindow.ShowDialog();
                addNewAccountForm.txbEmail.Focus();
                return false;
            }

            if (!ValidatorEmail.EmailIsValid(addNewAccountForm.txbEmail.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Địa chỉ email không hợp lệ");
                notifyWindow.ShowDialog();
                addNewAccountForm.txbEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(addNewAccountForm.txbGroup.Text))
            {
                 NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng chọn tổ công tác");
                 notifyWindow.ShowDialog();
                 addNewAccountForm.txbGroup.Focus();
                 return false;
             }
            return true;
        }
        public bool checkLeaderTeam(AddNewAccountForm addNewAccountForm)
        {
            List<USER> usersTeam = new List<USER>();
            string query = @"SELECT IDTO, ROLE
                             FROM BACSI BS, [USER] U
                               WHERE BS.IDUSER = U.ID";
            List<TeamRole> teamRoleList = db.Database.SqlQuery<TeamRole>(query).ToList();  
            foreach (var bacsi in teamRoleList)
            {
                if (bacsi.IDTO == int.Parse(groupName) as int?)
                    if (bacsi.ROLE == "leader")
                        return true;
            }
            return false;
        }

        class TeamRole
        {
            private int? iDTO;
            private string rOLE;

            public int? IDTO { get => iDTO; set => iDTO = value; }
            public string ROLE { get => rOLE; set => rOLE = value; }
        }


        public bool checkUsername(string usernameStaff)
        {
            return db.USERs.Any(x=> x.USERNAME == usernameStaff);
        }

        public string createPassword(int length)
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
        public void SendEmailAccountToFHMS(AddNewAccountForm addNewAccountForm, USER user)
        {
            string emailSubject = "FHMS Create Account";
            string emailBody = $"ADMIN USERNAME [{MainWindowViewModel.User.USERNAME}] đã tạo một tài khoản mới: " +
                                $"\n Tên đăng nhập đã tạo mới [{user.USERNAME}], VAI TRO: [{user.ROLE}]";
            string emailAddress = ConfigurationManager.AppSettings.Get("EmailAddress");
            string emailPassword = ConfigurationManager.AppSettings.Get("EmailPassword");
            EmailProcessing emailProcessing = new EmailProcessing(emailAddress, emailAddress,
                                                                    emailPassword, emailSubject, emailBody);
            emailProcessing.sendEmail();
        }


    }
}
