using HospitalManagement.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HospitalManagement.ViewModel
{
     public class LoginWindowViewModel :BaseViewModel
     {
        public ICommand OpenMainWindow { get; set; }
        public ICommand OpenSignUpForm { get; set; }
        public ICommand OpenReportForm { get; set; }
        public ICommand OpenForgotPasswordForm { get; set; }

        public LoginWindowViewModel()
        {
            OpenMainWindow = new LoginWindowCommand();
            OpenSignUpForm = new OpenSignUpFormCommand();
            OpenForgotPasswordForm = new OpenForgotPasswordFormCommand();

        }

    }
}
