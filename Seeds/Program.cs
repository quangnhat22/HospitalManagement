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

            SeedUSERs();
            SeedTANG();
            SeedTo();
            SeedsYTA();
            SeedsBACSI();
            Console.WriteLine("Seeds successful");
        }

        private static void CleanDatabase()
        {
            // Delete User
            List<USER> users = dataProvider.DB.USERs.ToList();
            dataProvider.DB.USERs.RemoveRange(users);
            dataProvider.DB.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('USER', RESEED, 0)");
            dataProvider.DB.SaveChanges();
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
            // Delete TANG
            List<TANG> tangList = dataProvider.DB.TANGs.ToList();
            dataProvider.DB.TANGs.RemoveRange(tangList);
            dataProvider.DB.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('TANG', RESEED, 0)");
            
        }

        private static void SeedTo()
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

        private static void SeedTANG()
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
                    yta.EMAIL = RandomInformation.GenerateEmail();
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
                    bacsi.EMAIL = RandomInformation.GenerateEmail();
                    bacsi.DIACHI = RandomInformation.GenerateAddress();
                    bacsi.GIOITINH = RandomInformation.GenerateGioiTinh();
                    bacsi.TO = to;
                    dataProvider.DB.BACSIs.Add(bacsi);
                }
            }
            dataProvider.DB.SaveChanges();
        }

        static void SeedUSERs()
        {

            List<String> USERname = new List<string> { "nimda", "test", "guest", "root", "info", "guess", "mysql", "user", "Myname" };
            List<String> Password = new List<string> { "password", "1234567890", "qwerty", "idontknow", "onetoeight", "admin", "123", "hihihi", "thisispassword", "me", "nothingelse" };

            USER admin = new USER();
            admin.USERNAME = "admin";
            admin.PASSWORD = Encryptor.Hash("1");
            dataProvider.DB.USERs.Add(admin);
            for (int i = 0; i < USERname.Count; i++)
            {
                USER user = new USER();
                user.HO = RandomInformation.GenerateHo();
                user.TEN = RandomInformation.GenerateTen();
                user.USERNAME = USERname[i]; //Unique reason
                user.EMAIL = user.USERNAME + "@gmail.com";
                user.PASSWORD = Encryptor.Hash(Password[random.Next(Password.Count)]);
                user.NGSINH = (new RandomDateTime(1970, 1995))?.Next();
                user.GIOITINH = random.Next(2) == 0;
                dataProvider.DB.USERs.Add(user);
            }
            dataProvider.DB.SaveChanges();
        }
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
        static List<string> listcccd = new List<string>();
        static Random rd = new Random();
        static List<String> Ho = new List<string> { "Nguyễn", "Trần", "Bùi", "Đỗ", "Lê", "Phan", "Võ", "Lý", "Ngô" };
        static List<String> Ten = new List<string> { "Anh", "Bình", "Châu", "Dũng", "Huy", "Khang", "Linh", "Mạnh", "Nghĩa", "Quang", "Tuấn" };
        static List<String> Email = new List<string> { "nimda", "test", "guest", "root", "info", "guess", "mysql", "user", "Myname" };
        static List<string> Province = new List<string>() { "Hồ Chí Minh", "Hà Nội", "Bình Dương", "Đồng Nai", "Kon Tum", "Bình Định", "Quãng Ngãi", "Quảng Nam", "Phú Yên" };
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
        public static string GenerateEmail()
        {
            return Email[rd.Next(Email.Count)] + "@gmail.com";
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
    }
}
