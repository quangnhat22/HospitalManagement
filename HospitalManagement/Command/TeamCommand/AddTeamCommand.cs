using HospitalManagement.ViewModel;
using HospitalManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using HospitalManagement.View;

namespace HospitalManagement.Command.TeamCommand
{
    class AddTeamCommand : ICommand
    {
        private TeamViewmodel teamViewmodel;

        public AddTeamCommand(TeamViewmodel teamViewmodel)
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
            if (currentFloor != null)
            {
                TO to = new TO
                {
                    IDTANG = teamViewmodel.Floors.Where(p => p.SOTANG == currentFloor).First().ID
                };
                DataProvider.Ins.DB.TOes.Add(to);
                DataProvider.Ins.DB.SaveChanges();
                teamViewmodel.CountedTeams = CountedTeam.GetCountedTeams(DataProvider.Ins.DB.TOes.Where(p => p.TANG.TOA.DISPLAYNAME == currentBuilding && p.TANG.SOTANG == currentFloor).ToList());
                NotifyWindow notifyWindow = new NotifyWindow("Success", "Thêm thành công tổ mới!");
                notifyWindow.Show();
            }
            else
            {
                NotifyWindow notifyWindow = new NotifyWindow("Error", "Vui lòng chọn tầng!");
                notifyWindow.Show();
            }
        }
    }
}
