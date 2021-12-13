using HospitalManagement.Command.AccountCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.ViewModel.AddAccountVM
{
    internal class UpdateInformationViewModel : BaseViewModel
    {
        public ICommand OpenChangeAccountWindowCommand { get; set; }
        public UpdateInformationViewModel()
{
           // OpenChangeAccountWindowCommand = new OpenChangeAccountWindowCommand();
        }

    }
}
