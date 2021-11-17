using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.Command
{
    class DeletePatientCommand : ICommand
    {
        private PatientViewModel patientViewModel;

        public DeletePatientCommand(PatientViewModel patientViewModel)
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
            var selectableItems = patientViewModel.Patients.Where(p => p.IsSelected).Select(x => x.Value);
            foreach (BENHNHAN bn in selectableItems)
            {
                bn.VATTUs.Clear();
                DataProvider.Ins.DB.BENHNHANs.Remove(bn);
            }
            DataProvider.Ins.DB.SaveChanges();
            patientViewModel.IsCheckedAll = false;
            patientViewModel.Patients = SelectableItem<BENHNHAN>.GetSelectableItems(DataProvider.Ins.DB.BENHNHANs.ToList());
        }
    }
}
