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

namespace HospitalManagement.ViewModel
{
    class TeamMemberViewModel: BaseViewModel
    {
        private static List<YTA> nurses;
        private static List<BACSI> doctors;
        private List<StaffInformation> staffInformations=new List<StaffInformation>();
        private static ObservableCollection<SelectableItem<StaffInformation>> members;
        private TO to;
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
        public ObservableCollection<SelectableItem<StaffInformation>> Members
        {
            get { return members; }
            set { members = value; OnPropertyChanged("Members"); }
        }
        public TO To { get => to; set => to = value; }
        public TeamMemberViewModel(int idTo)
        {
            To = DataProvider.Ins.DB.TOes.Find(idTo);
            Nurses = To.YTAs.ToList();
            Doctors = To.BACSIs.ToList();
            StaffInformation staff;
            foreach(YTA yTA in Nurses)
            {
                staff = new StaffInformation(yTA);
                StaffInformations.Add(staff);
            }
            foreach (BACSI bACSI in Doctors)
            {
                staff = new StaffInformation(bACSI);
                StaffInformations.Add(staff);
            }
            Members= SelectableItem<StaffInformation>.GetSelectableItems(StaffInformations);
        }
    }
}
