using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EryazProje.Models
{
    public class UrunlerveKategoriler
    {
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public string UrunAciklamasi { get; set; }
        public decimal UrunFiyati { get; set; }
        public int KategoriId { get; set; }
        public string KategoriAdi { get; set; }
        public List<SelectListItem> KategoriListesi { get; set; }

        public UrunlerveKategoriler()
        {
            KategoriListesi = new List<SelectListItem>();
        }

    }
}