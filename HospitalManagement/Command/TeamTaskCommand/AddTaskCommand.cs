using HospitalManagement.Model;
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
            congviec.BATDAU = addToDoFormViewModel.StartDate;
            congviec.KETTHUC = addToDoFormViewModel.EndDate;
            DataProvider.Ins.DB.CONGVIECs.Add(congviec);
            DataProvider.Ins.DB.SaveChanges();
        }
    }
}
