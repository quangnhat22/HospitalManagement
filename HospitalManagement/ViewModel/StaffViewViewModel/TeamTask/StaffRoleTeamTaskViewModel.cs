using GongSolutions.Wpf.DragDrop;
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
    internal class StaffRoleTeamTaskViewModel : BaseViewModel, IDropTarget
    {
        private ObservableCollection<ProgressTask> deleteTasks;

        private ObservableCollection<ProgressTask> progressTasks;
        private DateTime selectedDate;
        public ICommand OpenAddToDoFormCommand { get; set; }
        public ICommand ShowTaskInformation { get; set; }
        public ICommand DeleteTasksCommand { get; set; }
        public ICommand LoadTaskListByDateCommand { get; set; }
        public ICommand ExportTaskToExcelCommand { get; set; }
        public ICommand CheckToggleButtonTaskCommand { get; set; }
        public ObservableCollection<ProgressTask> DeleteTasks
        {
            get
            {
                return deleteTasks;
            }
            set
            {
                deleteTasks = value;
                OnPropertyChanged("DeleteTasks");
            }
        }

        public ObservableCollection<ProgressTask> ProgressTasks 
        {
            get 
            {
                return progressTasks;
            }
            set
            {
                progressTasks = value;
                OnPropertyChanged("ProgressTasks");
            }
                }

        public USER User = MainWindowViewModel.User;
        public Visibility LeaderTaskVisibility { get; set; }
        public DateTime SelectedDate { get => selectedDate; set => selectedDate = value; }

        public StaffRoleTeamTaskViewModel()
        {
            SelectedDate = System.DateTime.Now;
            OpenAddToDoFormCommand = new OpenAddToDoFormCommand(this);
            ShowTaskInformation = new ShowTaskInformationCommand(this);
            DeleteTasksCommand = new DeleteTasksCommand(this);
            ExportTaskToExcelCommand = new ExportTaskToExcelCommand(this);
            LoadTaskListByDateCommand = new LoadTaskListByDateCommand(this);
            CheckToggleButtonTaskCommand = new CheckToggleButtonTaskCommand(this)
;            LoadTaskList();
            if (User.ROLE == "leader")
                LeaderTaskVisibility = Visibility.Visible;
            else
                LeaderTaskVisibility = Visibility.Hidden;
        }

        public void LoadTaskList()
        {

            int toid = ToUtils.GetTOID(MainWindowViewModel.User);
            if(toid != -1)
            {
                TO to = DataProvider.Ins.DB.TOes.Find(toid);
                string query = @"
                            SELECT * FROM CONGVIEC 
                            WHERE IDTO = {0} 
                            AND CONGVIEC.BATDAU <= '{1}-{2}-{3}'
                            AND CONGVIEC.KETTHUC >= '{1}-{2}-{3}'";
                query = String.Format(query, toid, SelectedDate.Year, SelectedDate.Month, SelectedDate.Day);
                List<CONGVIEC> congviecs = DataProvider.Ins.DB.Database.SqlQuery<CONGVIEC>(query).ToList();
                List<ProgressTask> progressTasksList = ProgressTask.ChangeToListProgressTask(congviecs);
                ProgressTasks = new ObservableCollection<ProgressTask>(progressTasksList);
                DeleteTasks = new ObservableCollection<ProgressTask>();
            }
        }

        void IDropTarget.DragOver(IDropInfo dropInfo)
        {

            dropInfo.Effects = DragDropEffects.Move;
            dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
        }

        void IDropTarget.Drop(IDropInfo dropInfo)
        {
            if(dropInfo.TargetCollection != dropInfo.DragInfo.SourceCollection)
            {
                if (dropInfo.TargetCollection == DeleteTasks)
                {
                    ProgressTask p = (ProgressTask)dropInfo.Data;
                    DeleteTasks.Add(p);
                    var needRemove = ProgressTasks.SingleOrDefault<ProgressTask>(ptemp => ptemp.Value.ID == p.Value.ID);
                    ProgressTasks.Remove(needRemove);
                }
                else if (dropInfo.TargetCollection == ProgressTasks)
                {
                    ProgressTask p = (ProgressTask)dropInfo.Data;
                    var needRemove = DeleteTasks.SingleOrDefault<ProgressTask>(ptemp => ptemp.Value.ID == p.Value.ID);
                    DeleteTasks.Remove(needRemove);
                    ProgressTasks.Add(p);
                }
            }
        }
    }

    public class ProgressTask
    {
        private CONGVIEC value;


        public CONGVIEC Value { get => value; set => this.value = value; }
        public int NumberCompletedPeople { get => CaculateNumberCompletedPeople(); }
        public int NumberInvolvePeople { get => CaculateNumberInvolvePeople(); }

        public bool IsCurrentUserComplete { get => isCurrentUserComplete(); }
        public ProgressTask(CONGVIEC value)
        {
            Value = value;
        }

        private int CaculateNumberCompletedPeople()
        {
            if (Value != null)
            {
                return Value.BACSILIENQUANs.Where(p => p.TIENDO == true).Count() +
                                        Value.YTALIENQUANs.Where(p => p.TIENDO == true).Count();
            }
            return 0;
        }

        private int CaculateNumberInvolvePeople()
        {
            if (Value != null)
            {
                return Value.BACSILIENQUANs.Count + Value.YTALIENQUANs.Count;
            }
            return 0;
        }

        private bool isCurrentUserComplete()
        {
            try
            {
                using (QUANLYBENHVIENEntities dbContext = new QUANLYBENHVIENEntities())
                {
                    if (MainWindowViewModel.User.ROLE == "leader" || MainWindowViewModel.User.ROLE == "doctor")
                    {
                        BACSI bs = MainWindowViewModel.User?.BACSIs.FirstOrDefault();
                        if (bs != null && bs != default(BACSI))
                        {
                            BACSILIENQUAN bslq = dbContext.BACSILIENQUANs.Find(Value.ID, bs.CMND_CCCD);
                            if (bslq != null)
                                return bslq.TIENDO.HasValue ? bslq.TIENDO.Value : false;
                        }
                    }
                    else if (MainWindowViewModel.User?.ROLE == "nurse")
                    {
                        YTA yta = MainWindowViewModel.User?.YTAs.FirstOrDefault();
                        if (yta != null && yta != default(YTA))
                        {
                            YTALIENQUAN ytlq = dbContext.YTALIENQUANs.Find(Value.ID, MainWindowViewModel.User?.ID);
                            if (ytlq != null)
                                return ytlq.TIENDO.HasValue ? ytlq.TIENDO.Value : false;
                        }
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static List<ProgressTask> ChangeToListProgressTask(List<CONGVIEC> congviecs)
        {
            if (congviecs != null)
                return congviecs.ConvertAll<ProgressTask>(p => new ProgressTask(p));
            return null;
        }
    }
}
