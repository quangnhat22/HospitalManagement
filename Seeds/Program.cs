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
        }

        static async void SeedUSERs()
        {
            
            List<String> Ho = new List<string> { "Nguyễn", "Trần", "Bùi", "Đỗ", "Lê", "Phan", "Võ", "Lý", "Ngô" };
            List<String> Ten = new List<string> { "Anh", "Bình", "Châu", "Dũng", "Huy", "Khang", "Linh", "Mạnh", "Nghĩa" ,"Quang", "Tuấn" };
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
                user.HO = Ho[random.Next(Ho.Count)];
                user.TEN = Ten[random.Next(Ten.Count)];
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
}
