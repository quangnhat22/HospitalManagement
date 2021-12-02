using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using HospitalManagement.ViewModel;
using HospitalManagement.Model;
using HospitalManagement.View;

namespace HospitalManagement.Command
{
    class AddBuildingCommand : ICommand
    {
        private RoomUsercontrolViewModel roomUsercontrolViewModel;
        public AddBuildingCommand(RoomUsercontrolViewModel roomUsercontrolViewModel)
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
            int floorsNumber = (int)roomUsercontrolViewModel.FloorsNumber;
            string newBuildingName = roomUsercontrolViewModel.NewBuildingName;
            if (newBuildingName != "") newBuildingName.Trim();
            if (floorsNumber > 0 && newBuildingName != "") 
            {
                bool isValidName = true;
                foreach(var building in roomUsercontrolViewModel.Buildings)
                    if (building.DISPLAYNAME == newBuildingName)
                    {
                        isValidName = false;
                        break;
                    }
                if (!isValidName)
                {
                    NotifyWindow notifyWindow = new NotifyWindow("Error", "Tên toà đã tồn tại!");
                    notifyWindow.Show();
                }
                else
                {
                    var newBuilding = new TOA
                    {
                        SOTOA = roomUsercontrolViewModel.Buildings.Count > 0 ? roomUsercontrolViewModel.Buildings.Count + 1 : 1,
                        DISPLAYNAME = newBuildingName,
                        SLTANG = floorsNumber
                    };
                    DataProvider.Ins.DB.TOAs.Add(newBuilding);
                    roomUsercontrolViewModel.Buildings.Add(newBuilding);
                    for (int i = 1; i <= floorsNumber; i++)
                    {
                        var newFloor = new TANG
                        {
                            SOTANG = i,
                            SLPHONG = 10
                        };
                        newFloor.TOA = newBuilding;
                        DataProvider.Ins.DB.TANGs.Add(newFloor);
                        for (int j = 1; j <= 10; j++)
                        {
                            var newRoom = new PHONG
                            {
                                SOPHONG = j,
                                SUCCHUA = 8,
                            };
                            newRoom.TANG = newFloor;
                            DataProvider.Ins.DB.PHONGs.Add(newRoom);
                        }
                    }
                    DataProvider.Ins.DB.SaveChanges();
                    NotifyWindow notifyWindow = new NotifyWindow("Success", "Thêm thành công toà " + newBuilding.DISPLAYNAME);
                    notifyWindow.Show();
                    roomUsercontrolViewModel.CanMoveForward = true;
                }
            }
            else
            {
                NotifyWindow notifyWindow = new NotifyWindow("Error", "Vui lòng nhập đủ tên toà và số tầng!");
                notifyWindow.Show();
            }    
        }
    }
}
