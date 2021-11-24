using HospitalManagement.ViewModel.AddAccountVM;
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

namespace HospitalManagement.View.AddStaff
{
    /// <summary>
    /// Interaction logic for AddNewAccountForm.xaml
    /// </summary>
    public partial class AddNewAccountForm : Window
    {
        public AddNewAccountForm()
        {
            InitializeComponent();
            txbVaiTro.SelectedIndex = 0;
            this.DataContext = new AddNewAccountViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void txbVaiTro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (txbVaiTro.SelectedIndex == 0)
            {
                this.userName.Visibility = Visibility.Visible;
                this.userID.Visibility = Visibility.Visible;    
                this.stackPannelGroupBox.Visibility = Visibility.Collapsed;
            }
            else if (txbVaiTro.SelectedIndex == 1)
            {
                this.userName.Visibility = Visibility.Collapsed;
                this.userID.Visibility = Visibility.Collapsed;
                this.stackPannelGroupBox.Visibility = Visibility.Visible;
            }
        }
    }
}
