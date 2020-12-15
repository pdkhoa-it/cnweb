using NoiThat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoiThat.Models.DBIO
{
    public class DBIO
    {
        //Kết nối Database
        MyDB mydb = new MyDB();

        //Thêm đối tượng
        public void AddObject<T>(T obj)
        {
            mydb.Set(obj.GetType()).Add(obj);
        }    

        //Xóa đối tượng
        public void DeleteOBject<T>(T obj)
        {
            mydb.Set(obj.GetType()).Remove(obj);
        }    


        //Lưu thay đổi
        public void Save()
        {
            mydb.SaveChanges();
        }

        /////////////////////////////////////////////////Bảng Phân Quyền////////////////////////////////////////////////////////
        //Lấy danh sách quyền
        public List<PhanQuyen> GetList_PhanQuyen()
        {
            return mydb.PhanQuyens.ToList();
        }

        //Lấy ra 1 quyền
        public PhanQuyen GetObject_PhanQuyen(int ma)
        {
            return mydb.PhanQuyens.Where(p => p.Ma == ma).FirstOrDefault();
        }

        //Lấy mã của 1 quyền
        public int Get_ID_PhanQuyen(string ten)
        {
            PhanQuyen p = mydb.PhanQuyens.Where(c => c.Ten == ten).FirstOrDefault();
            return p.Ma;
        }

        //////////////////////////////////////////////////Kết thúc bảng phân quyền////////////////////////////////
        

        //////////////////////////////////////////////////Bảng Tài Khoản//////////////////////////////////////////
        public List<TaiKhoanViewModel> GetList_TaiKhoan()
        {
            List<TaiKhoanViewModel> list = new List<TaiKhoanViewModel>();
            list = (from tk in mydb.TaiKhoans
                    join pq in mydb.PhanQuyens on tk.MaQuyen equals pq.Ma
                    select new TaiKhoanViewModel()
                    {
                        ID = tk.ID,
                        TenDangNhap = tk.TenDangNhap,
                        MatKhau = tk.MatKhau,
                        Salt = tk.Salt,
                        Email = tk.Email,
                        HoTen = tk.HoTen,
                        NgaySinh = tk.NgaySinh,
                        GioiTinh = tk.GioiTinh,
                        DiaChi = tk.DiaChi,
                        Sdt = tk.Sdt,
                        MaQuyen = tk.MaQuyen,
                        TenQuyen = pq.Ten
                    }).ToList();
            return list;
        }

    }
}