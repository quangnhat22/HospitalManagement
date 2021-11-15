using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagement.Model;

namespace HospitalManagement.ViewModel
{
    class NurseInformationViewModel : INotifyPropertyChanged
    {
        private YTA nurse;

        public YTA Nurse { get => nurse; set => nurse = value; }
        public event PropertyChangedEventHandler PropertyChanged;
        public NurseInformationViewModel(YTA yt)
        {
            this.Nurse = yt;
        }      
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        //private Nurse nurse;

        //public Nurse Nurse
        //{
        //    get { return nurse; }
        //    set { nurse = value; OnPropertyChanged("Nurse"); }
        //}

        //public NurseInformationViewModel(Nurse nurse)
        //{
        //    Nurse = nurse;
        //}
    }
}
