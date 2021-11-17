using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.Command.FacilitiesListCommand
{
    class DeleteFacilitiesCommand : ICommand
    {
        private FacilitiesViewModel facilitiesViewModel;

        public DeleteFacilitiesCommand(FacilitiesViewModel facilitiesViewModel)
        {
            this.facilitiesViewModel = facilitiesViewModel;
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
            var selectableItems = facilitiesViewModel.Facilities.Where(p => p.IsSelected).Select(x => x.Value);
            foreach (VATTU vt in selectableItems)
            {
                vt.BENHNHANs.Clear();
                vt.TOes.Clear();
                DataProvider.Ins.DB.VATTUs.Remove(vt);
            }
            DataProvider.Ins.DB.SaveChanges();
            facilitiesViewModel.IsCheckedAll = false;
            facilitiesViewModel.Facilities = SelectableItem<VATTU>.GetSelectableItems(DataProvider.Ins.DB.VATTUs.ToList());
        }
    }
}
