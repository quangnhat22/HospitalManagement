using HospitalManagement.Model;
using HospitalManagement.View;
using HospitalManagement.ViewModel;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace HospitalManagement.Command.DashBoardCommand
{
    internal class InitColumnChartCommand : ICommand
    {
        private DashBoardViewModel dabVM;
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            DashBoardViewModel dabVM = parameter as DashBoardViewModel;
            InitColumnChart(dabVM);
        }

        public InitColumnChartCommand(DashBoardViewModel dashBoardViewModel)
        {
            this.dabVM = dashBoardViewModel;
        }

        public void InitColumnChart(DashBoardViewModel dab)
        {
            //dabVM.AxisXTitle = "Ngày";
            //string[] tmp = homeWindow.cboSelectTime.SelectedValue.ToString().Split(' ');
            //string selectedMonth = tmp[1];
            //string currentYear = DateTime.Now.Year.ToString();
            ChartValues<double> bedEmptyCount = new ChartValues<double>();
            bedEmptyCount.Add(DataProvider.Ins.DB.BACSIs.Count());
            ChartValues<double> bedFullCount = new ChartValues<double>();
            bedFullCount.Add(DataProvider.Ins.DB.USERs.Count());

            #region "Initial Column Chart"
            dabVM.SeriesCollection = new SeriesCollection
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
                }
            };
            #endregion
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
        //dabVM.SeriesCollection = new SeriesCollection
        //        {
        //            new ColumnSeries
        //            {
        //                Title = "Doanh thu",
        //                Fill = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FF1976D2"),
        //                Values = bedEmptyCount,
        //            },
        //            new ColumnSeries
        //            {
        //                Title = "Chi phí",
        //                Fill = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FFF44336"),
        //                Values = bedFullCount,
        //            }
        //        };

        //if (homeWindow.cboSelectPeriod.SelectedIndex == 0) //Theo tháng => 31 ngày
        //{
        //    if (homeWindow.cboSelectTime.SelectedIndex != -1)
        //    {
        //        AxisXTitle = "Ngày";
        //        string[] tmp = homeWindow.cboSelectTime.SelectedValue.ToString().Split(' ');
        //        string selectedMonth = tmp[1];
        //        string currentYear = DateTime.Now.Year.ToString();
        //        SeriesCollection = new SeriesCollection
        //        {
        //            new ColumnSeries
        //            {
        //                Title = "Doanh thu",
        //                Fill = (Brush)new BrushConverter().ConvertFrom("#FF1976D2"),
        //                Values = ReportDAL.Instance.QueryRevenueByMonth(selectedMonth, currentYear),
        //            },
        //            new ColumnSeries
        //            {
        //                Title = "Chi phí",
        //                Fill = (Brush)new BrushConverter().ConvertFrom("#FFF44336"),
        //                Values = ReportDAL.Instance.QueryOutcomeByMonth(selectedMonth, currentYear),
        //            }
        //        };
        //        Labels = ReportDAL.Instance.QueryDayInMonth(selectedMonth, currentYear);
        //        Formatter = value => string.Format("{0:N0}", value);
        //    }
        //}
        //else if (homeWindow.cboSelectPeriod.SelectedIndex == 1) //Theo quý => 4 quý
        //{
        //    if (homeWindow.cboSelectTime.SelectedIndex != -1)
        //    {
        //        AxisXTitle = "Quý";
        //        string[] tmp = homeWindow.cboSelectTime.SelectedValue.ToString().Split(' ');
        //        string selectedYear = tmp[1];
        //        SeriesCollection = new SeriesCollection
        //        {
        //            new ColumnSeries
        //            {
        //                Title = "Doanh thu",
        //                Fill = (Brush)new BrushConverter().ConvertFrom("#FF1976D2"),
        //                Values = ReportDAL.Instance.QueryRevenueByQuarter(selectedYear),
        //            },
        //            new ColumnSeries
        //            {
        //                Title = "Chi phí",
        //                Fill = (Brush)new BrushConverter().ConvertFrom("#FFF44336"),
        //                Values = ReportDAL.Instance.QueryOutcomeByQuarter(selectedYear),
        //            }
        //        };
        //        Labels = ReportDAL.Instance.QueryQuarterInYear(selectedYear);
        //        Formatter = value => string.Format("{0:N0}", value);
        //    }
        //}
        //else
        //{
        //    if (homeWindow.cboSelectTime.SelectedIndex != -1) //Theo năm => 12 tháng
        //    {
        //        AxisXTitle = "Tháng";
        //        string[] tmp = homeWindow.cboSelectTime.SelectedValue.ToString().Split(' ');
        //        string selectedYear = tmp[1];
        //        SeriesCollection = new SeriesCollection
        //        {
        //            new ColumnSeries
        //            {
        //                Title = "Doanh thu",
        //                Fill = (Brush)new BrushConverter().ConvertFrom("#FF1976D2"),
        //                Values = ReportDAL.Instance.QueryRevenueByYear(selectedYear),
        //            },
        //            new ColumnSeries
        //            {
        //                Title = "Chi phí",
        //                Fill = (Brush)new BrushConverter().ConvertFrom("#FFF44336"),
        //                Values = ReportDAL.Instance.QueryOutcomeByYear(selectedYear)
        //            }
        //        };
        //        Labels = ReportDAL.Instance.QueryMonthInYear(selectedYear);
        //        Formatter = value => string.Format("{0:N0}", value);
        //    }
        //}
    }
}
