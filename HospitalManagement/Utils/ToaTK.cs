using HospitalManagement.Model;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HospitalManagement.Utils
{
    public class ToaTK
    {
        public static ChartValues<int> NangList;
        public static ChartValues<int> TrungBinhList;
        public static ChartValues<int> NheList;
        public ToaTK()
        {
            NangList = new ChartValues<int>();
            TrungBinhList = new ChartValues<int>();
            NheList = new ChartValues<int>();
        }

        public void thongKeBenhNhanTheoToa()
        {
            var result = from toa in DataProvider.Ins.DB.TOAs
                         join tang in DataProvider.Ins.DB.TANGs on toa.IDTOA  equals tang.IDTOA into table1
                         from tang in table1.ToList()
                         join phong in DataProvider.Ins.DB.PHONGs on tang.ID equals phong.IDTANG  into table2
                         from phong in table2.ToList()
                         join benhNhan in DataProvider.Ins.DB.BENHNHANs on phong.ID equals benhNhan.IDPHONG into table3
                         from benhNhan in table3.ToList()
                         group benhNhan by toa.IDTOA into toaGroup
                         select new
                         {
                             idToa = toaGroup.Key,
                             toaItem = toaGroup.ToList(),
                         };
            result.ToList().ForEach(to =>
            {
                int soLuongBN_Nang = 0;
                int soLuongBN_TB = 0;
                int soLuongBN_Nhe = 0;
                to.toaItem.ForEach(x =>
                {
                    if (x.TINHTRANG == "Triệu chứng trở nặng")
                        soLuongBN_Nang++;
                    else if (x.TINHTRANG == "Có triệu chứng")
                        soLuongBN_TB++;
                    else
                        soLuongBN_Nhe++;
                });
                NangList.Add(soLuongBN_Nang);
                TrungBinhList.Add(soLuongBN_TB);
                NheList.Add(soLuongBN_Nhe);
                soLuongBN_Nang = 0;
                soLuongBN_TB = 0;
                soLuongBN_Nhe = 0;
            });
        }
    }
}
