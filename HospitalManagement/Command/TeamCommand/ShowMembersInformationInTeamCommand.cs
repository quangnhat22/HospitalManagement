using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using HospitalManagement.Model;
using HospitalManagement.ViewModel;
using HospitalManagement.Utils;
using HospitalManagement.View;
using HospitalManagement.View.Staff;
using HospitalManagement.ViewModel.StaffViewViewModel.TeamTask;
using static HospitalManagement.ViewModel.TeamMemberViewModel;

namespace HospitalManagement.Command.TeamCommand
{
    class ShowMembersInformationInTeamCommand : ICommand
    {
        private TeamViewmodel teamViewmodel;
        private AddToDoFormViewModel addToDoFormViewModel;
        private TeamMemberViewModel teamMemberViewModel;
        private bool isTeamMemberCalled=false;

        public ShowMembersInformationInTeamCommand(TeamViewmodel teamViewmodel)
        {
            this.teamViewmodel = teamViewmodel;
        }

        public ShowMembersInformationInTeamCommand(AddToDoFormViewModel addToDoFormViewModel)
        {
            this.addToDoFormViewModel = addToDoFormViewModel;
        }
        public ShowMembersInformationInTeamCommand(TeamMemberViewModel teamMemberViewModel)
        {
            this.teamMemberViewModel = teamMemberViewModel;
            isTeamMemberCalled = true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            int index = (int)parameter;
            
            if (isTeamMemberCalled)
            {
                StaffInformationWithTaskProgress member;
                member = teamMemberViewModel.StaffInformations[index];
                if (member.Value.PhanLoai == "Tổ Trưởng" || member.Value.PhanLoai == "Bác Sĩ")
                {
                    BACSI bs = DataProvider.Ins.DB.BACSIs.Find(member.Value.Cmnd_cccd);
                    DoctorInformationForm dif = new DoctorInformationForm(bs);
                    dif.Show();
                }
                else
                {
                    YTA yta = DataProvider.Ins.DB.YTAs.Find(member.Value.Cmnd_cccd);
                    NurseInformationForm nif = new NurseInformationForm(yta);
                    nif.Show();
                }
            }
            else
            {
                StaffInformation member;
                member = teamViewmodel.Members[index];
                if (member.PhanLoai == "Tổ Trưởng" || member.PhanLoai == "Bác Sĩ")
                {
                    BACSI bs = DataProvider.Ins.DB.BACSIs.Find(member.Cmnd_cccd);
                    DoctorInformationForm dif = new DoctorInformationForm(bs);
                    dif.Show();
                }
                else
                {
                    YTA yta = DataProvider.Ins.DB.YTAs.Find(member.Cmnd_cccd);
                    NurseInformationForm nif = new NurseInformationForm(yta);
                    nif.Show();
                }
            }
        }
    }
}
