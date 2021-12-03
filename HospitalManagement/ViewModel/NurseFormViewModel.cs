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
    internal class NurseFormViewModel: BaseViewModel
    {
        public DataProvider db;
        private NurseViewModel nurseViewModel;
        public ICommand AddNurse { get; set; }
        public NurseFormViewModel(NurseViewModel nurseViewModel)
        {
            db = new DataProvider();
            this.nurseViewModel = nurseViewModel;
            AddNurse = new AddNurseCommand(this, this.nurseViewModel);
        }
    }
}
