using HospitalManagement.View;
using HospitalManagement.View.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HospitalManagement.Command
{
    public class OpenRoomWindowCommand : ICommand
    {
        private int currentBuilding;
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }
        public OpenRoomWindowCommand(int currentBuilding)
        {
            this.currentBuilding = currentBuilding;
        }
        public bool CanExecute(object parameter)
        {
            return parameter == null ? false : true;
        }

        public void Execute(object parameter)
        {
            int idTang = (int)parameter;
            var roomWindow = new Room(idTang);
            Application.Current.MainWindow = roomWindow;
            roomWindow.Show();
        }
    }
}
