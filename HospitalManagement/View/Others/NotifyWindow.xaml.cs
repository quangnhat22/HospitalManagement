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
        public MessageBoxResult Result { get; set; }

        public NotifyWindow(string state, string Message = "",string cancel= "Collapsed", int width = 300, int height=200)
        {
            InitializeComponent();
            this.DataContext = new NotifyWindowViewModel(cancel);
            this.Tag = state;
            this.Title = Message;
            this.Height = height;
            this.Width = width;
            Result = MessageBoxResult.Cancel;
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

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Cancel;
            this.Close();
        }
    }
}
