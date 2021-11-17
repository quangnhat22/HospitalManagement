using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using HospitalManagement.Command;

namespace HospitalManagement.ViewModel
{
    class RoomUsercontrolViewModel : BaseViewModel
    {
        public ICommand OpenRoomWindow { get; set; }
        public string Tag { get; }

        public RoomUsercontrolViewModel()
        {
            OpenRoomWindow = new OpenRoomWindowCommand();
        }
    }
}
