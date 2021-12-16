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

namespace HospitalManagement.View.Staff
{
    /// <summary>
    /// Interaction logic for NurseForm.xaml
    /// </summary>
    public partial class NurseForm : Window
    {
        public NurseForm(YTA yt)
        {
            InitializeComponent();
            this.DataContext = new NurseInformationViewModel(yt);
            NurseInformationViewModel informationViewModel = new NurseInformationViewModel(yt);
            Closing += informationViewModel.OnWindowFormClosing;
        }
    }
}
