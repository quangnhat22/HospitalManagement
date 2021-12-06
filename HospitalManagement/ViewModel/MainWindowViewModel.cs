using HospitalManagement.Command;
using HospitalManagement.Model;
using HospitalManagement.View.EmptyView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HospitalManagement.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private BaseViewModel _selectedViewModel = new SwitchDashboardViewModel();

        static public USER User;
        static public bool IsSuperAdmin = false;

        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public ICommand SelectViewCommand { get; set; }
        public ICommand ShowAboutCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand OpenReportForm { get; set; }
        public ICommand OpenAccountSetting { get; set; }
        
        public Visibility AdminRolesVisibility { get; set; }
        public Visibility StaffRolesVisibility { get; set; }
        public Visibility PatientTabVisibility { get; set; }
        public MainWindowViewModel()
        {
            SelectViewCommand = new SelectViewCommand(this);
            ShowAboutCommand = new ShowAboutCommand();
            OpenReportForm = new OpenReportFormCommand();
            LogoutCommand = new LogoutCommand();
            OpenAccountSetting = new OpenAccountWindowCommand();
            if(User.ROLE == "sudo")
            {
                AdminRolesVisibility = Visibility.Visible;
                StaffRolesVisibility = Visibility.Collapsed;
                IsSuperAdmin = true;
            }
            else if (User.ROLE == "admin")
            {
                AdminRolesVisibility = Visibility.Visible;
                StaffRolesVisibility = Visibility.Collapsed;
            }
            else
            {
                AdminRolesVisibility = Visibility.Collapsed;
                StaffRolesVisibility = Visibility.Visible;
            }
            if(User.ROLE == "staff")
            {
                PatientTabVisibility = Visibility.Collapsed;
            }
            else
            {
                PatientTabVisibility = Visibility.Visible;
            }
        }
    }
}
