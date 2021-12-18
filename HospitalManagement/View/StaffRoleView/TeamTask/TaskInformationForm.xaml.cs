using HospitalManagement.Model;
using HospitalManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public TaskInformationForm(int IDCONGVIEC)
        {
            InitializeComponent();
            this.DataContext = new TaskInformationViewModel(IDCONGVIEC);
            if (MainWindowViewModel.User?.ROLE != "leader")
            {
                btnEdit.IsEnabled = false;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            btnOK.Visibility = Visibility.Visible;
            btnEdit.Visibility = Visibility.Collapsed;
            Separator.Visibility = Visibility.Visible;
            dtgMembers.Visibility = Visibility.Visible;
            txbSubject.IsReadOnly = false;
            dpStart.IsEnabled = true;
            tpStart.IsEnabled = true;
            dpEnd.IsEnabled = true;
            tpEnd.IsEnabled = true;
            cbType.IsEnabled = true;
            txbLocation.IsReadOnly = false;
            txbBody.IsReadOnly = false;;
            lbxMember.IsEnabled = true;
        }
    }
}
