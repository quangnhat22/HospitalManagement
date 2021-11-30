using HospitalManagement.Utils;
using HospitalManagement.ViewModel.StaffViewViewModel.TeamTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HospitalManagement.Command.TeamTaskCommand
{
    class RemoveInvolveMembersCommand : ICommand
    {
        private AddToDoFormViewModel addToDoFormViewModel;

        public RemoveInvolveMembersCommand(AddToDoFormViewModel addToDoFormViewModel)
        {
            this.addToDoFormViewModel = addToDoFormViewModel;
        }

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            FrameworkElement frameworkElement = parameter as FrameworkElement;
            StaffInformation staffInformation = frameworkElement.DataContext as StaffInformation;
            if(staffInformation != null)
            {
                addToDoFormViewModel.InvolveMembers.Remove(staffInformation);
            }
        }
    }
}
