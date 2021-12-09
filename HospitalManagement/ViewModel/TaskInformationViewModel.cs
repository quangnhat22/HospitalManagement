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

namespace HospitalManagement.ViewModel
{
    class TaskInformationViewModel : INotifyPropertyChanged
    {
        private CONGVIEC task;
        private DateTime startDate;
        private DateTime startHour;
        private DateTime endDate;
        private DateTime endHour;
        private ObservableCollection<StaffInformation> involveMembers = new ObservableCollection<StaffInformation>();
        public ObservableCollection<StaffInformation> InvolveMembers { get => involveMembers; set => involveMembers = value; }
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
            this.Task = cv;
            DateTime StartTime = Task.BATDAU.Value;
            DateTime EndTime = Task.KETTHUC.Value;
            StartDate = new DateTime(StartTime.Year, StartTime.Month, StartTime.Day);
            EndDate = new DateTime(EndTime.Year, EndTime.Month, EndTime.Day);
            StaffInformation staffInformation;
            foreach (BACSILIENQUAN bs in Task.BACSILIENQUANs)
            {
                staffInformation = new StaffInformation(bs.BACSI);
                InvolveMembers.Add(staffInformation);
            }
            foreach (YTALIENQUAN yt in Task.YTALIENQUANs)
            {
                staffInformation = new StaffInformation(yt.YTA);
                InvolveMembers.Add(staffInformation);
            }
            SaveChange = new SaveChangeTaskInformationCommand();
        }
    }
}

