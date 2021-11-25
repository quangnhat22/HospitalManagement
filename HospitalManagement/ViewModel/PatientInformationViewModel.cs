using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HospitalManagement.Command;
using HospitalManagement.Model;

namespace HospitalManagement.ViewModel
{
    class PatientInformationViewModel : INotifyPropertyChanged
    {
        private BENHNHAN benhnhan;
        public ICommand SaveChange { get; set; }
        public PatientInformationViewModel(BENHNHAN bn)
        {
            this.BenhNhan = bn;
            SaveChange = new SaveChangePatientInformationCommand();
        }

        public BENHNHAN BenhNhan { get => benhnhan; set => benhnhan = value; }
        
        public event PropertyChangedEventHandler PropertyChanged;
        

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

    }
}
