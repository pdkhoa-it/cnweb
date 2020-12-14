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

        public List<PhanQuyen> GetList_PhanQuyen()
        {
            return mydb.PhanQuyens.ToList();
        }

        public PhanQuyen GetObject_PhanQuyen(int ma)
        {
            return mydb.PhanQuyens.Where(p => p.Ma == ma).FirstOrDefault();
        }

        public int Get_ID_PhanQuyen(string ten)
        {
            PhanQuyen p = mydb.PhanQuyens.Where(c => c.Ten == ten).FirstOrDefault();
            return p.Ma;
        }
    }
}