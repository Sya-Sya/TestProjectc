using Luza_Project.Helper;
using Luza_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Luza_Project.Controllers
{
    public class LuzaController : Controller
    {
        //[HttpGet]
        //public ActionResult Luza()
        //{
        //    return View();
        //}
        //[HttpPost]
        public ActionResult Luza(LuzaModel luz)
        {
            if (ModelState.IsValid)
            {
                var chkuser = checkUser(luz);
                if (chkuser != null) 
                {
                    this.ShowPopup(1, "VERIFIED");
                    return View(luz);
                }
            }
            return View(luz);
        }

        public bool checkUser(LuzaModel luz)
        {
            var name = luz.user_name;
            var pass = luz.user_password;
            if (!string.IsNullOrEmpty(name)  || !string.IsNullOrEmpty(pass))
            {
                bool valid = false;
                valid = dbcheck(name, pass);
                if (valid)
                {
                    return true;
                }
                valid = dbcheck(name, pass);
            }
            var check = dbcheck(name, pass);
            return false;
        }

        public bool dbcheck(string name, string pass)
        {
            string sql = "sproc_test";
            sql += " @flag='test'";
            sql += ",@username=" + RepDao.FilterString(name);
            sql += ",@password=" + RepDao.FilterString(pass);
            var dbRes = RepDao.ExecSql(sql);
            if (dbRes.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}