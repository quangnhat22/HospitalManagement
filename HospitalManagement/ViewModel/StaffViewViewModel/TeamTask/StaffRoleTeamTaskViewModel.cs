using HospitalManagement.Command;
using HospitalManagement.Command.TeamCommand;
using HospitalManagement.Command.TeamTaskCommand;
using HospitalManagement.Model;
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
    internal class StaffRoleTeamTaskViewModel : BaseViewModel
    {
        private ObservableCollection<ProgressTask> progressTasks;
        public ICommand OpenAddToDoFormCommand { get; set; }
        public ICommand ShowTaskInformation { get; set; }
        public ObservableCollection<ProgressTask> ProgressTasks { get => progressTasks; set => progressTasks = value; }

        public USER User = MainWindowViewModel.User;
        public Visibility LeaderTaskVisibility { get; set; }

        public StaffRoleTeamTaskViewModel()
        {
            OpenAddToDoFormCommand = new OpenAddToDoFormCommand(this);
            ShowTaskInformation = new ShowTaskInformationCommand(this);
            LoadTaskList();
            if (User.ROLE == "leader")
                LeaderTaskVisibility = Visibility.Visible;
            else
                LeaderTaskVisibility = Visibility.Hidden;
        }

        public void LoadTaskList()
        {
            List<CONGVIEC> congviecs = DataProvider.Ins.DB.CONGVIECs.ToList();
            List<ProgressTask> progressTasksList = ProgressTask.ChangeToListProgressTask(congviecs);
            ProgressTasks = new ObservableCollection<ProgressTask>(progressTasksList);
        }
    }

    public class ProgressTask
    {
        private CONGVIEC value;


        public CONGVIEC Value { get => value; set => this.value = value; }
        public int NumberCompletedPeople { get => CaculateNumberCompletedPeople(); }
        public int NumberInvolvePeople { get => CaculateNumberInvolvePeople(); }
        public ProgressTask(CONGVIEC value)
        {
            Value = value;
        }

        private int CaculateNumberCompletedPeople()
        {
            if(Value != null)
            {
                return Value.BACSILIENQUANs.Where(p => p.TIENDO == true).Count() +
                                        Value.YTALIENQUANs.Where(p => p.TIENDO == true).Count();
            }
            return 0;
        }

        private int CaculateNumberInvolvePeople()
        {
            if(Value != null)
            {
                return Value.BACSILIENQUANs.Count + Value.YTALIENQUANs.Count;
            }
            return 0;
        }

        public static List<ProgressTask> ChangeToListProgressTask(List<CONGVIEC> congviecs)
        {
            if (congviecs != null)
                return congviecs.ConvertAll<ProgressTask>(p => new ProgressTask(p));
            return null;
        }
    }
}
