using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Model
{

    public class Facility : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; OnPropertyChanged("IsChecked"); }
        }

        public int ID { get; set; }

        public string DisplayName { get; set; }

        public string Type { get; set; }

        public int Amount { get; set; }

        public string Unit { get; set; }

        public string ManufactureDate { get; set; }

        public string Note { get; set; }

    }
}
