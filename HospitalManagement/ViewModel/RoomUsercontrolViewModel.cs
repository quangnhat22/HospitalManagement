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
        private static List<TANG> floors = DataProvider.Ins.DB.TANGs.Where(p => p.TOA.IDTOA == 1).ToList();
        public List<TANG> Floors
        {
            get { return floors; }
            set { floors = value; OnPropertyChanged("Floors"); }
        }

        public RoomUsercontrolViewModel()
        {
            OpenRoomWindow = new OpenRoomWindowCommand();
        }
    }
}
