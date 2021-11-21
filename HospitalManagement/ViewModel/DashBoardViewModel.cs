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
            ToaTK toaTK = new ToaTK();
            toaTK.thongKeBenhNhanTheoToa();
           
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
        }


    }
}
