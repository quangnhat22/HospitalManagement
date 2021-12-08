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
                DataProvider.Ins.DB.BACSILIENQUANs.RemoveRange(congviec.Value.BACSILIENQUANs);
                DataProvider.Ins.DB.YTALIENQUANs.RemoveRange(congviec.Value.YTALIENQUANs);
                DataProvider.Ins.DB.CONGVIECs.Remove(congviec.Value);
            }
            DataProvider.Ins.DB.SaveChanges();
            staffRoleTeamTaskViewModel.LoadTaskList();
        }
    }
}
