using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.View;
using HospitalManagement.View.Staff;
using HospitalManagement.ViewModel;

namespace HospitalManagement.Command.DoctorListCommand
{
    internal class OpenChangeDoctorFormCommand: ICommand
    {
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
            SelectableItem<BACSI> selectableItem = parameter as SelectableItem<BACSI>;
            BACSI bs = selectableItem.Value as BACSI;
            ChangeDoctorInformationForm changeDoctorInformationForm = new ChangeDoctorInformationForm(bs);
            changeDoctorInformationForm.Show();
        }
    }
}
