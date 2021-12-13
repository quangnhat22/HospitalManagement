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

namespace HospitalManagement.View.StaffRoleView.TeamMember
{
    /// <summary>
    /// Interaction logic for TeamMemberUsercontrol.xaml
    /// </summary>
    public partial class TeamMemberUsercontrol : UserControl
    {
        public TeamMemberUsercontrol()
        {
            InitializeComponent();
            USER user = MainWindowViewModel.User;
            this.DataContext = new TeamMemberViewModel(ToUtils.GetTOID(user));
        }
    }
}
