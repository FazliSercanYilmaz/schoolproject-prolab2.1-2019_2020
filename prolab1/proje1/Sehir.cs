using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje1
{
    class Sehir
    {
        public Sehir()
        {
            this.komsular = new List<Sehir>();
            this.komsuMesafeleri = new List<int>();
        }
        public string isim { get; set; }
        public int plaka { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public List<Sehir> komsular { get; set; }
        public List<int> komsuMesafeleri { get; set; }
    }
}
