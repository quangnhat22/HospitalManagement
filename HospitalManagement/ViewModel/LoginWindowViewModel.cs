using HospitalManagement.Command;
using HospitalManagement.Model;
using HospitalManagement.View;
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
        public DataProvider db;
        public ICommand OpenMainWindow { 
            get ; 
            set ; }
        public ICommand OpenSignUpForm { get; set; }
        public ICommand OpenReportForm { get; set; }
        public ICommand OpenForgotPasswordForm { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        public LoginWindowViewModel()
        {
            this.db = new DataProvider();
            OpenMainWindow = new LoginWindowCommand(this);
            OpenSignUpForm = new OpenSignUpFormCommand();
            OpenForgotPasswordForm = new OpenForgotPasswordFormCommand();
        }
    }
}
