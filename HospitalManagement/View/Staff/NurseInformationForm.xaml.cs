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
using HospitalManagement.Model;

namespace HospitalManagement.View.Staff
{
    /// <summary>
    /// Interaction logic for NurseInformationForm.xaml
    /// </summary>
    public partial class NurseInformationForm : Window
    {
        public NurseInformationForm(Nurse nurse)
        {
            InitializeComponent();
            this.DataContext = new NurseInformationViewModel(nurse);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
