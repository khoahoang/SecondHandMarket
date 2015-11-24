using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TraoDoiDoCu.Models;
using PagedList;
using PagedList.Mvc;

namespace TraoDoiDoCu.Controllers.Product
{
    public class ProductController : Controller
    {
        TraoDoiDoCuEntities td = new TraoDoiDoCuEntities();
        // GET: Product/Details/5
        public ActionResult Details(int id, int? page)
        {
            ViewBag.page = page;
            ViewBag.id = id;
            var product = td.Products
                            .Where(p => p.ID == id)
                            .FirstOrDefault();
            if (product != null)
            {
                ViewBag.PosterName = product.User.LastName + " " + product.User.FirstName;
                ViewBag.CatName = product.Category.Name;
                return View(product);
            }
            return HttpNotFound();
        }

        public PartialViewResult PosterInfoPartial(int posterID)
        {
            var poster = td.Users
                            .Where(p => p.ID == posterID)
                            .FirstOrDefault();
            return PartialView(poster);
        }

        public PartialViewResult ProductImagePartial(int productID)
        {
            return PartialView(td.ProductImages.Where(p => p.ProductID == productID).OrderBy(x => x.ID).ToList());
        }

        public PartialViewResult ProductCommentPartial(int productID, int? page)
        {
            ViewBag.productID = productID;
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            return PartialView(td.Comments.Where(p => p.ProductID == productID).OrderBy(x => x.ID).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public PartialViewResult SubmitComment(int productID)
        {
            ViewBag.productID = productID;
            ViewBag.UserID = new SelectList(td.Users.ToList().OrderBy(n => n.UserName), "ID", "UserName");
            ViewBag.ProductID = new SelectList(td.Products.ToList().OrderBy(n => n.ID), "ID", "Name");
            return PartialView();
        }

        [HttpPost]
        [ValidateInput(false)]
        public PartialViewResult SubmitComment(Comment comment, int productID)
        {
            ViewBag.productID = productID;
            if (ModelState.IsValid)
            {
                comment.PostingDate = DateTime.Now;
                comment.ProductID = productID;
                var u = (from d in td.Users
                         where d.UserName == User.Identity.Name
                         select new { d.ID }).SingleOrDefault();
                comment.UserID = u.ID;
                td.Comments.Add(comment);
                td.SaveChanges();
            }
            return PartialView();
        }
    }
}
