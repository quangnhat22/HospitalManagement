using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using HospitalManagement.Model;
using System.ComponentModel;
using HospitalManagement.Command;
using System.Collections.ObjectModel;
using HospitalManagement.Utils;
using HospitalManagement.Command.FacilitiesListCommand;

namespace HospitalManagement.ViewModel
{
    class FacilitiesViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public int CheckedCount;
        private static ObservableCollection<SelectableItem<VATTU>> facilities = SelectableItem<VATTU>.GetSelectableItems(DataProvider.Ins.DB.VATTUs.ToList());

        public ObservableCollection<SelectableItem<VATTU>> Facilities
        {
            get { return facilities; }
            set { facilities = value; OnPropertyChanged("Facilities"); }
        }

        private bool? isCheckedAll;

        public bool? IsCheckedAll
        {
            get { return isCheckedAll; }
            set { isCheckedAll = value; OnPropertyChanged("IsCheckedAll"); }
        }
        public ICommand AllCheckedCommand { get; set; }
        public ICommand SingleCheckedCommand { get; set; }
        public ICommand OpenFacilitiesForm { get; set; }
        public ICommand DeleteFacilitiesCommand { get; set; }

        public FacilitiesViewModel()
        {
            CheckedCount = 0;
            IsCheckedAll = false;
            DeleteFacilitiesCommand = new DeleteFacilitiesCommand(this);
            AllCheckedCommand = new RelayCommand<CheckBox>((p) => { return p == null ? false : true; }, (p) =>
            {
                bool allcheckbox = (p.IsChecked == true);
                for (int i = 0; i < Facilities.Count; i++)
                    Facilities[i].IsSelected = allcheckbox;
                p.IsChecked = allcheckbox;
            });

            SingleCheckedCommand = new RelayCommand<CheckBox>((p) => { return p == null ? false : true; }, (p) =>
            {
                IsCheckedAll = null;
                if (p.IsChecked == true)
                    CheckedCount++;
                else
                    CheckedCount--;

                if (CheckedCount == Facilities.Count)
                    IsCheckedAll = true;
                else
                    if (CheckedCount == 0)
                    IsCheckedAll = false;
            });
        }
    }
}
