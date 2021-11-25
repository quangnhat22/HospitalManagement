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

        private bool? isCheckedAll;

        public bool? IsCheckedAll
        {
            get { return isCheckedAll; }
            set { isCheckedAll = value; OnPropertyChanged("IsCheckedAll"); }
        }

        public ICommand OpenDoctorForm { get; set; }
        public ICommand AllCheckedCommand { get; set; }
        public ICommand SingleCheckedCommand { get; set; }
        public ICommand ShowDoctorInfomationCommand { get; set; }
        public DoctorViewModel()
        {
            IsCheckedAll = false;
            AllCheckedCommand = new RelayCommand<CheckBox>((p) => { return p == null ? false : true; }, (p) =>
            {
                bool allcheckbox = (p.IsChecked == true);
                for (int i = 0; i < Doctors.Count; i++)
                    Doctors[i].IsSelected = allcheckbox;
                p.IsChecked = allcheckbox;
            });

            SingleCheckedCommand = new RelayCommand<CheckBox>((p) => { return p == null ? false : true; }, (p) =>
            {
                IsCheckedAll = null;

                if (Doctors.Where(doctor => doctor.IsSelected).Count() == doctors.Count)
                    IsCheckedAll = true;
                else
                    if (Doctors.Where(doctor => doctor.IsSelected).Count() == 0)
                    IsCheckedAll = false;
            });
            ShowDoctorInfomationCommand = new ShowDoctorInfomationCommand();
            OpenDoctorForm = new OpenDoctorFormCommand();
        }
    }
}
