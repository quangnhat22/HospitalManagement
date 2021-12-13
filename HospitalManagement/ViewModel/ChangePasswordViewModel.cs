using HospitalManagement.Command;
using HospitalManagement.Command.AccountCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.ViewModel
{
    
    internal class ChangePasswordViewModel : BaseViewModel
    {
        public ICommand saveChangePassword { get; set; }
        public ChangePasswordViewModel()
        {
            saveChangePassword = new SaveChangePasswordCommand();
        }
    }
}
