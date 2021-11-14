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

namespace HospitalManagement.ViewModel
{
    class PatientViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public static List<BENHNHAN> patients = DataProvider.Ins.DB.BENHNHANs.ToList();

        private List<String> filterList = new List<string> { "CMND", 
                                                            "Họ", 
                                                            "Tên"
                                                            /*, "Họ và Tên"*/ };
        private string selectedFilter;
        private string searchBox;

        public List<BENHNHAN> Patients
        {
            get { return patients; }
            set { patients = value; OnPropertyChanged("Patients"); }
        }

        public ICommand OpenPatientForm { get; set; }
        public ICommand AllCheckedCommand { get; set; }
        public ICommand SingleCheckedCommand { get; set; }
        public ICommand ShowPatientInfomationCommand { get; set; }
        public ICommand SearchPatientCommand { get; set; }
        public string SearchBox
        {
            get => searchBox;
            set
            {
                searchBox = value;
                OnPropertyChanged("SearchBox");
                if(searchBox == string.Empty || searchBox == null)
                {
                    Patients = DataProvider.Ins.DB.BENHNHANs.ToList();
                }
            }
        }


        public List<string> FilterList { get => filterList; set => filterList = value; }
        public string SelectedFilter { get => selectedFilter; set => selectedFilter = value; }

        public PatientViewModel()
        {
            SelectedFilter = filterList[0];
            SearchBox = String.Empty;
            OpenPatientForm = new OpenPatientFormCommand();
            SearchPatientCommand = new SearchPatientCommand(this);
        }
    }
}
