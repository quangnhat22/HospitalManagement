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
using HospitalManagement.ViewModel.StaffViewViewModel.TeamTask;

namespace HospitalManagement.ViewModel
{
    class TaskInformationViewModel : INotifyPropertyChanged, IDropTarget
    {
        private string subjectText;
        private string infoText;
        private string locationText;
        private DateTime startDate;
        private DateTime startHour;
        private DateTime endDate;
        private DateTime endHour;
        private StaffRoleTeamTaskViewModel owner;
        private List<string> taskTypes = new List<string> { "Bình thường", "Ưu tiên", "Nguy cấp" };
        private string taskType;
        private ObservableCollection<StaffInformation> members = new ObservableCollection<StaffInformation>();
        private ObservableCollection<StaffInformation> involveMembers = new ObservableCollection<StaffInformation>();
        private QUANLYBENHVIENEntities dbContext;
        private CONGVIEC congviec;
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        public ICommand SaveChange { get; set; }
        public string SubjectText { get => subjectText; set => subjectText = value; }
        public string InfoText { get => infoText; set => infoText = value; }
        public string LocationText { get => locationText; set => locationText = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime StartHour { get => startHour; set => startHour = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }
        public DateTime EndHour { get => endHour; set => endHour = value; }
        internal StaffRoleTeamTaskViewModel Owner { get => owner; set => owner = value; }
        public List<string> TaskTypes { get => taskTypes; set => taskTypes = value; }
        public string TaskType
        {
            get => taskType;
            set
            {
                taskType = value;
                OnPropertyChanged("TaskType");
            }
        }
        public QUANLYBENHVIENEntities DbContext { get => dbContext; set => dbContext = value; }
        public CONGVIEC Congviec { get => congviec; set => congviec = value; }

        public TaskInformationViewModel(int IDCONGVIEC)
        {
            dbContext = new QUANLYBENHVIENEntities();
            this.Congviec = dbContext.CONGVIECs.Find(IDCONGVIEC);
            LoadInfo(IDCONGVIEC);
            SaveChange = new SaveChangeTaskInformationCommand(this);
        }

        private void LoadInfo(int IDCONGVIEC)
        {
            using (QUANLYBENHVIENEntities context = new QUANLYBENHVIENEntities())
            {
                CONGVIEC cv = context.CONGVIECs.Find(IDCONGVIEC);
                SubjectText = cv.TIEUDE;
                StartDate = StartHour = cv.BATDAU.HasValue ?  cv.BATDAU.Value : DateTime.Now;
                EndDate = EndHour = cv.KETTHUC.HasValue ?  cv.KETTHUC.Value : DateTime.Now;
                LocationText = cv.DIADIEM;
                InfoText = cv.NOIDUNG;
                TaskType = cv.TINHCHAT;
                foreach(BACSI bs in cv.TO.BACSIs)
                {
                    Members.Add(new StaffInformation(bs));
                }
                foreach (YTA yta in cv.TO.YTAs)
                {
                    Members.Add(new StaffInformation(yta));
                }
                foreach(BACSILIENQUAN bslq in cv.BACSILIENQUANs)
                {
                    StaffInformation staff = Members.Where(p => p.Cmnd_cccd == bslq.CMND_CCCD).FirstOrDefault();
                    if(staff != null && staff != default(StaffInformation))
                    {
                        Members.Remove(staff);
                        InvolveMembers.Add(staff);
                    }
                }
                foreach (YTALIENQUAN ytlq  in cv.YTALIENQUANs)
                {
                    StaffInformation staff = Members.Where(p => p.Cmnd_cccd == ytlq.CMND_CCCD).FirstOrDefault();
                    if (staff != null && staff != default(StaffInformation))
                    {
                        Members.Remove(staff);
                        InvolveMembers.Add(staff);
                    }
                }
            }
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

