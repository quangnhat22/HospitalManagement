using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
            string currentBuilding = teamViewmodel.CurrentBuilding;
            int? currentFloor = teamViewmodel.CurrentFloor;
            ComboBox cb = parameter as ComboBox;
            if (cb.Name == "cbBuilding")
            {
                teamViewmodel.CurrentFloor = null;
                currentFloor = null;
            }
            if (currentFloor != null)
            {
                List<TO> teams = TeamViewmodel.DbContext.TOes.Where(p => p.TANG.TOA.DISPLAYNAME == currentBuilding && p.TANG.SOTANG == currentFloor).ToList();
                teamViewmodel.InitTeams(teams);
            }
            else
            {
                List<TO> teams = TeamViewmodel.DbContext.TOes.Where(p => p.TANG.TOA.DISPLAYNAME == currentBuilding).ToList();
                teamViewmodel.InitTeams(teams);
            }
            teamViewmodel.CurrentTeam = "";
            if (teamViewmodel.Members != null && teamViewmodel.Members.Count > 0) 
                teamViewmodel.Members = new List<Utils.StaffInformation>();
        }
    }
}
