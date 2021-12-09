using HospitalManagement.Model;
using HospitalManagement.View;
using HospitalManagement.View.StaffRoleView.TeamTask;
using HospitalManagement.ViewModel;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace HospitalManagement.Command
{
    internal class SaveChangeTaskInformationCommand : ICommand
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
            TaskInformationForm taskForm = parameter as TaskInformationForm;
            if (Check(taskForm))
            {
                BindingExpression beSubject = taskForm.txbSubject.GetBindingExpression(TextBox.TextProperty);
                beSubject.UpdateSource();
                BindingExpression beStart = taskForm.dpStart.GetBindingExpression(DatePicker.TextProperty);
                beStart.UpdateSource();
                BindingExpression beTimeStart = taskForm.tpStart.GetBindingExpression(TimePicker.TextProperty);
                beTimeStart.UpdateSource();
                BindingExpression beEnd = taskForm.dpEnd.GetBindingExpression(DatePicker.TextProperty);
                beEnd.UpdateSource();
                BindingExpression beTimeEnd = taskForm.tpStart.GetBindingExpression(TimePicker.TextProperty);
                beTimeEnd.UpdateSource();
                BindingExpression beLocation = taskForm.txbLocation.GetBindingExpression(TextBox.TextProperty);
                beLocation.UpdateSource();
                BindingExpression beBody = taskForm.txbBody.GetBindingExpression(TextBox.TextProperty);
                beBody.UpdateSource();
                BindingExpression beType = taskForm.cbType.GetBindingExpression(ComboBox.TextProperty);
                beType.UpdateSource();
                DataProvider.Ins?.DB?.SaveChanges();
                NotifyWindow notifyWindow = new NotifyWindow("Success", "Đã cập nhập thành công");
                notifyWindow.ShowDialog();
            }
        }
        public bool Check(TaskInformationForm tif)
        {
            if (tif == null) return false;

            if (string.IsNullOrWhiteSpace(tif.txbSubject.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập tiêu đề");
                notifyWindow.ShowDialog();
                tif.txbSubject.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(tif.txbLocation.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập địa điểm");
                notifyWindow.ShowDialog();
                tif.txbLocation.Focus();
                return false;
            }

            if (DateTime.Compare((DateTime)tif.dpStart.SelectedDate, (DateTime)tif.dpEnd.SelectedDate) > 0)
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Thời điểm bắt đầu phải sớm hơn thời\n"+"                  điểm kết thúc");
                notifyWindow.ShowDialog();
                tif.dpStart.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(tif.txbBody.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập nội dung công việc");
                notifyWindow.ShowDialog();
                tif.txbBody.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(tif.cbType.Text.ToString()))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng chọn mức độ ưu tiên");
                notifyWindow.ShowDialog();
                tif.cbType.Focus();
                return false;
            }

            if (tif.lbxMember.Items.Count==0)
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng chọn thành viên tham gia");
                notifyWindow.ShowDialog();
                tif.lbxMember.Focus();
                return false;
            }
            return true;
        }
    }
}
