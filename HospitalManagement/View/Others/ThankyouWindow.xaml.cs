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

namespace HospitalManagement.View.Others
{
    /// <summary>
    /// Interaction logic for ThankyouWindow.xaml
    /// </summary>
    public partial class ThankyouWindow : Window
    {
        public ThankyouWindow()
        {
            InitializeComponent();
        }
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Window_Deactivated(null, null);
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
