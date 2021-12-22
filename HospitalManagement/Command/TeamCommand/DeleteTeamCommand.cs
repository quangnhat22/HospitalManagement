using HospitalManagement.ViewModel;
using HospitalManagement.Model;
using HospitalManagement.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HospitalManagement.Command.TeamCommand
{
    class DeleteTeamCommand : ICommand
    {
        private TeamViewmodel teamViewmodel;

        public DeleteTeamCommand(TeamViewmodel teamViewmodel)
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
            ObservableCollection<CountedTeam> countedTeams = teamViewmodel.CountedTeams;
            CountedTeam countedTeam = countedTeams.Where(p => p.Value.ID == id).First();
            if (countedTeam.Count != 0) 
            {
                NotifyWindow notifyWindow = new NotifyWindow("Error", "Vẫn còn bác sĩ và y tá trong tổ!");
                notifyWindow.Show();
            }
            else
            {
                teamViewmodel.CountedTeams.Remove(countedTeam);
                using(QUANLYBENHVIENEntities dbContext = new QUANLYBENHVIENEntities())
				{
                    TO to = dbContext.TOes.Find(countedTeam.Value.ID);
                    dbContext.TOes.Remove(to);
                    dbContext.SaveChanges();
                }
                NotifyWindow notifyWindow = new NotifyWindow("Success", "Xoá thành công tổ " + id.ToString() + "!");
                notifyWindow.Show();
            }
        }
    }
}
