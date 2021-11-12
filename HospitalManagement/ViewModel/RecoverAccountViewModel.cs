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
    internal class RecoverAccountViewModel : BaseViewModel
    {
        public DataProvider db;
        public ICommand openForgotPasswordFormCommand { get; set; }
        public ICommand openLoginWindow { get; set; }
        public ICommand recoverAccountCommand { get; set; }
        public ICommand resendEmailCommand { get; set; }
        public RecoverAccountViewModel()
        {
            db = new DataProvider();
            openForgotPasswordFormCommand = new OpenForgotPasswordFormCommand();
            openLoginWindow = new OpenLoginWindowCommand();
            recoverAccountCommand = new RecoverAccountCommand(this);
            resendEmailCommand = new ResendEmailRecoverCommand();
        }
    }
}
