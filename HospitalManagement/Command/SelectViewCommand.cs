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
        public event EventHandler CanExecuteChanged;

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
                    mainWindowViewModel.SelectedViewModel = new DashBoardViewModel();
                }
                if (tag == "Doctor")
                {
                    mainWindowViewModel.SelectedViewModel = new DoctorViewModel();
                }
                if (tag == "Patient")
                {
                    mainWindowViewModel.SelectedViewModel = new PatientViewModel();
                }
                if (tag == "Facilities")
                {
                    mainWindowViewModel.SelectedViewModel = new FacilitiesViewModel();
                }
            }
        }
    }
}