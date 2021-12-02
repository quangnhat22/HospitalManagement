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
        private string currentBuildingName;
        private bool canMoveForward;
        private bool canMoveBackward = false;
        private int floorsNumber;
        private string newBuildingName;
        private string selectedBuildingName;
        private string addVisibility;
        private string deleteVisibility;

        private static List<TOA> buildings;
        private static List<TANG> floors = DataProvider.Ins.DB.TANGs.Where(p => p.TOA.IDTOA == 1).ToList();
        
        public int CurrentBuilding
        {
            get { return currentBuilding; }
            set { currentBuilding = value; OnPropertyChanged("CurrentBuilding"); }
        }

        public string CurrentBuildingName
        {
            get { return currentBuildingName; }
            set { currentBuildingName = value; OnPropertyChanged("CurrentBuildingName"); }
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

        public string NewBuildingName
        {
            get { return newBuildingName; }
            set { newBuildingName = value; OnPropertyChanged("NewBuildingName"); }
        }

        public string SelectedBuildingName
        {
            get { return selectedBuildingName; }
            set { selectedBuildingName = value; OnPropertyChanged("SelectedBuildingName"); }
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
            LoadData();
            OpenRoomWindow = new OpenRoomWindowCommand(currentBuilding);
            ChangeBuildingCommand = new ChangeBuildingCommand(this);
            AddBuildingCommand = new AddBuildingCommand(this);
            DeleteBuildingCommand = new DeleteBuildingCommand(this);
            AddOrDeleteBuildingCommand = new AddOrDeleteBuildingCommand(this);
        }

        private async void LoadData()
        {
            await Task.Run(async () =>
            {
                buildings = await Task.Run(() =>
                {
                    using (QUANLYBENHVIENEntities dbContext = new QUANLYBENHVIENEntities())
                    {
                        return dbContext.TOAs.ToList();
                    }
                });
                currentBuilding = 0;
                currentBuildingName = buildings[0].DISPLAYNAME;
                newBuildingName = "";
                canMoveForward = Buildings.Count > 1 ? true : false;
                selectedBuildingName = buildings[0].DISPLAYNAME;
                addVisibility = "Collapsed";
                deleteVisibility = "Collapsed";
            });
        }
    }
} 