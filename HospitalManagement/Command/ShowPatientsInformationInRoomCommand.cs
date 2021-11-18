using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HospitalManagement.Model;
using HospitalManagement.ViewModel;
using HospitalManagement.View;
using System.Windows;

namespace HospitalManagement.Command
{
    class ShowPatientsInformationInRoomCommand : ICommand
    {
        private RoomViewModel roomViewModel;

        public ShowPatientsInformationInRoomCommand(RoomViewModel roomViewModel)
        {
            this.roomViewModel = roomViewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            int index = (int)parameter;
            BENHNHAN bn = roomViewModel.RoomPatients[index];
            PatientInformationForm patientInformationForm = new PatientInformationForm(bn);
            patientInformationForm.Show();
        }
    }
}
