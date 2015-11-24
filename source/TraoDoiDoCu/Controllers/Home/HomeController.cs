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
            var products = from p in _DB.Products
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
            string cost = Request.QueryString["cost"];

            ViewBag.SearchString = searchStr;
            ViewBag.CurrentName = name;
            ViewBag.Position = pos;
            ViewBag.Cost = cost;

            TraoDoiDoCuEntities _DB = new TraoDoiDoCuEntities();
            var products = _DB.Products.ToList();
            if (name != "Tất cả")
                products = _BUS.GetProductsByCategoriesNameAndPos(name, pos);
            else
                products = _DB.Products.ToList();
            if (searchStr != null && searchStr != "")
                products = products.Where(p => p.Name.ToUpper().Contains(searchStr.ToUpper())).ToList();

            switch (cost)
            {
                case "Dưới 100k":
                    products = products.Where(p => p.Price < 100000).ToList();
                    break;
                case "100k - 500k":
                    products = products.Where(p => (p.Price >= 100000 && p.Price < 500000)).ToList();
                    break;
                case "500k - 2tr":
                    products = products.Where(p => (p.Price >= 500000 && p.Price < 2000000)).ToList();
                    break;
                case "2tr - 5tr":
                    products = products.Where(p => (p.Price >= 2000000 && p.Price < 5000000)).ToList();
                    break;
                case "5tr - 10tr":
                    products = products.Where(p => (p.Price >= 500000 && p.Price < 10000000)).ToList();
                    break;
                case "10tr - 50tr":
                    products = products.Where(p => (p.Price >= 10000000 && p.Price < 50000000)).ToList();
                    break;
                case "Trên 50tr":
                    products = products.Where(p => p.Price >= 50000000).ToList();
                    break;
            }

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

        public ActionResult UserProduct(string sortOrder, int? page)
        {
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.CostSortParm = sortOrder == "Cost" ? "cost_desc" : "Cost";

            string username = Request.QueryString["username"];

            ViewBag.CurrentUserName = username;

            TraoDoiDoCuEntities _DB = new TraoDoiDoCuEntities();
            var products = _DB.Products.ToList();
            int id = -1;
            foreach (var user in _DB.Users.ToList())
            {
                if (user.UserName == username)
                    id = user.ID;
            }

            if (id != -1)
            {
                products = products.Where(p => p.UserID == id).ToList();
            }

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