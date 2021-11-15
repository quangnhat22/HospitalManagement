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
    internal class InitPieChartCommand : ICommand
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
            DashBoard dashBoard = (DashBoard)parameter;
            InitPieChart(dashBoard);
        }

        public InitPieChartCommand(DashBoardViewModel dashBoardViewModel)
        {
            this.dabVM = dashBoardViewModel; 
        }

        public void InitPieChart(DashBoard dab)
        {
            dabVM.LabelPoint = chartPoint => string.Format("{0:N0}", chartPoint.Y);
            ChartValues<double> bedEmptyCount = new ChartValues<double>();
            bedEmptyCount.Add(DataProvider.Ins.DB.BACSIs.Count());
            ChartValues<double> bedFullCount = new ChartValues<double>();
            bedFullCount.Add(DataProvider.Ins.DB.USERs.Count());
            dabVM.PieSeriesCollection = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "Bác sĩ",
                        //Values = ReportDAL.Instance.QueryRevenueFromSellingInDay(currentDay, currentMonth, currentYear),
                        Values = bedEmptyCount,
                        Fill = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FF1976D2"),
                        DataLabels = true,
                        FontSize = 16,
                        LabelPoint = dabVM.LabelPoint,
                    },
                    new PieSeries
                    {
                        Title="Y tá",
                        Values = bedFullCount,
                        Fill = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FF27AE60"),
                        DataLabels = true,
                        FontSize = 16,
                        LabelPoint = dabVM.LabelPoint,
                    },
                };
            //if (homeWindow.cboSelectTimePie.SelectedIndex == 0)
            //{
            //    string currentDay = DateTime.Now.Day.ToString();
            //    string currentMonth = DateTime.Now.Month.ToString();
            //    string currentYear = DateTime.Now.Year.ToString();
            //    PieSeriesCollection = new SeriesCollection
            //    {
            //        new PieSeries
            //        {
            //            Title = "Bán hàng",
            //            Values = ReportDAL.Instance.QueryRevenueFromSellingInDay(currentDay, currentMonth, currentYear),
            //            Fill = (Brush)new BrushConverter().ConvertFrom("#FF1976D2"),
            //            DataLabels = true,
            //            FontSize = 16,
            //            LabelPoint = labelPoint,
            //        },
            //        new PieSeries
            //        {
            //            Title="Sân bóng",
            //            Values = ReportDAL.Instance.QueryRevenueFromFieldInDay(currentDay, currentMonth, currentYear),
            //            Fill = (Brush)new BrushConverter().ConvertFrom("#FF27AE60"),
            //            DataLabels = true,
            //            FontSize = 16,
            //            LabelPoint = labelPoint,
            //        },
            //    };
            //}
            //else
            //{
            //    string currentMonth = DateTime.Now.Month.ToString();
            //    string currentYear = DateTime.Now.Year.ToString();
            //    PieSeriesCollection = new SeriesCollection
            //    {
            //        new PieSeries
            //        {
            //            Title = "Bán hàng",
            //            Values = ReportDAL.Instance.QueryRevenueFromSellingInMonth(currentMonth, currentYear),
            //            Fill = (Brush)new BrushConverter().ConvertFrom("#FF1976D2"),
            //            DataLabels = true,
            //            FontSize = 16,
            //            LabelPoint = labelPoint,
            //        },
            //        new PieSeries
            //        {
            //            Title="Sân bóng",
            //            Values = ReportDAL.Instance.QueryRevenueFromFieldInMonth(currentMonth, currentYear),
            //            Fill = (Brush)new BrushConverter().ConvertFrom("#FF27AE60"),
            //            DataLabels = true,
            //            FontSize = 16,
            //            LabelPoint = labelPoint,
            //        },
            //    };
            //}
        }
    }
}
