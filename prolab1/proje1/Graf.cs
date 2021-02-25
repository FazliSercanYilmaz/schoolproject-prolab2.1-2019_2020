
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje1
{
    class Graf
    {
       public struct Sonuc
        {
            public int mesafe;
            public List<Sehir> gidilenPlakalar;
            public bool oluSonuc;
        }
        static int enKisa = 20000;
       public static List<List<int>> mesafeMatrisi;
      public  static List<Sehir> sehirler;
        public static List<List<Sonuc>> tumsonuc;
        static Sonuc sehreGit2(Sehir baslangic, Sehir bitis, List<Sehir> gidilenPlakalar, int mesafe, List<Sonuc> tumsonuclar,int first)
        {
            if (mesafe > enKisa)
            {
                Sonuc sonuc = new Sonuc();
                sonuc.oluSonuc = true;
                return sonuc;
            }
            /*Version 2
              double anlikMesafe = Math.Sqrt(Math.Pow(sehirler[bitis.plaka - 1].x - sehirler[baslangic.plaka - 1].x, 2)
                + Math.Pow(sehirler[bitis.plaka - 1].y - sehirler[baslangic.plaka - 1].y, 2)); 
             */
             //version 1
            double anlikMesafe = mesafeMatrisi[baslangic.plaka - 1][bitis.plaka - 1];
            //
            int sehirIndex = -1;
            List<Sonuc> sonuclar = new List<Sonuc>();
            foreach (var komsu in baslangic.komsular)
            {
                sehirIndex++;
                if (gidilenPlakalar.IndexOf(komsu) != -1)
                    continue;
                /*version 2
              if (
                Math.Sqrt(Math.Pow(sehirler[bitis.plaka - 1].x
- sehirler[komsu.plaka - 1].x, 2)
+ Math.Pow(sehirler[bitis.plaka - 1].y - sehirler[komsu.plaka - 1].y, 2)) >= anlikMesafe)
                    continue;
                */
                //version 1
                if (mesafeMatrisi[komsu.plaka - 1][bitis.plaka - 1] >= anlikMesafe)
                    continue;
                //
                int yenimesafe = mesafe + baslangic.komsuMesafeleri[sehirIndex];
                if (komsu == bitis)
                {
                    if (first == 1)
                        gidilenPlakalar.Add(baslangic);
                    gidilenPlakalar.Add(komsu);
                    Sonuc sonuc = new Sonuc();
                    sonuc.gidilenPlakalar = gidilenPlakalar;
                    sonuc.mesafe = yenimesafe;
                    if (yenimesafe < enKisa)
                    {
                        enKisa = yenimesafe;
                        sonuc.oluSonuc = false;
                  
                        tumsonuclar.Add(sonuc);
                    }
                    else
                        sonuc.oluSonuc = true;
                    return sonuc;
                }
                List<Sehir> yeniGidilenPlakalar = gidilenPlakalar.Select(item => item).ToList();
                yeniGidilenPlakalar.Add(komsu);
                Sonuc yerelSonuc = sehreGit2(komsu, bitis, yeniGidilenPlakalar, yenimesafe, tumsonuclar,0);
                if (!yerelSonuc.oluSonuc)
                    sonuclar.Add(yerelSonuc);

            }
            if (sonuclar.Count == 0)
            {
                Sonuc sonuc = new Sonuc();
                sonuc.oluSonuc = true;
                return sonuc;
            }
            int enKisaMesafe = 500000;
            int enKisaYolIndex = -1;
            for (int i = 0; i < sonuclar.Count; i++)
            {
                if (sonuclar[i].mesafe < enKisaMesafe)
                {
                    enKisaMesafe = sonuclar[i].mesafe;
                    enKisaYolIndex = i;
                }
            }
            Sonuc sonuc2 = sonuclar[enKisaYolIndex];
            sonuc2.oluSonuc = false;
            return sonuc2;

        }
       public static List<List<Sonuc>> sehiryolhesapla(List<int> girilensehirler)
        {
            List<Sehir> gidilenSehirler;
            List<Sonuc> sonuc1;
            List<List<Sonuc>> eniyisonuclar = new List<List<Sonuc>>();
            int baslangic = 40;
            gidilenSehirler = new List<Sehir>();
            List<Sonuc> minsonuc = new List<Sonuc>();
            List<Sonuc> tumSonuclar = new List<Sonuc>();
            Sonuc min;
            int i;
            int j;
            Sonuc sonuc;
            min.oluSonuc = true;
            min.mesafe = 999999;
            while (girilensehirler.Count != 0)
            {
                gidilenSehirler.Add(sehirler[baslangic]);
                for (i = 0; i < girilensehirler.Count; i++)
                {
                    enKisa = 20000;
                    tumSonuclar.Clear();
                    gidilenSehirler.Clear();
                    gidilenSehirler.Add(sehirler[baslangic]);
                    int index = 0;
                    int deger = 0;
                    int kontrolbit = 0;
                   foreach(Sehir kontrol in sehirler[baslangic].komsular)
                    {
                      if(kontrol.plaka == sehirler[girilensehirler[i]].plaka)
                        {
                            Sonuc temp;
                            temp.gidilenPlakalar = new List<Sehir>();
                            temp.gidilenPlakalar.Add(sehirler[baslangic]);
                            temp.gidilenPlakalar.Add(sehirler[girilensehirler[i]]);
                            temp.oluSonuc = false;
                            temp.mesafe = sehirler[baslangic].komsuMesafeleri[index];
                            minsonuc.Clear();
                            minsonuc.Add(temp);
                            kontrolbit = 1;
                            break;
                        }
                        index++;
                    }
                    index = 0;
                    if (kontrolbit == 1)
                    {
                        kontrolbit = 0;
                      
                        break;
                    }
                    sonuc = sehreGit2(sehirler[baslangic], sehirler[girilensehirler[i]], gidilenSehirler, 0, tumSonuclar,1);
                    ;
                    if (min.mesafe > sonuc.mesafe || min.oluSonuc == true)
                    {
                        minsonuc.Clear();
                        min = sonuc;
                        for (j = 1; j <= tumSonuclar.Count; j++)
                        {
                           /* if (j > 5)
                                break;*/
                                if(tumSonuclar.Count - j != -1)
                            minsonuc.Add(tumSonuclar[tumSonuclar.Count - j]);
                            else
                            {
                                minsonuc.Add(tumSonuclar[0]);


                            }
                        }
                    }
                }
                var c = minsonuc;
                int a = minsonuc[0].gidilenPlakalar.Last().plaka-1;
                     a = girilensehirler.IndexOf(a);
                girilensehirler.RemoveAt(a);
                ;
                eniyisonuclar.Add(new List<Sonuc>(minsonuc));
                min.oluSonuc = true;
                min.mesafe = 999999;
                
                baslangic = minsonuc[0].gidilenPlakalar.Last().plaka - 1;
                minsonuc.Clear();
            }
            gidilenSehirler.Add(sehirler[baslangic]);
            enKisa = 20000;
            tumSonuclar.Clear();
            gidilenSehirler.Clear();
            gidilenSehirler.Add(sehirler[baslangic]);
            Sonuc sonuc2 = sehreGit2(sehirler[baslangic], sehirler[40], gidilenSehirler, 0, tumSonuclar,1);

            minsonuc.Clear();
            min = sonuc2;
            for (j = 1; j <= tumSonuclar.Count; j++)
            {
                //if (j > 5)
                  //  break;
                minsonuc.Add(tumSonuclar[tumSonuclar.Count - j]);
            }

            int b = minsonuc[0].gidilenPlakalar[minsonuc[0].gidilenPlakalar.Count - 1].plaka - 1;
            eniyisonuclar.Add(new List<Sonuc>(minsonuc));
            min.oluSonuc = true;
            min.mesafe = 999999;
            minsonuc.Clear();
            return eniyisonuclar;
        }

    }
}
