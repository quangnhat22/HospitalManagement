using HospitalManagement.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HospitalManagement.ViewModel
{
    class StaffViewModel : BaseViewModel
    {
        public DoctorViewModel DoctorViewModel { get; set; }
        public NurseViewModel NurseViewModel { get; set; }
        public StaffViewModel()
        {
            DoctorViewModel = new DoctorViewModel();
            NurseViewModel = new NurseViewModel();
        }
    }
}
