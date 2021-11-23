using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            int id = (int)parameter;
            PHONG phong = DataProvider.Ins.DB.PHONGs.Where(p => p.ID == id).First();
            roomViewModel.Patients = new ObservableCollection<BENHNHAN>(phong.BENHNHANs.ToList());
            roomViewModel.CurrentRoom = "Phòng " + phong.TANG.SOTANG.ToString() + "." + phong.SOPHONG.ToString();
        }
    }
}
