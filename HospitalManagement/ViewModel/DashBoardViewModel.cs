using HospitalManagement.Command.DashBoardCommand;
using HospitalManagement.Model;
using HospitalManagement.View;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace HospitalManagement.ViewModel
{
    class DashBoardViewModel : BaseViewModel
    {
        private int staffCount;
        private int patientCount;
        private int bedCount;
        private DashBoard dab;

        //get information for card
        public int StaffCount { get => staffCount; set => staffCount = value; }
        public int PatientCount { get => patientCount; set => patientCount = value; }
        public int BedCount { get => bedCount; set => bedCount = value; }

        public int SelectedIndex { get; set; }


        //Column Chart
        private SeriesCollection seriesCollection;
        public SeriesCollection SeriesCollection { get => seriesCollection; set { seriesCollection = value; OnPropertyChanged(); } }

        //Column Chart
        //private ObservableCollection<string> itemSourceTime = new ObservableCollection<string>();
        //public ObservableCollection<string> ItemSourceTime { get => itemSourceTime; set { itemSourceTime = value; OnPropertyChanged(); } }

        //private SeriesCollection seriesCollection;
        //public SeriesCollection SeriesCollection { get => seriesCollection; set { seriesCollection = value; OnPropertyChanged(); } }

        //private Func<double, string> formatter;
        //public Func<double, string> Formatter { get => formatter; set { formatter = value; OnPropertyChanged(); } }

        //private string axisYTitle;
        //public string AxisYTitle { get => axisYTitle; set { axisYTitle = value; OnPropertyChanged(); } }

        //private string axisXTitle;
        //public string AxisXTitle { get => axisXTitle; set { axisXTitle = value; OnPropertyChanged(); } }

        //private string[] labels;
        //public string[] Labels { get => labels; set { labels = value; OnPropertyChanged(); } }

        public ICommand InitPieChartCommand { get; set; }
        public ICommand InitColumnChartCommand { get; set; }


        public DashBoardViewModel(DashBoard dab)
        {
            this.dab = dab;
            //InitColumnChartCommand = new InitColumnChartCommand(this);
            
            SelectedIndex = 0;

            StaffCount = DataProvider.Ins.DB.BACSIs.Count() + DataProvider.Ins.DB.YTAs.Count();
            PatientCount = DataProvider.Ins.DB.BENHNHANs.Count();
            BedCount = DataProvider.Ins.DB.PHONGs.Count() * 6;
           
            #region "Initial Stacked Column Chart"

            SeriesCollection = new SeriesCollection
            {
                new StackedColumnSeries
                {
                    Values = new ChartValues<double> {4, 5, 6, 8},
                    StackMode = StackMode.Values, // this is not necessary, values is the default stack mode
                    DataLabels = true
                },
                new StackedColumnSeries
                {
                    Values = new ChartValues<double> {2, 5, 6, 7},
                    StackMode = StackMode.Values,
                    DataLabels = true
                },
                new StackedColumnSeries
                {
                    Values = new ChartValues<double> {2, 4, 9, 12},
                    StackMode = StackMode.Values, // this is not necessary, values is the default stack mode
                    DataLabels = true
                },
            };

            #endregion
        }


    }
}
