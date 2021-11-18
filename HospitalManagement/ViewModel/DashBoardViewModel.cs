using HospitalManagement.Command.DashBoardCommand;
using HospitalManagement.Model;
using HospitalManagement.View;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.ViewModel
{
    class DashBoardViewModel : BaseViewModel
    {
        private int staffCount;
        private int patientCount;
        private int bedCount;
        private DashBoard dab;

        public int StaffCount { get => staffCount; set => staffCount = value; }
        public int PatientCount { get => patientCount; set => patientCount = value; }
        public int BedCount { get => bedCount; set => bedCount = value; }

        public int SelectedIndex { get; set; }
        //Pie chart
        private SeriesCollection pieSeriesCollection;
        public SeriesCollection PieSeriesCollection { get => pieSeriesCollection; set { pieSeriesCollection = value; OnPropertyChanged(); } }

        private Func<ChartPoint, string> labelPoint;
        public Func<ChartPoint, string> LabelPoint { get => labelPoint; set => labelPoint = value; }

        //Column Chart
        private ObservableCollection<string> itemSourceTime = new ObservableCollection<string>();
        public ObservableCollection<string> ItemSourceTime { get => itemSourceTime; set { itemSourceTime = value; OnPropertyChanged(); } }

        private SeriesCollection seriesCollection;
        public SeriesCollection SeriesCollection { get => seriesCollection; set { seriesCollection = value; OnPropertyChanged(); } }

        private Func<double, string> formatter;
        public Func<double, string> Formatter { get => formatter; set { formatter = value; OnPropertyChanged(); } }

        private string axisYTitle;
        public string AxisYTitle { get => axisYTitle; set { axisYTitle = value; OnPropertyChanged(); } }

        private string axisXTitle;
        public string AxisXTitle { get => axisXTitle; set { axisXTitle = value; OnPropertyChanged(); } }

        private string[] labels;
        public string[] Labels { get => labels; set { labels = value; OnPropertyChanged(); } }

        public ICommand InitPieChartCommand { get; set; }
        public ICommand InitColumnChartCommand { get; set; }

        public DashBoardViewModel(DashBoard dab = null)
        {
            this.dab = dab;
            InitColumnChartCommand = new InitColumnChartCommand(this);
            InitPieChartCommand = new InitPieChartCommand(this);
            SelectedIndex = 0;
            //if (dab != null)
            //{
            //    InitPieChartCommand.Execute(dab);
            //}
            StaffCount = DataProvider.Ins.DB.BACSIs.Count() + DataProvider.Ins.DB.YTAs.Count();
            PatientCount = DataProvider.Ins.DB.BENHNHANs.Count();
            BedCount = DataProvider.Ins.DB.PHONGs.Count() * 6;
        }

        
    }
}
