using HospitalManagement.Command;
using HospitalManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.ViewModel
{
    internal class PatientFormViewModel : BaseViewModel
    {
        public DataProvider db;
        private PatientViewModel patientViewModel;
        public ICommand AddPatient { get; set; }
        public PatientFormViewModel(PatientViewModel patientViewModel)
        {
            db = new DataProvider();
            this.patientViewModel = patientViewModel;
            AddPatient = new AddPatientCommand(this, this.patientViewModel);
        }
    }
}
