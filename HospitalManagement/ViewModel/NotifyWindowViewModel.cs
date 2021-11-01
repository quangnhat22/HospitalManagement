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
        public ICommand CloseWindow { get; set; }
        public ICommand LoadContent { get; set; }

        public NotifyWindowViewModel()
        { 
            LoadContent = new NotifyWindowCommand();
            CloseWindow = new CloseWindowCommand();
        }
    }
}
