using HospitalManagement.Command;
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

        public FirstLoginViewModel()
        {
            OpenChangeAccountWindowCommand = new OpenChangeAccoutWindowCommand();
        }
    }
}
