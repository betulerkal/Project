using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EryazProje.Models;

namespace EryazProje.Controllers
{
    public class UrunlerController : Controller
    {
        SqlConnection baglanti = new SqlConnection(ConfigurationManager.ConnectionStrings["Baglanti"].ConnectionString);

        // GET: Urunler
        public ActionResult Index()
        {
            //List<Urunler> urunler = new List<Urunler>();
            //var model =urunler.ToList();
            //if (!string.IsNullOrEmpty(ara))
            //{
            //    model = model.Where(x => x.UrunAdi.Contains(ara)).ToList();
            //}
            return View();
        }
        public JsonResult GetUrunList()
        {
            //SqlCommand komut = new SqlCommand("select UrunId, UrunAdi,UrunAciklamasi,UrunFiyati,KategoriId from Urunler", baglanti);
            SqlCommand komut = new SqlCommand("UrunListele", baglanti); //store procedure

            baglanti.Open();
            SqlDataReader rdr = komut.ExecuteReader();

            List<UrunlerveKategoriler> liste = new List<UrunlerveKategoriler>();  //bak unutma eski hali
            while (rdr.Read())
            {
                liste.Add(new UrunlerveKategoriler()
                {
                    UrunId = rdr.GetInt32(0),
                    UrunAdi = rdr.GetString(1),
                    UrunAciklamasi = rdr.IsDBNull(2) ? string.Empty : rdr.GetString(2),
                    UrunFiyati = rdr.GetDecimal(3),
                    KategoriId = rdr.GetInt32(4)

                });
            }
            baglanti.Close();
            return Json(liste, JsonRequestBehavior.AllowGet);
        }
        
        //public ActionResult YeniGoster()
        //{
        //    return PartialView("_PartialUrunEkle");
        //}
        public ActionResult UrunEkle(UrunlerveKategoriler model)
        {
            SqlCommand komut2 = new SqlCommand("select KategoriAdi from Kategoriler", baglanti);
            baglanti.Open();
            SqlDataReader rdr = komut2.ExecuteReader();
            
            List<Kategoriler> liste = new List<Kategoriler>();  //bak unutma eski hali
            while (rdr.Read())
            {
                
                liste.Add(new Kategoriler()
                {
                    KategoriAdi = rdr.GetString(1),
                    KategoriId = rdr.GetInt32(0)
                });
            }
            ViewBag.liste2 = new SelectList(liste, "ID", "Title", model.KategoriId);
            baglanti.Close();
           // ViewBag.liste = liste.ToList();


            SqlCommand komut = new SqlCommand("UrunEkle", baglanti); //store procedure
            komut.CommandType = CommandType.StoredProcedure; //sen prosedür çalıştıracaksın demek.

            komut.Parameters.AddWithValue("@UrunId", model.UrunId);
            komut.Parameters.AddWithValue("@UrunAdi", model.UrunAdi);
            komut.Parameters.AddWithValue("@UrunAciklamasi", model.UrunAciklamasi);
            komut.Parameters.AddWithValue("@UrunFiyati", model.UrunFiyati);
            komut.Parameters.AddWithValue("@KategoriId", model.KategoriId);
            
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            return RedirectToAction("Index");

        }
        public ActionResult UrunDuzenle(UrunlerveKategoriler model)
        {
            SqlCommand komut2 = new SqlCommand("select * from Kategoriler", baglanti);
            baglanti.Open();
            SqlDataReader rdr = komut2.ExecuteReader();
            List<Kategoriler> liste = new List<Kategoriler>();  //bak unutma eski hali
            while (rdr.Read())
            {

                liste.Add(new Kategoriler()
                {
                    KategoriAdi = rdr.GetString(1),
                    KategoriId = rdr.GetInt32(0)
                });
            }
            ViewBag.liste2 = new SelectList(liste, "ID", "Title", model.KategoriId);
            baglanti.Close();


            SqlCommand komut = new SqlCommand("UrunDuzenle", baglanti); //store procedure
            komut.CommandType = CommandType.StoredProcedure; //sen prosedür çalıştıracaksın demek.

            komut.Parameters.AddWithValue("@UrunId", model.UrunId);
            komut.Parameters.AddWithValue("@UrunAdi", model.UrunAdi);
            komut.Parameters.AddWithValue("@UrunAciklamasi", model.UrunAciklamasi);
            komut.Parameters.AddWithValue("@UrunFiyati", model.UrunFiyati);
            komut.Parameters.AddWithValue("@KategoriId", model.KategoriId);

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(UrunlerveKategoriler model)
        {
            SqlCommand komut2 = new SqlCommand("select * from Kategoriler", baglanti);
            baglanti.Open();
            SqlDataReader rdr = komut2.ExecuteReader();
            List<Kategoriler> liste = new List<Kategoriler>();  //bak unutma eski hali
            while (rdr.Read())
            {

                liste.Add(new Kategoriler()
                {
                    KategoriAdi = rdr.GetString(1),
                    KategoriId = rdr.GetInt32(0)
                });
            }
            ViewBag.liste2 = new SelectList(liste, "ID", "Title", model.KategoriId);
            baglanti.Close();


            SqlCommand komut = new SqlCommand("UrunSil", baglanti); //store procedure
            komut.CommandType = CommandType.StoredProcedure; //sen prosedür çalıştıracaksın demek.

            komut.Parameters.AddWithValue("@UrunId", model.UrunId);

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            return RedirectToAction("Index");
        }

        public JsonResult GetEditDeleteUrun(int UrunId)
        {
            SqlCommand komut = new SqlCommand();
            komut.CommandText = "select * from Urunler where UrunId=@UrunId";
            komut.Parameters.AddWithValue("@UrunId", UrunId);
            komut.Connection = baglanti;
            baglanti.Open();
            SqlDataReader rdr = komut.ExecuteReader();
            Urunler u = new Urunler();
            while (rdr.Read())
            {
                u.UrunId = rdr.GetInt32(0);
                u.UrunAdi = rdr.GetString(1);
                u.UrunAciklamasi = rdr.IsDBNull(2) ? string.Empty : rdr.GetString(2);
                u.UrunFiyati = rdr.GetDecimal(3);
                u.KategoriId = rdr.GetInt32(4);
            }
            baglanti.Close();
            return Json(u, JsonRequestBehavior.AllowGet);

            
        }
    }
}