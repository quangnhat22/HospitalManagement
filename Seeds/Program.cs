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
            SeedUSERs();
            SeedTANG();
            SeedTo();
            //SeedsYTA();
        }

        private static void SeedTo()
        {
            
        }

        private static void SeedTANG()
        {
            List<TANG> tangList = dataProvider.DB.TANGs.ToList();

            dataProvider.DB.TANGs.RemoveRange(tangList);
            Task<int> reset = dataProvider.DB.Database.ExecuteSqlCommandAsync("DBCC CHECKIDENT ('TANG', RESEED, 0)");
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
            List<YTA> list = new List<YTA>();
            dataProvider.DB.YTAs.RemoveRange(list);

            for (int i = 0; i < 100; i++)
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
            }
        }

        static async void SeedUSERs()
        {

            List<String> USERname = new List<string> { "nimda", "test", "guest", "root", "info", "guess", "mysql", "user", "Myname" };
            List<String> Password = new List<string> { "password", "1234567890", "qwerty", "idontknow", "onetoeight", "admin", "123", "hihihi", "thisispassword", "me", "nothingelse" };

            List<USER> users = dataProvider.DB.USERs.ToList();

            dataProvider.DB.USERs.RemoveRange(users);
            Task<int> reset = dataProvider.DB.Database.ExecuteSqlCommandAsync("DBCC CHECKIDENT ('USER', RESEED, 0)");
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
            await reset;
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
        
        #endregion
    }
}
