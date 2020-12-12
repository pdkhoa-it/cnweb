using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class DBIO
    {
        QLNoiThatDataContext db = new QLNoiThatDataContext();

        public List<PhanQuyen> GetDS()
        {
            List<PhanQuyen> list = new List<PhanQuyen>();
            list = db.PhanQuyens.ToList();
            return list;
        }

        public int ThemPhanQuyen(PhanQuyen p)
        {
            db.PhanQuyens.InsertOnSubmit(p);
            db.SubmitChanges();

            int ma = (from pq in db.PhanQuyens
                      where pq.Ma == p.Ma
                      select pq.Ma).FirstOrDefault();
            return ma;
        }   
        
        public void XoaPhanQuyen(int ma)
        {
            PhanQuyen p = new PhanQuyen();
            p = db.PhanQuyens.Where(c => c.Ma == ma).FirstOrDefault();
            db.PhanQuyens.DeleteOnSubmit(p);
            db.SubmitChanges();
        }
    }
}
