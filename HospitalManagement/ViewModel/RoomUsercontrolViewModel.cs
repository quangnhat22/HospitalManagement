using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using HospitalManagement.Command;
using HospitalManagement.Model;

namespace HospitalManagement.ViewModel
{
    class RoomUsercontrolViewModel : BaseViewModel
    {
        public ICommand OpenRoomWindow { get; set; }
        public ICommand ChangeBuildingCommand { get; set; }
        public ICommand DisableButtonCommand { get; set; }

        private int currentBuilding;
        private static List<TOA> buildings;
        private static List<TANG> floors = DataProvider.Ins.DB.TANGs.Where(p => p.TOA.IDTOA == 1).ToList();
        
        public int CurrentBuilding
        {
            get { return currentBuilding; }
            set { currentBuilding = value; OnPropertyChanged("CurrentBuilding"); }
        }
        
        public List<TOA> Buildings
        {
            get { return buildings; }
            set { buildings = value; OnPropertyChanged("Buildings"); }
        }
        public List<TANG> Floors
        {
            get { floors.Reverse(); return floors; }
            set { floors = value; OnPropertyChanged("Floors"); }
        }

        public RoomUsercontrolViewModel()
        {
            currentBuilding = 1;
            Buildings = DataProvider.Ins.DB.TOAs.ToList();
            OpenRoomWindow = new OpenRoomWindowCommand(currentBuilding);
            ChangeBuildingCommand = new ChangeBuildingCommand(this);
            DisableButtonCommand = new DisableButtonCommand();
        }
    }
}
