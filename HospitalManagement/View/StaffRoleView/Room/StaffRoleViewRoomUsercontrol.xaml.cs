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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HospitalManagement.View.StaffRoleView.Room
{
    /// <summary>
    /// Interaction logic for StaffRoleViewRoomUsercontrol.xaml
    /// </summary>
    public partial class StaffRoleViewRoomUsercontrol : UserControl
    {
        public StaffRoleViewRoomUsercontrol()
        {
            InitializeComponent();
            USER user = MainWindowViewModel.User;
            var leader = user.BACSIs.FirstOrDefault();
            if (leader != null || leader != default(BACSI))
            {
                this.DataContext = new RoomViewModel(leader.TO.TANG.ID);
            }
        }
    }
}
