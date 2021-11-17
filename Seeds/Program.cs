using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seeds
{
    internal class Program
    {
        static Random random = new Random();
        static DataProvider dataProvider = new DataProvider();

        static void Main(string[] args)
        {
            CleanDatabase();

            SeedsUSERs();
            Console.WriteLine("Seeds User successful");
            SeedsTANG();
            Console.WriteLine("Seeds TANG successful");
            SeedsTo();
            Console.WriteLine("Seeds TO successful");
            SeedsYTA();
            Console.WriteLine("Seeds YTA successful");
            SeedsBACSI();
            Console.WriteLine("Seeds BACSI successful");
            SeedsPHONG();
            Console.WriteLine("Seeds PHONG successful");
            SeedsBENHNHAN();
            Console.WriteLine("Seeds BENHNHAN successful");
            SeedsVATTU();
            Console.WriteLine("Seeds VATTU successful");
            Console.WriteLine("Seeds successful");
            VATTU vATTU = new VATTU();
        }

        private static void CleanDatabase()
        {
            // Delete User
            List<USER> users = dataProvider.DB.USERs.ToList();
            dataProvider.DB.USERs.RemoveRange(users);
            dataProvider.DB.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('USER', RESEED, 0)");
            dataProvider.DB.SaveChanges();
            // Delete VATTU
            List<VATTU> vtList = dataProvider.DB.VATTUs.ToList();
            foreach (VATTU vattu in vtList)
            {
                vattu.BENHNHANs.Clear();
                vattu.TOes.Clear();
            }
            dataProvider.DB.VATTUs.RemoveRange(vtList);
            dataProvider.DB.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('VATTU', RESEED, 0)");
            // Detele YTA
            List<YTA> ytalist = dataProvider.DB.YTAs.ToList();
            dataProvider.DB.YTAs.RemoveRange(ytalist);
            dataProvider.DB.SaveChanges();
            // Delete BACSI
            List<BACSI> bacsilist = dataProvider.DB.BACSIs.ToList();
            dataProvider.DB.BACSIs.RemoveRange(bacsilist);
            dataProvider.DB.SaveChanges();
            // Delete TO
            List<TO> toList = dataProvider.DB.TOes.ToList();
            dataProvider.DB.TOes.RemoveRange(toList);
            dataProvider.DB.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('TO', RESEED, 0)");
            // Delete BENHNHAN
            List<BENHNHAN> bnList = dataProvider.DB.BENHNHANs.ToList();
            dataProvider.DB.BENHNHANs.RemoveRange(bnList);
            // Delete PHONG
            List<PHONG> phong = dataProvider.DB.PHONGs.ToList();
            dataProvider.DB.PHONGs.RemoveRange(phong);
            dataProvider.DB.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('PHONG', RESEED, 0)");
            // Delete TANG
            List<TANG> tangList = dataProvider.DB.TANGs.ToList();
            dataProvider.DB.TANGs.RemoveRange(tangList);
            dataProvider.DB.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('TANG', RESEED, 0)");
            
        }

        #region seeds medthod
        private static void SeedsTo()
        {
            
            List<TANG> ts = dataProvider.DB.TANGs.ToList();
            
            foreach(TANG tang in ts)
            {
                for(int i = 0; i < 3; i++)
                {
                    TO t = new TO();
                    t.TANG = tang;
                    dataProvider.DB.TOes.Add(t);
                }
            }
            dataProvider.DB.SaveChanges();
        }
        private static void SeedsTANG()
        {
            for(int i = 0; i < 6; i++)
            {
                TANG tang = new TANG();
                tang.SOTANG = i + 1;
                tang.SLPHONG = 10;
                dataProvider.DB.TANGs.Add(tang);
            }
            dataProvider.DB.SaveChanges();
        }
        private static void SeedsYTA()
        {
            List<TO> toList = dataProvider.DB.TOes.ToList();
            foreach(TO to in toList)
            {
                for(int i = 0; i < 2; i++)
                {
                    YTA yta = new YTA();
                    yta.CMND_CCCD = RandomInformation.GenerateCCCD();
                    yta.HO = RandomInformation.GenerateHo();
                    yta.TEN = RandomInformation.GenerateTen();
                    yta.SDT = RandomInformation.GenerateSDT();
                    yta.NGSINH = RandomInformation.GenerateDate(1970, 1995);
                    yta.QUOCTICH = "Việt Nam";
                    yta.EMAIL = RandomInformation.GenerateEmail(yta.HO, yta.TEN);
                    yta.DIACHI = RandomInformation.GenerateAddress();
                    yta.GIOITINH = RandomInformation.GenerateGioiTinh();
                    yta.TO = to;
                    dataProvider.DB.YTAs.Add(yta);
                }
            }
            dataProvider.DB.SaveChanges();
        }
        private static void SeedsBACSI()
        {
            List<TO> toList = dataProvider.DB.TOes.ToList();
            foreach (TO to in toList)
            {
                for (int i = 0; i < 2; i++)
                {
                    BACSI bacsi = new BACSI();
                    bacsi.CMND_CCCD = RandomInformation.GenerateCCCD();
                    bacsi.HO = RandomInformation.GenerateHo();
                    bacsi.TEN = RandomInformation.GenerateTen();
                    bacsi.SDT = RandomInformation.GenerateSDT();
                    bacsi.NGSINH = RandomInformation.GenerateDate(1970, 1995);
                    bacsi.QUOCTICH = "Việt Nam";
                    bacsi.EMAIL = RandomInformation.GenerateEmail(bacsi.HO, bacsi.TEN);
                    bacsi.DIACHI = RandomInformation.GenerateAddress();
                    bacsi.GIOITINH = RandomInformation.GenerateGioiTinh();
                    bacsi.TO = to;
                    dataProvider.DB.BACSIs.Add(bacsi);
                }
            }
            dataProvider.DB.SaveChanges();
        }
        private static void SeedsUSERs()
        {

            List<String> Password = new List<string> { "password", "1234567890", "qwerty", "idontknow" };
            USER admin = new USER();
            admin.USERNAME = "admin";
            admin.PASSWORD = Encryptor.Hash("1");
            dataProvider.DB.USERs.Add(admin);
            for (int i = 0; i < 4; i++)
            {
                USER user = new USER();
                user.HO = RandomInformation.GenerateHo();
                user.TEN = RandomInformation.GenerateTen();
                user.USERNAME = RandomInformation.GenerateUsername(user.HO, user.TEN); //Unique reason
                user.EMAIL = user.USERNAME + "@gmail.com";
                user.PASSWORD = Encryptor.Hash(Password[i]);
                user.NGSINH = (new RandomDateTime(1970, 1995))?.Next();
                user.GIOITINH = random.Next(2) == 0;
                dataProvider.DB.USERs.Add(user);
            }
            dataProvider.DB.SaveChanges();
        }
        private static void SeedsPHONG()
        {
            List<TANG> ts = dataProvider.DB.TANGs.ToList();
            
            foreach(TANG tang in ts)
            {
                for(int i = 0; i < tang.SLPHONG; i++)
                {
                    PHONG p = new PHONG();
                    p.SOPHONG = i + 1;
                    p.SUCCHUA = random.Next(2, 5) * 2;
                    p.TANG = tang;
                    dataProvider.DB.PHONGs.Add(p);
                }
            }
            dataProvider.DB.SaveChanges();
        }
        private static void SeedsBENHNHAN()
        {
            List<PHONG> phongList = dataProvider.DB.PHONGs.ToList();
            List<string> tinhTrang = new List<string>() {"Không triệu chứng", "Có triệu chứng", "Triệu chứng trở nặng" };
            List<string> benhNen = new List<string>() { "Cao huyết áp", "Viêm phổi", "Đau dạ dày", "Béo phì" };
            foreach (PHONG p in phongList)
            {
                int succhua = 0;
                if (p.SUCCHUA != null)
                    succhua = (int)p.SUCCHUA;
                for (int i = 0; i < random.Next(succhua); i++)
                {
                    BENHNHAN bn = new BENHNHAN();
                    bn.CMND_CCCD = RandomInformation.GenerateCCCD();
                    bn.HO = RandomInformation.GenerateHo();
                    bn.TEN = RandomInformation.GenerateTen();
                    bn.MABENHNHAN = "BN" + random.Next(10000, 50000).ToString();
                    bn.SDT = RandomInformation.GenerateSDT();
                    bn.EMAIL = RandomInformation.GenerateEmail(bn.HO, bn.TEN);
                    bn.DIACHI = RandomInformation.GenerateAddress();
                    bn.GIOITINH = RandomInformation.GenerateGioiTinh();
                    bn.NGSINH = RandomInformation.GenerateDate(1960, 2000);
                    bn.GIUONGBENH = (i + 1).ToString();
                    bn.NGNHAPVIEN = RandomInformation.GenerateDate(2020, 2021);
                    bn.QUOCTICH = "Việt Nam";
                    bn.TINHTRANG = tinhTrang[random.Next(tinhTrang.Count)];
                    bn.BENHNEN = benhNen[random.Next(benhNen.Count)];
                    bn.PHONG = p;
                    dataProvider.DB.BENHNHANs.Add(bn);
                }
            }
            dataProvider.DB.SaveChanges();
        }
        private static void SeedsVATTU()
        {
            List<string> thuocList = new List<string>() { "Paracetamol",  "Oresol", "Vitamin",
                                                          "Natri clorit", "Dexamethason", "Prednisolon",
                                                          "Rivaroxaban", "Apixaban" };
            List<string> thietbiList = new List<string> { "Kit xét nghiệm nhanh", "Máy thở",
                                                          "Hệ thống ECMO", "Máy phun khử khuẩn",
                                                           "Hệ thống Oxy", "Máy theo dõi bệnh nhân"};
            List<TO> ts = dataProvider.DB.TOes.ToList();
            List<BENHNHAN> bs = dataProvider.DB.BENHNHANs.ToList();
            for (int i = 0; i < 100; i++)
            {
                VATTU vt = new VATTU();
                if (random.Next(2) == 0) // Thuoc
                {
                    vt.DISPLAYNAME = thuocList[random.Next(thuocList.Count)];
                    vt.LOAIVATTU = "Thuốc";
                    vt.DVTINH = "Viên";
                    vt.NGSX = RandomInformation.GenerateDate(2020, 2021);
                    vt.SLUONG = random.Next(500, 5000);
                    vt.GHICHU = "Thời hạn sử dụng là " + random.Next(1, 5).ToString() + " năm kể từ ngày sản xuất";
                }
                else
                {
                    vt.DISPLAYNAME = thietbiList[random.Next(thietbiList.Count)];
                    vt.LOAIVATTU = "Thiết bị";
                    vt.DVTINH = "Máy";
                    vt.NGSX = RandomInformation.GenerateDate(2010, 2021);
                    vt.SLUONG = random.Next(50, 500);
                }
                foreach(TO to in ts)
                {
                    if(random.Next(2) == 0)
                    {
                        vt.TOes.Add(to);
                    }
                }

                foreach(BENHNHAN bn in bs)
                {
                    vt.BENHNHANs.Add(bn);
                }
                dataProvider.DB.VATTUs.Add(vt);
            }
            dataProvider.DB.SaveChanges();
        }
        #endregion
    }
    class RandomDateTime
    {
        DateTime start;
        DateTime end;
        static Random gen = new Random();
        int range;

        public RandomDateTime(int yearstart, int yearend)
        {
            start = new DateTime(yearstart, 1, 1);
            end = new DateTime(yearend, 12, 31);
            range = (end - start).Days;
        }
        public DateTime Next()
        {
            return start.AddDays(gen.Next(range)).AddHours(gen.Next(0, 24)).AddMinutes(gen.Next(0, 60)).AddSeconds(gen.Next(0, 60));
        }
    }


    class RandomInformation
    {
        #region randomValue
        static List<string> listcccd = new List<string>();
        static Random rd = new Random();
        static List<String> Ho = new List<string> { "Nguyễn", "Trần", "Bùi", "Đỗ", "Lê", "Phan", "Võ", "Lý", "Ngô" };
        static List<String> Ten = new List<string> { "Anh", "Bình", "Châu", "Dũng", "Huy", "Khang", "Linh", "Mạnh", "Nghĩa", "Quang", "Tuấn" };
        static List<String> Email = new List<string> { "nimda", "test", "guest", "root", "info", "guess", "mysql", "user", "Myname" };
        static List<string> Province = new List<string>() { "Hồ Chí Minh", "Hà Nội", "Bình Dương", "Đồng Nai", "Kon Tum", "Bình Định", "Quãng Ngãi", "Quảng Nam", "Phú Yên" };
        #endregion
        #region generatemethod
        public static string GenerateCCCD()
        {
            bool unique = false;
            string cccd = string.Empty;
            do
            {   
                for (int i = 0; i < 11; i++)
                {
                    cccd += rd.Next(10).ToString();
                }
                if(!listcccd.Contains(cccd))
                {
                    unique = true;
                }
                else
                {
                    cccd = String.Empty;
                }
            }
            while(!unique);
            return cccd;
        }
        public static string GenerateHo()
        {
            return Ho[rd.Next(Ho.Count)];
        }
        public static string GenerateTen()
        {
            return Ten[rd.Next(Ten.Count)];
        }

        public static string GenerateSDT()
        {
            string std = String.Empty;
            for (int i = 0; i < 11; i++)
            {
                std += rd.Next(10).ToString();
            }
            return std;
        }

        public static DateTime GenerateDate(int start, int end)
        {
            RandomDateTime randomDateTime = new RandomDateTime(start, end);
            if(randomDateTime != null)
            {
                return randomDateTime.Next();
            }
            else
            {
                return new DateTime(1970,1,1);
            }
        }
        public static string GenerateEmail(string Ho, string Ten)
        {
            return GenerateUsername(Ho, Ten) + "@gmail.com";
        }
        public static string GenerateUsername(string Ho, string Ten)
        {
            return convertToUnSign2(Ho + Ten) + rd.Next(10000).ToString();
        }
        public static string GenerateAddress()
        {
            return Province[rd.Next(Province.Count)];
        }
        public static bool GenerateGioiTinh()
        {
            return rd.Next(2) == 1;
        }

        #endregion
        #region utils
        public static string convertToUnSign2(string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                System.Globalization.UnicodeCategory uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }
            sb = sb.Replace('Đ', 'D');
            sb = sb.Replace('đ', 'd');
            return (sb.ToString().Normalize(NormalizationForm.FormD));
        }
        #endregion
    }


}
