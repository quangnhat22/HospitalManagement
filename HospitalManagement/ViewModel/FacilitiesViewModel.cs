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
        private static ObservableCollection<SelectableItem<VATTU>> facilities = SelectableItem<VATTU>.GetSelectableItems(DataProvider.Ins.DB.VATTUs.ToList());

        public ObservableCollection<SelectableItem<VATTU>> Facilities
        {
            get { return facilities; }
            set { facilities = value; OnPropertyChanged("Facilities"); }
        }

        private bool? isCheckedAll;
        private List<String> filterList = new List<string> { "Tên vật tư",
                                                            "Loại vật tư"
                                                            /*, "Họ và Tên"*/ };
        private string selectedFilter;
        private string searchBox;

        public bool? IsCheckedAll
        {
            get { return isCheckedAll; }
            set { isCheckedAll = value; OnPropertyChanged("IsCheckedAll"); }
        }
        public ICommand AllCheckedCommand { get; set; }
        public ICommand SingleCheckedCommand { get; set; }
        public ICommand OpenFacilitiesForm { get; set; }
        public ICommand SearchFacilitiesCommand { get; set; }
        public ICommand DeleteFacilitiesCommand { get; set; }
        public string SelectedFilter { get => selectedFilter; set => selectedFilter = value; }
        public string SearchBox { get => searchBox; set => searchBox = value; }
        public List<string> FilterList { get => filterList; set => filterList = value; }

        public FacilitiesViewModel()
        {
            DeleteFacilitiesCommand = new DeleteFacilitiesCommand(this);
            SearchFacilitiesCommand = new SearchFacilitiesCommand(this);
            if(FilterList.Count > 0)
            {
                SelectedFilter = FilterList[0];
            }

            IsCheckedAll = false;
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
                IsCheckedAll = null;

                if (Facilities.Where(f => f.IsSelected).Count() == facilities.Count)
                    IsCheckedAll = true;
                else
                    if (Facilities.Where(f => f.IsSelected).Count() == 0)
                        IsCheckedAll = false;
            });
        }
    }
}
