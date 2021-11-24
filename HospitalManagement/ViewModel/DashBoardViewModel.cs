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
        private DateTime columnChartDate;

        //get information for card
        public int StaffCount { get => staffCount; set => staffCount = value; }
        public int PatientCount { get => patientCount; set => patientCount = value; }
        public int BedCount { get => bedCount; set => bedCount = value; }

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

        public DashBoardViewModel(DashBoard dab)
        {
            ColumnChartDate = System.DateTime.Now;
            this.dab = dab;
            //InitColumnChartCommand = new InitColumnChartCommand(this);
            ReloadBuildingStatisticCommand = new ReloadBuildingStatisticCommand(this);

            SelectedIndex = 0;

            StaffCount = DataProvider.Ins.DB.BACSIs.Count() + DataProvider.Ins.DB.YTAs.Count();
            PatientCount = DataProvider.Ins.DB.BENHNHANs.Count();
            BedCount = DataProvider.Ins.DB.PHONGs.Count() * 6;

            #region "Initial Stacked Column Chart"
            ToaTK toaTK = new ToaTK();
            toaTK.thongKeBenhNhanTheoToa();

            Labels = ToaTK.LabelList;
            SeriesCollection = new SeriesCollection
            {
                new StackedColumnSeries
                {
                    Values = ToaTK.NangList,
                    StackMode = StackMode.Values, // this is not necessary, values is the default stack mode
                    DataLabels = true
                },
                new StackedColumnSeries
                {
                    Values = ToaTK.TrungBinhList,
                    StackMode = StackMode.Values,
                    DataLabels = true
                },
                new StackedColumnSeries
                {
                    Values = ToaTK.NheList,
                    StackMode = StackMode.Values, // this is not necessary, values is the default stack mode
                    DataLabels = true
                },
            };
            #endregion
            #region "Initial Line Chart"
            LineLabels = new List<string>();
            LineChartSeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Bệnh nhân trung bình",
                    Values = GenerateLive(),
                    LabelPoint = point => ": " + point.Y + " người",
                },
            };
            #endregion
        }

        public ChartValues<double> GenerateLive(DateTime? from = null, DateTime? to = null)
        {
            // This method is suck but it works
            var sqlString = "SELECT COUNT(*), MONTH(NGNHAPVIEN) AS THANG, YEAR(NGNHAPVIEN) AS NAM " +
                "FROM BENHNHAN " +
                "GROUP BY MONTH(NGNHAPVIEN), YEAR(NGNHAPVIEN) " +
                "ORDER BY YEAR(NGNHAPVIEN), MONTH(NGNHAPVIEN)";
            ChartValues<double> value = new ChartValues<double>();
            EntityConnectionStringBuilder entityConnectionStringBuilder = new EntityConnectionStringBuilder();
            entityConnectionStringBuilder.ConnectionString = ConfigurationManager.ConnectionStrings["QUANLYBENHVIENEntities"].ConnectionString;
            string connectionString = entityConnectionStringBuilder.ProviderConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlString, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            value.Add((int)reader[0]);
                            string date = reader[1].ToString() + '/' + reader[2].ToString();
                            LineLabels.Add(date);
                        }
                    }
                }
            }
            return value;
        }
    }
}
