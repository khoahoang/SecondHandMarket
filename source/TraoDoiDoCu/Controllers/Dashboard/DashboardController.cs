using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TraoDoiDoCu.Models;

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
                // Code gui tin nhan
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
        
    }
}