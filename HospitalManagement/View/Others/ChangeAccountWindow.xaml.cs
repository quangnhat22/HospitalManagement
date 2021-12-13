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

namespace HospitalManagement.View.Others
{
    /// <summary>
    /// Interaction logic for ChangeAccountWindow.xaml
    /// </summary>
    public partial class ChangeAccountWindow : Window
    {
        public ChangeAccountWindow()
        {
            InitializeComponent();
            this.DataContext = new ChangeAccountViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
