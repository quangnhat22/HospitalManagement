using ClosedXML.Excel;
using HospitalManagement.Model;
using HospitalManagement.ViewModel;
using HospitalManagement.ViewModel.StaffViewViewModel.TeamTask;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.Command.TeamTaskCommand
{
    class ExportTaskToExcelCommand : ICommand
    {
        private StaffRoleTeamTaskViewModel staffRoleTeamTaskViewModel;

        public ExportTaskToExcelCommand(StaffRoleTeamTaskViewModel staffRoleTeamTaskViewModel)
        {
            this.staffRoleTeamTaskViewModel = staffRoleTeamTaskViewModel;
        }

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            List<ExcelData> excelDatas = getExcelDatas();
            DataTable dataTable = ExcelDataToDataTable(excelDatas);
            using(var wb = new XLWorkbook())
            {
                IXLWorksheet xLWorksheet = wb.Worksheets.Add(dataTable, "Danh sách công việc");
                xLWorksheet.Columns("A", "G").AdjustToContents();
                xLWorksheet.Column("G").Style.Alignment.WrapText = true;
                xLWorksheet.Column("G").Width = 64;
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if(saveFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        wb.SaveAs(saveFileDialog.FileName);
                    }
                    catch
                    {
                        Console.WriteLine("Không thể lưu file excel");
                    }
                }
            }
        } 

        private List<ExcelData> getExcelDatas()
        {
            List<ExcelData> excelDatas = new List<ExcelData>();
            int toId = Utils.ToUtils.GetTOID(MainWindowViewModel.User);
            List<CONGVIEC> congviecs = DataProvider.Ins.DB.CONGVIECs.Where(p => p.IDTO == toId).ToList();
            foreach(CONGVIEC congviec in congviecs)
            {
                if(congviec.BACSILIENQUANs.Count == 0 && congviec.YTALIENQUANs.Count == 0)
                {
                    excelDatas.Add(new ExcelData() { TIEUDE = congviec.TIEUDE, BATDAU = congviec.BATDAU, KETTHUC = congviec.KETTHUC, TINHCHAT = congviec.TINHCHAT });
                }
                else
                {
                    foreach(BACSILIENQUAN bslq in congviec.BACSILIENQUANs)
                    {
                        excelDatas.Add(new ExcelData() { TIEUDE = congviec.TIEUDE, BATDAU = congviec.BATDAU, KETTHUC = congviec.KETTHUC, HO = bslq.BACSI.HO, TEN = bslq.BACSI.TEN, TINHCHAT = congviec.TINHCHAT });
                    }
                    foreach (YTALIENQUAN ytlq in congviec.YTALIENQUANs)
                    {
                        excelDatas.Add(new ExcelData() { TIEUDE = congviec.TIEUDE, BATDAU = congviec.BATDAU, KETTHUC = congviec.KETTHUC, HO = ytlq.YTA.HO, TEN = ytlq.YTA.TEN, TINHCHAT = congviec.TINHCHAT });
                    }
                }
            }
            return excelDatas;
        }

        private DataTable ExcelDataToDataTable(List<ExcelData> excelDatas)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Tiêu đề");
            dataTable.Columns.Add("Bắt đầu");
            dataTable.Columns.Add("Kết thúc");
            dataTable.Columns.Add("Địa Điểm");
            dataTable.Columns.Add("Tính chất");
            dataTable.Columns.Add("Người thực hiện");
            dataTable.Columns.Add("Nội dung");
            foreach (ExcelData excelData in excelDatas)
            {
                dataTable.Rows.Add(excelData.TIEUDE, excelData.BATDAU, excelData.KETTHUC, excelData.DIADIEM, excelData.TINHCHAT, excelData.HO + " " +excelData.TEN, excelData.NOIDUNG);
            }
            return dataTable;
        }

        private class ExcelData
        {
            public string TIEUDE { get; set; }
            public string NOIDUNG { get; set; }
            public DateTime? BATDAU { get; set; }
            public DateTime? KETTHUC { get; set; }
            public string DIADIEM { get; set; }
            public string TINHCHAT { get; set; }
            public string HO { get; set; }
            public string TEN { get; set; }
        }
    }
}
