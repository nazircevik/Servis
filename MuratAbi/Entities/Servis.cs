using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuratAbi.Entities
{
   public class Servis
    {
        public int Id { get; set; }
        public string GelKm { get; set; }
        public string Benzin { get; set; }
        public string Not { get; set; }

        public string Plaka { get; set; }
        public string Tarih { get; set; }
        public int Bakim { get; set; }
        public int Yil { get; set; }
        public double Iscilik { get; set; }
    }
}
