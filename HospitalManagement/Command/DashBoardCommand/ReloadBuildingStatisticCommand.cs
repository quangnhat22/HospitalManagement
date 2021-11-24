using HospitalManagement.Utils;
using HospitalManagement.ViewModel;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.Command.DashBoardCommand
{
    class ReloadBuildingStatisticCommand : ICommand
    {
        private DashBoardViewModel dashBoardViewModel;

        public ReloadBuildingStatisticCommand(DashBoardViewModel dashBoardViewModel)
        {
            this.dashBoardViewModel = dashBoardViewModel;
        }

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
            ToaTK toaTK = new ToaTK();
            toaTK.thongKeBenhNhanTheoToa(dashBoardViewModel.ColumnChartDate);
            dashBoardViewModel.SeriesCollection = new SeriesCollection
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
        }
    }
}
