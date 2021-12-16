using HospitalManagement.Command;
using HospitalManagement.Command.DoctorListCommand;
using HospitalManagement.Model;
using HospitalManagement.View.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.ViewModel
{
    internal class FirstLoginViewModel : BaseViewModel
    {
        public ICommand OpenChangeAccountWindowCommand { get; set; }
        static public USER User;

        public FirstLoginViewModel()
        {
            OpenChangeAccountWindowCommand = new FirstLoginOpening();


        }
    }
}
