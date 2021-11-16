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
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }
        public bool CanExecute(object parameter)
        {
            return parameter == null ? false : true;
        }

        public void Execute(object parameter)
        {
            Button button = parameter as Button;
            var roomWindow = new Room(button.Name);
            Application.Current.MainWindow = roomWindow;
            roomWindow.Show();
        }
    }
}
