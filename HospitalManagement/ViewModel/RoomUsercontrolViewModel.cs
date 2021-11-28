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
        public ICommand AddBuildingCommand { get; set; }
        public ICommand DeleteBuildingCommand { get; set; }
        public ICommand AddOrDeleteBuildingCommand { get; set; }

        private int currentBuilding;
        private bool canMoveForward;
        private bool canMoveBackward = false;
        private int floorsNumber;
        private int selectedBuilding;
        private string addVisibility;
        private string deleteVisibility;

        private static List<TOA> buildings;
        private static List<TANG> floors = DataProvider.Ins.DB.TANGs.Where(p => p.TOA.IDTOA == 1).ToList();
        
        public int CurrentBuilding
        {
            get { return currentBuilding; }
            set { currentBuilding = value; OnPropertyChanged("CurrentBuilding"); }
        }

        public bool CanMoveForward
        {
            get { return canMoveForward; }
            set { canMoveForward = value; OnPropertyChanged("CanMoveForward"); }
        }

        public bool CanMoveBackward
        {
            get { return canMoveBackward; }
            set { canMoveBackward = value; OnPropertyChanged("CanMoveBackward"); }
        }

        public int FloorsNumber
        {
            get { return floorsNumber; }
            set { floorsNumber = value; OnPropertyChanged("FloorsNumber"); }
        }

        public int SelectedBuilding
        {
            get { return selectedBuilding; }
            set { selectedBuilding = value; OnPropertyChanged("SelectedBuilding"); }
        }

        public string AddVisibility
        {
            get { return addVisibility; }
            set { addVisibility = value; OnPropertyChanged("AddVisibility"); }
        }

        public string DeleteVisibility
        {
            get { return deleteVisibility; }
            set { deleteVisibility = value; OnPropertyChanged("DeleteVisibility"); }
        }

        public List<TOA> Buildings
        {
            get { return buildings; }
            set { buildings = value; OnPropertyChanged("Buildings"); }
        }

        public List<TANG> Floors
        {
            get 
            {
                if (floors.Count > 0 && floors[0].SOTANG == 1) floors.Reverse();
                return floors;
            }
            set { floors = value; OnPropertyChanged("Floors"); }
        }

        public RoomUsercontrolViewModel()
        {
            currentBuilding = 1;
            Buildings = DataProvider.Ins.DB.TOAs.ToList();
            canMoveForward = Buildings.Count > 1 ? true : false;
            selectedBuilding = (int)buildings[0].SOTOA;
            addVisibility = "Collapsed";
            deleteVisibility = "Collapsed";
            OpenRoomWindow = new OpenRoomWindowCommand(currentBuilding);
            ChangeBuildingCommand = new ChangeBuildingCommand(this);
            AddBuildingCommand = new AddBuildingCommand(this);
            DeleteBuildingCommand = new DeleteBuildingCommand(this);
            AddOrDeleteBuildingCommand = new AddOrDeleteBuildingCommand(this);
        }
    }
} 