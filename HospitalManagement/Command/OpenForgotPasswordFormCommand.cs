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
    public class OpenForgotPasswordFormCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add {  }
            remove { }
        }

        public bool CanExecute(object parameter)
        {
            return parameter == null ? false : true;
        }

        public void Execute(object parameter)
        {
            Window window = parameter as Window;
            var forgotPasswordWindow = new ForgotPasswordWindow();
            Application.Current.MainWindow = forgotPasswordWindow;
            forgotPasswordWindow.Show();
            window.Close();
        }
    }
}
