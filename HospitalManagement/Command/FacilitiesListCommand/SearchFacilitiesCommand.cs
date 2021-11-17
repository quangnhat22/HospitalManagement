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
    class SearchFacilitiesCommand : ICommand
    {
        private FacilitiesViewModel facilitiesViewModel;

        public SearchFacilitiesCommand(FacilitiesViewModel facilitiesViewModel)
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
            facilitiesViewModel.SearchBox = facilitiesViewModel.SearchBox.Trim();
            if (facilitiesViewModel.SearchBox == string.Empty || facilitiesViewModel.SearchBox == null)
            {
                facilitiesViewModel.Facilities = SelectableItem<VATTU>.GetSelectableItems(DataProvider.Ins.DB.VATTUs.ToList());
            }
            else
            {
                string RemoveSignString = VietnameseSign.convertToUnSign2(facilitiesViewModel.SelectedFilter).ToLower();
                if (RemoveSignString == "ten vat tu")
                {
                    facilitiesViewModel.Facilities = SelectableItem<VATTU>.GetSelectableItems(
                                                                                            DataProvider.Ins.DB.VATTUs.Where(
                                                                                                p => p.DISPLAYNAME.Contains(facilitiesViewModel.SearchBox)).ToList());
                }
                else if (RemoveSignString == "loai vat tu")
                {
                    //Patients = DataProvider.Ins.DB.BENHNHANs.Where(p => p.HO.Contains(searchBox)).ToList();
                    facilitiesViewModel.Facilities = SelectableItem<VATTU>.GetSelectableItems(
                    DataProvider.Ins.DB.VATTUs.Where(delegate (VATTU vt)
                    {
                        return VietnameseSign.ContainsUnsigned(vt.LOAIVATTU, facilitiesViewModel.SearchBox);
                    }).AsQueryable().ToList()
                    );
                }
            }
        }
    }
}
