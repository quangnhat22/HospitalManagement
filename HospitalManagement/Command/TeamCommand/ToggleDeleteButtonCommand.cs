using HospitalManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HospitalManagement.Command.TeamCommand
{
    class ToggleDeleteButtonCommand : ICommand
    {
        private TeamViewmodel teamViewmodel;

        public ToggleDeleteButtonCommand(TeamViewmodel teamViewmodel)
        {
            this.teamViewmodel = teamViewmodel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (teamViewmodel.DeleteButtonVisibility == "Visible")
                teamViewmodel.DeleteButtonVisibility = "Collapsed";
            else
                teamViewmodel.DeleteButtonVisibility = "Visible";
        }
    }
}
