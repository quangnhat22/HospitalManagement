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
    /// Interaction logic for NurseUsercontrol.xaml
    /// </summary>
    public partial class NurseUsercontrol : UserControl
    {
        public NurseUsercontrol()
        {
            InitializeComponent();
            this.DataContext = new NurseViewModel();
        }
        //private void ScrollViewer_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        //{

        //}
    }
}
