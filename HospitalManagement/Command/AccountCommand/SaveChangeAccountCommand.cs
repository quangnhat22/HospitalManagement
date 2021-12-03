using HospitalManagement.Model;
using HospitalManagement.View;
using HospitalManagement.View.Others;
using HospitalManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.Command
{
    internal class SaveChangeAccountCommand : ICommand
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
            ChangeAccountWindow cw = parameter as ChangeAccountWindow;
            if(Check(cw))
            {
                using (QUANLYBENHVIENEntities dbContext = new QUANLYBENHVIENEntities())
                {
                    List<USER> users = dbContext.USERs?.ToList();
                    foreach (USER user in users)
                    {
                        if (user.USERNAME == MainWindowViewModel.User.USERNAME && user.PASSWORD == MainWindowViewModel.User.PASSWORD)
                        {
                            if(user.ROLE == "admin")
                            {
                                var admin = user.ADMINs.FirstOrDefault();
                                if(admin != null || admin != default)
                                {
                                    admin.HO = cw.txbLastName.Text;
                                    admin.TEN = cw.txbFistName.Text;
                                    admin.EMAIL = cw.txbEmail.Text;
                                    admin.NGSINH = cw.tbDateTimePicker.DisplayDate;
                                    admin.GIOITINH = (cw.cbSex.Text != "Nam");
                                }
                            }
                            else
                            {
                                var leader = user.TOes.FirstOrDefault().TOTRUONG;
                                if (leader != null || leader != default)
                                {
                                    leader.HO = cw.txbLastName.Text;
                                    leader.TEN = cw.txbFistName.Text;
                                    leader.EMAIL = cw.txbEmail.Text;
                                    leader.NGSINH = cw.tbDateTimePicker.DisplayDate;
                                    leader.GIOITINH = (cw.cbSex.Text != "Nam");
                                }
                            } 
                                
                            dbContext.SaveChanges();
                            break;
                        }
                    }
                }          
                NotifyWindow notifyWindow = new NotifyWindow("Success", "Đã cập nhập thành công");
                notifyWindow.ShowDialog();
            }    
        }
        public bool Check(ChangeAccountWindow cw)
        {
            if (cw == null) return false;
            if (string.IsNullOrWhiteSpace(cw.txbFistName.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập Tên");
                notifyWindow.ShowDialog();
                cw.txbFistName.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(cw.txbLastName.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập Họ");
                notifyWindow.ShowDialog();
                cw.txbLastName.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(cw.txbEmail.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập Email");
                notifyWindow.ShowDialog();
                cw.txbEmail.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(cw.tbDateTimePicker.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập ngày sinh");
                notifyWindow.ShowDialog();
                cw.tbDateTimePicker.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(cw.cbSex.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng chọn giới tính");
                notifyWindow.ShowDialog();
                cw.cbSex.Focus();
                return false;
            }
            return true;
        }
    }
}
