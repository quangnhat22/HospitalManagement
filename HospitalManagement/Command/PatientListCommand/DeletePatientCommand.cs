using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.View;
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
            if(selectableItems.Count() > 0)
            {
                NotifyWindow notifyWindow;
                if (selectableItems.Count() == 1)
                {
                    notifyWindow = new NotifyWindow("Check", "Bạn có chắc chắn muốn xoá bệnh nhân này?", "Visible", 400);
                }    
                else
                {
                    notifyWindow = new NotifyWindow("Check", "Bạn có chắc chắn muốn xoá những bệnh nhân này?", "Visible", 400);
                }    
                notifyWindow.ShowDialog();
                if (notifyWindow.Result == System.Windows.MessageBoxResult.OK)
                {
                    foreach (BENHNHAN bn in selectableItems)
                    {
                        DataProvider.Ins.DB.BENHNHANs.Remove(bn);
                    }
                    DataProvider.Ins.DB.SaveChanges();
                    patientViewModel.IsCheckedAll = false;
                    patientViewModel.Patients = SelectableItem<BENHNHAN>.GetSelectableItems(DataProvider.Ins.DB.BENHNHANs.ToList());
                    notifyWindow = new NotifyWindow("Success", "Đã xóa thành công!");
                    notifyWindow.Show();
                }
            }                
        }
    }
}
