using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using HospitalManagement.Model;
using HospitalManagement.ViewModel;

namespace HospitalManagement.Command.TeamCommand
{
    class ChangeBuildingTeamCommand : ICommand
    {
        private TeamViewmodel teamViewmodel;

        public ChangeBuildingTeamCommand(TeamViewmodel teamViewmodel)
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
            teamViewmodel.CountedTeams = CountedTeam.GetCountedTeams(DataProvider.Ins.DB.TOes.Where(p => p.TANG.TOA.DISPLAYNAME == teamViewmodel.CurrentBuilding).ToList());
        }
    }
}
