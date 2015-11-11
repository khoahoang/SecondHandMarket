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
            
            GlobalData xoaThongBao = new GlobalData();
            foreach (GlobalData  globalData in tde.GlobalDatas)
            {
                if (globalData.Name.Equals("ThongBao"))
                {
                    xoaThongBao = globalData;
                }
            }

            if (xoaThongBao.Name != null)
                tde.GlobalDatas.Remove(xoaThongBao);

            GlobalData thongBaoMoi = new GlobalData();
            thongBaoMoi.Name = "ThongBao";
            thongBaoMoi.Content = noiDung;

            tde.GlobalDatas.Add(thongBaoMoi);
            tde.SaveChangesAsync();
        }

        public void XoaToanBoThongBao()
        {
            List<GlobalData> xoaThongBaos = new List<GlobalData>();
            foreach (GlobalData globalData in tde.GlobalDatas)
            {
                if (globalData.Name.Equals("ThongBao"))
                {
                    xoaThongBaos.Add(globalData);
                }
            }

            foreach (GlobalData xoaThongBao in xoaThongBaos)
            {
                tde.GlobalDatas.Remove(xoaThongBao);
            }

            tde.SaveChangesAsync();
        }

        public string LayThongBao()
        {
            foreach (GlobalData globalData in tde.GlobalDatas)
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