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
        public static List<string> LabelList;
        private static QUANLYBENHVIENEntities dbContext = new QUANLYBENHVIENEntities();
        public ToaTK()
        {
            NangList = new ChartValues<int>();
            TrungBinhList = new ChartValues<int>();
            NheList = new ChartValues<int>();
            LabelList = new List<string>();
        }

        public async Task thongKeBenhNhanTheoToa(DateTime? date = null)
        {
            await Task.Run(async () =>
            {
                List<Task<CategorizedPatientBuilding>> categorizedPatientBuildingTasks = new List<Task<CategorizedPatientBuilding>>();
                foreach (TOA toa in dbContext.TOAs)
                {
                    var categorizedPatientBuildingTask = CategorizedPatientBuilding.CategorizedPatientBuildingFactory(toa, date);
                    categorizedPatientBuildingTasks.Add(categorizedPatientBuildingTask);
                }
                CategorizedPatientBuilding[] categorizedPatientBuildings = await Task.WhenAll(categorizedPatientBuildingTasks);
                foreach (CategorizedPatientBuilding categorizedPatientBuilding in categorizedPatientBuildings)
                {
                    NangList.Add(categorizedPatientBuilding.Nang);
                    TrungBinhList.Add(categorizedPatientBuilding.Trungbinh);
                    NheList.Add(categorizedPatientBuilding.Nhe);
                    LabelList.Add(categorizedPatientBuilding.Toa.DISPLAYNAME);
                }
            });
        }
    }

    public class CategorizedPatientBuilding
    {
        private int nang;
        private int trungbinh;
        private int nhe;
        private TOA toa;

        public async static Task<CategorizedPatientBuilding> CategorizedPatientBuildingFactory(TOA toa, DateTime? date = null)
        {
            if (date is null)
                date = System.DateTime.Now;
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
                if (x.NGNHAPVIEN < date)
                {
                    if (x.TINHTRANG == "Triệu chứng trở nặng")
                        categorizedPatientBuilding.nang++;
                    else if (x.TINHTRANG == "Có triệu chứng")
                        categorizedPatientBuilding.trungbinh++;
                    else
                        categorizedPatientBuilding.nhe++;
                }
            }
            return categorizedPatientBuilding;
        }

        public int Nang { get => nang; set => nang = value; }
        public int Trungbinh { get => trungbinh; set => trungbinh = value; }
        public int Nhe { get => nhe; set => nhe = value; }
        public TOA Toa { get => toa; set => toa = value; }
    }
}

