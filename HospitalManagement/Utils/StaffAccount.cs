using HospitalManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Utils
{
    public partial class StaffAccount
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
        public int GroupName { get => groupName; set => groupName = value; }
       
        public StaffAccount()
        {
            
        }
        public static void InitAccountList()
        {
            foreach(var to in DataProvider.Ins.DB.TOes.ToList())
            {
                foreach(var bacsi in to.BACSIs.ToList())
                {
                    string userName = bacsi.TO.USER.USERNAME.ToString();
                    if (userName != "")
                    {
                        var inforUser = new StaffAccount();
                        inforUser.Cmnd_cccd = bacsi.CMND_CCCD;
                        inforUser.Ho = bacsi.HO;
                        inforUser.Ten = bacsi.TEN;
                        inforUser.Email = bacsi.EMAIL;
                        inforUser.Sdt = bacsi.SDT;
                        inforUser.UserName = userName;
                        staffAccountsList.Add(inforUser);
                    }
                }
                foreach (var yta in to.YTAs.ToList())
                {
                    string userName = yta.TO.USER.USERNAME.ToString();
                    if (userName != "")
                    {
                        var inforUser = new StaffAccount();
                        inforUser.Cmnd_cccd = yta.CMND_CCCD;
                        inforUser.Ho = yta.HO;
                        inforUser.Ten = yta.TEN;
                        inforUser.Email = yta.EMAIL;
                        inforUser.Sdt = yta.SDT;
                        inforUser.UserName = userName;
                        staffAccountsList.Add(inforUser);
                    }
                }
            }
        }
    }
}
