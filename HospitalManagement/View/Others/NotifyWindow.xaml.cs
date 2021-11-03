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
using HospitalManagement.ViewModel;

namespace HospitalManagement.View
{
    /// <summary>
    /// Interaction logic for NotifyWindow.xaml
    /// </summary>
    public partial class NotifyWindow : Window
    {
        public NotifyWindow(string state, string Message = "")
        {
            InitializeComponent();
            this.DataContext = new NotifyWindowViewModel();
            this.Tag = state;
            this.Title = Message;
        }
        private void Window_Deactivated(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch
            {
                ;
            }
        }
    }
}
