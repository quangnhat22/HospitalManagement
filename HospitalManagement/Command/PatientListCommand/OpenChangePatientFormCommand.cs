using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.Command.PatientListCommand
{
    internal class OpenChangePatientFormCommand:ICommand
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
            SelectableItem<BENHNHAN> selectableItem = parameter as SelectableItem<BENHNHAN>;
            BENHNHAN bn = selectableItem.Value as BENHNHAN;
            ChangePatientInformationForm changePatientInformationForm = new ChangePatientInformationForm(bn);
            changePatientInformationForm.Show();
        }
    }
}
