using HospitalManagement.Model;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Utils
{
    public class ToaTK
    {
        public static ChartValues<int> NangList ;
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
            List<TOA> listToa = DataProvider.Ins.DB.TOAs.ToList();
            foreach (TOA toa in listToa)
            {
                int soLuongBN_Nang = 0;
                int soLuongBN_TB = 0;
                int soLuongBN_Nhe = 0;
                List<TANG> listTang = toa.TANGs.ToList();
                foreach (TANG tang in listTang)
                {
                    List<PHONG> listPhong = tang.PHONGs.ToList();
                    foreach (PHONG phong in listPhong)
                    {
                        List<BENHNHAN> listBN = phong.BENHNHANs.ToList();
                        foreach (BENHNHAN bn in listBN)
                        {
                            if (bn.TINHTRANG == "Triệu chứng trở nặng")
                                soLuongBN_Nang++;
                            else if (bn.TINHTRANG == "Có triệu chứng")
                                soLuongBN_TB++;
                            else
                                soLuongBN_Nhe++;
                        }
                    }
                }
                NangList.Add(soLuongBN_Nang);
                TrungBinhList.Add(soLuongBN_TB);
                NheList.Add(soLuongBN_Nhe);
            }
        }
    }
}
