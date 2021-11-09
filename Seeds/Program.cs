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
            SeedUsers();
        }

        static async void SeedUsers()
        {
            
            List<String> Ho = new List<string> { "Nguyễn", "Trần", "Bùi", "Đỗ", "Lê", "Phan", "Võ", "Lý", "Ngô" };
            List<String> Ten = new List<string> { "Anh", "Bình", "Châu", "Dũng", "Huy", "Khang", "Linh", "Mạnh", "Nghĩa" ,"Quang", "Tuấn" };
            List<String> Username = new List<string> { "nimda", "test", "guest", "root", "info", "guess", "mysql", "user", "Myname" };
            List<String> Password = new List<string> { "password", "1234567890", "qwerty", "idontknow", "onetoeight", "admin", "123", "hihihi", "thisispassword", "me", "nothingelse" };
            
            List<User> users = dataProvider.DB.Users.ToList();

            dataProvider.DB.Users.RemoveRange(users);
            Task<int> reset = dataProvider.DB.Database.ExecuteSqlCommandAsync("DBCC CHECKIDENT ('Users', RESEED, 0)");
            User admin = new User();
            admin.USERNAME = "admin";
            admin.PWD = "1";
            dataProvider.DB.Users.Add(admin);
            for (int i = 0; i < Username.Count; i++)
            {
                User user = new User();
                user.HO = Ho[random.Next(Ho.Count)];
                user.TEN = Ten[random.Next(Ten.Count)];
                user.USERNAME = Username[i]; //Unique reason
                user.EMAIL = user.USERNAME + "@gmail.com";
                user.PWD = Password[random.Next(Password.Count)];
                user.NGSINH = (new RandomDateTime(1970, 1995))?.Next();
                user.GIOITINH = random.Next(2) == 0;
                dataProvider.DB.Users.Add(user);
            }
            reset.Wait();
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
