using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.View;
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
    public class LoginWindowCommand : ICommand
    {
        private LoginWindowViewModel loginWindowViewModel;

        public LoginWindowCommand(LoginWindowViewModel loginWindowViewModel)
        {
            this.loginWindowViewModel = loginWindowViewModel;
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
            if (loginWindowViewModel.Username == null)
            {
                LoginFailed("Tên đăng nhập không được để trống");
            }
            else if (loginWindowViewModel.Password == null)
            {
                LoginFailed("Mật khẩu không được để trống");
            }
            else if (CheckAuthentication(loginWindowViewModel.Username, loginWindowViewModel.Password))
            {
                LoginSuccessful();
                return;
            }
            else
                LoginFailed("Sai tên đăng nhập hoặc mật khẩu");
        }

        private void LoginFailed(string errorMessage)
        {
            NotifyWindow notifyWindow = new NotifyWindow("Error", errorMessage);
            notifyWindow.Show();
        }

        private void LoginSuccessful()
        {
            Window window = Application.Current.MainWindow as Window;
            MainWindow mainWindow = new MainWindow();

            Application.Current.MainWindow = new MainWindow();
            Application.Current.MainWindow.Show();
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
            MainWindowViewModel.User = DataProvider.Ins.DB.USERs.Where(x => x.USERNAME == loginWindowViewModel.Username).First();
        }

        private bool CheckAuthentication(string username, string password)
        {
            string hashPassword = Encryptor.Hash(password);
            return DataProvider.Ins.DB.USERs.Where(p => p.USERNAME == username && p.PASSWORD == hashPassword).Count() > 0;
        }
    }
}
