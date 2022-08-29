using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuratAbi.Entities
{
 public   class Parca
    {
        public int Id { get; set; }
        public string ParcaAciklama { get; set; }
        public double Tutar { get; set; }
        public int Adet { get; set; }
        public int servisId { get; set; }
    }
}
