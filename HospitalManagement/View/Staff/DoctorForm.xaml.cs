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
    /// Interaction logic for DoctorForm.xaml
    /// </summary>
    public partial class DoctorForm : Window
    {
        public DoctorForm()
        {
            InitializeComponent();
            this.DataContext = new DoctorFormViewModel();
        }
    }
}
