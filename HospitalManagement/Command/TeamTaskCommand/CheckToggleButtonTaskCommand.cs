using HospitalManagement.Model;
using HospitalManagement.ViewModel;
using HospitalManagement.ViewModel.StaffViewViewModel.TeamTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.Command.TeamTaskCommand
{
    class CheckToggleButtonTaskCommand : ICommand
    {
        private StaffRoleTeamTaskViewModel staffRoleTeamTaskViewModel;
        private static QUANLYBENHVIENEntities dbContext = new QUANLYBENHVIENEntities();

        public CheckToggleButtonTaskCommand(StaffRoleTeamTaskViewModel staffRoleTeamTaskViewModel)
        {
            this.staffRoleTeamTaskViewModel = staffRoleTeamTaskViewModel;
        }

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public bool CanExecute(object parameter)
        {
            if (parameter == null)
                return false;
            int IDCONGVIEC = int.Parse(parameter.ToString());
            CONGVIEC congviec = dbContext.CONGVIECs.Find(IDCONGVIEC);
            if (MainWindowViewModel.User.ROLE == "leader" || MainWindowViewModel.User.ROLE == "doctor")
            {
                BACSI bs = MainWindowViewModel.User?.BACSIs.FirstOrDefault();
                if (bs != null && bs != default(BACSI))
                {
                    BACSILIENQUAN bslq = dbContext.BACSILIENQUANs.Find(congviec.ID, bs.CMND_CCCD);
                    return bslq != null;
                }
            }
            else if (MainWindowViewModel.User?.ROLE == "nurse")
            {
                YTA yta = MainWindowViewModel.User?.YTAs.FirstOrDefault();
                if (yta != null && yta != default(YTA))
                {
                    YTALIENQUAN ytlq = dbContext.YTALIENQUANs.Find(congviec.ID, yta.CMND_CCCD);
                    return ytlq != null;
                }
                return false;
            }
            return false;
        }

        public void Execute(object parameter)
        {
            if (parameter == null)
                return;
            int IDCONGVIEC = int.Parse(parameter.ToString());
            CONGVIEC congviec = dbContext.CONGVIECs.Find(IDCONGVIEC);
            if (MainWindowViewModel.User.ROLE == "leader" || MainWindowViewModel.User.ROLE == "doctor")
            {
                BACSI bs = MainWindowViewModel.User?.BACSIs.FirstOrDefault();
                if (bs != null && bs != default(BACSI))
                {
                    BACSILIENQUAN bslq = dbContext.BACSILIENQUANs.Find(congviec.ID, bs.CMND_CCCD);
                    if(bslq != null)
                    {
                        bslq.TIENDO = !(bslq.TIENDO.HasValue? bslq.TIENDO.Value : false);
                    }
                }
            }
            else if (MainWindowViewModel.User?.ROLE == "nurse")
            {
                YTA yta = MainWindowViewModel.User?.YTAs.FirstOrDefault();
                if (yta != null && yta != default(YTA))
                {
                    YTALIENQUAN ytlq = dbContext.YTALIENQUANs.Find(congviec.ID, yta.CMND_CCCD);
                    if(ytlq != null)
                    {
                        ytlq.TIENDO = !(ytlq.TIENDO.HasValue ? ytlq.TIENDO.Value : false);
                    }
                }
            }
            dbContext.SaveChangesAsync();
            DataProvider.Ins.DB = new QUANLYBENHVIENEntities();
        }
    }
}
