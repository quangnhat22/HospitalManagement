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
                    mainWindowViewModel.SelectedViewModel = new SwitchStaffViewModel();
                }
                if (tag == "Patient")
                {
                    mainWindowViewModel.SelectedViewModel = new SwitchPatientViewModel();
                }
                if (tag == "Facilities")
                {
                    mainWindowViewModel.SelectedViewModel = new SwitchFacilitiesViewModel();
                }
                if (tag == "Rooms")
                {
                    mainWindowViewModel.SelectedViewModel = new SwitchRoomViewModel();
                }
                if (tag == "Teams")
                {
                    mainWindowViewModel.SelectedViewModel = new SwitchTeamViewModel();
                }
            }
        }
    }
}