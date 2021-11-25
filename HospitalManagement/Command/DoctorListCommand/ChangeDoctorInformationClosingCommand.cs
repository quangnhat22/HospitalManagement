using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.View;
using HospitalManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.Command
{
    internal class ChangeDoctorInformationClosingCommand : ICommand
    {
        private DoctorInformationViewModel doctorFormViewModel;
        public ChangeDoctorInformationClosingCommand(DoctorInformationViewModel doctorFormViewModel)
        {
            this.doctorFormViewModel = doctorFormViewModel;
        }


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
            DoctorForm doctorForm = parameter as DoctorForm;
            NotifyWindow notifyWindow = new NotifyWindow("Success", "OK");
            notifyWindow.ShowDialog();
        }
    }
}
