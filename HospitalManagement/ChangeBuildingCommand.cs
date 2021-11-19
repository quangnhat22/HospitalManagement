using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            
            if (btn.Name == "btnLeft")
            {
                if (currentBuilding > 1)
                {
                    currentBuilding--;
                    roomUsercontrolViewModel.Floors= DataProvider.Ins.DB.TANGs.Where(p => p.TOA.IDTOA == currentBuilding).ToList();
                }
                    
            }
            else
            {
                if (currentBuilding < roomUsercontrolViewModel.Buildings.Count)
                {
                    currentBuilding++;
                    roomUsercontrolViewModel.Floors = DataProvider.Ins.DB.TANGs.Where(p => p.TOA.IDTOA == currentBuilding).ToList();
                }
                    
            }
            roomUsercontrolViewModel.CurrentBuilding = currentBuilding;
        }
    }
}
