using HospitalManagement.Command.TeamCommand;
using HospitalManagement.Command.TeamTaskCommand;
using HospitalManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.ViewModel.StaffViewViewModel.TeamTask
{
    internal class StaffRoleTeamTaskViewModel : BaseViewModel
    {
        public ICommand OpenAddToDoFormCommand { get; set; }
        

        public StaffRoleTeamTaskViewModel()
        {
            OpenAddToDoFormCommand = new OpenAddToDoFormCommand();
        }
    }
}
