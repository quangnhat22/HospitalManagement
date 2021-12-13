using HospitalManagement.View;
using HospitalManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HospitalManagement.Command
{
    public class OpenPatientFormCommand : ICommand
    {
        private PatientViewModel patientViewModel;
        public OpenPatientFormCommand(PatientViewModel patientViewModel)
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
            return parameter == null ? false : true;
        }

        public void Execute(object parameter)
        {
            Window window = parameter as Window;
            var patientForm = new PatientForm(patientViewModel);
            Application.Current.MainWindow = patientForm;
            patientForm.Show();
        }
    }
}

