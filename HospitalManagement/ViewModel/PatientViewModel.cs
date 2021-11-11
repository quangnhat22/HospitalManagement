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

namespace HospitalManagement.ViewModel
{
    class PatientViewModel : BaseViewModel, INotifyPropertyChanged
    {
        //public int CheckedCount;
        //public List<Patient> patients = new List<Patient>();

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void OnPropertyChanged(string name)
        //{
        //    if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        //}

        //public List<Patient> Patients
        //{
        //    get { return patients; }
        //    set { patients = value; OnPropertyChanged("Patients"); }
        //}

        //private bool? isCheckedAll;

        //public bool? IsCheckedAll
        //{
        //    get { return isCheckedAll; }
        //    set { isCheckedAll = value; OnPropertyChanged("IsCheckedAll"); }
        //}
        public ICommand OpenPatientForm { get; set; }
        public ICommand AllCheckedCommand { get; set; }
        public ICommand SingleCheckedCommand { get; set; }
        public ICommand ShowPatientInfomationCommand { get; set; }

        public PatientViewModel()
        {
            //Patients.Add(new Patient()
            //{
            //    ID = 1,
            //    Name = "Quang 2k4",
            //    Sex = SexType.Nam,
            //    Birthday = (new DateTime(2004, 1, 1)).ToString("dd/MM/yyyy"),
            //    DateCheckIn = (new DateTime(2020, 1, 1)).ToString("dd/MM/yyyy"),
            //    Status = "Đang điều trị",
            //});
            //Patients.Add(new Patient()
            //{
            //    ID = 2,
            //    Name = "Quang 2k2",
            //    Sex = SexType.Nam,
            //    Birthday = (new DateTime(2002, 1, 1)).ToString("dd/MM/yyyy"),
            //    DateCheckIn = (new DateTime(2019, 1, 1)).ToString("dd/MM/yyyy"),
            //    Status = "Đang điều trị",
            //});
            //Patients.Add(new Patient()
            //{
            //    ID = 3,
            //    Name = "Lộc wibu",
            //    Sex = SexType.Nam,
            //    Birthday = (new DateTime(2002, 1, 1)).ToString("dd/MM/yyyy"),
            //    DateCheckIn = (new DateTime(2020, 2, 1)).ToString("dd/MM/yyyy"),
            //    Status = "Đang điều trị",
            //});
            //Patients.Add(new Patient()
            //{
            //    ID = 4,
            //    Name = "Nghĩa ăn hại",
            //    Sex = SexType.Nam,
            //    Birthday = (new DateTime(2002, 1, 1)).ToString("dd/MM/yyyy"),
            //    DateCheckIn = (new DateTime(2021, 1, 1)).ToString("dd/MM/yyyy"),
            //    Status = "Đang điều trị",
            //});
            //Patients.Add(new Patient()
            //{
            //    ID = 5,
            //    Name = "Tuấn khỉ",
            //    Sex = SexType.Nam,
            //    Birthday = (new DateTime(2002, 1, 1)).ToString("dd/MM/yyyy"),
            //    DateCheckIn = (new DateTime(2021, 1, 3)).ToString("dd/MM/yyyy"),
            //    Status = "Đang điều trị",
            //});
            //CheckedCount = 0;
            //IsCheckedAll = false;

            //ShowPatientInfomationCommand = new ShowPatientInfomationCommand();

            //AllCheckedCommand = new RelayCommand<CheckBox>((p) => { return p == null ? false : true; }, (p) =>
            //{
            //    bool allcheckbox = (p.IsChecked == true);
            //    for (int i = 0; i < Patients.Count; i++)
            //        Patients[i].IsChecked = allcheckbox;
            //});

            //SingleCheckedCommand = new RelayCommand<CheckBox>((p) => { return p == null ? false : true; }, (p) =>
            //{
            //    IsCheckedAll = null;
            //    if (p.IsChecked == true)
            //        CheckedCount++;
            //    else
            //        CheckedCount--;

            //    if (CheckedCount == patients.Count)
            //        IsCheckedAll = true;
            //    else
            //        if (CheckedCount == 0)
            //        IsCheckedAll = false;
            //});
            //OpenPatientForm = new OpenPatientFormCommand();  
        }
    }
}
