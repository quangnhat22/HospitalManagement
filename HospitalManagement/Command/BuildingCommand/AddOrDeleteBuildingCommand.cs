using HospitalManagement.ViewModel;
using HospitalManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;

namespace HospitalManagement.Command
{
    class AddOrDeleteBuildingCommand : ICommand
    {
        private RoomUsercontrolViewModel roomUsercontrolViewModel;
        public AddOrDeleteBuildingCommand(RoomUsercontrolViewModel roomUsercontrolViewModel)
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
            if(btn.Name== "btnAddBuilding")
            {
                if (roomUsercontrolViewModel.AddVisibility == "Collapsed") 
                {
                    roomUsercontrolViewModel.AddVisibility = "Visible";
                    roomUsercontrolViewModel.DeleteVisibility = "Collapsed";
                }else
                    roomUsercontrolViewModel.AddVisibility = "Collapsed";
            }
            else
            {
                if (roomUsercontrolViewModel.DeleteVisibility == "Collapsed")
                {
                    roomUsercontrolViewModel.DeleteVisibility = "Visible";
                    roomUsercontrolViewModel.AddVisibility = "Collapsed";
                }
                else
                    roomUsercontrolViewModel.DeleteVisibility = "Collapsed";
            }
        }
    }
}
