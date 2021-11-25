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
using HospitalManagement.Command.DoctorListCommand;

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

        public ICommand OpenDoctorForm { get; set; }
        public ICommand AllCheckedCommand { get; set; }
        public ICommand SingleCheckedCommand { get; set; }
        public ICommand ShowDoctorInfomationCommand { get; set; }
        public ICommand OpenChangeDoctorForm { get; set; }
        public DoctorViewModel()
        {          
            ShowDoctorInfomationCommand = new ShowDoctorInfomationCommand();
            OpenChangeDoctorForm = new OpenChangeDoctorFormCommand();           
            OpenDoctorForm = new OpenDoctorFormCommand();
        }
    }
}
