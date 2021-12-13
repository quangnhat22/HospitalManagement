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
using HospitalManagement.ViewModel;

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
            return true;
        }

        public void Execute(object parameter)
        {
            SelectableItem<BENHNHAN> selectableItem = parameter as SelectableItem<BENHNHAN>;
            BENHNHAN bn = selectableItem.Value as BENHNHAN;
            PatientInformationForm patientInformationForm = new PatientInformationForm(bn);
            patientInformationForm.Show();
        }
    }
}
