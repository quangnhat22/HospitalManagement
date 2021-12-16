using HospitalManagement.Model;
using HospitalManagement.View;
using HospitalManagement.View.Others;
using HospitalManagement.View.Staff;
using HospitalManagement.ViewModel;
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
    internal class FirstLoginOpening:ICommand
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
            Window window = Application.Current.MainWindow as Window;
            USER user = FirstLoginViewModel.User;
            if (user.ROLE == "leader" || user.ROLE == "doctor")
            {
                BACSI bacsi = user.BACSIs.FirstOrDefault();
                DoctorForm firstLoginWindow = new DoctorForm(bacsi);
                Application.Current.MainWindow = firstLoginWindow;
                Application.Current.MainWindow.Show();

            }
            if (user.ROLE == "nurse")
            {
                YTA yta = user.YTAs.FirstOrDefault();
                NurseForm firstLoginWindow = new NurseForm(yta);
                Application.Current.MainWindow = firstLoginWindow;
                Application.Current.MainWindow.Show();
            }
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
        }
    }
}
