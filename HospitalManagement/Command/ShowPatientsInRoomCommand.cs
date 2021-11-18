using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            SelectableItem<PHONG> selectableItem = parameter as SelectableItem<PHONG>;
            PHONG ph = selectableItem.Value as PHONG;
            roomViewModel.Patients= DataProvider.Ins.DB.BENHNHANs.Where(p => p.PHONG.SOPHONG==ph.SOPHONG).ToList();
        }
    }
}
