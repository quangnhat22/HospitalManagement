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
using HospitalManagement.Model;
using HospitalManagement.View;
using HospitalManagement.ViewModel;

namespace HospitalManagement.View
{
    /// <summary>
    /// Interaction logic for PatientInformationForm.xaml
    /// </summary>
    public partial class PatientInformationForm : Window
    {
        public PatientInformationForm(BENHNHAN bn)
        {
            InitializeComponent();
            this.DataContext = new PatientInformationViewModel(bn);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
