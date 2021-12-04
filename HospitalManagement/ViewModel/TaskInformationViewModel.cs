using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HospitalManagement.Model;
using HospitalManagement.Command;

namespace HospitalManagement.ViewModel
{
    class TaskInformationViewModel : INotifyPropertyChanged
    {
        private CONGVIEC task;
        private string doctorNameList = "";
        private string nurseNameList = "";
        public CONGVIEC Task { get => task; set => task = value; }
        public string DoctorNameList { get => doctorNameList; set => doctorNameList = value; }
        public string NurseNameList { get => nurseNameList; set => nurseNameList = value; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        public TaskInformationViewModel(CONGVIEC cv)
        {
            this.Task = cv;
            foreach(BACSILIENQUAN bs in Task.BACSILIENQUANs)
            {
                DoctorNameList += " " + bs.BACSI.HO.ToString() + " " + bs.BACSI.TEN.ToString() + ",";
            }
            if (DoctorNameList.Length > 0)
                DoctorNameList = DoctorNameList.Remove(DoctorNameList.Length - 1, 1);
            foreach (YTALIENQUAN yt in Task.YTALIENQUANs)
            {
                NurseNameList += " " + yt.YTA.HO.ToString() + " " + yt.YTA.TEN.ToString() + ",";
            }
            if (NurseNameList.Length > 0)
                NurseNameList = NurseNameList.Remove(NurseNameList.Length - 1, 1);
        }
    }
}

