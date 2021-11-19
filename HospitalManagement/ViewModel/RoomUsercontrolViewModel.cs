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
        
        public string Tag { get; }

        private int currentBuilding;
        public int CurrentBuilding
        {
            get { return currentBuilding; }
            set { currentBuilding = value; OnPropertyChanged("CurrentBuilding"); }
        }

        private static List<TOA> buildings;
        public List<TOA> Buildings
        {
            get { return buildings; }
            set { buildings = value; OnPropertyChanged("Buildings"); }
        }

        public RoomUsercontrolViewModel()
        {
            currentBuilding = 1;
            Buildings = DataProvider.Ins.DB.TOAs.ToList();
            OpenRoomWindow = new OpenRoomWindowCommand();
            ChangeBuildingCommand = new ChangeBuildingCommand(this);
            DisableButtonCommand = new DisableButtonCommand();
        }
    }
}
