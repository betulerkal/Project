using EryazProje.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EryazProje.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection baglanti = new SqlConnection(ConfigurationManager.ConnectionStrings["Baglanti"].ConnectionString);

        // GET: Home
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                SqlCommand komut = new SqlCommand("select * from Kullanicilar where KullaniciAdi=@KullaniciAdi and Sifre=@Sifre", baglanti);
                komut.Parameters.AddWithValue("@KullaniciAdi", model.Username);
                komut.Parameters.AddWithValue("@Sifre", model.Password);
        
                baglanti.Open();

                SqlDataReader rdr = komut.ExecuteReader();
                Kullanicilar k = new Kullanicilar();
                while (rdr.Read())
                {
                    k.ID = rdr.GetInt32(0);
                    k.KullaniciAdi = rdr.GetString(1);
                }

                Session["login"] = k;
                if(k!=null)
                    return RedirectToAction("Index");
                else
                    return View();
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

       
    }
}