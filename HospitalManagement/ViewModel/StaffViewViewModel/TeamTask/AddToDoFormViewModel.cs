using HospitalManagement.Command.TeamCommand;
using HospitalManagement.Command.TeamTaskCommand;
using HospitalManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.ViewModel.StaffViewViewModel.TeamTask
{
    class AddToDoFormViewModel : BaseViewModel
    {
        private string subjectText;
        private string infoText;
        private string locationText;
        private DateTime startDate;
        private DateTime endDate;
        private string currentTeam;
        private static List<StaffInformation> members;

        #region "prop"
        public string CurrentTeam
        {
            get { return currentTeam; }
            set
            {
                currentTeam = value; OnPropertyChanged("CurrentTeam");
            }
        }
        public List<StaffInformation> Members
        {
            get { return members; }
            set { members = value; OnPropertyChanged("Members"); }
        }
        public ICommand ShowMembersInformationInTeamCommand { get; set; }
        public ICommand ShowMembersInTeamCommand { get; set; }
        public string SubjectText { get => subjectText; set => subjectText = value; }
        public string InfoText { get => infoText; set => infoText = value; }
        public string LocationText { get => locationText; set => locationText = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }

        public ICommand AddTaskCommand { get; set; }
        #endregion

        public AddToDoFormViewModel()
        {
            AddTaskCommand = new AddTaskCommand(this);
            StartDate = EndDate = DateTime.Now;
            currentTeam = "";
            ShowMembersInTeamCommand = new ShowMembersInTeamCommand(this);
        }
    }
}
