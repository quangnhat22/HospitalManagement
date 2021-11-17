﻿using HospitalManagement.View;
using HospitalManagement.View.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HospitalManagement.Command
{
    class OpenChangeAccoutWindowCommand : ICommand
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
            ChangeAccountWindow account = new ChangeAccountWindow();
            Application.Current.MainWindow = account;
            account.Show();
        }
    }
}