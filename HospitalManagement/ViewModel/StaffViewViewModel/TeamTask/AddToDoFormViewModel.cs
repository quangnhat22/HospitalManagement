using GongSolutions.Wpf.DragDrop;
using HospitalManagement.Command.TeamCommand;
using HospitalManagement.Command.TeamTaskCommand;
using HospitalManagement.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HospitalManagement.ViewModel.StaffViewViewModel.TeamTask
{
    class AddToDoFormViewModel : BaseViewModel, IDropTarget
    {
        private string subjectText;
        private string infoText;
        private string locationText;
        private DateTime startDate;
        private DateTime startHour;
        private DateTime endDate;
        private DateTime endHour;
        private ObservableCollection<StaffInformation> members;
        private ObservableCollection<StaffInformation> involveMembers;

        #region "prop"
        public ObservableCollection<StaffInformation> Members
        {
            get { return members; }
            set { members = value; OnPropertyChanged("Members"); }
        }
        public ObservableCollection<StaffInformation> InvolveMembers { get => involveMembers; set => involveMembers = value; }
        public ICommand ShowMembersInformationInTeamCommand { get; set; }
        public ICommand ShowMembersInTeamCommand { get; set; }
        public ICommand RemoveInvolveMembersCommand { get; set; }
        public ICommand AddTaskCommand { get; set; }
        public string SubjectText { get => subjectText; set => subjectText = value; }
        public string InfoText { get => infoText; set => infoText = value; }
        public string LocationText { get => locationText; set => locationText = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }

        public DateTime StartHour { get => startHour; set => startHour = value; }
        public DateTime EndHour { get => endHour; set => endHour = value; }
        #endregion

        public AddToDoFormViewModel()
        {
            InvolveMembers = new ObservableCollection<StaffInformation>();
            AddTaskCommand = new AddTaskCommand(this);
            RemoveInvolveMembersCommand = new RemoveInvolveMembersCommand(this);
            StartDate = EndDate = DateTime.Now;
            ShowMembersInTeamCommand = new ShowMembersInTeamCommand(this);
            ShowMembersInTeamCommand.Execute(null);
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
            if (dropInfo.TargetCollection == InvolveMembers)
            {
                StaffInformation p = (StaffInformation)dropInfo.Data;
                if(!InvolveMembers.Contains(p))
                {
                    InvolveMembers.Add(p);
                }
            }
        }
    }
}
