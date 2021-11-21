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
    class DoctorViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private static ObservableCollection<SelectableItem<BACSI>> doctors = SelectableItem<BACSI>.GetSelectableItems(DataProvider.Ins.DB.BACSIs.ToList());
        public ObservableCollection<SelectableItem<BACSI>> Doctors
        {
            get { return doctors; }
            set { doctors = value; OnPropertyChanged("Doctors"); }
        }

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void OnPropertyChanged(string name)
        //{
        //    if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        //}

        //public List<Doctor> Doctors
        //{
        //    get { return doctors; }
        //    set { doctors = value; OnPropertyChanged("Doctors"); }
        //}

        //private bool? isCheckedAll;

        //public bool? IsCheckedAll
        //{
        //    get { return isCheckedAll; }
        //    set { isCheckedAll = value; OnPropertyChanged("IsCheckedAll"); }
        //}

        public ICommand OpenDoctorForm { get; set; }
        public ICommand AllCheckedCommand { get; set; }
        public ICommand SingleCheckedCommand { get; set; }
        public ICommand ShowDoctorInfomationCommand { get; set; }
        public ICommand AddDoctorComand { get; set; }

        public DoctorViewModel()
        {
            //for (int i = 0; i < 200; i++)
            //{
            //    Doctors.Add(new Doctor()
            //    {
            //        ID = 1,
            //        FirstName = "Quang",
            //        LastName = "2k4",
            //        Phone = "0232343211",
            //        Mail = "1@kteam.com",
            //        Sex = SexType.Nam,
            //        Birthday = (new DateTime(2004, 1, 1)).ToString("dd/MM/yyyy"),
            //        Specialization = "CN 1",
            //        Rule = "Cô y tá may mắn"
            //    });
            //    Doctors.Add(new Doctor()
            //    {
            //        ID = 1,
            //        FirstName = "Quang",
            //        LastName = "2k4",
            //        Phone = "0232343211",
            //        Mail = "1@kteam.com",
            //        Sex = SexType.Nam,
            //        Birthday = (new DateTime(2004, 1, 1)).ToString("dd/MM/yyyy"),
            //        Specialization = "CN 1",
            //        Rule = "Cô y tá may mắn"
            //    });
            //    Doctors.Add(new Doctor()
            //    {
            //        ID = 2,
            //        FirstName = "Quang",
            //        LastName = "2k2",
            //        Phone = "0232343212",
            //        Mail = "2@kteam.com",
            //        Sex = SexType.Nam,
            //        Birthday = (new DateTime(2002, 1, 1)).ToString("dd/MM/yyyy"),
            //        Specialization = "CN 2",
            //        Rule = "Bác sĩ"
            //    });
            //    Doctors.Add(new Doctor()
            //    {
            //        ID = 3,
            //        FirstName = "Lộc",
            //        LastName = "wibu",
            //        Phone = "0232343213",
            //        Mail = "3@kteam.com",
            //        Sex = SexType.Nam,
            //        Birthday = (new DateTime(2002, 1, 1)).ToString("dd/MM/yyyy"),
            //        Specialization = "CN 2",
            //        Rule = "Bác sĩ"
            //    });
            //    Doctors.Add(new Doctor()
            //    {
            //        ID = 4,
            //        FirstName = "Nghĩa",
            //        LastName = "tay to",
            //        Phone = "0232343214",
            //        Mail = "3@kteam.com",
            //        Sex = SexType.Nam,
            //        Birthday = (new DateTime(2002, 1, 1)).ToString("dd/MM/yyyy"),
            //        Specialization = "CN 2",
            //        Rule = "Bác sĩ"
            //    });
            //    Doctors.Add(new Doctor()
            //    {
            //        ID = 5,
            //        FirstName = "Tuấn",
            //        LastName = "khỉ",
            //        Phone = "0232343215",
            //        Mail = "3@kteam.com",
            //        Sex = SexType.Nam,
            //        Birthday = (new DateTime(2002, 1, 1)).ToString("dd/MM/yyyy"),
            //        Specialization = "CN 2",
            //        Rule = "Bác sĩ"
            //    });
            //}
            //CheckedCount = 0;
            //IsCheckedAll = false;
            AddDoctorComand = new AddDoctorCommand(this);
            ShowDoctorInfomationCommand = new ShowDoctorInfomationCommand();

            //AllCheckedCommand = new RelayCommand<CheckBox>((p) => { return p == null ? false : true; }, (p) =>
            //{
            //    bool allcheckbox = (p.IsChecked == true);
            //    for (int i = 0; i < Doctors.Count; i++)
            //        Doctors[i].IsChecked = allcheckbox;
            //});

            //SingleCheckedCommand = new RelayCommand<CheckBox>((p) => { return p == null ? false : true; }, (p) =>
            //{
            //    IsCheckedAll = null;
            //    if (p.IsChecked == true)
            //        CheckedCount++;
            //    else
            //        CheckedCount--;

            //    if (CheckedCount == doctors.Count)
            //        IsCheckedAll = true;
            //    else
            //        if (CheckedCount == 0)
            //        IsCheckedAll = false;
            //});

            OpenDoctorForm = new OpenDoctorFormCommand();
        }
    }
}
