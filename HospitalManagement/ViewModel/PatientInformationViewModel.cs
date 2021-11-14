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
        private BENHNHAN benhnhan;
        private string cmnd;

        public PatientInformationViewModel(BENHNHAN bn)
        {
            this.BenhNhan = bn;
            Cmnd = BenhNhan.CMND_CCCD;
        }

        public BENHNHAN BenhNhan { get => benhnhan; set => benhnhan = value; }
        public string Cmnd { get => cmnd; set => cmnd = value; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

    }
}
