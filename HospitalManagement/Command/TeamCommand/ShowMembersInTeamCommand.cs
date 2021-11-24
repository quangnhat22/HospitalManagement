using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using HospitalManagement.Utils;
using HospitalManagement.ViewModel;
using HospitalManagement.Model;

namespace HospitalManagement.Command.TeamCommand
{
    class ShowMembersInTeamCommand : ICommand
    {
        private TeamViewmodel teamViewmodel;
        public ShowMembersInTeamCommand(TeamViewmodel teamViewmodel)
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
            int id = (int)parameter;
            teamViewmodel.CurrentTeam = id.ToString();
            CountedTeam to = new CountedTeam(DataProvider.Ins.DB.TOes.Find(id));
            List<StaffInformation> staffAccounts = new List<StaffInformation>();
            var BacSiList = to.Value.BACSIs.ToList().ConvertAll(p => new StaffInformation(p));
            var YTaList = to.Value.YTAs.ToList().ConvertAll(p => new StaffInformation(p));
            staffAccounts.AddRange(BacSiList);
            staffAccounts.AddRange(YTaList);
            teamViewmodel.Members = staffAccounts;
        }
    }
}
