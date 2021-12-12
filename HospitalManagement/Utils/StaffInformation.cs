using HospitalManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Utils
{
    public partial class StaffInformation
    {
        //staff information (bacsi, yta)
        private string cmnd_cccd;
        private string ho;
        private string ten;
        private string email;
        private string sdt;
        private string quocTich;
        private string diaChi;
        private DateTime? ngSinh;
        private bool? gioiTinh;
        private string vaiTro;
        private string chuyenKhoa;
        private string ghiChu;
        private int? idTo;
        private string phanLoai;
        //account information
        private string userName;
        #region "Prop"
        public string Cmnd_cccd { get => cmnd_cccd; set => cmnd_cccd = value; }
        public string Ho { get => ho; set => ho = value; }
        public string Ten { get => ten; set => ten = value; }
        public string Email { get => email; set => email = value; }
        public string Sdt { get => sdt; set => sdt = value; }
        public string QuocTich { get => quocTich; set => quocTich = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public DateTime? NgSinh { get => ngSinh; set => ngSinh = value; }
        public bool? GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public string VaiTro { get => vaiTro; set => vaiTro = value; }
        public string ChuyenKhoa { get => chuyenKhoa; set => chuyenKhoa = value; }
        public string GhiChu { get => ghiChu; set => ghiChu = value; }
        public int? IdTo { get => idTo; set => idTo = value; }
        public string UserName { get => userName; set => userName = value; }
        public string PhanLoai { get => phanLoai; set => phanLoai = value; }
        #endregion

        public StaffInformation()
        {

        }
        public StaffInformation(BACSI bacsi)
        {
            this.Cmnd_cccd = bacsi.CMND_CCCD;
            this.Ho = bacsi.HO;
            this.Ten = bacsi.TEN;
            this.Email = bacsi.EMAIL;
            this.Sdt = bacsi.SDT;
            this.QuocTich = bacsi.QUOCTICH;
            this.DiaChi = bacsi.DIACHI;
            this.NgSinh = bacsi.NGSINH;
            this.GioiTinh = bacsi.GIOITINH.HasValue ? bacsi.GIOITINH.Value : false;
            this.VaiTro = bacsi.VAITRO;
            this.ChuyenKhoa = bacsi.CHUYENKHOA;
            this.GhiChu = bacsi.GHICHU;
            this.IdTo = bacsi.IDTO;
            this.userName = bacsi.USER.USERNAME;
            if(bacsi.USER.ROLE == "doctor")
                this.PhanLoai = "Bác Sĩ";
            if (bacsi.USER.ROLE == "leader")
                this.PhanLoai = "Tổ Trưởng";
        }

        public StaffInformation(YTA yta)
        {
            this.Cmnd_cccd = yta.CMND_CCCD;
            this.Ho = yta.HO;
            this.Ten = yta.TEN;
            this.Email = yta.EMAIL;
            this.Sdt = yta.SDT;
            this.QuocTich = yta.QUOCTICH;
            this.DiaChi = yta.DIACHI;
            this.NgSinh = yta.NGSINH;
            this.GioiTinh = yta.GIOITINH.HasValue ? yta.GIOITINH.Value : false;
            this.VaiTro = yta.VAITRO;
            this.ChuyenKhoa = yta.CHUYENKHOA;
            this.GhiChu = yta.GHICHU;
            this.IdTo = yta.IDTO;
            this.userName = yta.USER.USERNAME;
            this.PhanLoai = "Y Tá";
        }

        public StaffInformation(ADMIN admin)
        {
            this.Cmnd_cccd = admin.ID;
            this.Ho = admin.HO;
            this.Ten = admin.TEN;
            this.Email = admin.EMAIL;
            this.Sdt = admin.SDT;
            this.QuocTich = admin.QUOCTICH;
            this.DiaChi = admin.DIACHI;
            this.NgSinh = admin.NGSINH;
            this.GioiTinh = admin.GIOITINH.HasValue ? admin.GIOITINH.Value : false;
            this.UserName = admin.USER.USERNAME;
            this.PhanLoai = "admin";
        }

        public async static Task<List<StaffInformation>> InitAccountList()
        {
            string query = @"
                        select CMND_CCCD as Cmnd_cccd, HO as Ho, TEN as Ten, EMAIL as Email,
	                           SDT as Sdt, QUOCTICH as QuocTich, DIACHI as DiaChi,
	                           NGSINH as NgSinh, GIOITINH as GioiTinh, IIF([USER].ROLE != 'leader', 'bacsi', 'leader' ) as PhanLoai ,
	                           IDTO as IdTo, USERNAME as UserName,VAITRO as VaiTro, CHUYENKHOA as ChuyenKhoa, GHICHU as GhiChu
                        FROM BACSI, [USER]
                        WHERE BACSI.IDUSER = [USER].ID
                        UNION
                        select CMND_CCCD as Cmnd_cccd, HO as Ho, TEN as Ten, EMAIL as Email,
	                           SDT as Sdt, QUOCTICH as QuocTich, DIACHI as DiaChi,
	                           NGSINH as NgSinh, GIOITINH as GioiTinh, 'yta' as PhanLoai,
	                           IDTO as IdTo, USERNAME as UserName,VAITRO as VaiTro, CHUYENKHOA as ChuyenKhoa, GHICHU as GhiChu
                        FROM YTA, [USER]
                        WHERE YTA.IDUSER = [USER].ID
                        UNION
                        select [ADMIN].ID as Cmnd_cccd, HO as Ho, TEN as Ten, EMAIL as Email,
	                           SDT as Sdt, QUOCTICH as QuocTich, DIACHI as DiaChi,
	                           NGSINH as NgSinh, GIOITINH as GioiTinh, 'admin' as PHANLOAI,
	                           null as IdTo, USERNAME as UserName,null as VaiTro, null as ChuyenKhoa, null as GhiChu
                        FROM [ADMIN], [USER]
                        WHERE [ADMIN].IDUSER = [USER].ID
                        ";
            //return await Task.Run(() =>
            //{
            //    List<StaffInformation> staffInformation = DataProvider.Ins.DB.Database.SqlQuery<StaffInformation>(query).ToList(); ;
            //    return staffInformation;
            //});
            return await DataProvider.Ins.DB.Database.SqlQuery<StaffInformation>(query).ToListAsync();
        }
    }
}
