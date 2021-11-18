using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HospitalManagement.Command
{
    class ShowPatientsInRoomCommand : ICommand
    {
        private RoomViewModel roomViewModel;

        public ShowPatientsInRoomCommand(RoomViewModel roomViewModel)
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
            int sp = (int)parameter;
            roomViewModel.RoomPatients = roomViewModel.Patients.Where(p => p.PHONG.SOPHONG == sp).ToList();
        }
    }
}
