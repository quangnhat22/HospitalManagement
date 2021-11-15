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

namespace HospitalManagement.View.Room
{
    /// <summary>
    /// Interaction logic for RoomUsercontrol.xaml
    /// </summary>
    public partial class RoomUsercontrol : UserControl
    {
        public RoomUsercontrol()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Room room = new Room();
            room.Show();
        }
    }
}
