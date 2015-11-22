using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TraoDoiDoCu.Models;
using TraoDoiDoCu.DataAcessLayer;

namespace TraoDoiDoCu.Controllers.Dashboard
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            return View("Dashboard");
        }
        // GET: Dashboard
        public ActionResult ThongBao()
        {
            return View("ThongBao");
        }

        [AcceptVerbs("POST")]
        public ActionResult ThongBao(string noiDungThongBao, string nguoiNhan, FormCollection form)
        {
            if (nguoiNhan != null)
            {
                UserModel um = new UserModel();
                AccountDAL adal = new AccountDAL();

                int IDSender = adal.getIDFromUsername(User.Identity.Name);
                int IDReciver = adal.getIDFromUsername(nguoiNhan);
                um.SendMessage(IDSender, IDReciver, noiDungThongBao);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                GlobalDataModel gdm = new GlobalDataModel();
                gdm.ThemThongBao(noiDungThongBao);
                return RedirectToAction("Index", "Home");
            }
            return null;
        }

        public ActionResult XuLyTaiKhoan()
        {
            return View("XuLyTaiKhoan");
        }

        public ActionResult BlockUser(string listUser)
        {
            string html_result = "Khóa Thành Công";
            string[] users = listUser.Split(',');
            UserModel um = new UserModel();
            um.BanAccount(users.ToList());
            return Content(html_result, "text/plain");
        }

        public ActionResult DeleteUser(string listUser)
        {
            string html_result = "Xóa Thành Công";
            string[] users = listUser.Split(',');
            UserModel um = new UserModel();
            um.DeleteAccount(users.ToList());
            return Content(html_result, "text/plain");
        }

        public ActionResult CheckBanAccount()
        {
            UserModel um = new UserModel();
            um.CheckBanAccount();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Search(string term)
        {
            UserModel um = new UserModel();
            return Json(um.Search(term), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SearchUser(string username)
        {
            UserModel um = new UserModel();
            List<String> results = um.Search(username);
            String html_result = "";
            
            foreach (String result in results)
            {
                html_result += "<div class=\"row search_user_result\">";
                html_result += "<div class=\"col-md-9\">";
                html_result += "<div class=\"input-group\">";
                html_result += "<span class=\"input-group-addon\">";
                html_result += "<input id=\""+ result + "\" type=\"checkbox\" aria-label=\"...\">";
                html_result += "</span>";
                html_result += "<input id=\"test1\" type=\"text\" disabled style=\"width: 400px\" value=\"" + result + "\" class=\"form-control\" aria-label=\"...\">";
                html_result += "</div>";
                html_result += "</div>";
                html_result += "</div>";
            }
            html_result += "<br />";
            html_result += "<div class=\"row search_user_result\">";
            html_result += "<div class=\"col-md-8 col-md-offset-1\">";
            html_result += "<button type=\"button\" id=\"khoataikhoan\" class=\"btn btn-warning btn-xulytk\">Khóa Tài Khoản</button>";
            html_result += "<button type=\"button\" id=\"xoataikhoan\" class=\"btn btn-danger btn-xulytk\">Xóa Tài Khoản</button>";
            html_result += "<button type=\"button\" class=\"btn btn-info btn-xulytk\">Hủy Bỏ</button>";
            html_result += " </div>";
            html_result += "</div>";

            return Content(html_result, "text/plain");
        }
        TraoDoiDoCuEntities td = new TraoDoiDoCuEntities();

        public ActionResult Statistics()
        {
            int sl_acc = (from p in td.Users
                          select p).Count();
            ViewBag.SL_acc = sl_acc;
            var now = DateTime.Now;
            int sl_post = (from p in td.Product
                           where (p.PostingDate.Value.Month == now.Month &&
                               p.PostingDate.Value.Year == now.Year)
                           select p).Count();
            ViewBag.SL_post = sl_post;

            int sl_mess = (from p in td.Messages
                           where (p.Datepost.Value.Month == now.Month &&
                               p.Datepost.Value.Year == now.Year)
                           select p).Count();
            ViewBag.SL_mess = sl_mess;
            return View();
        }
        public ActionResult Sta_post(int Year)
        {
            Dictionary<String, Int32> info = new Dictionary<string, int>();
            int sl_post;
            if (Year == 0)
            {
                sl_post = (from p in td.Product
                           select p).Count();
                ViewBag.SSL_post = sl_post;

            }
            else
            {
                sl_post = (from p in td.Product
                           where p.PostingDate.Value.Year <= Year
                           select p
                             ).Count();
                ViewBag.SSL_post = sl_post;
            }
            if (sl_post == 0) sl_post = 1;
            for (int i = 1; i <= 12; i++)
            {
                int sll = (from p in td.Product
                           where (p.PostingDate.Value.Month == i
                           && p.PostingDate.Value.Year == Year)
                           select p).Count();

                int temp = sll * 80 / sl_post;
                if (i == 1) info.Add("Thang 1", temp);
                if (i == 2) info.Add("Thang 2", temp);
                if (i == 3) info.Add("Thang 3", temp);
                if (i == 4) info.Add("Thang 4", temp);
                if (i == 5) info.Add("Thang 5", temp);
                if (i == 6) info.Add("Thang 6", temp);
                if (i == 7) info.Add("Thang 7", temp);
                if (i == 8) info.Add("Thang 8", temp);
                if (i == 9) info.Add("Thang 9", temp);
                if (i == 10) info.Add("Thang 10", temp);
                if (i == 11) info.Add("Thang 11", temp);
                if (i == 12) info.Add("Thang 12", temp);
            }
            ViewBag.Chart_post = info;
            return View();






        }
        [HttpPost]
        public ActionResult Sta_post(string year)
        {

            Dictionary<String, Int32> info = new Dictionary<string, int>();
            int Year = Int32.Parse(year);
            int sl_post;
            if (Year == 0)
            {
                sl_post = (from p in td.Product
                           select p).Count();
                ViewBag.SSL_post = sl_post;

            }
            else
            {
                sl_post = (from p in td.Product
                           where p.PostingDate.Value.Year <= Year
                           select p
                             ).Count();
                ViewBag.SSL_post = sl_post;
            }

            if (sl_post == 0) sl_post = 1;
            for (int i = 1; i <= 12; i++)
            {
                int sll = (from p in td.Product
                           where (p.PostingDate.Value.Month == i
                           && p.PostingDate.Value.Year == Year)
                           select p).Count();

                int temp = sll * 80 / sl_post;
                if (i == 1) info.Add("Thang 1", temp);
                if (i == 2) info.Add("Thang 2", temp);
                if (i == 3) info.Add("Thang 3", temp);
                if (i == 4) info.Add("Thang 4", temp);
                if (i == 5) info.Add("Thang 5", temp);
                if (i == 6) info.Add("Thang 6", temp);
                if (i == 7) info.Add("Thang 7", temp);
                if (i == 8) info.Add("Thang 8", temp);
                if (i == 9) info.Add("Thang 9", temp);
                if (i == 10) info.Add("Thang 10", temp);
                if (i == 11) info.Add("Thang 11", temp);
                if (i == 12) info.Add("Thang 12", temp);
            }
            ViewBag.Chart_post = info;
            return View();


        }
        [HttpGet]
        public ActionResult Sta_acc(int Year)
        {

            Dictionary<String, Int32> info = new Dictionary<string, Int32>();
            if (Year == 0)
            {
                int sl_acc = (from p in td.Users
                              select p).Count();
                ViewBag.SSL_acc = sl_acc;
                int sl_admin = (from p in td.Users
                                where p.Admin == true
                                select p).Count();
                ViewBag.SSL_admin = sl_admin;
                int sl_accbaned = (from p in td.Users
                                   where p.Ban != null
                                   select p).Count();
                ViewBag.SSL_accbaned = sl_accbaned;



            }
            else
            {
                int sl_acc = (from p in td.Users
                              where p.DateRequest.Value.Year <= Year
                              select p
                              ).Count();
                ViewBag.SSL_acc = sl_acc;
                int sl_admin = (from p in td.Users
                                where p.Admin == true && p.DateRequest.Value.Year <= Year
                                select p).Count();
                ViewBag.SSL_admin = sl_admin;
                int sl_accbaned = (from p in td.Users
                                   where p.Ban != null && p.DateRequest.Value.Year <= Year
                                   select p).Count();
                ViewBag.SSL_accbaned = sl_accbaned;
            }

            int sl_acc1 = (from p in td.Users
                           where p.DateRequest.Value.Year == DateTime.Now.Year
                           select p
                             ).Count();
            if (sl_acc1 == 0) sl_acc1 = 1;
            for (int i = 1; i <= 12; i++)
            {
                int sll = (from p in td.Users
                           where (p.DateRequest.Value.Month == i
                           && p.DateRequest.Value.Year == DateTime.Now.Year)
                           select p).Count();
                int temp = sll * 80 / sl_acc1;
                if (i == 1) info.Add("Thang 1", temp);
                if (i == 2) info.Add("Thang 2", temp);
                if (i == 3) info.Add("Thang 3", temp);
                if (i == 4) info.Add("Thang 4", temp);
                if (i == 5) info.Add("Thang 5", temp);
                if (i == 6) info.Add("Thang 6", temp);
                if (i == 7) info.Add("Thang 7", temp);
                if (i == 8) info.Add("Thang 8", temp);
                if (i == 9) info.Add("Thang 9", temp);
                if (i == 10) info.Add("Thang 10", temp);
                if (i == 11) info.Add("Thang 11", temp);
                if (i == 12) info.Add("Thang 12", temp);
            }
            ViewBag.Chart_acc = info;
            return View();

        }

        [HttpPost]
        public ActionResult Sta_acc(string year)
        {
            Dictionary<String, Int32> info = new Dictionary<string, int>();
            //tmp = info.Keys.ToList();
            //info[tmp[i]] = 0;



            int Year = Int32.Parse(year);


            int sl_acc = (from p in td.Users
                          where p.DateRequest.Value.Year <= Year
                          select p
                          ).Count();
            ViewBag.SSL_acc = sl_acc;
            int sl_admin = (from p in td.Users
                            where p.Admin == true && p.DateRequest.Value.Year <= Year
                            select p).Count();
            ViewBag.SSL_admin = sl_admin;
            int sl_accbaned = (from p in td.Users
                               where p.Ban != null && p.DateRequest.Value.Year <= Year
                               select p).Count();
            ViewBag.SSL_accbaned = sl_accbaned;
            if (sl_acc == 0) sl_acc = 1;
            for (int i = 1; i <= 12; i++)
            {
                int sll = (from p in td.Users
                           where (p.DateRequest.Value.Month == i
                           && p.DateRequest.Value.Year == Year)
                           select p).Count();

                int temp = sll * 80 / sl_acc;
                if (i == 1) info.Add("Thang 1", temp);
                if (i == 2) info.Add("Thang 2", temp);
                if (i == 3) info.Add("Thang 3", temp);
                if (i == 4) info.Add("Thang 4", temp);
                if (i == 5) info.Add("Thang 5", temp);
                if (i == 6) info.Add("Thang 6", temp);
                if (i == 7) info.Add("Thang 7", temp);
                if (i == 8) info.Add("Thang 8", temp);
                if (i == 9) info.Add("Thang 9", temp);
                if (i == 10) info.Add("Thang 10", temp);
                if (i == 11) info.Add("Thang 11", temp);
                if (i == 12) info.Add("Thang 12", temp);
            }
            ViewBag.Chart_acc = info;
            return View();


        }
        public ActionResult Sta_mess(int Year)
        {
            Dictionary<String, Int32> info = new Dictionary<string, int>();
            int sl_mess;
            if (Year == 0)
            {
                sl_mess = (from p in td.Messages
                           select p).Count();
                ViewBag.SSL_mess = sl_mess;

            }
            else
            {
                sl_mess = (from p in td.Messages
                           where p.Datepost.Value.Year <= Year
                           select p
                              ).Count();
                ViewBag.SSL_post = sl_mess;
            }

            if (sl_mess == 0) sl_mess = 1;
            for (int i = 1; i <= 12; i++)
            {
                int sll = (from p in td.Messages
                           where (p.Datepost.Value.Month == i
                           && p.Datepost.Value.Year == Year)
                           select p).Count();

                int temp = sll * 80 / sl_mess;
                if (i == 1) info.Add("Thang 1", temp);
                if (i == 2) info.Add("Thang 2", temp);
                if (i == 3) info.Add("Thang 3", temp);
                if (i == 4) info.Add("Thang 4", temp);
                if (i == 5) info.Add("Thang 5", temp);
                if (i == 6) info.Add("Thang 6", temp);
                if (i == 7) info.Add("Thang 7", temp);
                if (i == 8) info.Add("Thang 8", temp);
                if (i == 9) info.Add("Thang 9", temp);
                if (i == 10) info.Add("Thang 10", temp);
                if (i == 11) info.Add("Thang 11", temp);
                if (i == 12) info.Add("Thang 12", temp);
            }
            ViewBag.Chart_mess = info;
            return View();
        }
        [HttpPost]
        public ActionResult Sta_mess(string year)
        {
            Dictionary<String, Int32> info = new Dictionary<string, int>();
            int sl_mess;
            int Year = Int32.Parse(year);

            if (Year == 0)
            {
                sl_mess = (from p in td.Messages
                           select p).Count();
                ViewBag.SSL_mess = sl_mess;

            }
            else
            {
                sl_mess = (from p in td.Messages
                           where p.Datepost.Value.Year <= Year
                           select p
                              ).Count();
                ViewBag.SSL_mess = sl_mess;
            }

            if (sl_mess == 0) sl_mess = 1;
            for (int i = 1; i <= 12; i++)
            {
                int sll = (from p in td.Messages
                           where (p.Datepost.Value.Month == i
                           && p.Datepost.Value.Year == Year)
                           select p).Count();

                int temp = sll * 80 / sl_mess;
                if (i == 1) info.Add("Thang 1", temp);
                if (i == 2) info.Add("Thang 2", temp);
                if (i == 3) info.Add("Thang 3", temp);
                if (i == 4) info.Add("Thang 4", temp);
                if (i == 5) info.Add("Thang 5", temp);
                if (i == 6) info.Add("Thang 6", temp);
                if (i == 7) info.Add("Thang 7", temp);
                if (i == 8) info.Add("Thang 8", temp);
                if (i == 9) info.Add("Thang 9", temp);
                if (i == 10) info.Add("Thang 10", temp);
                if (i == 11) info.Add("Thang 11", temp);
                if (i == 12) info.Add("Thang 12", temp);
            }
            ViewBag.Chart_mess = info;
            return View();
        }
        
    }
}