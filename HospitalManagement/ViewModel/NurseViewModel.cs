using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using HospitalManagement.Model;
using HospitalManagement.Command;

namespace HospitalManagement.ViewModel
{
    class NurseViewModel : BaseViewModel, INotifyPropertyChanged
    {
        //public int CheckedCount;
        public List<YTA> nurses = DataProvider.Ins.DB.YTAs.ToList();

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void OnPropertyChanged(string name)
        //{
        //    if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        //}

        public List<YTA> Nurses
        {
            get { return nurses; }
            set { nurses = value; OnPropertyChanged("Nurses"); }
        }

        //private bool? isCheckedAll;

        //public bool? IsCheckedAll
        //{
        //    get { return isCheckedAll; }
        //    set { isCheckedAll = value; OnPropertyChanged("IsCheckedAll"); }
        //}

        public ICommand OpenNurseForm { get; set; }
        public ICommand AllCheckedCommand { get; set; }
        public ICommand SingleCheckedCommand { get; set; }
        public ICommand ShowNurseInfomationCommand { get; set; }

        public NurseViewModel()
        {
            //for (int i = 0; i < 200; i++)
            //{
            //    Nurses.Add(new Nurse()
            //    {
            //        ID = 1,
            //        FirstName = "Quang",
            //        LastName = "2k4",
            //        Phone = "0232343211",
            //        Mail = "1@kteam.com",
            //        Sex = SexType.Nam,
            //        Birthday = (new DateTime(2004, 1, 1)).ToString("dd/MM/yyyy"),
            //        Rule = "Cô y tá may mắn"
            //    });
            //    Nurses.Add(new Nurse()
            //    {
            //        ID = 2,
            //        FirstName = "Quang",
            //        LastName = "2k2",
            //        Phone = "0232343212",
            //        Mail = "2@kteam.com",
            //        Sex = SexType.Nam,
            //        Birthday = (new DateTime(2002, 1, 1)).ToString("dd/MM/yyyy"),
            //        Rule = "Y tá"
            //    });
            //    Nurses.Add(new Nurse()
            //    {
            //        ID = 3,
            //        FirstName = "Lộc",
            //        LastName = "wibu",
            //        Phone = "0232343213",
            //        Mail = "3@kteam.com",
            //        Sex = SexType.Nam,
            //        Birthday = (new DateTime(2002, 1, 1)).ToString("dd/MM/yyyy"),
            //        Rule = "Y tá"
            //    });
            //    Nurses.Add(new Nurse()
            //    {
            //        ID = 4,
            //        FirstName = "Nghĩa",
            //        LastName = "tay to",
            //        Phone = "0232343214",
            //        Mail = "3@kteam.com",
            //        Sex = SexType.Nam,
            //        Birthday = (new DateTime(2002, 1, 1)).ToString("dd/MM/yyyy"),
            //        Rule = "Y tá"
            //    });
            //    Nurses.Add(new Nurse()
            //    {
            //        ID = 5,
            //        FirstName = "Tuấn",
            //        LastName = "khỉ",
            //        Phone = "0232343215",
            //        Mail = "3@kteam.com",
            //        Sex = SexType.Nam,
            //        Birthday = (new DateTime(2002, 1, 1)).ToString("dd/MM/yyyy"),
            //        Rule = "Y tá"
            //    });
            //}

            //CheckedCount = 0;
            //IsCheckedAll = false;
            ShowNurseInfomationCommand = new ShowNurseInfomationCommand();

            //AllCheckedCommand = new RelayCommand<CheckBox>((p) => { return p == null ? false : true; }, (p) =>
            //{
            //    bool allcheckbox = (p.IsChecked == true);
            //    for (int i = 0; i < Nurses.Count; i++)
            //        Nurses[i].IsChecked = allcheckbox;
            //});

            //SingleCheckedCommand = new RelayCommand<CheckBox>((p) => { return p == null ? false : true; }, (p) =>
            //{
            //    IsCheckedAll = null;
            //    if (p.IsChecked == true)
            //        CheckedCount++;
            //    else
            //        CheckedCount--;

            //    if (CheckedCount == nurses.Count)
            //        IsCheckedAll = true;
            //    else
            //        if (CheckedCount == 0)
            //        IsCheckedAll = false;
            //});

            OpenNurseForm = new OpenNurseFormCommand();
        }
    }
}
