using HospitalManagement.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HospitalManagement.Command
{
    internal class OpenForgotPasswordFormCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return parameter == null ? false : true;
        }

        public void Execute(object parameter)
        {
            Window window = parameter as Window;
            var mainWindow = new ForgotPasswordWindow();
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
            window.Close();
        }
    }
}
