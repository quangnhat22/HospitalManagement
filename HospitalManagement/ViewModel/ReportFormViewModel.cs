using HospitalManagement.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.ViewModel
{
    internal class ReportFormViewModel : BaseViewModel
    {
        public ICommand sendEmailReport { get; set; }
        public ReportFormViewModel()
        {
            sendEmailReport = new SendEmailReportCommand();
        }
    }
}
