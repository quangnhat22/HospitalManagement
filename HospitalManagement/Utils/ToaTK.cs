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

        public async void thongKeBenhNhanTheoToa()
        {
            //var result = from toa in DataProvider.Ins.DB.TOAs
            //             join tang in DataProvider.Ins.DB.TANGs on toa.IDTOA  equals tang.IDTOA into table1
            //             from tang in table1.ToList()
            //             join phong in DataProvider.Ins.DB.PHONGs on tang.ID equals phong.IDTANG into table2
            //             from phong in table2.ToList()
            //             join benhNhan in DataProvider.Ins.DB.BENHNHANs on phong.ID equals benhNhan.IDPHONG into table3
            //             from benhNhan in table3.ToList()
            //             group benhNhan by toa.IDTOA into toaGroup
            //             select new
            //             {
            //                 idToa = toaGroup.Key,
            //                 toaItem = toaGroup.ToList(),
            //             };
            //var connectionToaTang = from toa in DataProvider.Ins.DB.TOAs
            //                        join tang in DataProvider.Ins.DB.TANGs on toa.IDTOA equals tang.IDTOA
            //                        select new
            //                        {
            //                            idToa = toa.IDTOA,
            //                            idTang = tang.ID
            //                        };
            //var connectionTangPhong = from tang in connectionToaTang
            //                          join phong in DataProvider.Ins.DB.PHONGs on tang.idTang equals phong.IDTANG
            //                          select new
            //                          {
            //                              idToa = tang.idToa,
            //                              idPhong = phong.ID
            //                          };
            //var result = from phong in connectionTangPhong
            //                              join benhNhan in DataProvider.Ins.DB.BENHNHANs on phong.idPhong equals benhNhan.IDPHONG
            //                              group benhNhan by phong.idToa into listBenhNhan
            //                              select new
            //                              {
            //                                  idToa = listBenhNhan.Key,
            //                                  benhNhans = listBenhNhan.ToList()
            //                              };

            //result.ToList().ForEach(to =>
            //{
            //    int soLuongBN_Nang = 0;
            //    int soLuongBN_TB = 0;
            //    int soLuongBN_Nhe = 0;
            //    to.benhNhans.ForEach(x =>
            //    {
            //        if (x.TINHTRANG == "Triệu chứng trở nặng")
            //            soLuongBN_Nang++;
            //        else if (x.TINHTRANG == "Có triệu chứng")
            //            soLuongBN_TB++;
            //        else
            //            soLuongBN_Nhe++;
            //    });
            //    NangList.Add(soLuongBN_Nang);
            //    TrungBinhList.Add(soLuongBN_TB);
            //    NheList.Add(soLuongBN_Nhe);
            //    soLuongBN_Nang = 0;
            //    soLuongBN_TB = 0;
            //    soLuongBN_Nhe = 0;
            //});
            List<Task<CategorizedPatientBuilding>> categorizedPatientBuildingTasks = new List<Task<CategorizedPatientBuilding>>();
            foreach (TOA toa in DataProvider.Ins.DB.TOAs)
            {
                var categorizedPatientBuildingTask = CategorizedPatientBuilding.CategorizedPatientBuildingFactory(toa);
                categorizedPatientBuildingTasks.Add(categorizedPatientBuildingTask);
            }
            CategorizedPatientBuilding[] categorizedPatientBuildings = await Task.WhenAll(categorizedPatientBuildingTasks);
            foreach (CategorizedPatientBuilding categorizedPatientBuilding in categorizedPatientBuildings)
            {
                NangList.Add(categorizedPatientBuilding.Nang);
                TrungBinhList.Add(categorizedPatientBuilding.Trungbinh);
                NheList.Add(categorizedPatientBuilding.Nhe);
            }
        }

        class CategorizedPatientBuilding
        {
            private int nang;
            private int trungbinh;
            private int nhe;
            private TOA toa;

            public async static Task<CategorizedPatientBuilding> CategorizedPatientBuildingFactory(TOA toa)
            {
                CategorizedPatientBuilding categorizedPatientBuilding = new CategorizedPatientBuilding();
                categorizedPatientBuilding.toa = toa;
                List<BENHNHAN> benhnhan = new List<BENHNHAN>();
                foreach (TANG tang in categorizedPatientBuilding.toa.TANGs)
                {
                    foreach (PHONG phong in tang.PHONGs)
                    {
                        benhnhan.AddRange(phong.BENHNHANs);
                    }
                }
                foreach (BENHNHAN x in benhnhan)
                {
                    if (x.TINHTRANG == "Triệu chứng trở nặng")
                        categorizedPatientBuilding.nang++;
                    else if (x.TINHTRANG == "Có triệu chứng")
                        categorizedPatientBuilding.trungbinh++;
                    else
                        categorizedPatientBuilding.nhe++;
                }
                return categorizedPatientBuilding;
            }

            public int Nang { get => nang; set => nang = value; }
            public int Trungbinh { get => trungbinh; set => trungbinh = value; }
            public int Nhe { get => nhe; set => nhe = value; }
            public TOA Toa { get => toa; set => toa = value; }
        }
    }
}
