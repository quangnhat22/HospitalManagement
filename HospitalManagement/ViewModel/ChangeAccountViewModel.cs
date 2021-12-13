using HospitalManagement.Command;
using HospitalManagement.Command.AccountCommand;
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
        private string ho;
        private string ten;
        private bool gioiTinh;
        private string email;
        private string ngaySinhDatePicker;

        public string Ho { get => ho; set => ho = value; }
        public string Ten { get => ten; set => ten = value; }
        public bool GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public string Email { get => email; set => email = value; }
        public string NgaySinhDatePicker { get => ngaySinhDatePicker; set => ngaySinhDatePicker = value; }

        public ICommand SaveChangeAccount { get; set; }
        public ICommand OpenChangePasswordWindow { get; set; }
        public ICommand ReturnAccountWindow { get; set; }

        public ChangeAccountViewModel()
        {
            using (QUANLYBENHVIENEntities db = new QUANLYBENHVIENEntities())
            {
                USER usertemp = db.USERs.Find(MainWindowViewModel.User.ID);
                if (usertemp.ROLE == "admin")
                {
                    var admin = usertemp.ADMINs.FirstOrDefault();
                    if (admin != null || admin != default)
                    {
                        Ho = admin.HO;
                        Ten = admin.TEN;
                        if (admin.NGSINH.HasValue)
                        {
                            DateTime ngsinh = (DateTime)admin.NGSINH;
                            NgaySinhDatePicker = ngsinh.ToString("dd/MM/yyyy");
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
                    if (usertemp.ROLE == "leader" || usertemp.ROLE == "doctor")
                    {
                        var doctor = usertemp.BACSIs.FirstOrDefault();
                        if (doctor != null || doctor != default)
                        {
                            Ho = doctor.HO;
                            Ten = doctor.TEN;
                            if (doctor.NGSINH.HasValue)
                            {
                                DateTime ngsinh = (DateTime)doctor.NGSINH;
                                NgaySinhDatePicker = ngsinh.ToString("dd/MM/yyyy");
                            }
                            if (doctor.GIOITINH.HasValue)
                                GioiTinh = doctor.GIOITINH.Value;
                            Email = doctor.EMAIL;
                        }
                    }
                    else
                    {
                        var nurse = usertemp.YTAs.FirstOrDefault();
                        if (nurse != null || nurse != default)
                        {
                            Ho = nurse.HO;
                            Ten = nurse.TEN;
                            if (nurse.NGSINH.HasValue)
                            {
                                DateTime ngsinh = (DateTime)nurse.NGSINH;
                                NgaySinhDatePicker = ngsinh.ToString("dd/MM/yyyy");
                            }
                            if (nurse.GIOITINH.HasValue)
                                GioiTinh = nurse.GIOITINH.Value;
                            Email = nurse.EMAIL;
                        }
                    }
                }
            }
            OpenChangePasswordWindow = new OpenChangePasswordWindowCommand();
            SaveChangeAccount = new SaveChangeAccountCommand();
            ReturnAccountWindow = new ReturnAccountWindowCommand();
        }
    }
}
