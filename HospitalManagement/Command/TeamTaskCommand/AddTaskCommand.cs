using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.ViewModel.StaffViewViewModel.TeamTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.Command.TeamTaskCommand
{
    class AddTaskCommand : ICommand
    {
        private AddToDoFormViewModel addToDoFormViewModel;

        public AddTaskCommand(AddToDoFormViewModel addToDoFormViewModel)
        {
            this.addToDoFormViewModel = addToDoFormViewModel;
        }

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            CONGVIEC congviec = new CONGVIEC();
            congviec.TIEUDE = addToDoFormViewModel.SubjectText;
            congviec.NOIDUNG = addToDoFormViewModel.InfoText;
            congviec.DIADIEM = addToDoFormViewModel.LocationText;
            congviec.BATDAU = GenerateDatetimeFromDateAndHour(addToDoFormViewModel.StartDate, addToDoFormViewModel.StartHour);
            congviec.KETTHUC = GenerateDatetimeFromDateAndHour(addToDoFormViewModel.EndDate, addToDoFormViewModel.EndHour);
            //congviec.TINHCHAT = null;
            foreach(StaffInformation staffInformation in addToDoFormViewModel.InvolveMembers)
            {
                BACSI bs = DataProvider.Ins.DB.BACSIs.Find(staffInformation.Cmnd_cccd);
                YTA yta = DataProvider.Ins.DB.YTAs.Find(staffInformation.Cmnd_cccd);
                if(bs != null)
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
            DataProvider.Ins.DB.CONGVIECs.Add(congviec);
            DataProvider.Ins.DB.SaveChanges();
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
