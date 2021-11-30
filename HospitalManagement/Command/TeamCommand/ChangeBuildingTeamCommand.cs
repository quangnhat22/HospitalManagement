﻿using System;
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
                teamViewmodel.CountedTeams = CountedTeam.GetCountedTeams(DataProvider.Ins.DB.TOes.Where(p => p.TANG.TOA.DISPLAYNAME == currentBuilding && p.TANG.SOTANG == currentFloor).ToList());
            else
                teamViewmodel.CountedTeams = CountedTeam.GetCountedTeams(DataProvider.Ins.DB.TOes.Where(p => p.TANG.TOA.DISPLAYNAME == currentBuilding).ToList());
            teamViewmodel.CurrentTeam = "";
            if (teamViewmodel.Members != null && teamViewmodel.Members.Count > 0) 
                teamViewmodel.Members = new List<Utils.StaffInformation>();
        }
    }
}
