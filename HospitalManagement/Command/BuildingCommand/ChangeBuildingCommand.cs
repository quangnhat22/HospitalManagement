using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HospitalManagement.Command;
using HospitalManagement.Model;
using HospitalManagement.ViewModel;

namespace HospitalManagement
{
    class ChangeBuildingCommand : ICommand
    {
        private RoomUsercontrolViewModel roomUsercontrolViewModel;

        public ChangeBuildingCommand(RoomUsercontrolViewModel roomUsercontrolViewModel)
        {
            this.roomUsercontrolViewModel = roomUsercontrolViewModel;
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
            Button btn = parameter as Button;
            int currentBuilding = roomUsercontrolViewModel.CurrentBuilding;
            int currentBuildingID;
            if (btn.Name == "btnLeft")
            {
                if (currentBuilding > 0)
                {
                    currentBuilding--;
                    currentBuildingID = (int)roomUsercontrolViewModel.Buildings[currentBuilding].IDTOA;
                    roomUsercontrolViewModel.Floors= DataProvider.Ins.DB.TANGs.Where(p => p.TOA.IDTOA == currentBuildingID).ToList();
                    roomUsercontrolViewModel.OpenRoomWindow = new OpenRoomWindowCommand(currentBuilding);
                }
            }
            else
            {
                if (currentBuilding < roomUsercontrolViewModel.Buildings.Count - 1)
                {
                    currentBuilding++;
                    currentBuildingID = (int)roomUsercontrolViewModel.Buildings[currentBuilding].IDTOA;
                    roomUsercontrolViewModel.Floors = DataProvider.Ins.DB.TANGs.Where(p => p.TOA.IDTOA == currentBuildingID).ToList();
                    roomUsercontrolViewModel.OpenRoomWindow = new OpenRoomWindowCommand(currentBuilding);
                }
            }
            roomUsercontrolViewModel.CurrentBuilding = currentBuilding;
            roomUsercontrolViewModel.CurrentBuildingName = roomUsercontrolViewModel.Buildings[currentBuilding].DISPLAYNAME;
            if (currentBuilding == 0)
                roomUsercontrolViewModel.CanMoveBackward = false;
            else
                if (currentBuilding == roomUsercontrolViewModel.Buildings.Count - 1)
                roomUsercontrolViewModel.CanMoveForward = false;
            else
            {
                roomUsercontrolViewModel.CanMoveForward = true;
                roomUsercontrolViewModel.CanMoveBackward = true;
            }
        }
    }
}
