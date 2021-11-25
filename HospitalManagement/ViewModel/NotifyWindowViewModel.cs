using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HospitalManagement.Command;
using static HospitalManagement.View.NotifyWindow;

namespace HospitalManagement.ViewModel
{
    class NotifyWindowViewModel
    {
        public ICommand LoadContent { get; set; }

        private string cancel;
        public string Cancel
        {
            get { return cancel; }
            set { cancel = value; }
        }

        public NotifyWindowViewModel(string cancel)
        {
            this.cancel = cancel;
            LoadContent = new NotifyWindowCommand();
        }
    }
}
