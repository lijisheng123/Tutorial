using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Users.Servies;
using Users.Models;
using Users;



namespace BankSystem.Controllers
{
    public class UsersController : Controller
    {
        
        S_DataAccesss Access = new S_DataAccesss();
        UserInfo uInfo = new UserInfo();
        // GET: Users
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns>接受数据并返回加验证</returns>

        #region 登录
        public ActionResult Index()
        {
            return View(uInfo);
        }
        public ActionResult CheckCode()
        {
            return File(Core.CheckCode.Code.CheckCode.RndCodeImg(), "image/gif");
        }
        [HttpPost]
       
        public ActionResult Index(UserInfo sk, FormCollection fc)
        {
            string UserInput = fc["CheckCode"] ?? string.Empty;
            string SavedCode = (string)Session["rndcode"];
            if (!UserInput.ToLower().Equals(SavedCode))
            {
                return Content("验证码输入错误！");
            }

            if (!ModelState.IsValid)
            {
                return View(sk);
            }
            //sk.LOGINPASS = ReUse.BllUtility.MD5AndSHA1.MD5Encode(sk.LOGINPASS, "32");
                if (Access.Login(sk.ACCOUNT,sk.LOGINPASS))
                {

                UserState.SaveUserState(sk);
                return RedirectToAction("MenuView", "Menu");
                }
                else
                {
                    return Content("登录失败，如有疑问请联系管理员");
                }
           
           

        }
        #endregion
        public ActionResult AdminView()
        {
            return View(uInfo);
        }
        [HttpPost]
        public ActionResult AdminView(UserInfo sk,FormCollection sd)
        {

           

            if (!ModelState.IsValid)
            {
                return View(sk);
            }
            else
            {
                
                if (sd["ACCOUNT"] =="00000000"&&sd["LOGINPASS"] =="123456")
                {
                    Session["Login"] = sk.LOGINPASS;
                    return   RedirectToAction("AdmiaView","deleat");
                }
                else
                {
                    return Content("管理员如忘记密码，请联系制作人员！！！！！");
                }
            }


        }

       
        public ActionResult RegView()
        {
            return View();
            
        }
        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Users/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
