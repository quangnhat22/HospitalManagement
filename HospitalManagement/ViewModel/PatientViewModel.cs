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
using HospitalManagement.Utils;
using System.Collections.ObjectModel;

namespace HospitalManagement.ViewModel
{
    class PatientViewModel : BaseViewModel, INotifyPropertyChanged
    {

        public int CheckedCount;
        private static ObservableCollection<SelectableItem<BENHNHAN>> patients = SelectableItem<BENHNHAN>.GetSelectableItems(DataProvider.Ins.DB.BENHNHANs.ToList());

        private List<String> filterList = new List<string> { "CMND", 
                                                            "Họ", 
                                                            "Tên"
                                                            /*, "Họ và Tên"*/ };
        private string selectedFilter;
        private string searchBox;
        private string cmnd;
        public ObservableCollection<SelectableItem<BENHNHAN>> Patients
        {
            get { return patients; }
            set { patients = value; OnPropertyChanged("Patients"); }
        }

        public ICommand OpenPatientForm { get; set; }
        public ICommand AllCheckedCommand { get; set; }
        public ICommand SingleCheckedCommand { get; set; }
        public ICommand ShowPatientInfomationCommand { get; set; }
        public ICommand SearchPatientCommand { get; set; }
        public ICommand DeletePatientCommand { get; set; }


        private bool? isCheckedAll;

        public bool? IsCheckedAll
        {
            get { return isCheckedAll; }
            set { isCheckedAll = value; OnPropertyChanged("IsCheckedAll"); }
        }

        public string SearchBox
        {
            get => searchBox;
            set
            {
                searchBox = value;
                OnPropertyChanged("SearchBox");
                if(searchBox == string.Empty || searchBox == null)
                {
                    Patients = SelectableItem<BENHNHAN>.GetSelectableItems(DataProvider.Ins.DB.BENHNHANs.ToList());
                }
            }
        }


        public List<string> FilterList { get => filterList; set => filterList = value; }
        public string SelectedFilter { get => selectedFilter; set => selectedFilter = value; }
        public string CMND { get => cmnd; set => cmnd = value; }

        public PatientViewModel()
        {
            SelectedFilter = filterList[0];
            SearchBox = String.Empty;
            OpenPatientForm = new OpenPatientFormCommand();
            SearchPatientCommand = new SearchPatientCommand(this);
            ShowPatientInfomationCommand = new ShowPatientInfomationCommand(this);
            DeletePatientCommand = new DeletePatientCommand();

            CheckedCount = 0;
            IsCheckedAll = false;
            AllCheckedCommand = new RelayCommand<CheckBox>((p) => { return p == null ? false : true; }, (p) =>
            {
                bool allcheckbox = (p.IsChecked == true);
                for (int i = 0; i < Patients.Count; i++)
                    Patients[i].IsSelected = allcheckbox;
                p.IsChecked = allcheckbox;
            });

            SingleCheckedCommand = new RelayCommand<CheckBox>((p) => { return p == null ? false : true; }, (p) =>
            {
                IsCheckedAll = null;
                if (p.IsChecked == true)
                    CheckedCount++;
                else
                    CheckedCount--;

                if (CheckedCount == patients.Count)
                    IsCheckedAll = true;
                else
                    if (CheckedCount == 0)
                    IsCheckedAll = false;
            });
        }
    }
}
