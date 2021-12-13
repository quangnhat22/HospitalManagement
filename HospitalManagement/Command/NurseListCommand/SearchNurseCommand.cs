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
    internal class SearchNurseCommand: ICommand
    {
        private NurseViewModel nurseViewModel;

        public SearchNurseCommand(NurseViewModel NurseViewModel)
        {
            this.nurseViewModel = NurseViewModel;
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
            nurseViewModel.SearchBox = nurseViewModel.SearchBox.Trim();
            if (nurseViewModel.SearchBox == string.Empty || nurseViewModel.SearchBox == null)
            {
                nurseViewModel.Nurses = SelectableItem<YTA>.GetSelectableItems(DataProvider.Ins.DB.YTAs.ToList());
            }
            else
            {
                string RemoveSignString = VietnameseSign.convertToUnSign2(nurseViewModel.SelectedFilter).ToLower();
                if (RemoveSignString == "cmnd")
                {
                    nurseViewModel.Nurses = SelectableItem<YTA>.GetSelectableItems(
                                                                                            DataProvider.Ins.DB.YTAs.Where(
                                                                                                yt => yt.CMND_CCCD.Contains(nurseViewModel.SearchBox)).ToList());
                }
                else if (RemoveSignString == "ho")
                {
                    //Nurses = DataProvider.Ins.DB.YTAs.Where(p => p.HO.Contains(searchBox)).ToList();
                    nurseViewModel.Nurses = SelectableItem<YTA>.GetSelectableItems(
                    DataProvider.Ins.DB.YTAs.Where(delegate (YTA yt)
                    {
                        return VietnameseSign.ContainsUnsigned(yt.HO, nurseViewModel.SearchBox);
                    }).AsQueryable().ToList()
                    );
                }
                else if (RemoveSignString == "ten")
                {
                    //Nurses = DataProvider.Ins.DB.YTAs.Where(p => p.TEN.Contains(searchBox)).ToList();
                    nurseViewModel.Nurses = SelectableItem<YTA>.GetSelectableItems(
                        DataProvider.Ins.DB.YTAs.Where(delegate (YTA yt)
                        {
                            return VietnameseSign.ContainsUnsigned(yt.TEN, nurseViewModel.SearchBox);
                        }).AsQueryable().ToList()
                    );
                }
                else if (RemoveSignString == "ho va ten")
                {
                    nurseViewModel.Nurses = SelectableItem<YTA>.GetSelectableItems(
                        DataProvider.Ins.DB.YTAs.Where(delegate (YTA yt)
                        {
                        // Has some problems
                        return VietnameseSign.ContainsUnsigned(yt.HO + " " + yt.TEN, nurseViewModel.SearchBox);
                        }).AsQueryable().ToList()
                    );
                }
            }
            }       
    }
}
