using HospitalManagement.Model;
using HospitalManagement.View;
using HospitalManagement.View.Others;
using HospitalManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Data.Entity.Migrations;

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
            if (Check(cw))
            {
                using (QUANLYBENHVIENEntities dbContext = new QUANLYBENHVIENEntities())
                {
                    USER userChange = dbContext.USERs.Find(MainWindowViewModel.User.ID);
                    if (userChange.ROLE == "admin")
                    {
                        var admin = userChange.ADMINs.FirstOrDefault();

                        if (admin != null)
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
                        if (userChange.ROLE == "leader" || userChange.ROLE == "doctor")
                        {
                            var doctor = userChange.BACSIs.FirstOrDefault();
                            if (doctor != null || doctor != default)
                            {
                                doctor.HO = cw.txbLastName.Text;
                                doctor.TEN = cw.txbFistName.Text;
                                doctor.EMAIL = cw.txbEmail.Text;
                                doctor.NGSINH = cw.tbDateTimePicker.SelectedDate;
                                doctor.GIOITINH = (cw.cbSex.Text != "Nam");
                            }
                        }
                        else
                        {
                            var nurse = userChange.YTAs.FirstOrDefault();
                            if (nurse != null || nurse != default)
                            {
                                nurse.HO = cw.txbLastName.Text;
                                nurse.TEN = cw.txbFistName.Text;
                                nurse.EMAIL = cw.txbEmail.Text;
                                nurse.NGSINH = cw.tbDateTimePicker.DisplayDate;
                                nurse.GIOITINH = (cw.cbSex.Text != "Nam");
                            }
                        }
                    }
                    dbContext.SaveChanges();
                }
                NotifyWindow notifyWindow = new NotifyWindow("Success", "Đã cập nhật thành công");
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
