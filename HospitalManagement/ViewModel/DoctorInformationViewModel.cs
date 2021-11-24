using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HospitalManagement.Model;
using HospitalManagement.Command;

namespace HospitalManagement.ViewModel
{
     class DoctorInformationViewModel : INotifyPropertyChanged
    {
        private BACSI doctor;        
        public BACSI Doctor { get => doctor; set => doctor = value;}       
        public ICommand SaveChange { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        public DoctorInformationViewModel(BACSI bs)
        {
            this.Doctor = bs;
            SaveChange = new SaveChangeDoctorInformationCommand();
        }

        //private Doctor doctor;

        //public Doctor Doctor
        //{
        //    get { return doctor; }
        //    set { doctor = value; OnPropertyChanged("Doctor"); }
        //}

        //public DoctorInformationViewModel(Doctor doctor)
        //{
        //    Doctor = doctor;
        //}
    }
}
