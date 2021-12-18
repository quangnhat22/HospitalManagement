using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ClosedXML.Excel;
using HospitalManagement.ViewModel;
using HospitalManagement.Model;
using Microsoft.Win32;

namespace HospitalManagement.Command.PatientListCommand
{
    class ExportPatientToExcelCommand : ICommand
    {
        private PatientViewModel patientViewModel;

        public ExportPatientToExcelCommand(PatientViewModel patientViewModel)
        {
            this.patientViewModel = patientViewModel;
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
                xLWorksheet.Column("B").Width = 20;
                xLWorksheet.Column("C").Width = 40;
                xLWorksheet.Column("D").Width = 30;
                xLWorksheet.Column("E").Width = 20;
                xLWorksheet.Column("F").Width = 30;
                xLWorksheet.Column("G").Width = 40;
                xLWorksheet.Column("H").Width = 20;
                xLWorksheet.Column("I").Width = 20;
                xLWorksheet.Column("J").Width = 30;
                xLWorksheet.Column("K").Width = 20;
                xLWorksheet.Column("L").Width = 50;
                xLWorksheet.Column("M").Width = 30;
                xLWorksheet.Column("N").Width = 50;

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
            List<BENHNHAN> benhnhans = DataProvider.Ins.DB.BENHNHANs.ToList();
            foreach (BENHNHAN benhnhan in benhnhans)
            {
                excelDatas.Add(new ExcelData()
                {
                    CMND_CCCD = benhnhan.CMND_CCCD,
                    HO = benhnhan.HO,
                    TEN = benhnhan.TEN,
                    MABENHNHAN = benhnhan.MABENHNHAN,
                    SDT = benhnhan.SDT,
                    EMAIL = benhnhan.EMAIL,
                    DIACHI = benhnhan.DIACHI,
                    NGSINH = benhnhan.NGSINH,
                    GIOITINH = benhnhan.GIOITINH == true?"Nữ":"Nam",
                    NGNHAPVIEN = benhnhan.NGNHAPVIEN,
                    QUOCTICH = benhnhan.QUOCTICH,
                    GHICHU =  benhnhan.GHICHU,
                    TINHTRANG = benhnhan.TINHTRANG,
                    BENHNEN = benhnhan.BENHNEN
                });
            }
            return excelDatas;
        }

        private DataTable ExcelDataToDataTable(List<ExcelData> excelDatas)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("CMND/CCCD");
            dataTable.Columns.Add("Mã bệnh nhân");
            dataTable.Columns.Add("Họ và tên");
            dataTable.Columns.Add("Ngày sinh");
            dataTable.Columns.Add("Giới tính");
            dataTable.Columns.Add("Số điện thoại");
            dataTable.Columns.Add("Email");
            dataTable.Columns.Add("Địa chỉ");
            dataTable.Columns.Add("Quốc tịch");
            dataTable.Columns.Add("Ngày nhập viện");
            dataTable.Columns.Add("Bệnh nền");
            dataTable.Columns.Add("Tình trạng");
            dataTable.Columns.Add("Ghi chú");
            foreach (ExcelData excelData in excelDatas)
            {
                dataTable.Rows.Add(excelData.CMND_CCCD, 
                                    excelData.MABENHNHAN, 
                                    excelData.HO + " " + excelData.TEN, 
                                    excelData.NGSINH, 
                                    excelData.GIOITINH, 
                                    excelData.SDT,  
                                    excelData.EMAIL,
                                    excelData.DIACHI,
                                    excelData.QUOCTICH,
                                    excelData.NGNHAPVIEN,
                                    excelData.BENHNEN,
                                    excelData.TINHTRANG,
                                    excelData.GHICHU);
            }
            return dataTable;
        }

        private class ExcelData
        {
            public string CMND_CCCD { get; set; }
            public string HO { get; set; }
            public string TEN { get; set; }
            public string MABENHNHAN { get; set; }
            public string SDT { get; set; }
            public string EMAIL { get; set; }
            public string DIACHI { get; set; }
            public DateTime? NGSINH { get; set; }
            public string GIOITINH { get; set; }
            public DateTime? NGNHAPVIEN { get; set; }
            public string QUOCTICH { get; set; }
            public string GHICHU { get; set; }
            public string TINHTRANG { get; set; }
            public string BENHNEN { get; set; }
        }
    }
}
