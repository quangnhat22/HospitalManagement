using HospitalManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.ViewModel
{
    internal class ChangeAccountViewModel : BaseViewModel
    {
        public DataProvider db;
        public ICommand SaveChangeAccount { get; set; }
        public ChangeAccountViewModel()
        {
            db = new DataProvider();    
            //SaveChangeAccount = 
        }
    }
}
