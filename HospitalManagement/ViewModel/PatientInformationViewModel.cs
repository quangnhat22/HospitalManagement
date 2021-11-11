using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagement.Model;

namespace HospitalManagement.ViewModel
{
    class PatientInformationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        //private Patient patient;

        //public Patient Patient
        //{
        //    get { return patient; }
        //    set { patient = value; OnPropertyChanged("Patient"); }
        //}

        //public PatientInformationViewModel(Patient patient)
        //{
        //    Patient = patient;
        //}
    }
}
