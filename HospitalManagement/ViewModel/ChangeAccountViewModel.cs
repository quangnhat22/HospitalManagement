using HospitalManagement.Command;
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
        private string ho;
        private string ten;
        private string ngaySinh;
        private bool gioiTinh;
        private string email;
        private string ngaySinhDatePicker;
        
        public string Ho { get => ho; set => ho = value; }
        public string Ten { get => ten; set => ten = value; }
        public string NgaySinh { get => ngaySinh; set => ngaySinh = value; }
        public bool GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public string Email { get => email; set => email = value; }
        public string NgaySinhDatePicker { get => ngaySinhDatePicker; set => ngaySinhDatePicker = value; }

        public ICommand SaveChangeAccount { get; set; }
        

        public ChangeAccountViewModel()
        {
            db = new DataProvider();
            Ho = MainWindowViewModel.User.HO;
            Ten = MainWindowViewModel.User.TEN;

            if (MainWindowViewModel.User.NGSINH.HasValue)
            {
                DateTime ngsinh = (DateTime)MainWindowViewModel.User.NGSINH;
                NgaySinh = ngsinh.ToString("dd/MM/yyyy");
            }

            if (MainWindowViewModel.User.NGSINH.HasValue)
            {
                DateTime ngsinh = (DateTime)MainWindowViewModel.User.NGSINH;
                NgaySinhDatePicker = ngsinh.ToString("MM/dd/yyyy");
            }

            if (MainWindowViewModel.User.GIOITINH.HasValue)
            {
                GioiTinh = MainWindowViewModel.User.GIOITINH.Value;
            }
            Email = MainWindowViewModel.User.EMAIL;

            //SaveChangeAccount = new SaveChangeAccountCommand(); 
        }
    }
}
