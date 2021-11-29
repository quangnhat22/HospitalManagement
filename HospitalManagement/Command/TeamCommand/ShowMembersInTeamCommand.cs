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
using HospitalManagement.ViewModel.StaffViewViewModel.TeamTask;

namespace HospitalManagement.Command.TeamCommand
{
    class ShowMembersInTeamCommand : ICommand
    {
        private TeamViewmodel teamViewmodel;
        private AddToDoFormViewModel addToDoFormViewModel;
        public ShowMembersInTeamCommand(TeamViewmodel teamViewmodel)
        {
            this.teamViewmodel = teamViewmodel;
        }

        public ShowMembersInTeamCommand(AddToDoFormViewModel addToDoFormViewModel)
        {
            this.addToDoFormViewModel = addToDoFormViewModel;
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
            List<StaffInformation> staffAccounts = new List<StaffInformation>();
            if (teamViewmodel != null)
            {
                int id = (int)parameter;
                teamViewmodel.CurrentTeam = id.ToString();
                CountedTeam to = new CountedTeam(DataProvider.Ins.DB.TOes.Find(id));
                var BacSiList = to.Value.BACSIs.ToList().ConvertAll(p => new StaffInformation(p));
                var YTaList = to.Value.YTAs.ToList().ConvertAll(p => new StaffInformation(p));
                staffAccounts.AddRange(BacSiList);
                staffAccounts.AddRange(YTaList);
                teamViewmodel.Members = staffAccounts;
            }
            if (addToDoFormViewModel != null)
            {
                var members = MainWindowViewModel.User.TOes.ToList();
                foreach(var member in members)
                {
                    var BacSiList = member.BACSIs.ToList().ConvertAll(p => new StaffInformation(p));
                    var YTaList = member.YTAs.ToList().ConvertAll(p=> new StaffInformation(p));
                    staffAccounts.AddRange(BacSiList);
                    staffAccounts.AddRange(YTaList);
                }    
                addToDoFormViewModel.Members = staffAccounts;
            }
        }
    }
}
