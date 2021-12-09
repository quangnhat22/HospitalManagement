using HospitalManagement.Model;
using HospitalManagement.ViewModel.StaffViewViewModel.TeamTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.Command.TeamTaskCommand
{
    class DeleteTasksCommand : ICommand
    {
        private StaffRoleTeamTaskViewModel staffRoleTeamTaskViewModel;

        public DeleteTasksCommand(StaffRoleTeamTaskViewModel staffRoleTeamTaskViewModel)
        {
            this.staffRoleTeamTaskViewModel = staffRoleTeamTaskViewModel;
        }

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            foreach(ProgressTask congviec in staffRoleTeamTaskViewModel.DeleteTasks)
            {
                int idCongViec = congviec.Value.ID;
                DataProvider.Ins.DB.Database.ExecuteSqlCommand("DELETE FROM BACSILIENQUAN WHERE IDCONGVIEC = " + idCongViec);
                DataProvider.Ins.DB.Database.ExecuteSqlCommand("DELETE FROM YTALIENQUAN WHERE IDCONGVIEC = " + idCongViec);
                DataProvider.Ins.DB.Database.ExecuteSqlCommand("DELETE FROM CONGVIEC WHERE ID = " + idCongViec);
            }
            DataProvider.Ins.DB.SaveChanges();
            staffRoleTeamTaskViewModel.LoadTaskList();
        }
    }
}
