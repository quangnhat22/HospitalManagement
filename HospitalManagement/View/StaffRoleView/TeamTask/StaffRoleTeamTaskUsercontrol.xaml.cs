using HospitalManagement.Model;
using HospitalManagement.ViewModel;
using HospitalManagement.ViewModel.StaffViewViewModel.TeamTask;
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

namespace HospitalManagement.View.StaffRoleView.TeamTask
{
    /// <summary>
    /// Interaction logic for StaffRoleTeamTaskUsercontrol.xaml
    /// </summary>
    public partial class StaffRoleTeamTaskUsercontrol : UserControl
    {
        public StaffRoleTeamTaskUsercontrol()
        {
            InitializeComponent();
            using(QUANLYBENHVIENEntities dbContext = new QUANLYBENHVIENEntities())
			{
                USER user = dbContext.USERs.Find(MainWindowViewModel.User.ID);
                if (user.ROLE == "leader")
                    this.DataContext = new StaffRoleTeamTaskViewModel();
                else if (user.ROLE == "doctor" || MainWindowViewModel.User.ROLE == "nurse")
                    this.DataContext = new StaffRoleTeamTaskViewModelStaff();
            }
            
        }
    }
}
