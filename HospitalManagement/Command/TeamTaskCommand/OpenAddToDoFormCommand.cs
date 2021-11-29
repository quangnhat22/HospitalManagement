using HospitalManagement.View.StaffRoleView.TeamTask;
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
            Application.Current.MainWindow = addToDoForm;
            addToDoForm.Show();
        }
    }
}
