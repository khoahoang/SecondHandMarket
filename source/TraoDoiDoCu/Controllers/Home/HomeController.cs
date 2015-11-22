using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TraoDoiDoCu.BusinessLayer;
using TraoDoiDoCu.Models;
using PagedList;

namespace TraoDoiDoCu.Controllers.Home
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        HomeBUS _BUS = new HomeBUS();
        public ActionResult Index(string sortOrder, int? page)
        {
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.CostSortParm = sortOrder == "Cost" ? "cost_desc" : "Cost";

            TraoDoiDoCuEntities _DB = new TraoDoiDoCuEntities();
            var products = from p in _DB.Product
                           select p;

            switch (sortOrder)
            {
                case "date_desc":
                    products = products.OrderBy(s => s.PostingDate);
                    break;
                case "Cost":
                    products = products.OrderBy(s => s.Price);
                    break;
                case "cost_desc":
                    products = products.OrderByDescending(s => s.Price);
                    break;
                default:
                    products = products.OrderByDescending(s => s.PostingDate);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Categories(string sortOrder, int? page)
        {
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.CostSortParm = sortOrder == "Cost" ? "cost_desc" : "Cost";

            string name = Request.QueryString["name"];
            ViewBag.CurrentName = name;
            var products = _BUS.GetProductsByCategoriesName(name);

            switch (sortOrder)
            {
                case "date_desc":
                    products = products.OrderBy(s => s.PostingDate).ToList();
                    break;
                case "Cost":
                    products = products.OrderBy(s => s.Price).ToList();
                    break;
                case "cost_desc":
                    products = products.OrderByDescending(s => s.Price).ToList();
                    break;
                default:
                    products = products.OrderByDescending(s => s.PostingDate).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Search(string sortOrder, int? page)
        {
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.CostSortParm = sortOrder == "Cost" ? "cost_desc" : "Cost";

            string name = Request.QueryString["name"];
            string searchStr = Request.QueryString["searchStr"];
            string pos = Request.QueryString["pos"];

            ViewBag.CurrentName = name;
            ViewBag.SearchString = searchStr;
            ViewBag.Position = pos;
            
            TraoDoiDoCuEntities _DB = new TraoDoiDoCuEntities();
            var products = _DB.Product.ToList();
            if (name != "Tất cả")
                products = _BUS.GetProductsByCategoriesNameAndPos(name, pos);
            else
                products = _DB.Product.ToList();
            products = products.Where(p => p.Name.Contains(searchStr)).ToList();

            switch (sortOrder)
            {
                case "date_desc":
                    products = products.OrderBy(s => s.PostingDate).ToList();
                    break;
                case "Cost":
                    products = products.OrderBy(s => s.Price).ToList();
                    break;
                case "cost_desc":
                    products = products.OrderByDescending(s => s.Price).ToList();
                    break;
                default:
                    products = products.OrderByDescending(s => s.PostingDate).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }
	}
}