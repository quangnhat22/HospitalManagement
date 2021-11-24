using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.View;
using HospitalManagement.View.Staff;
using HospitalManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.Command
{
    internal class SaveChangeDoctorInformationCommand: ICommand
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
            DataProvider.Ins?.DB?.SaveChanges();
            NotifyWindow notifyWindow = new NotifyWindow("Success", "Đã cập nhập thành công");
            notifyWindow.ShowDialog();
        }
    }
}
