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
        public ICommand AddPatient { get; set; }
        public PatientFormViewModel()
        {
            db = new DataProvider();
            AddPatient = new AddPatientCommand(this);
        }
    }
}
