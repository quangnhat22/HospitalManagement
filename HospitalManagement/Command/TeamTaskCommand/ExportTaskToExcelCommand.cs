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
               
        }

        private DataTable CreateDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add
        }
    }
}
