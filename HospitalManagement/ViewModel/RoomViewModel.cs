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
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace HospitalManagement.ViewModel
{
    class RoomViewModel : INotifyPropertyChanged, IDropTarget
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<BENHNHAN> patients;
        private ObservableCollection<CountedRoom> countedRooms;
        private TANG tang;
        private string currentRoom;


        public ICommand ShowPatientsInRoom { get; set; }
        public ICommand ShowPatientsInformationInRoomCommand { get; set; }
        public ICommand RoomProgressBarCommand { get; set; }


        public ObservableCollection<BENHNHAN> Patients
        {
            get { return patients; }
            set { patients = value; OnPropertyChanged("Patients"); }
        }

        public ObservableCollection<CountedRoom> CountedRooms
        {
            get { return countedRooms; }
            set { 
                countedRooms = value; 
                OnPropertyChanged("CountedRooms"); 
            }
        }

        public string CurrentRoom
        {
            get => currentRoom;
            set
            {
                currentRoom = value;
                OnPropertyChanged("CurrentRoom");
            }
        }
        public TANG Tang { get => tang; set => tang = value; }
        

        public RoomViewModel(int idTang)
        {
            ShowPatientsInRoom = new ShowPatientsInRoomCommand(this);
            ShowPatientsInformationInRoomCommand = new ShowPatientsInformationInRoomCommand(this);
            RoomProgressBarCommand = new RoomProgressBarCommand();
            Tang = DataProvider.Ins.DB.TANGs.Find(idTang);
            CountedRooms = CountedRoom.GetCountedRooms(Tang.PHONGs.ToList());
            CurrentRoom = "Tầng " + Tang.SOTANG;
        }

        void IDropTarget.DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.Data is BENHNHAN && (dropInfo.TargetItem is CountedRoom))
            {
                dropInfo.Effects = DragDropEffects.Move;
                dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
            }
        }

        void IDropTarget.Drop(IDropInfo dropInfo)
        {
            if (dropInfo.TargetItem is CountedRoom)
            {
                CountedRoom p = (CountedRoom)dropInfo.TargetItem;
                BENHNHAN benhnhan = (BENHNHAN)dropInfo.Data;
                PHONG oldp = benhnhan.PHONG;
                benhnhan.IDPHONG = p.Value.ID;
                DataProvider.Ins.DB.SaveChanges();
                UpdateView();
                if (CheckSameDataContext(dropInfo))
                {
                    ShowPatientsInRoom.Execute(oldp.ID);
                }
                else
                {
                    ShowPatientsInRoom.Execute(p.Value.ID);
                    DataGrid dataGrid = dropInfo.DragInfo.VisualSource as DataGrid;
                    RoomViewModel oldRoomViewModel = dataGrid.DataContext as RoomViewModel;
                    oldRoomViewModel.UpdateView();
                    oldRoomViewModel.ShowPatientsInRoom.Execute(oldp.ID);
                }
            }
            else if (dropInfo.TargetItem is BENHNHAN)
            {
                ;
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
            CountedRooms = CountedRoom.GetCountedRooms(Tang.PHONGs.ToList());
        }
    }

    public class CountedRoom : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private int count;
        private PHONG value;

        public int Count
        {
            get { return count; }
            set { count = value; OnPropertyChanged("Count"); }
        }

        public PHONG Value { get => value; set => this.value = value; }

        public CountedRoom(PHONG p)
        {
            Value = p;
            Count = p.BENHNHANs.Count;
        }

        public static ObservableCollection<CountedRoom> GetCountedRooms(List<PHONG> ls)
        {
            ObservableCollection<CountedRoom> countedRooms = new ObservableCollection<CountedRoom>();
            foreach (PHONG p in ls)
            {
                CountedRoom countedRoom = new CountedRoom(p);
                countedRooms.Add(countedRoom);
            }
            return countedRooms;
        }
    }
}
