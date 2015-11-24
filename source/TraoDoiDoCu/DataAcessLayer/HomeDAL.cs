using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraoDoiDoCu.DataAcessLayer
{
    public class HomeDAL
    {
        Models.TraoDoiDoCuEntities _DB = new Models.TraoDoiDoCuEntities();

        public int GetCatIDFromCatName(string name)
        {
            foreach (var c in _DB.Categories)
            {
                if (c.Name == name)
                    return c.ID;
            }

            return -1;
        }

        public List<Models.Product> GetProductFromCatID(int id)
        {
            var products = from p in _DB.Products
                           where p.CategoryID == id
                           select p;

            return products.ToList();
        }
    }
}