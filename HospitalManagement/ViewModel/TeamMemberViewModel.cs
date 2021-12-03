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
        private List<StaffInformation> staffInformations=new List<StaffInformation>();
        private TO to;
        private BACSI toTruong;
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
        public List<StaffInformation> StaffInformations
        {
            get { return staffInformations; }
            set { staffInformations = value; OnPropertyChanged("StaffInformations"); }
        }
        public TO To { get => to; set => to = value; }
        public BACSI ToTruong { get => toTruong; set => toTruong = value; }
        public ICommand ShowMemberInformation { get; set; }
        public TeamMemberViewModel(int idTo)
        {
            To = DataProvider.Ins.DB.TOes.Find(idTo);
            Nurses = To.YTAs.ToList();
            Doctors = To.BACSIs.ToList();
            StaffInformation staff;
            foreach (BACSI bACSI in Doctors)
            {
                staff = new StaffInformation(bACSI);
                if (staff.PhanLoai == "Tổ Trưởng")
                    ToTruong = bACSI;
                StaffInformations.Add(staff);
            }
            foreach (YTA yTA in Nurses)
            {
                staff = new StaffInformation(yTA);
                StaffInformations.Add(staff);
            }
            ShowMemberInformation = new ShowMembersInformationInTeamCommand(this);
        }
    }
}
