using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.View;
using HospitalManagement.View.Others;
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
            USER user = DataProvider.Ins.DB.USERs.Where(x => x.USERNAME == loginWindowViewModel.Username).First();
            Window window = Application.Current.MainWindow as Window;
            if (isFirstTimeLogin(user))
            {
                FirstLoginViewModel.User = user;
                FirstLoginWindow firstLoginWindow = new FirstLoginWindow();
                Application.Current.MainWindow = firstLoginWindow;
                Application.Current.MainWindow.Show();
            }
            else
            {
                MainWindowViewModel.User = user;
                MainWindow mainWindow = new MainWindow();
                Application.Current.MainWindow = mainWindow;
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

        private bool CheckAuthentication(string username, string password)
        {
            try
            {
                string hashPassword = Encryptor.Hash(password);
                return DataProvider.Ins.DB.USERs.Where(p => p.USERNAME == username && p.PASSWORD == hashPassword).Count() > 0;
            }
            catch
            {
                NotifyWindow notifyWindow = new NotifyWindow("Danger", "Không thể kết nói tới server");
                notifyWindow.ShowDialog();
                return false;
            }
        }

        private bool isFirstTimeLogin(USER user)
        {
            if (user.ROLE == "doctor" || user.ROLE == "leader")
            {
                BACSI bacsi = user.BACSIs.FirstOrDefault();
                if(bacsi != null)
                {
                    if (isAllArgumentsNull(bacsi.HO, bacsi.TEN, bacsi.SDT, bacsi.QUOCTICH, bacsi.DIACHI))
                    {
                        return true;
                    }
                }    
                
            }
            else if (user.ROLE == "nurse")
            {
                YTA yta = user.YTAs.FirstOrDefault();
                if(yta != null)
                {
                    if (isAllArgumentsNull(yta.HO, yta.TEN, yta.SDT, yta.QUOCTICH, yta.DIACHI))
                    {
                        return true;
                    }
                }    
                
            }
            else if (user.ROLE == "admin")
            {
                ADMIN admin = user.ADMINs.FirstOrDefault();
                if(admin != null)
                {
                    if (isAllArgumentsNull(admin.HO, admin.TEN, admin.SDT, admin.QUOCTICH, admin.DIACHI))
                    {
                        return true;
                    }
                }                  
            }
            return false;
        }

        private bool isAllArgumentsNull(params Object[] args)
        {
            foreach (Object o in args)
            {
                if (o == null)
                    return true;
            }
            return false;
        }
    }
}
