using HospitalManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Utils
{
    class LineChartInitialization
    {
        private static string sqlString = "SELECT COUNT(*) AS SL, MONTH(NGNHAPVIEN) AS THANG, YEAR(NGNHAPVIEN) AS NAM " +
                "FROM BENHNHAN " +
                "GROUP BY MONTH(NGNHAPVIEN), YEAR(NGNHAPVIEN) " +
                "ORDER BY YEAR(NGNHAPVIEN), MONTH(NGNHAPVIEN)";
        private int sl;
        private int thang;
        private int nam;

        public int SL { get => sl; set => sl = value; }
        public int THANG { get => thang; set => thang = value; }
        public int NAM { get => nam; set => nam = value; }

        public static List<LineChartInitialization> ChartInitialize()
        {
            List<LineChartInitialization> list = DataProvider.Ins.DB.Database.SqlQuery<LineChartInitialization>(sqlString).ToList();
            return list;
        }
    }
}
