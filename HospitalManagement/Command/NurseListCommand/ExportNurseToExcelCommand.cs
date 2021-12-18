using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HospitalManagement.ViewModel;
using HospitalManagement.Model;
using System.Data;
using ClosedXML.Excel;
using Microsoft.Win32;

namespace HospitalManagement.Command.NurseListCommand
{
    class ExportNurseToExcelCommand : ICommand
    {
        private NurseViewModel nurseViewModel;

        public ExportNurseToExcelCommand(NurseViewModel nurseViewModel)
        {
            this.nurseViewModel = nurseViewModel;
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
            using (var wb = new XLWorkbook())
            {
                IXLWorksheet xLWorksheet = wb.Worksheets.Add(dataTable, "Danh sách công việc");
                xLWorksheet.Column("A").Width = 30;
                xLWorksheet.Column("B").Width = 40;
                xLWorksheet.Column("C").Width = 30;
                xLWorksheet.Column("D").Width = 20;
                xLWorksheet.Column("E").Width = 30;
                xLWorksheet.Column("F").Width = 40;
                xLWorksheet.Column("G").Width = 40;
                xLWorksheet.Column("H").Width = 20;
                xLWorksheet.Column("I").Width = 30;
                xLWorksheet.Column("J").Width = 30;
                xLWorksheet.Column("K").Width = 20;
                xLWorksheet.Column("L").Width = 50;

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Workbook (*.xlsm) | *.xlsm";
                if (saveFileDialog.ShowDialog() == true)
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
            List<YTA> ytas = DataProvider.Ins.DB.YTAs.ToList();
            foreach (YTA yta in ytas)
            {
                excelDatas.Add(new ExcelData()
                {
                    CMND_CCCD = yta.CMND_CCCD,
                    HO = yta.HO,
                    TEN = yta.TEN,
                    SDT = yta.SDT,
                    EMAIL = yta.EMAIL,
                    DIACHI = yta.DIACHI,
                    QUOCTICH = yta.QUOCTICH,
                    NGSINH = yta.NGSINH,
                    GIOITINH = yta.GIOITINH == true ? "Nữ" : "Nam",
                    VAITRO = yta.VAITRO,
                    CHUYENKHOA = yta.CHUYENKHOA,
                    IDTO = yta.IDTO,
                    GHICHU = yta.GHICHU
                });
            }
            return excelDatas;
        }

        private DataTable ExcelDataToDataTable(List<ExcelData> excelDatas)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("CMND/CCCD");
            dataTable.Columns.Add("Họ và tên");
            dataTable.Columns.Add("Ngày sinh");
            dataTable.Columns.Add("Giới tính");
            dataTable.Columns.Add("Số điện thoại");
            dataTable.Columns.Add("Email");
            dataTable.Columns.Add("Địa chỉ");
            dataTable.Columns.Add("Quốc tịch");
            dataTable.Columns.Add("Chuyên khoa");
            dataTable.Columns.Add("Vai trò");
            dataTable.Columns.Add("ID tổ");
            dataTable.Columns.Add("Ghi chú");
            foreach (ExcelData excelData in excelDatas)
            {
                dataTable.Rows.Add(excelData.CMND_CCCD,
                                    excelData.HO + " " + excelData.TEN,
                                    excelData.NGSINH,
                                    excelData.GIOITINH,
                                    excelData.SDT,
                                    excelData.EMAIL,
                                    excelData.DIACHI,
                                    excelData.QUOCTICH,
                                    excelData.CHUYENKHOA,
                                    excelData.VAITRO,
                                    excelData.IDTO,
                                    excelData.GHICHU);
            }
            return dataTable;
        }

        private class ExcelData
        {
            public string CMND_CCCD { get; set; }
            public string HO { get; set; }
            public string TEN { get; set; }
            public string SDT { get; set; }
            public string EMAIL { get; set; }
            public string QUOCTICH { get; set; }
            public string DIACHI { get; set; }
            public DateTime? NGSINH { get; set; }
            public string GIOITINH { get; set; }
            public string VAITRO { get; set; }
            public string CHUYENKHOA { get; set; }
            public string GHICHU { get; set; }
            public int? IDTO { get; set; }
        }
    }
}
