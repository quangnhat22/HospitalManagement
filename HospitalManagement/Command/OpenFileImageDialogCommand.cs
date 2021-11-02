using HospitalManagement.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HospitalManagement.Command
{
    internal class OpenFileImageDialogCommand : ICommand
    {

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            try
            {
                ReportForm rpf = parameter as ReportForm;
                OpenFileDialog attachment = new OpenFileDialog();
                attachment.Multiselect = true;
                attachment.Filter = "Images(.jpg,.png)|*.png;*.jpg;|Pdf File|*.pdf";
                Nullable<bool> dialogOK = attachment.ShowDialog();
                if (dialogOK == true)
                {
                    string sFileNames = "";
                    foreach (string sFileName in attachment.FileNames)
                        sFileNames += ";" + sFileName;
                    sFileNames = sFileNames.Substring(1);
                    rpf.btnAttachment.Content = sFileNames;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
