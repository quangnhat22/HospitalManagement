using GongSolutions.Wpf.DragDrop;
using HospitalManagement.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using HospitalManagement.Command.TeamCommand;
using HospitalManagement.Utils;

namespace HospitalManagement.ViewModel
{
    class TeamViewmodel : BaseViewModel
    {
        private ObservableCollection<CountedTeam> countedTeams;
        private static List<StaffInformation> members;
        private List<TOA> buildings;
        private static List<TANG> floors = DataProvider.Ins.DB.TANGs.Where(p => p.TOA.IDTOA == 1).ToList();
        private Visibility progressBarVisibility;
        private Visibility listBoxVisibility;

        private string currentBuilding;
        private int? currentFloor = null;
        private string currentTeam;
        private string deleteButtonVisibility = "Collapsed";
        private static QUANLYBENHVIENEntities dbContext;

        public ICommand ChangeBuildingTeamCommand { get; set; }
        public ICommand ShowMembersInTeamCommand { get; set; }
        public ICommand ShowMembersInformationInTeamCommand { get; set; }
        public ICommand ToggleDeleteButtonCommand { get; set; }
        public ICommand AddTeamCommand { get; set; }
        public ICommand DeleteTeamCommand { get; set; }

        public ObservableCollection<CountedTeam> CountedTeams
        {
            get { return countedTeams; }
            set
            {
                countedTeams = value;
                OnPropertyChanged("CountedTeams");
            }
        }

        public List<StaffInformation> Members
        {
            get { return members; }
            set { members = value; OnPropertyChanged("Members"); }
        }

        public List<TOA> Buildings
        {
            get { return buildings; }
            set
            {
                buildings = value;
                OnPropertyChanged("Buildings");
            }
        }

        public List<TANG> Floors
        {
            get { return floors; }
            set { floors = value; OnPropertyChanged("Floors"); }
        }

        public string CurrentBuilding
        {
            get { return currentBuilding; }
            set { currentBuilding = value; OnPropertyChanged("CurrentBuilding"); }
        }

        public int? CurrentFloor
        {
            get { return currentFloor; }
            set { currentFloor = value; OnPropertyChanged("CurrentFloor"); }
        }

        public string CurrentTeam
        {
            get { return currentTeam; }
            set
            {
                currentTeam = value;OnPropertyChanged("CurrentTeam");
            }
        }

        public Visibility ProgressBarVisibility 
        { 
            get => progressBarVisibility; 
            set
            {
                progressBarVisibility = value;
                OnPropertyChanged("ProgressBarVisibility");
            }
        }
        public Visibility ListBoxVisibility 
        { 
            get => listBoxVisibility; 
            set
            {
                listBoxVisibility = value;
                OnPropertyChanged("ListBoxVisibility");
            }
        }

        public string DeleteButtonVisibility
        {
            get { return deleteButtonVisibility; }
            set { deleteButtonVisibility = value; OnPropertyChanged("DeleteButtonVisibility"); }
        }

        public static QUANLYBENHVIENEntities DbContext 
        { 
            get
            {
                if (dbContext == null)
                    dbContext = new QUANLYBENHVIENEntities();
                return dbContext;
            }
            set => dbContext = value; 
        }

        public TeamViewmodel()
        {
            buildings = DbContext.TOAs.ToList();
            currentBuilding = buildings[0].DISPLAYNAME;
            currentTeam = "";
            List<TO> teams = DbContext.TOes.Where(p => p.TANG.TOA.DISPLAYNAME == currentBuilding).ToList();
            InitTeams(teams);
            ChangeBuildingTeamCommand = new ChangeBuildingTeamCommand(this);
            ShowMembersInTeamCommand = new ShowMembersInTeamCommand(this);
            ShowMembersInformationInTeamCommand = new ShowMembersInformationInTeamCommand(this);
            ToggleDeleteButtonCommand = new ToggleDeleteButtonCommand(this);
            AddTeamCommand = new AddTeamCommand(this);
            DeleteTeamCommand = new DeleteTeamCommand(this);
        }

        public async void InitTeams(List<TO> teams)
        {
            ListBoxVisibility = Visibility.Collapsed;
            ProgressBarVisibility = Visibility.Visible;
            await Task.Run(() =>
            {
                CountedTeams = CountedTeam.GetCountedTeams(teams);
            });
            ListBoxVisibility = Visibility.Visible;
            ProgressBarVisibility = Visibility.Collapsed;
        }
    }

    public class CountedTeam : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int count;
        private TO value;

        public int Count
        {
            get { return count; }
            set { count = value; OnPropertyChanged("Count"); }
        }

        public TO Value { get => value; set => this.value = value; }

        public CountedTeam(TO p)
        {
            Value = p;
            Count = 0;
            if (p.BACSIs != null) Count += p.BACSIs.Count;
            if (p.YTAs != null) Count += p.YTAs.Count;
        }

        public static ObservableCollection<CountedTeam> GetCountedTeams(List<TO> ls)
        {
            ObservableCollection<CountedTeam> countedTeams = new ObservableCollection<CountedTeam>();
            foreach (TO p in ls)
            {
                CountedTeam countedTeam = new CountedTeam(p);
                countedTeams.Add(countedTeam);
            }
            return countedTeams;
        }
    }
}
