using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.View;
using HospitalManagement.View.StaffRoleView.TeamTask;
using HospitalManagement.ViewModel;
using HospitalManagement.ViewModel.StaffViewViewModel.TeamTask;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace HospitalManagement.Command
{
    internal class SaveChangeTaskInformationCommand : ICommand
    {
        private TaskInformationViewModel taskInformationViewModel;

        public SaveChangeTaskInformationCommand(TaskInformationViewModel taskInformationViewModel)
        {
            this.taskInformationViewModel = taskInformationViewModel;
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
            TaskInformationForm taskForm = parameter as TaskInformationForm;
            if (Check(taskForm))
            {
                NotifyWindow notifyWindow;
                try
                {


                    using (QUANLYBENHVIENEntities dbContext = new QUANLYBENHVIENEntities())
                    {
                        CONGVIEC congviec = dbContext.CONGVIECs.Find(taskInformationViewModel.Congviec.ID);
                        congviec.TIEUDE = taskInformationViewModel.SubjectText;
                        congviec.NOIDUNG = taskInformationViewModel.InfoText;
                        congviec.DIADIEM = taskInformationViewModel.LocationText;
                        DateTime start = GenerateDatetimeFromDateAndHour(taskInformationViewModel.StartDate, taskInformationViewModel.StartHour);
                        congviec.BATDAU = start;
                        DateTime end = GenerateDatetimeFromDateAndHour(taskInformationViewModel.EndDate, taskInformationViewModel.EndHour);
                        congviec.KETTHUC = end;
                        if (DateTime.Compare(start, end) > 0)
                        {
                            notifyWindow = new NotifyWindow("Warning", "Thời điểm bắt đầu phải sớm hơn thời điểm kết thúc!");
                            notifyWindow.ShowDialog();
                            return;
                        }
                        congviec.TINHCHAT = taskInformationViewModel.TaskType;
                        congviec.BACSILIENQUANs.Clear();
                        congviec.YTALIENQUANs.Clear();
                        foreach (StaffInformation staffInformation in taskInformationViewModel.InvolveMembers)
                        {
                            BACSI bs = dbContext.BACSIs.Find(staffInformation.Cmnd_cccd);
                            YTA yta = dbContext.YTAs.Find(staffInformation.Cmnd_cccd);
                            if (bs != null)
                            {
                                BACSILIENQUAN lienquan = new BACSILIENQUAN();
                                lienquan.BACSI = bs;
                                lienquan.CONGVIEC = congviec;
                                lienquan.TIENDO = false;
                                congviec.BACSILIENQUANs.Add(lienquan);
                            }
                            else if (yta != null)
                            {
                                YTALIENQUAN lienquan = new YTALIENQUAN();
                                lienquan.YTA = yta;
                                lienquan.CONGVIEC = congviec;
                                lienquan.TIENDO = false;
                                congviec.YTALIENQUANs.Add(lienquan);
                            }
                        }
                        int toid = ToUtils.GetTOID(MainWindowViewModel.User);
                        congviec.TO = dbContext.TOes.Find(toid);
                        dbContext.SaveChanges();
                        DataProvider.Ins.DB = new QUANLYBENHVIENEntities();
                        notifyWindow = new NotifyWindow("Success", "Sửa thành công");
                        notifyWindow.ShowDialog();
                        if (taskInformationViewModel.Owner != null)
                        {
                            StaffRoleTeamTaskViewModel staffRoleTeamTaskViewModel = taskInformationViewModel.Owner as StaffRoleTeamTaskViewModel;
                            staffRoleTeamTaskViewModel.LoadTaskList();
                        }
                    }
                }
                catch
                {
                    notifyWindow = new NotifyWindow("Danger", "Có lỗi xảy ra");
                    notifyWindow.ShowDialog();
                }
            }
        }
        public bool Check(TaskInformationForm tif)
        {
            if (tif == null) return false;

            if (string.IsNullOrWhiteSpace(tif.txbSubject.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập tiêu đề");
                notifyWindow.ShowDialog();
                tif.txbSubject.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(tif.txbLocation.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập địa điểm");
                notifyWindow.ShowDialog();
                tif.txbLocation.Focus();
                return false;
            }

            if (DateTime.Compare((DateTime)tif.dpStart.SelectedDate, (DateTime)tif.dpEnd.SelectedDate) > 0)
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Thời điểm bắt đầu phải sớm hơn thời\n" + "                  điểm kết thúc");
                notifyWindow.ShowDialog();
                tif.dpStart.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(tif.txbBody.Text))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng nhập nội dung công việc");
                notifyWindow.ShowDialog();
                tif.txbBody.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(tif.cbType.Text.ToString()))
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng chọn mức độ ưu tiên");
                notifyWindow.ShowDialog();
                tif.cbType.Focus();
                return false;
            }

            if (tif.lbxMember.Items.Count == 0)
            {
                NotifyWindow notifyWindow = new NotifyWindow("Warning", "Vui lòng chọn thành viên tham gia");
                notifyWindow.ShowDialog();
                tif.lbxMember.Focus();
                return false;
            }
            return true;
        }

        private DateTime GenerateDatetimeFromDateAndHour(DateTime date, DateTime hour)
        {
            int Year = date.Year;
            int Month = date.Month;
            int Day = date.Day;
            int Hour = hour.Hour;
            int Minute = hour.Minute;
            int Second = hour.Second;
            return new DateTime(Year, Month, Day, Hour, Minute, Second);
        }
    }
}
