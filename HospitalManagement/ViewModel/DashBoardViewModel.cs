using HospitalManagement.Command.DashBoardCommand;
using HospitalManagement.Model;
using HospitalManagement.Utils;
using HospitalManagement.View;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace HospitalManagement.ViewModel
{
    class DashBoardViewModel : BaseViewModel
    {
        private int staffCount;
        private int patientCount;
        private int? bedCount;
        private DashBoard dab;
        private DateTime columnChartDate;
        private Visibility progressBarVisibility;
        private Visibility columnChartVisibility;

        //get information for card
        public int StaffCount { get => staffCount; set => staffCount = value; }
        public int PatientCount { get => patientCount; set => patientCount = value; }
        public int? BedCount { get => bedCount; set => bedCount = value; }

        public int SelectedIndex { get; set; }


        //Column Chart
        private SeriesCollection seriesCollection;
        public SeriesCollection SeriesCollection { get => seriesCollection; set { seriesCollection = value; OnPropertyChanged(); } }
        private List<string> labels;
        public List<string> Labels { get => labels; set => labels = value; }
        //Line Chart
        private SeriesCollection lineChartSeriesCollection;
        public SeriesCollection LineChartSeriesCollection { get => lineChartSeriesCollection; set => lineChartSeriesCollection = value; }
        private List<string> lineLabels;
        public List<string> LineLabels { get => lineLabels; set => lineLabels = value; }
        public DateTime ColumnChartDate { get => columnChartDate; set => columnChartDate = value; }
        public ICommand InitPieChartCommand { get; set; }
        public ICommand InitColumnChartCommand { get; set; }
        public ICommand ReloadBuildingStatisticCommand { get; set; }
        public Visibility ProgressBarVisibility 
        { 
            get => progressBarVisibility; 
            set
            {
                progressBarVisibility = value;
                OnPropertyChanged("ProgressBarVisibility");
            }
        }
        public Visibility ColumnChartVisibility 
        { 
            get => columnChartVisibility;
            set
            {
                columnChartVisibility = value;
                OnPropertyChanged("ColumnChartVisibility");
            }
        }

        public DashBoardViewModel(DashBoard dab)
        {
            ColumnChartDate = System.DateTime.Now;
            this.dab = dab;
            //InitColumnChartCommand = new InitColumnChartCommand(this);
            ReloadBuildingStatisticCommand = new ReloadBuildingStatisticCommand(this);

            SelectedIndex = 0;

            StaffCount = DataProvider.Ins.DB.BACSIs.Count() + DataProvider.Ins.DB.YTAs.Count() + DataProvider.Ins.DB.ADMINs.Count();
            PatientCount = DataProvider.Ins.DB.BENHNHANs.Count();
            bedCount = 0;
            var roomList = DataProvider.Ins.DB.PHONGs.ToList() ;
            roomList.ForEach(x => bedCount += x.SUCCHUA);
            GenerateColumnChart();
        }

        public void GenerateColumnChart()
        {
            #region "Initial Stacked Column Chart"
            ProgressBarVisibility = Visibility.Visible;
            ColumnChartVisibility = Visibility.Collapsed;
            ToaTK toaTK = new ToaTK();
            toaTK.thongKeBenhNhanTheoToa().GetAwaiter().OnCompleted(() =>
            {
                Labels = ToaTK.LabelList;
                SeriesCollection = new SeriesCollection
                                    {
                                        new StackedColumnSeries
                                        {
                                            Title = "Trở nặng",
                                            Values = ToaTK.NangList,
                                            StackMode = StackMode.Values, // this is not necessary, values is the default stack mode
                                            DataLabels = true,
                                            Fill = new SolidColorBrush(Color.FromRgb(224, 46, 68))
                                        },
                                        new StackedColumnSeries
                                        {
                                            Title = "Có triệu chứng",
                                            Values = ToaTK.TrungBinhList,
                                            StackMode = StackMode.Values,
                                            DataLabels = true,
                                            Fill = new SolidColorBrush(Color.FromRgb(255,129,0))
                                        },
                                        new StackedColumnSeries
                                        {
                                            Title = "Không có triệu chứng",
                                            Values = ToaTK.NheList,
                                            StackMode = StackMode.Values, // this is not necessary, values is the default stack mode
                                            DataLabels = true,
                                            Fill = new SolidColorBrush(Color.FromRgb(7,210,0))
                                        },
                                    };
                ProgressBarVisibility = Visibility.Collapsed;
                ColumnChartVisibility = Visibility.Visible;
            });
            #endregion
            #region "Initial Line Chart"
            LineLabels = new List<string>();
            LineChartSeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Số bệnh nhân trong tháng",
                    Values = GenerateLive(),
                    LabelPoint = point => ": " + point.Y + " người",
                },
            };
            #endregion
        }

        public ChartValues<int> GenerateLive(DateTime? from = null, DateTime? to = null)
        {
            ChartValues<int> chartValues = new ChartValues<int>();
            List<LineChartInitialization> list = LineChartInitialization.ChartInitialize();
            foreach(LineChartInitialization lineChartInitialization in list)
            {
                chartValues.Add(lineChartInitialization.SL);
                LineLabels.Add(lineChartInitialization.THANG.ToString() + '/' + lineChartInitialization.NAM.ToString());
            }
            return chartValues;
        }
    }
}
