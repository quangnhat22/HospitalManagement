using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static HospitalManagement.View.NotifyWindow;
using HospitalManagement.View;
using System.Windows.Media;
using System.Drawing;

namespace HospitalManagement.Command
{
    class NotifyWindowCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return parameter == null ? false : true;
        }

        public void Execute(object parameter)
        {
            NotifyWindow w = parameter as NotifyWindow;
            w.txtMessage.Text = w.Title.ToString();
            
            if (w.Tag.ToString() == "Success")
                SetProperties(w, "#74EBD5", "CheckboxMarkedCircleOutline");
            else
                if (w.Tag.ToString() == "Warning")
                    SetProperties(w, "#FCCF31", "AlertCircleOutline");
                else
                    if (w.Tag.ToString() == "Error")
                        SetProperties(w, "#F6416C", "AlertOutline");
        }

        public void SetProperties(NotifyWindow w, string colorCode, string iconName)
        {
            w.headBorder.Background = (SolidColorBrush)new BrushConverter().ConvertFrom(colorCode);
            w.bodyBorder.Background = (SolidColorBrush)new BrushConverter().ConvertFrom(colorCode);
            w.btnOK.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom(colorCode);
            w.btnOK.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom(colorCode);
            w.Icon.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom(colorCode);
            w.ToolTip = iconName;
        }
    }
}
