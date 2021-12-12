using HospitalManagement.Model;
using HospitalManagement.Utils;
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
                this.DataContext = new RoomViewModel(ToUtils.GetTOID(user));
        }
    }
}
