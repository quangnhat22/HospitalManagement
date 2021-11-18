using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using HospitalManagement.Model;
using HospitalManagement.View.Room;
using HospitalManagement.Command;
using System.Windows.Input;

namespace HospitalManagement.ViewModel
{
    class RoomViewModel : BaseViewModel
    {
        private string floorNumber;
        private static List<BENHNHAN> patients;
        private static List<BENHNHAN> roomPatients;
        private static List<PHONG> rooms;
        public ICommand ShowPatientsInRoom { get; set; }
        public string FloorNumber
        {
            get { return floorNumber; }
            set { floorNumber = value; OnPropertyChanged("FloorNumber"); }
        }
        public List<BENHNHAN> Patients
        {
            get { return patients; }
            set { patients = value; OnPropertyChanged("Patients"); }
        }
        public List<BENHNHAN> RoomPatients
        {
            get { return roomPatients; }
            set { roomPatients = value; OnPropertyChanged("RoomPatients"); }
        }

        public List<PHONG> Rooms
        {
            get { return rooms; }
            set { rooms = value; OnPropertyChanged("Rooms"); }
        }

        public RoomViewModel(string Floor)
        {
            switch (Floor)
            {
                case "Floor1":
                    patients= DataProvider.Ins.DB.BENHNHANs.Where(p => p.PHONG.TANG.SOTANG == 1).ToList();
                    rooms = DataProvider.Ins.DB.PHONGs.Where(p => p.TANG.SOTANG == 1).ToList();
                    FloorNumber = "1";
                    break;
                case "Floor2":
                    patients = DataProvider.Ins.DB.BENHNHANs.Where(p => p.PHONG.TANG.SOTANG == 2).ToList();
                    rooms = DataProvider.Ins.DB.PHONGs.Where(p => p.TANG.SOTANG == 2).ToList();
                    FloorNumber = "2";
                    break;
                case "Floor3":
                    patients = DataProvider.Ins.DB.BENHNHANs.Where(p => p.PHONG.TANG.SOTANG == 3).ToList();
                    rooms = DataProvider.Ins.DB.PHONGs.Where(p => p.TANG.SOTANG == 3).ToList();
                    FloorNumber = "3";
                    break;
                case "Floor4":
                    patients = DataProvider.Ins.DB.BENHNHANs.Where(p => p.PHONG.TANG.SOTANG == 4).ToList();
                    rooms = DataProvider.Ins.DB.PHONGs.Where(p => p.TANG.SOTANG == 4).ToList();
                    FloorNumber = "4";
                    break;
                case "Floor5":
                    patients = DataProvider.Ins.DB.BENHNHANs.Where(p => p.PHONG.TANG.SOTANG == 5).ToList();
                    rooms = DataProvider.Ins.DB.PHONGs.Where(p => p.TANG.SOTANG == 5).ToList();
                    FloorNumber = "5";
                    break;
                case "Floor6":
                    patients = DataProvider.Ins.DB.BENHNHANs.Where(p => p.PHONG.TANG.SOTANG == 6).ToList();
                    rooms = DataProvider.Ins.DB.PHONGs.Where(p => p.TANG.SOTANG == 6).ToList();
                    FloorNumber = "6";
                    break;
            }
            ShowPatientsInRoom = new ShowPatientsInRoomCommand(this);
        }
    }
}
