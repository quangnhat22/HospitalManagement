using HospitalManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.Command
{
    class DeletePatientCommand : ICommand
    {
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
            BENHNHAN bnPara = parameter as BENHNHAN;
            BENHNHAN delete = DataProvider.Ins.DB.BENHNHANs.Where(p => bnPara.CMND_CCCD == p.CMND_CCCD).First();
            //DataProvider.Ins.DB.BENHNHANs.Remove(delete);
            DataProvider.Ins.DB.SaveChanges();
        }
    }
}
