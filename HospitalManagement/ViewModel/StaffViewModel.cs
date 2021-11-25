using HospitalManagement.Command;
using HospitalManagement.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace HospitalManagement.ViewModel
{
    class StaffViewModel : BaseViewModel
    {
        public DoctorViewModel DoctorViewModel { get; set; }
        public NurseViewModel NurseViewModel { get; set; }
        public ICommand DeleteNurse { get; set; }
        public ICommand DeleteDoctor { get; set; }
        public StaffViewModel()
        {
            DoctorViewModel = new DoctorViewModel();
            NurseViewModel = new NurseViewModel();
            DeleteNurse = new DeleteNurseCommand(NurseViewModel);
        }
    }
}
