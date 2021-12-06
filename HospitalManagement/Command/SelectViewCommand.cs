using HospitalManagement.View.EmptyView;
using HospitalManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace HospitalManagement.Command
{
    class SelectViewCommand : ICommand
    {
        private MainWindowViewModel mainWindowViewModel;
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public SelectViewCommand(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ListViewItem listViewItem = parameter as ListViewItem;
            if (listViewItem != null && listViewItem.Tag != null)
            {
                string tag = listViewItem.Tag.ToString();
                if (tag == "Home")
                {
                    mainWindowViewModel.SelectedViewModel = new SwitchDashboardViewModel();
                }
                if (tag == "Staff")
                {
                    if(MainWindowViewModel.User.ROLE == "staff" || MainWindowViewModel.User.ROLE == "leader")
                    {
                        mainWindowViewModel.SelectedViewModel = new SwithStaffViewTeamMemberViewModel();
                    }
                    else
                    {
                        mainWindowViewModel.SelectedViewModel = new SwitchStaffViewModel();
                    }
                }
                if (tag == "Patient")
                {
                    mainWindowViewModel.SelectedViewModel = new SwitchPatientViewModel();
                }
                if (tag == "Rooms")
                {
                    if (MainWindowViewModel.User.ROLE == "admin" || MainWindowViewModel.User.ROLE == "sudo")
                        mainWindowViewModel.SelectedViewModel = new SwitchRoomViewModel();
                    else
                        mainWindowViewModel.SelectedViewModel = new SwitchStaffViewRoomViewModel();
                }
                if (tag == "Teams")
                {
                    mainWindowViewModel.SelectedViewModel = new SwitchTeamViewModel();
                }
                if(tag == "AddStaff")
                {
                    mainWindowViewModel.SelectedViewModel = new SwitchAddStaffViewModel();
                }

                if(tag== "TeamTask")
                {
                    mainWindowViewModel.SelectedViewModel = new SwitchStaffViewTeamTaskViewModel();
                }
            }
        }
    }
}