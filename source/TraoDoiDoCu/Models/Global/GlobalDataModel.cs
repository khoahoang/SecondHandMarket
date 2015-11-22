using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraoDoiDoCu.Models
{
    public class GlobalDataModel
    {
        TraoDoiDoCuEntities tde = new TraoDoiDoCuEntities();

        public void ThemThongBao(string noiDung)
        {
            
            GlobalDatas xoaThongBao = new GlobalDatas();
            foreach (GlobalDatas globalData in tde.GlobalDatas)
            {
                if (globalData.Name.Equals("ThongBao"))
                {
                    xoaThongBao = globalData;
                }
            }

            if (xoaThongBao.Name != null)
                tde.GlobalDatas.Remove(xoaThongBao);

            GlobalDatas thongBaoMoi = new GlobalDatas();
            thongBaoMoi.Name = "ThongBao";
            thongBaoMoi.Content = noiDung;

            tde.GlobalDatas.Add(thongBaoMoi);
            tde.SaveChangesAsync();
        }

        public void XoaToanBoThongBao()
        {
            List<GlobalDatas> xoaThongBaos = new List<GlobalDatas>();
            foreach (GlobalDatas globalData in tde.GlobalDatas)
            {
                if (globalData.Name.Equals("ThongBao"))
                {
                    xoaThongBaos.Add(globalData);
                }
            }

            foreach (GlobalDatas xoaThongBao in xoaThongBaos)
            {
                tde.GlobalDatas.Remove(xoaThongBao);
            }

            tde.SaveChangesAsync();
        }

        public string LayThongBao()
        {
            foreach (GlobalDatas globalData in tde.GlobalDatas)
            {
                if (globalData.Equals("ThongBao"))
                {
                    return globalData.Content;
                }
            }

            return null;
        }
    }
}