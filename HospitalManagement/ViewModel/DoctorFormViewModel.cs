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
    internal class DoctorFormViewModel : BaseViewModel
    {
        public DataProvider db;
        public ICommand AddDoctor { get; set; }
        public DoctorFormViewModel()
        {
            db = new DataProvider();
            AddDoctor = new AddDoctorCommand(this);
        }
    }
}