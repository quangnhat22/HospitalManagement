using HospitalManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Utils
{
    public class StaffAccount
    {
        private string cmnd_cccd;
        private string ho;
        private string ten;
        private string email;
        private string sdt;
        private string userName;
        //private string password;
        private int groupName;
        public static List<StaffAccount> staffAccountsList= new List<StaffAccount>();

        public string Cmnd_cccd { get => cmnd_cccd; set => cmnd_cccd = value; }
        public string Ho { get => ho; set => ho = value; }
        public string Ten { get => ten; set => ten = value; }
        public string Email { get => email; set => email = value; }
        public string Sdt { get => sdt; set => sdt = value; }
        public string UserName { get => userName; set => userName = value; }
        //public string Password { get => password; set => password = value; }
        public int GroupName { get => groupName; set => groupName = value; }
       
        public StaffAccount()
        {
            
        }
        public static void InitAccountList()
        {
            var result = from toTruc in DataProvider.Ins.DB.TOes
                         join bacSi in DataProvider.Ins.DB.BACSIs on toTruc.IDTOTRUONG equals bacSi.CMND_CCCD
                         join user in DataProvider.Ins.DB.USERs on toTruc.IDUSER equals user.ID
                         select new
                         {
                             CMND_CCCD = bacSi.CMND_CCCD,
                             Ho = bacSi.HO,
                             Ten = bacSi.TEN,
                             Email = bacSi.EMAIL,
                             SDT = bacSi.SDT,
                             UserName = user.USERNAME,
                             GroupName = toTruc.ID,
                         };
            result.ToList().ForEach(s =>
            {
                StaffAccount newAccount = new StaffAccount();
                newAccount.Cmnd_cccd = s.CMND_CCCD;
                newAccount.Ho = s.Ho;
                newAccount.Ten = s.Ten;
                newAccount.Email = s.Email;
                newAccount.Sdt = s.SDT;
                newAccount.UserName = s.UserName;
                newAccount.GroupName = s.GroupName;
                staffAccountsList.Add(newAccount);
            });
        }
    }
}
