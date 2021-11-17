using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HospitalManagement.Command;
using HospitalManagement.Model;
using System.Windows;

namespace HospitalManagement.ViewModel
{
    internal class AccountViewModel : BaseViewModel
    {
        private string hoTen;
        private string tenDangNhap;
        private string ngaySinh;
        private bool gioiTinh;
        private string email;

        public string HoTen { get => hoTen; set => hoTen = value; }
        public string TenDangNhap { get => tenDangNhap; set => tenDangNhap = value; }
        public string NgaySinh { get => ngaySinh; set => ngaySinh = value; }
        public bool GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public string Email { get => email; set => email = value; }
        public ICommand OpenChangeAccount { get; set; }
        
        
        public AccountViewModel()
        {
            OpenChangeAccount = new OpenChangeAccoutWindowCommand();
            
            HoTen = MainWindowViewModel.User.HO + " " + MainWindowViewModel.User.TEN;
            TenDangNhap = MainWindowViewModel.User.USERNAME;

            if(MainWindowViewModel.User.NGSINH.HasValue)
            {
                DateTime ngsinh = (DateTime)MainWindowViewModel.User.NGSINH;
                NgaySinh = ngsinh.ToString("dd/MM/yyyy");
            }

            if (MainWindowViewModel.User.GIOITINH.HasValue)
            {
                GioiTinh = MainWindowViewModel.User.GIOITINH.Value;
            }
            Email = MainWindowViewModel.User.EMAIL;
        }
    }
}
