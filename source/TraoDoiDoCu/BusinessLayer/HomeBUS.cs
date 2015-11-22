using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TraoDoiDoCu.DataAcessLayer;

namespace TraoDoiDoCu.BusinessLayer
{
    public class HomeBUS
    {
        HomeDAL _DAL = new HomeDAL();
        public List<Models.Product> GetProductsByCategoriesName(string name)
        {
            int id = _DAL.GetCatIDFromCatName(name);
            return _DAL.GetProductFromCatID(id);
        }

        public List<Models.Product> GetProductsByCategoriesNameAndPos(string name, string pos)
        {
            int id = _DAL.GetCatIDFromCatName(name);
            List<Models.Product> products = new List<Models.Product>();
            foreach (var p in _DAL.GetProductFromCatID(id))
            {
                if (p.Location == pos)
                    products.Add(p);
            }

            return products;
        }
    }
}