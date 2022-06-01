using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EryazProje.Models
{
    public class Urunler
    {
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public string UrunAciklamasi { get; set; }
        public decimal UrunFiyati { get; set; }
        public int KategoriId { get; set; }
    }
}