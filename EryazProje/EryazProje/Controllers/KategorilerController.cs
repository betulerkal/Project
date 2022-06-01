using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EryazProje.Models;

namespace EryazProje.Controllers
{

    public class KategorilerController : Controller
    {
        SqlConnection baglanti = new SqlConnection(ConfigurationManager.ConnectionStrings["Baglanti"].ConnectionString);

        // GET: Kategoriler
        public ActionResult Index()
        {
            return View();
            
        }
        public JsonResult GetKategoriList()
        {
            SqlCommand komut = new SqlCommand();
            komut.CommandText = "KategoriListele";
            komut.Connection = baglanti;
            baglanti.Open();
            SqlDataReader rdr = komut.ExecuteReader();
            List<Kategoriler> liste = new List<Kategoriler>();
            while (rdr.Read())
            {
                liste.Add(new Kategoriler()
                {
                    KategoriId=rdr.GetInt32(0),
                    KategoriAdi = rdr.GetString(1)
                });
            }
            baglanti.Close();
            return Json(liste,JsonRequestBehavior.AllowGet);
        }

        //public ActionResult YeniGoster(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    return PartialView("_PartialKategoriEkle");
        //}
        [HttpPost]
        public ActionResult KategoriEkle(Kategoriler model)
        {
            SqlCommand komut = new SqlCommand("KategoriEkle", baglanti); //store procedure
            komut.CommandType = CommandType.StoredProcedure; //sen prosedür çalıştıracaksın demek.

            komut.Parameters.AddWithValue("@KategoriAdi", model.KategoriAdi);

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            return RedirectToAction("Index");

        }
        public ActionResult KategoriDuzenle(Kategoriler model)
        {
            SqlCommand komut = new SqlCommand("KategoriDuzenle", baglanti); //store procedure
            komut.CommandType = CommandType.StoredProcedure; //sen prosedür çalıştıracaksın demek.

            komut.Parameters.AddWithValue("@KategoriId", model.KategoriId);
            komut.Parameters.AddWithValue("@KategoriAdi", model.KategoriAdi);
            

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriSil(Kategoriler model)
        {
            SqlCommand komut = new SqlCommand("KategoriSil", baglanti); //store procedure
            komut.CommandType = CommandType.StoredProcedure; //sen prosedür çalıştıracaksın demek.

            komut.Parameters.AddWithValue("@KategoriId", model.KategoriId);

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            return RedirectToAction("Index");
        }

        public JsonResult GetEditDeleteKategori(int KategoriId)
        {
            SqlCommand komut = new SqlCommand();
            komut.CommandText = "select * from Kategoriler where KategoriId=@KategoriId";
            komut.Parameters.AddWithValue("@KategoriId", KategoriId);
            komut.Connection = baglanti;
            baglanti.Open();
            SqlDataReader rdr = komut.ExecuteReader();
            Kategoriler k = new Kategoriler();
            while (rdr.Read())
            {
                k.KategoriId = rdr.GetInt32(0);
                k.KategoriAdi = rdr.GetString(1);
            }
            baglanti.Close();
            return Json(k, JsonRequestBehavior.AllowGet);

            //SqlCommand komut = new SqlCommand();
            //komut.CommandText = "select KategoriAdi from Kategoriler where KategoriId=@KategoriId";
            //komut.Parameters.AddWithValue("@KategoriId", KategoriId);

            //komut.Connection = baglanti;
            //baglanti.Open();
            //string kategoriadi=(string)komut.ExecuteScalar();
            //baglanti.Close();
            //return Json(kategoriadi, JsonRequestBehavior.AllowGet);
        }
    }
}