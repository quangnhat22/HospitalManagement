using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.View.StaffRoleView.TeamTask;
using HospitalManagement.ViewModel;
using HospitalManagement.ViewModel.StaffViewViewModel.TeamTask;

namespace HospitalManagement.Command
{
    class ShowTaskInformationCommand : ICommand
    {
        StaffRoleTeamTaskViewModel staffRoleTeamTaskViewModel;
        public ShowTaskInformationCommand(StaffRoleTeamTaskViewModel staffRoleTeamTaskViewModel)
        {
            this.staffRoleTeamTaskViewModel = staffRoleTeamTaskViewModel;
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
            int index = (int)parameter;
            CONGVIEC cv = staffRoleTeamTaskViewModel.ProgressTasks[index].Value;
            TaskInformationForm taskInformationForm = new TaskInformationForm(cv);
            TaskInformationViewModel taskInformationViewModel = taskInformationForm.DataContext as TaskInformationViewModel;
            taskInformationViewModel.Owner = staffRoleTeamTaskViewModel;
            taskInformationForm.Show();
        }
    }
}

