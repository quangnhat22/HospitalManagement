using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using HospitalManagement.Model;
using HospitalManagement.View.Room;

namespace HospitalManagement.ViewModel
{
    class RoomViewModel : BaseViewModel
    {
        public static List<BENHNHAN> patients;
        public List<BENHNHAN> Patients
        {
            get { return patients; }
            set { patients = value; OnPropertyChanged("Patients"); }
        }
        //public class Patient
        //{
        //    public string ten { get; set; }
        //    public string tinhtrang { get; set; }
        //}

        public class Room
        {
            public int soluong { get; set; }
            public int soluongmax { get; set; }
            public string ten { get; set; }
        }

        //private List<Patient> patientList = new List<Patient>();
        //public List<Patient> PatientList
        //{
        //    get { return patientList; }
        //    set { patientList = value; OnPropertyChanged("PatientList"); }
        //}

        private List<Room> roomList = new List<Room>();
        public List<Room> RoomList
        {
            get { return roomList; }
            set { roomList = value; OnPropertyChanged("RoomList"); }
        }

        public RoomViewModel(string Floor)
        {
            switch (Floor)
            {
                case "Floor1":
                    patients= DataProvider.Ins.DB.BENHNHANs.Where(p => p.PHONG.TANG.SOTANG == 1).ToList();
                    break;
                case "Floor2":
                    patients = DataProvider.Ins.DB.BENHNHANs.Where(p => p.PHONG.TANG.SOTANG == 2).ToList();
                    break;
                case "Floor3":
                    patients = DataProvider.Ins.DB.BENHNHANs.Where(p => p.PHONG.TANG.SOTANG == 3).ToList();
                    break;

            }
            //patientList.Add(new Patient() { ten = "Do Phu Quang", tinhtrang = "Tốt" });
            //patientList.Add(new Patient() { ten = "Do Phu Quang", tinhtrang = "Xấu" });
            //patientList.Add(new Patient() { ten = "Do Phu Quang", tinhtrang = "Tốt" });
            //patientList.Add(new Patient() { ten = "Do Phu Quang", tinhtrang = "Tốt" });
            //patientList.Add(new Patient() { ten = "Do Phu Quang", tinhtrang = "Xấu" });
            //patientList.Add(new Patient() { ten = "Do Phu Quang", tinhtrang = "Xấu" });
            //patientList.Add(new Patient() { ten = "Do Phu Quang", tinhtrang = "Tốt" });
            //patientList.Add(new Patient() { ten = "Do Phu Quang", tinhtrang = "Tốt" });
            roomList.Add(new Room() { ten = "Phòng 304 - A18", soluong = 6, soluongmax = 8 });
            roomList.Add(new Room() { ten = "Phòng 304 - A18", soluong = 6, soluongmax = 8 });
            roomList.Add(new Room() { ten = "Phòng 304 - A18", soluong = 8, soluongmax = 8 });
            roomList.Add(new Room() { ten = "Phòng 304 - A18", soluong = 6, soluongmax = 8 });
            roomList.Add(new Room() { ten = "Phòng 304 - A18", soluong = 8, soluongmax = 8 });
            roomList.Add(new Room() { ten = "Phòng 304 - A18", soluong = 6, soluongmax = 8 });
        }
    }
}
