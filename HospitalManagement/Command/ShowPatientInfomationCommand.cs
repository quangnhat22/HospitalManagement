using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using HospitalManagement.Model;
using HospitalManagement.View;
using HospitalManagement.ViewModel;

namespace HospitalManagement.Command
{
    class ShowPatientInfomationCommand : ICommand
    {
        private PatientViewModel patientViewModel;

        public ShowPatientInfomationCommand(PatientViewModel patientViewModel)
        {
            this.patientViewModel = patientViewModel;
        }

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
            BENHNHAN bn = parameter as BENHNHAN;
            PatientInformationForm patientInformationForm = new PatientInformationForm(bn);
            patientInformationForm.Show();
        }
    }
}
