using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using HospitalManagement.Model;
using System.ComponentModel;
using HospitalManagement.Command;
using System.Collections.ObjectModel;
using HospitalManagement.Utils;
using HospitalManagement.Command.TeamCommand;

namespace HospitalManagement.ViewModel
{
    class TeamMemberViewModel: BaseViewModel
    {
        private static List<YTA> nurses;
        private static List<BACSI> doctors;
        private List<StaffInformationWithTaskProgress> staffInformations=new List<StaffInformationWithTaskProgress>();
        private TO to;
        private BACSI toTruong;
        private QUANLYBENHVIENEntities dbContext;
        public List<YTA> Nurses
        {
            get { return nurses; }
            set { nurses = value; OnPropertyChanged("Nurses"); }
        }
        public List<BACSI> Doctors
        {
            get { return doctors; }
            set { doctors = value; OnPropertyChanged("Doctors"); }
        }
        public List<StaffInformationWithTaskProgress> StaffInformations
        {
            get { return staffInformations; }
            set { staffInformations = value; OnPropertyChanged("StaffInformations"); }
        }


        public TO To { get => to; set => to = value; }
        public BACSI ToTruong { get => toTruong; set => toTruong = value; }
        public ICommand ShowMemberInformation { get; set; }
        public TeamMemberViewModel(int idTo)
        {
            dbContext = new QUANLYBENHVIENEntities();
            To = dbContext.TOes.Find(idTo);
            Nurses = To.YTAs.ToList();
            Doctors = To.BACSIs.ToList();
            StaffInformationWithTaskProgress staff;
            foreach (BACSI bACSI in Doctors)
            {
                staff = new StaffInformationWithTaskProgress(bACSI);
                if (staff.Value.PhanLoai == "Tổ Trưởng")
                    ToTruong = bACSI;
                StaffInformations.Add(staff);
            }
            foreach (YTA yTA in Nurses)
            {
                staff = new StaffInformationWithTaskProgress(yTA);
                StaffInformations.Add(staff);
            }
            ShowMemberInformation = new ShowMembersInformationInTeamCommand(this);
        }

        public class StaffInformationWithTaskProgress
        {
            private StaffInformation value;

            public StaffInformationWithTaskProgress(BACSI value)
            {
                this.value = new StaffInformation(value);
                this.assignedTask = value.BACSILIENQUANs.Count;
                this.completeTask = value.BACSILIENQUANs.Where(p => p.TIENDO == true).Count();
            }

            public StaffInformationWithTaskProgress(YTA value)
            {
                this.value = new StaffInformation(value);
                this.assignedTask = value.YTALIENQUANs.Count;
                this.completeTask = value.YTALIENQUANs.Where(p => p.TIENDO == true).Count();
            }

            private int completeTask;
            private int assignedTask;

            public StaffInformation Value { get => value; set => this.value = value; }
            public int CompleteTask { get => completeTask; set => completeTask = value; }
            public int AssignedTask { get => assignedTask; set => assignedTask = value; }
        }
    }
}
