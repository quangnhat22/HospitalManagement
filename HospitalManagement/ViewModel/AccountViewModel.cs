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
                if (MainWindowViewModel.User.ROLE == "leader"|| MainWindowViewModel.User.ROLE == "doctor")
                {
                    var doctor = MainWindowViewModel.User.BACSIs.FirstOrDefault();
                    if (doctor != null || doctor != default)
                    {
                        HoTen = doctor.HO + " " + doctor.TEN;
                        TenDangNhap = doctor.USER.USERNAME;
                        if (doctor.NGSINH.HasValue)
                        {
                            DateTime ngsinh = (DateTime)doctor.NGSINH;
                            NgaySinh = ngsinh.ToString("dd/MM/yyyy");
                        }
                        if (doctor.GIOITINH.HasValue)
                        {
                            GioiTinh = doctor.GIOITINH.Value;
                        }
                        Email = doctor.EMAIL;
                    }
                }
                else
                {
                    var nurse = MainWindowViewModel.User.YTAs.FirstOrDefault();
                    if (nurse != null || nurse != default)
                    {
                        HoTen = nurse.HO + " " + nurse.TEN;
                        TenDangNhap = nurse.USER.USERNAME;
                        if (nurse.NGSINH.HasValue)
                        {
                            DateTime ngsinh = (DateTime)nurse.NGSINH;
                            NgaySinh = ngsinh.ToString("dd/MM/yyyy");
                        }
                        if (nurse.GIOITINH.HasValue)
                        {
                            GioiTinh = nurse.GIOITINH.Value;
                        }
                        Email = nurse.EMAIL;
                    }
                }
            }
        }
    }
}
