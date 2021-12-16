using HospitalManagement.Command;
using HospitalManagement.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HospitalManagement.ViewModel
{
    class StaffViewModel : BaseViewModel
    {
        private DoctorViewModel doctorViewModel=new DoctorViewModel();
        private NurseViewModel nurseViewModel=new NurseViewModel();
        //private Visibility buttonVisibility;
        public DoctorViewModel DoctorViewModel
        {
            get { return doctorViewModel; }
            set { doctorViewModel = value; OnPropertyChanged("DoctorViewModel"); }
        }
        public NurseViewModel NurseViewModel 
        {
            get { return nurseViewModel; }
            set { nurseViewModel = value; OnPropertyChanged("NurseViewModel"); }
        }
        //public Visibility ButtonVisibility { get => buttonVisibility; set => buttonVisibility = value; }
        public StaffViewModel()
        {
            //if (MainWindowViewModel.User.ROLE == "staff" || MainWindowViewModel.User.ROLE == "leader")
            //{
            //    ButtonVisibility = Visibility.Hidden;
            //}
            //else
            //{
            //    ButtonVisibility = Visibility.Visible;
            //}
        }
    }
}
