using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagement.View.Others;
using System.Windows;
using System.Windows.Input;


namespace HospitalManagement.Command
{
    internal class OpenChangePasswordWindowCommand: ICommand
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
            ChangePasswordWindow passwordWindow = new ChangePasswordWindow();
            Application.Current.MainWindow = passwordWindow;
            passwordWindow.Show();
        }
    }
}
