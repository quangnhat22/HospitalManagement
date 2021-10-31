using HospitalManagement.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
        private BaseViewModel _selectedViewModel = new DashBoardViewModel();

        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public ICommand SelectViewCommand
        {
            get;
            set;
        }

        public ICommand ShowAboutCommand
        {
            get;
            set;
        }

        public ICommand LogoutCommand
        { 
            get;
            set; 
        }

        public ICommand OpenReportForm
        {
            get;
            set;
        }

        public MainWindowViewModel()
        {
            SelectViewCommand = new SelectViewCommand(this);
            ShowAboutCommand = new ShowAboutCommand();
            OpenReportForm = new OpenReportFormCommand();
            LogoutCommand = new LogoutCommand();
        }
    }
}
