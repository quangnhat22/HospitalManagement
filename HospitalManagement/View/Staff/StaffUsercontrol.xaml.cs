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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HospitalManagement.View.Staff
{
    /// <summary>
    /// Interaction logic for StaffUsercontrol.xaml
    /// </summary>
    public partial class StaffUsercontrol : UserControl
    {
        public StaffUsercontrol()
        {
            InitializeComponent();
            this.DataContext = new StaffViewModel();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DoctorStackPanelComponent.Visibility = NurseStackPanelComponent.Visibility = Visibility.Collapsed;
            TabControl tabControl = sender as TabControl;
            TabItem tabItem = tabControl.SelectedItem as TabItem;
            if(tabItem != null)
            {
                if(tabItem.Tag?.ToString() == "Doctor")
                {
                    DoctorStackPanelComponent.Visibility= Visibility.Visible;
                }
                else if(tabItem.Tag?.ToString() == "Nurse")
                {
                    NurseStackPanelComponent.Visibility= Visibility.Visible;
                }
            }
        }
    }
}
