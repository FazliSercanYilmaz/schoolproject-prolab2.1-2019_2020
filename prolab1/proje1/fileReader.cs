using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje1
{
    class FileReader
    {
        public static void komsulariEkle(List<Sehir> sehirler)
        {
            var mesafeler = mesafeMatrisiniOlustur();
            string str = File.ReadAllText("sehir.txt");
            string[] satirlar = str.Split('\n');
            int sehirIndex = 0;
            foreach (string      s in satirlar)
            {
                if (s == "")
                    break;
                string[] sutunlar = s.Split(',');
                for (int i = 2; i < sutunlar.Length; i++)
                {
           
                    Sehir komsu = sehirler[SehirPlakaBul(sutunlar[i].Split('\r')[0])];
                    sehirler[sehirIndex].komsular.Add(komsu);
                    sehirler[sehirIndex].komsuMesafeleri.Add(mesafeler[sehirIndex][komsu.plaka-1]);
                }
                sehirIndex++;
            }
        }
        public static List<Sehir> sehirleriOku()
        {

            List<Sehir> sehirler = new List<Sehir>();
            string str = File.ReadAllText("sehir.txt");
            string[] satirlar = str.Split('\n');
            foreach (string s in satirlar)
            {
                if (s == "")
                    break;
                string[] sutunlar = s.Split(',');
                Sehir sehir = new Sehir();
                sehir.plaka = Convert.ToInt32(sutunlar[0]);
                sehir.isim = sutunlar[1].Trim();
                sehirler.Add(sehir);
            }
            str = File.ReadAllText("sehirPikselleri.txt");
            satirlar = str.Split('\n');
            int i = 0;
            foreach (string s in satirlar)
            {
                if (s == "")
                    break;
                string[] sutunlar = s.Split('-');
                string[] points = sutunlar[1].Split('.');
                sehirler[i].x = Convert.ToInt32(points[0]);
                sehirler[i].y = Convert.ToInt32(points[1]);
                i++;
                    }
            return sehirler;
        }
        public static List<List<int>> mesafeMatrisiniOlustur()
        {
            List<List<int>> matris = new List<List<int>>();
            string str = File.ReadAllText("mesafeler.txt");
            string[] satirlar = str.Split('\n');
            foreach (string s in satirlar)
            {
                if (s == "")
                    break;
                var li = new List<int>();
                string[] sutunlar = s.Split('\t');
                for (int j = 0; j < sutunlar.Length; j++)
                {
                    li.Add(Convert.ToInt32(sutunlar[j]));
                }
                matris.Add(li);
            }
            return matris;
        }
        public static List<int> SehirlerPlakaBul(string str)
        {
            List<int> sonuc = new List<int>();
            string[] satirlar = str.Split(' ');
            foreach (string s in satirlar)
            {
             foreach(Sehir a in Graf.sehirler)
                {
                    if (a.isim == s)
                    {
                        sonuc.Add(a.plaka - 1);
                        break;
                    }
                }
            }
            if (sonuc.Count < satirlar.Length)
            return null;
            return sonuc;
        }
        static int SehirPlakaBul(string str)
        {
           
            foreach (Sehir a in Graf.sehirler)
            {
                if (a.isim == str)
                {
                    return a.plaka-1;
                  
                }
            }
            return -1;
        }
    }
}
