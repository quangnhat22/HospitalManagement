using HospitalManagement.Command;
using HospitalManagement.Model;
using HospitalManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.ViewModel
{
    internal class ForgotPasswordFormViewModel : BaseViewModel
    {
        public DataProvider db;
        public ICommand OpenLoginWindow { get; set; }
        public ICommand ForgotPasswordValidation { get; set; }

        public ForgotPasswordFormViewModel()
        {
            db = new DataProvider();
            OpenLoginWindow = new OpenLoginWindowCommand();
            ForgotPasswordValidation = new ForgotPasswordValidationCommand(this);
        }
    }
}