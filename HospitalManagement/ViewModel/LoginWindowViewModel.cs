using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HospitalManagement.ViewModel
{
     public class LoginWindowViewModel :BaseViewModel
     {
        public ICommand OpenMainWindow { get; set; }
        public LoginWindowViewModel()
        {
            OpenMainWindow = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) => { ShowMainWindow(p); });
        }
        void ShowMainWindow(Window p)
        {
            var mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
            //I assume loginWindow is a reference to the LoginWindow being shown
            p.Close();
        }

    }
}
