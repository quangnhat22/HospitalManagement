using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HospitalManagement.Command;


namespace HospitalManagement.ViewModel
{
    internal class AccountViewModel:BaseViewModel
    {
        public ICommand OpenChangeAccount { get; set; }
        public AccountViewModel()
        {
            OpenChangeAccount = new OpenChangeAccoutWindowCommand();
        }
    }
}
