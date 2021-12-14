using HospitalManagement.View.StaffRoleView.TeamTask;
using HospitalManagement.ViewModel.StaffViewViewModel.TeamTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HospitalManagement.Command.TeamTaskCommand
{
    internal class OpenAddToDoFormCommand : ICommand
    {
        private StaffRoleTeamTaskViewModel staffRoleTeamTaskViewModel;

        public OpenAddToDoFormCommand(StaffRoleTeamTaskViewModel staffRoleTeamTaskViewModel)
        {
            this.StaffRoleTeamTaskViewModel = staffRoleTeamTaskViewModel;
        }

        public StaffRoleTeamTaskViewModel StaffRoleTeamTaskViewModel { get => staffRoleTeamTaskViewModel; set => staffRoleTeamTaskViewModel = value; }

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
            Window window = parameter as Window;
            AddToDoForm addToDoForm = new AddToDoForm();
            AddToDoFormViewModel addToDoFormViewModel = addToDoForm.DataContext as AddToDoFormViewModel;
            addToDoFormViewModel.Owner = StaffRoleTeamTaskViewModel;
            Application.Current.MainWindow = addToDoForm;
            addToDoForm.Show();
        }
    }
}
