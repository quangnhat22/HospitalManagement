using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HospitalManagement.Command;

namespace HospitalManagement.ViewModel
{
    class NotifyWindowViewModel
    {
        public ICommand OpenForgotPasswordForm { get; set; }
        public ICommand CloseWindow { get; set; }
        public NotifyWindowViewModel()
        {
            OpenForgotPasswordForm = new OpenForgotPasswordFormCommand();
            CloseWindow = new CloseWindowCommand();
        }
    }
}
