using HospitalManagement.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace HospitalManagement.Command
{
    internal class OpenLoginWindowCommand : ICommand
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
            var loginWindow = new LoginWindow();
            Application.Current.MainWindow = loginWindow;
            window.Close();
            Thread windowThread = new Thread(new ThreadStart(() =>
            {
               window.Closed += (s, e) =>
               Dispatcher.CurrentDispatcher.BeginInvokeShutdown(DispatcherPriority.Background);
               System.Windows.Threading.Dispatcher.Run();
            }));
            windowThread.SetApartmentState(ApartmentState.STA);
            windowThread.IsBackground = true;
            windowThread.Start();
            loginWindow.Show();
        }
    }
}



