using HospitalManagement.Model;
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

namespace HospitalManagement.View.StaffRoleView.TeamTask
{
    /// <summary>
    /// Interaction logic for TaskInformationForm.xaml
    /// </summary>
    public partial class TaskInformationForm : Window
    {
        public TaskInformationForm(CONGVIEC cv)
        {
            InitializeComponent();
            this.DataContext = new TaskInformationViewModel(cv);
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
