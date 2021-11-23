using HospitalManagement.Command.AccountListCommand;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.ViewModel.AddAccountVM
{
    public class AddNewAccountViewModel : BaseViewModel
    {
        public ICommand AddNewAccountCommand { get; set; }
        public AddNewAccountViewModel()
        {
            AddNewAccountCommand = new AddNewAccountCommand();
        }
    }
}
