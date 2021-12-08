using HospitalManagement.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.Command.TeamTaskCommand
{
    class ExportTaskToExcelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged { add { } remove { } }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ;
        }

        private class ExcelData
        {
            public string TIEUDE { get; set; }
            public string NOIDUNG { get; set; }
            public DateTime? BATDAU { get; set; }
            public DateTime? KETTHUC { get; set; }
            public string DIADIEM { get; set; }
            public string TINHCHAT { get; set; }
            public string CMND_CCCD { get; set; }
        }
    }
}
