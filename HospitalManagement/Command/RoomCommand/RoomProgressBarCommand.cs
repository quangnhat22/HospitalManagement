using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace HospitalManagement.Command
{
    class RoomProgressBarCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public bool CanExecute(object parameter)
        {
            return parameter == null ? false : true;
        }

        public void Execute(object parameter)
        {
            ProgressBar pgb = parameter as ProgressBar;
            if (pgb.Value >= pgb.Maximum) pgb.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#F6416C");
            else pgb.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#2FDD92");
        }
    }
}
