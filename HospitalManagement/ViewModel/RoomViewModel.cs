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
using System.Windows;
using System.ComponentModel;

namespace HospitalManagement.ViewModel
{
    class RoomViewModel : INotifyPropertyChanged
    { 
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private string floorNumber;
        private string currentRoom;

        public class Phong : PHONG, INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            public virtual void OnPropertyChanged(string name)
            {
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
            private int count;

            public int COUNT
            {
                get { return count; }
                set { count = value; OnPropertyChanged("COUNT"); }
            }
            public Phong()
            {
                count = 0;
            }
        }

        private static List<BENHNHAN> patients;
        private static List<BENHNHAN> roomPatients;
        private static List<PHONG> rooms;
        private static List<Phong> roomsExtended;
        public ICommand ShowPatientsInRoom { get; set; }
        public ICommand ShowPatientsInformationInRoomCommand { get; set; }
        public ICommand RoomProgressBarCommand { get; set; }

        public string FloorNumber
        {
            get { return floorNumber; }
            set { floorNumber = value; OnPropertyChanged("FloorNumber"); }
        }
        public string CurrentRoom
        {
            get { return currentRoom; }
            set { currentRoom = value; OnPropertyChanged("CurrentRoom"); }
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

        public List<Phong> RoomsExtended
        {
            get { return roomsExtended; }
            set { roomsExtended = value; OnPropertyChanged("RoomsExtended"); }
        }

        public RoomViewModel(string Floor)
        {
            switch (Floor)
            {
                case "Floor1":
                    patients= DataProvider.Ins.DB.BENHNHANs.Where(p => p.PHONG.TANG.SOTANG == 1).ToList();
                    roomPatients= DataProvider.Ins.DB.BENHNHANs.Where(p => p.PHONG.TANG.SOTANG == 1).ToList();
                    rooms = DataProvider.Ins.DB.PHONGs.Where(p => p.TANG.SOTANG == 1).ToList();
                    currentRoom = "Tầng 1";
                    floorNumber = "1";
                    break;
                case "Floor2":
                    patients = DataProvider.Ins.DB.BENHNHANs.Where(p => p.PHONG.TANG.SOTANG == 2).ToList();
                    roomPatients = DataProvider.Ins.DB.BENHNHANs.Where(p => p.PHONG.TANG.SOTANG == 2).ToList();
                    rooms = DataProvider.Ins.DB.PHONGs.Where(p => p.TANG.SOTANG == 2).ToList();
                    currentRoom = "Tầng 2";
                    floorNumber = "2";
                    break;
                case "Floor3":
                    patients = DataProvider.Ins.DB.BENHNHANs.Where(p => p.PHONG.TANG.SOTANG == 3).ToList();
                    roomPatients = DataProvider.Ins.DB.BENHNHANs.Where(p => p.PHONG.TANG.SOTANG == 3).ToList();
                    rooms = DataProvider.Ins.DB.PHONGs.Where(p => p.TANG.SOTANG == 3).ToList();
                    currentRoom = "Tầng 3";
                    floorNumber = "3";
                    break;
                case "Floor4":
                    patients = DataProvider.Ins.DB.BENHNHANs.Where(p => p.PHONG.TANG.SOTANG == 4).ToList();
                    roomPatients = DataProvider.Ins.DB.BENHNHANs.Where(p => p.PHONG.TANG.SOTANG == 4).ToList();
                    rooms = DataProvider.Ins.DB.PHONGs.Where(p => p.TANG.SOTANG == 4).ToList();
                    currentRoom = "Tầng 4";
                    floorNumber = "4";
                    break;
                case "Floor5":
                    patients = DataProvider.Ins.DB.BENHNHANs.Where(p => p.PHONG.TANG.SOTANG == 5).ToList();
                    roomPatients = DataProvider.Ins.DB.BENHNHANs.Where(p => p.PHONG.TANG.SOTANG == 5).ToList();
                    rooms = DataProvider.Ins.DB.PHONGs.Where(p => p.TANG.SOTANG == 5).ToList();
                    currentRoom = "Tầng 5";
                    floorNumber = "5";
                    break;
                case "Floor6":
                    patients = DataProvider.Ins.DB.BENHNHANs.Where(p => p.PHONG.TANG.SOTANG == 6).ToList();
                    roomPatients = DataProvider.Ins.DB.BENHNHANs.Where(p => p.PHONG.TANG.SOTANG == 6).ToList();
                    rooms = DataProvider.Ins.DB.PHONGs.Where(p => p.TANG.SOTANG == 6).ToList();
                    currentRoom = "Tầng 6";
                    floorNumber = "6";
                    break;
            }
            ShowPatientsInRoom = new ShowPatientsInRoomCommand(this);
            ShowPatientsInformationInRoomCommand = new ShowPatientsInformationInRoomCommand(this);
            RoomProgressBarCommand = new RoomProgressBarCommand();
            //roomextend = rooms
            //  .Select(x => new Phong() { ID = x.ID, IDTANG = x.IDTANG, SOPHONG = x.SOPHONG, TANG = x.TANG, SUCCHUA = x.SUCCHUA, GHICHU = x.GHICHU })
            //  .ToList();

            roomsExtended = rooms.ConvertAll(x => new Phong { ID = x.ID, IDTANG = x.IDTANG, SOPHONG = x.SOPHONG, TANG = x.TANG, SUCCHUA = x.SUCCHUA, GHICHU = x.GHICHU});

            foreach (var patient in patients)
                roomsExtended[(int)patient.PHONG.SOPHONG - 1].COUNT++;
        }
    }
}
