using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HospitalManagement.Model;
using HospitalManagement.Command;
using HospitalManagement.Utils;
using System.Collections.ObjectModel;
using GongSolutions.Wpf.DragDrop;
using System.Windows;

namespace HospitalManagement.ViewModel
{
    class TaskInformationViewModel : INotifyPropertyChanged, IDropTarget
    {
        private CONGVIEC task;
        private DateTime startDate;
        private DateTime startHour;
        private DateTime endDate;
        private DateTime endHour;
        private ObservableCollection<StaffInformation> members = new ObservableCollection<StaffInformation>();
        private ObservableCollection<StaffInformation> involveMembers = new ObservableCollection<StaffInformation>();
        public ObservableCollection<StaffInformation> InvolveMembers
        {
            get => involveMembers;
            set
            {
                involveMembers = value;
                OnPropertyChanged("InvolveMembers");
            }
        }
        public ObservableCollection<StaffInformation> Members
        {
            get => members;
            set
            {
                members = value;
                OnPropertyChanged("Members");
            }
        }
        private List<string> taskTypes = new List<string> { "Bình thường", "Ưu tiên", "Nguy cấp" };
        public CONGVIEC Task { get => task; set => task = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }
        public DateTime StartHour { get => startHour; set => startHour = value; }
        public DateTime EndHour { get => endHour; set => endHour = value; }
        public List<string> TaskTypes { get => taskTypes; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        public ICommand SaveChange { get; set; }


        public TaskInformationViewModel(CONGVIEC cv)
        {
            this.Task = DataProvider.Ins.DB.CONGVIECs.Find(cv.ID);
            int idTo = ToUtils.GetTOID(MainWindowViewModel.User);
            TO to = DataProvider.Ins.DB.TOes.Find(idTo);
            foreach(BACSI bs in to.BACSIs)
            {
                StaffInformation staff = new StaffInformation(bs);
                Members.Add(staff);
            }
            foreach (YTA yta in to.YTAs)
            {
                StaffInformation staff = new StaffInformation(yta);
                Members.Add(staff);
            }
            DateTime StartTime = Task.BATDAU.Value;
            DateTime EndTime = Task.KETTHUC.Value;
            StartDate = new DateTime(StartTime.Year, StartTime.Month, StartTime.Day);
            EndDate = new DateTime(EndTime.Year, EndTime.Month, EndTime.Day);
            StaffInformation staffInformation;
            foreach (BACSILIENQUAN bs in Task.BACSILIENQUANs)
            {
                staffInformation = new StaffInformation(bs.BACSI);
                InvolveMembers.Add(staffInformation);
                StaffInformation member = Members.Where(p => p.Cmnd_cccd == staffInformation.Cmnd_cccd).FirstOrDefault();
                if(member != null || member != default(StaffInformation))
                    Members.Remove(member);
            }
            foreach (YTALIENQUAN yt in Task.YTALIENQUANs)
            {
                staffInformation = new StaffInformation(yt.YTA);
                InvolveMembers.Add(staffInformation);
                StaffInformation member = Members.Where(p => p.Cmnd_cccd == staffInformation.Cmnd_cccd).FirstOrDefault();
                if (member != null || member != default(StaffInformation))
                    Members.Remove(member);
            }
            SaveChange = new SaveChangeTaskInformationCommand();
        }

        void IDropTarget.DragOver(IDropInfo dropInfo)
        {

            if (dropInfo.Data is StaffInformation && dropInfo.TargetCollection is ObservableCollection<StaffInformation>)
            {
                dropInfo.Effects = DragDropEffects.Move;
                dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
            }
        }

        void IDropTarget.Drop(IDropInfo dropInfo)
        {
            if (dropInfo.TargetCollection != dropInfo.DragInfo.SourceCollection)
            {
                if (dropInfo.TargetCollection == InvolveMembers)
                {
                    StaffInformation p = (StaffInformation)dropInfo.Data;
                    InvolveMembers.Add(p);
                    Members.Remove(p);
                }
                else if (dropInfo.TargetCollection == Members)
                {
                    StaffInformation p = (StaffInformation)dropInfo.Data;
                    Members.Add(p);
                    InvolveMembers.Remove(p);
                }
            }
        }
    }
}

