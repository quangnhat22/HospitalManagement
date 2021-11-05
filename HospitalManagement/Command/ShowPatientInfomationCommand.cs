using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using HospitalManagement.Model;
using HospitalManagement.View;

namespace HospitalManagement.Command
{
    class ShowPatientInfomationCommand : ICommand
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
            DataGrid dg = parameter as DataGrid;
            Model.Patient p = dg.SelectedItem as Model.Patient;
            PatientInformationForm patientif = new PatientInformationForm(p);
            patientif.Show();
        }
    }
}
