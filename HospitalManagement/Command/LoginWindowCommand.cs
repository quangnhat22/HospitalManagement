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
            if (CheckUsername(loginWindowViewModel.Username))
                if (CheckPassword(loginWindowViewModel.Password))
                {
                    LoginSuccessful();
                    return;
                }    
            LoginFailed();
                    
        }

        private void LoginFailed()
        {
            NotifyWindow notifyWindow = new NotifyWindow("Error", "Sai tên đăng nhập hoặc mật khẩu");
            notifyWindow.Show();
        }

        private void LoginSuccessful()
        {
            Window window = Application.Current.MainWindow as Window;
            MainWindow mainWindow = new MainWindow();

            Application.Current.MainWindow = new MainWindow();
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
            Application.Current.MainWindow.Show();
            MainWindowViewModel.User = DataProvider.Ins.DB.USERs.Where(x => x.USERNAME == loginWindowViewModel.Username).First();
        }

        private bool CheckPassword(string plainText)
        {
            string hashedPasswordInput = Encryptor.Hash(plainText);
            return DataProvider.Ins.DB.USERs?.Where(p => p.PASSWORD == hashedPasswordInput).Count() > 0;
        }

        private bool CheckUsername(string username)
        {
            return DataProvider.Ins.DB.USERs.Where(p => p.USERNAME == username).Count() > 0;
        }
    }
}
