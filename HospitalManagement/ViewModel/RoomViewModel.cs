using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.ViewModel
{
    class RoomViewModel : BaseViewModel
    {
        public class Patient
        {
            public string ten { get; set; }
            public string tinhtrang { get; set; }
        }
        private List<Patient> lp = new List<Patient>();

        public List<Patient> LP
        {
            get { return lp; }
            set { lp = value; OnPropertyChanged("LP"); }
        }

        public RoomViewModel()
        {
            lp.Add(new Patient() { ten = "Do Phu Quang", tinhtrang = "Tot" });
            lp.Add(new Patient() { ten = "Do Phu Quang", tinhtrang = "Tot" });
            lp.Add(new Patient() { ten = "Do Phu Quang", tinhtrang = "Tot" });
            lp.Add(new Patient() { ten = "Do Phu Quang", tinhtrang = "Tot" });
            lp.Add(new Patient() { ten = "Do Phu Quang", tinhtrang = "Tot" });
            lp.Add(new Patient() { ten = "Do Phu Quang", tinhtrang = "Tot" });
            lp.Add(new Patient() { ten = "Do Phu Quang", tinhtrang = "Tot" });
            lp.Add(new Patient() { ten = "Do Phu Quang", tinhtrang = "Tot" });
        }
    }
}
