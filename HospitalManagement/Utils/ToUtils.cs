using HospitalManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Utils
{
    class ToUtils
    {
        public static TO GetTO(USER user)
        {
            if(user.ROLE == "doctor" || user.ROLE == "leader")
            {
                BACSI bacsi = user.BACSIs.FirstOrDefault();
                if(bacsi != null || bacsi != default(BACSI))
                {
                    return bacsi.TO;
                }
            }
            else if (user.ROLE == "nurse")
            {
                YTA yta = user.YTAs.FirstOrDefault();
                if (yta != null || yta != default(YTA))
                {
                    return yta.TO;
                }
            }
            return null;
        }
    }
}
