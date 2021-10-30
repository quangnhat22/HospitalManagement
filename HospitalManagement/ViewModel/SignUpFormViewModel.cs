using HospitalManagement.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.ViewModel
{
    internal class SignUpFormViewModel: BaseViewModel
    {
        public ICommand OpenLoginWindow { get; set; }
        public SignUpFormViewModel()
        {
            OpenLoginWindow = new OpenLoginWindowCommand();
        }
    }
}
