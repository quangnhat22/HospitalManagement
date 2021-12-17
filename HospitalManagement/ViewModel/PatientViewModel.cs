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
using HospitalManagement.Command.PatientListCommand;
using System.Windows;

namespace HospitalManagement.ViewModel
{
    public class PatientViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private static ObservableCollection<SelectableItem<BENHNHAN>> patients = SelectableItem<BENHNHAN>.GetSelectableItems(DataProvider.Ins.DB.BENHNHANs.ToList());
        private List<String> filterList = new List<string> { "CMND", 
                                                            "Họ", 
                                                            "Tên"
                                                            , "Họ và Tên" };
        private string selectedFilter;
        private string searchBox;
        //private string cmnd;
        private Visibility buttonVisibility;
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
        public ICommand OpenChangePatientForm { get; set; }
        public ICommand ExportPatientToExcelCommand { get; set; }

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
        //public string CMND { get => cmnd; set => cmnd = value; }
        public Visibility ButtonVisibility { get => buttonVisibility; set => buttonVisibility = value; }

        public PatientViewModel()
        {
            SelectedFilter = filterList[0];
            SearchBox = String.Empty;
            OpenPatientForm = new OpenPatientFormCommand(this);
            SearchPatientCommand = new SearchPatientCommand(this);
            ShowPatientInfomationCommand = new ShowPatientInfomationCommand();
            DeletePatientCommand = new DeletePatientCommand(this);
            OpenChangePatientForm = new OpenChangePatientFormCommand();
            ExportPatientToExcelCommand = new ExportPatientToExcelCommand(this);
            if (MainWindowViewModel.User.ROLE == "staff" || MainWindowViewModel.User.ROLE == "leader")
            {
                ButtonVisibility = Visibility.Collapsed;
            }
            else
            {
                ButtonVisibility = Visibility.Visible;
            }

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

                if (Patients.Where(patient => patient.IsSelected).Count() == patients.Count)
                    IsCheckedAll = true;
                else
                    if (Patients.Where(patient => patient.IsSelected).Count() == 0)
                        IsCheckedAll = false;
            });
        }
    }
}
