using HospitalManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HospitalManagement.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        
        public LoginWindow()
        {
            InitializeComponent();
            this.DataContext = new LoginWindowViewModel();
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            LoginWindowViewModel loginWindowViewModel = this.DataContext as LoginWindowViewModel;
            loginWindowViewModel.Password = txtPassword.Password;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (Application.Current.MainWindow is null)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
