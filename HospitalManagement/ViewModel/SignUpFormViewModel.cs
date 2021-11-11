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
    internal class SignUpFormViewModel: BaseViewModel
    {
        public DataProvider db;
        public ICommand OpenLoginWindow { get; set; }
        public ICommand SignUpValidation { get; set; }

        public SignUpFormViewModel()
        {
            db = new DataProvider();
            OpenLoginWindow = new OpenLoginWindowCommand();
            SignUpValidation = new SignUpValidationCommand(this);
        }
    }
}
