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

namespace HospitalManagement.ViewModel
{
    class TeamViewmodel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<CountedTeam> countedTeams;
        private List<TOA> buildings;
        private TOA toa;

        private int? teamNumber;
        private string currentBuilding;

        public ICommand ChangeBuildingTeamCommand { get; set; }

        public ObservableCollection<CountedTeam> CountedTeams
        {
            get { return countedTeams; }
            set
            {
                countedTeams = value;
                OnPropertyChanged("CountedTeams");
            }
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

        public string CurrentBuilding
        {
            get { return currentBuilding; }
            set { currentBuilding = value; OnPropertyChanged("CurrentBuilding"); }
        }

        public int? TeamNumber
        {
            get { return teamNumber; }
            set { teamNumber = value; OnPropertyChanged("TeamNumber"); }
        }

        public TOA Toa { get => toa; set => toa = value; }

        public TeamViewmodel()
        {
            Buildings = DataProvider.Ins.DB.TOAs.ToList();
            currentBuilding = buildings[0].DISPLAYNAME;
            CountedTeams = CountedTeam.GetCountedTeams(DataProvider.Ins.DB.TOes.Where(p => p.TANG.TOA.DISPLAYNAME == currentBuilding).ToList());
            ChangeBuildingTeamCommand = new ChangeBuildingTeamCommand(this);
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
            Count = p.BACSIs.Count + p.YTAs.Count;
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
