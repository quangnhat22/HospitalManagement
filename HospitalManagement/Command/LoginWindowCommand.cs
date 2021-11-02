﻿using HospitalManagement.Utils;
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
        }

        private bool CheckPassword(string plainText)
        {
            string hashedPasswordInput = Encryptor.Hash(plainText);
            string databasePassword = "cdd96d3cc73d1dbdaffa03cc6cd7339b";
            return string.Compare(databasePassword, hashedPasswordInput, true) == 0;
        }

        private bool CheckUsername(string username)
        {
            string databaseUsername = "admin";
            return username == databaseUsername;
        }
    }
}
