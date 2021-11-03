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
    class LogoutCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public bool CanExecute(object parameter)
        {
            return parameter == null ? false : true;
        }

        public void Execute(object parameter)
        {
            Window mainWindow = parameter as Window;
            LoginWindow loginWindow = new LoginWindow();
            mainWindow.Close();
            Thread windowThread = new Thread(new ThreadStart(() =>
            {
                mainWindow.Closed += (s, e) =>
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
