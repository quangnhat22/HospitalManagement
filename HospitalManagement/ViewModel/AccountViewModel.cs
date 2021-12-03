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
            if (MainWindowViewModel.User.ROLE == "admin")
            {
                var admin = MainWindowViewModel.User.ADMINs.FirstOrDefault();
                if (admin != null || admin != default)
                {
                    HoTen = admin.HO + " " + admin.TEN;
                    TenDangNhap = admin.USER.USERNAME;
                    if (admin.NGSINH.HasValue)
                    {
                        DateTime ngsinh = (DateTime)admin.NGSINH;
                        NgaySinh = ngsinh.ToString("dd/MM/yyyy");
                    }
                    if (admin.GIOITINH.HasValue)
                    {
                        GioiTinh = admin.GIOITINH.Value;
                    }
                    Email = admin.EMAIL;
                }
            }   
            else 
            {
                var leader = MainWindowViewModel.User.TOes.FirstOrDefault().TOTRUONG;
                if (leader != null || leader != default)
                {
                    HoTen = leader.HO + " " + leader.TEN;
                    TenDangNhap = leader.TO.USER.USERNAME;
                    if (leader.NGSINH.HasValue)
                    {
                        DateTime ngsinh = (DateTime)leader.NGSINH;
                        NgaySinh = ngsinh.ToString("dd/MM/yyyy");
                    }
                    if (leader.GIOITINH.HasValue)
                    {
                        GioiTinh = leader.GIOITINH.Value;
                    }
                    Email = leader.EMAIL;
                }
            }
        }
    }
}
