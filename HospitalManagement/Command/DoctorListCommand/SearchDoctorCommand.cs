using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.Command
{
    internal class SearchDoctorCommand : ICommand
    {
        private DoctorViewModel doctorViewModel;

        public SearchDoctorCommand(DoctorViewModel doctorViewModel)
        {
            this.doctorViewModel = doctorViewModel;
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
            doctorViewModel.SearchBox = doctorViewModel.SearchBox.Trim();
            if (doctorViewModel.SearchBox == string.Empty || doctorViewModel.SearchBox == null)
            {
                doctorViewModel.Doctors = SelectableItem<BACSI>.GetSelectableItems(DataProvider.Ins.DB.BACSIs.ToList());
            }
            else
            {
                string RemoveSignString = VietnameseSign.convertToUnSign2(doctorViewModel.SelectedFilter).ToLower();
                if (RemoveSignString == "cmnd")
                {
                    doctorViewModel.Doctors = SelectableItem<BACSI>.GetSelectableItems(
                                                                                            DataProvider.Ins.DB.BACSIs.Where(
                                                                                                bs => bs.CMND_CCCD.Contains(doctorViewModel.SearchBox)).ToList());
                }
                else if (RemoveSignString == "ho")
                {
                    //Doctors = DataProvider.Ins.DB.BACSIs.Where(p => p.HO.Contains(searchBox)).ToList();
                    doctorViewModel.Doctors = SelectableItem<BACSI>.GetSelectableItems(
                    DataProvider.Ins.DB.BACSIs.Where(delegate (BACSI bs)
                    {
                        return VietnameseSign.ContainsUnsigned(bs.HO, doctorViewModel.SearchBox);
                    }).AsQueryable().ToList()
                    );
                }
                else if (RemoveSignString == "ten")
                {
                    //Doctors = DataProvider.Ins.DB.BACSIs.Where(p => p.TEN.Contains(searchBox)).ToList();
                    doctorViewModel.Doctors = SelectableItem<BACSI>.GetSelectableItems(
                        DataProvider.Ins.DB.BACSIs.Where(delegate (BACSI bs)
                        {
                            return VietnameseSign.ContainsUnsigned(bs.TEN, doctorViewModel.SearchBox);
                        }).AsQueryable().ToList()
                    );
                }
                else if (RemoveSignString == "ho va ten")
                {
                    doctorViewModel.Doctors = SelectableItem<BACSI>.GetSelectableItems(
                        DataProvider.Ins.DB.BACSIs.Where(delegate (BACSI bs)
                        {
                            // Has some problems
                            return VietnameseSign.ContainsUnsigned(bs.HO + " " + bs.TEN, doctorViewModel.SearchBox);
                        }).AsQueryable().ToList()
                    );
                }
            }
        }
    }
}
