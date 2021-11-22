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
using GongSolutions.Wpf.DragDrop;
using GongSolutions.Wpf.DragDrop.Utilities;
using System.Collections;

namespace HospitalManagement.ViewModel
{
    class RoomViewModel : BaseViewModel, IDropTarget
    { 
        private int? floorNumber;
        private int currentBuilding;
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

        private List<BENHNHAN> patients;
        private List<BENHNHAN> roomPatients;
        private List<PHONG> rooms;
        private List<Phong> roomsExtended;
        public ICommand ShowPatientsInRoom { get; set; }
        public ICommand ShowPatientsInformationInRoomCommand { get; set; }
        public ICommand RoomProgressBarCommand { get; set; }

        public int? FloorNumber
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

        public int CurrentBuilding { get => currentBuilding; set => currentBuilding = value; }

        public RoomViewModel(int currentBuilding, int? Floor)
        {
            patients = DataProvider.Ins.DB.BENHNHANs.Where(p => (p.PHONG.TANG.TOA.IDTOA == currentBuilding && p.PHONG.TANG.SOTANG == Floor)).ToList();
            roomPatients = patients;
            rooms = DataProvider.Ins.DB.PHONGs.Where(p => (p.TANG.TOA.IDTOA == currentBuilding && p.TANG.SOTANG == Floor)).ToList();
            currentRoom = "Tầng " + Floor.ToString();
            floorNumber =(int) Floor;
            this.currentBuilding = currentBuilding;

            ShowPatientsInRoom = new ShowPatientsInRoomCommand(this);
            ShowPatientsInformationInRoomCommand = new ShowPatientsInformationInRoomCommand(this);
            RoomProgressBarCommand = new RoomProgressBarCommand();

            roomsExtended = rooms.ConvertAll(x => new Phong { ID = x.ID, IDTANG = x.IDTANG, SOPHONG = x.SOPHONG, TANG = x.TANG, SUCCHUA = x.SUCCHUA, GHICHU = x.GHICHU, COUNT=x.BENHNHANs.Count});
        }

        void IDropTarget.DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.Data is BENHNHAN && (dropInfo.TargetItem is Phong))
            {
                dropInfo.Effects = DragDropEffects.Move;
                dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
            }
        }

        void IDropTarget.Drop(IDropInfo dropInfo)
        {
            if(dropInfo.TargetItem is Phong)
            {
                Phong p = (Phong)dropInfo.TargetItem;
                BENHNHAN benhnhan = (BENHNHAN)dropInfo.Data;
                PHONG oldp = benhnhan.PHONG;
                benhnhan.IDPHONG = p.ID;
                DataProvider.Ins.DB.SaveChanges();
                UpdateView();
                if (CheckSameDataContext(dropInfo))
                {
                    ShowPatientsInRoom.Execute(oldp.ID);
                }
                else
                {
                    ShowPatientsInRoom.Execute(p.ID);
                    DataGrid dataGrid = dropInfo.DragInfo.VisualSource as DataGrid;
                    RoomViewModel oldRoomViewModel = dataGrid.DataContext as RoomViewModel;
                    oldRoomViewModel.UpdateView();
                    oldRoomViewModel.ShowPatientsInRoom.Execute(oldp.ID);
                }
            }
            else if (dropInfo.TargetItem is BENHNHAN)
            {

            }
        }

        private bool CheckSameDataContext(IDropInfo dropInfo)
        {
            DataGrid dataGrid = dropInfo.DragInfo.VisualSource as DataGrid;
            RoomViewModel oldRoomViewModel = dataGrid.DataContext as RoomViewModel;
            return oldRoomViewModel.Equals(this);
        }

        public void UpdateView()
        {
            rooms = DataProvider.Ins.DB.PHONGs.Where(p => (p.TANG.TOA.IDTOA == CurrentBuilding && p.TANG.SOTANG == floorNumber)).ToList();
            RoomsExtended = rooms.ConvertAll(x => new Phong { ID = x.ID, IDTANG = x.IDTANG, SOPHONG = x.SOPHONG, TANG = x.TANG, SUCCHUA = x.SUCCHUA, GHICHU = x.GHICHU, COUNT = x.BENHNHANs.Count });
        }
    }
}
