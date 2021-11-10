using HospitalManagement.Command;
using HospitalManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.ViewModel
{
    internal class RecoverAccountViewModel : BaseViewModel
    {
        public ICommand openForgotPasswordFormCommand { get; set; }
        public ICommand openLoginWindow { get; set; }
        public ICommand recoverAccouneCommand { get; set; }
        public ICommand resendEmailCommand { get; set; }    
        public RecoverAccountViewModel()
        {
            openForgotPasswordFormCommand = new OpenForgotPasswordFormCommand();
            openLoginWindow = new OpenLoginWindowCommand();
            recoverAccouneCommand = new RecoverAccountCommand();
            resendEmailCommand = new ResendEmailRecoverCommand();
        }
    }
}
