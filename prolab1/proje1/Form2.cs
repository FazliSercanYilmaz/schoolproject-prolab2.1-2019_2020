using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static proje1.Graf;

namespace proje1
{
    public partial class Form2 : Form
    {
          List<ComboBox> TumSonuc { get; set; }

        public Form2()
        { 
            InitializeComponent();
            a = new List<ComboBox>();
            b = new List<Label>();
            a.Add(comboBox1);
            a.Add(comboBox2);
            a.Add(comboBox3);
            a.Add(comboBox4);
            a.Add(comboBox5);
            a.Add(comboBox6);
            a.Add(comboBox7);
            a.Add(comboBox8);
            a.Add(comboBox9);
            a.Add(comboBox10);
            a.Add(comboBox11);
            b.Add(label1);
            b.Add(label2);
            b.Add(label3);
            b.Add(label4);
            b.Add(label5);
            b.Add(label6);
            b.Add(label7);
            b.Add(label8);
            b.Add(label9);
            b.Add(label10);
            b.Add(label11);

            pictureBox2.Image = Image.FromFile("türkiyeSiyasiHaritasi.png");
            
            for (int i = 0; i < Graf.tumsonuc.Count; i++)
            {
                b[i].Visible = true;
                a[i].Visible = true;
                a[i].DropDownHeight = a[i].ItemHeight * 5;
                a[i].Items.Add("Secili Degil");
                foreach (var sonuc in Graf.tumsonuc[i])
                {
                    b[i].Text = sonuc.gidilenPlakalar[0].isim + " - "+sonuc.gidilenPlakalar[sonuc.gidilenPlakalar.Count - 1].isim;
                    
                    a[i].Items.Add(sonuc.mesafe+" KM");
                }
            }
          
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboboxchooser();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboboxchooser();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboboxchooser();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboboxchooser();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboboxchooser();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboboxchooser();
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboboxchooser();
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboboxchooser();
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboboxchooser();
        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboboxchooser();
        }

        private void comboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboboxchooser();
        }
        void comboboxchooser()
        {
            List<Sonuc> yeniSecilenler = new List<Sonuc>();
            List<int> messafeler = new List<int>();
            int index = 0;
            foreach (var sonuc in a)
            {
                if (sonuc.SelectedIndex == -1 || sonuc.SelectedIndex == 0)
                {
                    index++;
                    continue;
                }
                if (sonuc.Visible == false)
                {
                    break;
                }
                yeniSecilenler.Add(tumsonuc[index][sonuc.SelectedIndex - 1]);
                index++;
            }
           for(int i = 0; i < a.Count; i++)
            {
                var element = a[i];
                 if (element.SelectedIndex == -1 || element.SelectedIndex == 0)
                {
                    continue;
                }

                messafeler.Add(int.Parse(element.Text.ToString().Split('K')[0]));
            }
           if(messafeler.Count == Graf.tumsonuc.Count)
            {
                int toplamyol = 0;
                foreach(var sayi in messafeler)
                {
                    toplamyol += sayi;
                }
                label12.Text ="Hesaplanan Mesafe : "+toplamyol.ToString() + "KM";
                messafeler = new List<int>();
                foreach (var result in Graf.tumsonuc)
                {
                    messafeler.Add(mesafeMatrisi[result[0].gidilenPlakalar[0].plaka - 1][result[0].gidilenPlakalar[result[0].gidilenPlakalar.Count - 1].plaka - 1]);
                }
                toplamyol = 0;
                foreach (var sayi in messafeler)
                {
                    toplamyol += sayi;
                }
                label12.Text = label12.Text.ToString() + " Gercek Mesafe" + toplamyol.ToString() + "KM";
            }
            index = 0;
            fotoisle(yeniSecilenler);

        }
        void  fotoisle(List<Sonuc>  sonuclar)
        {
            // 
            var img = Bitmap.FromFile("türkiyeSiyasiHaritasi.png");
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0),14);
            Pen pengreen = new Pen(Color.FromArgb(255, 255, 0, 255), 14);
            Pen penred = new Pen(Color.FromArgb(255, 255, 0, 0), 14);
            pen.StartCap = LineCap.RoundAnchor;
            pen.EndCap = LineCap.ArrowAnchor;
            pen.DashStyle = DashStyle.Dot;
            pengreen.StartCap = LineCap.RoundAnchor;
            pengreen.EndCap = LineCap.ArrowAnchor;
            pengreen.DashStyle = DashStyle.Dot;
            penred.StartCap = LineCap.RoundAnchor;
            penred.EndCap = LineCap.ArrowAnchor;
            penred.DashStyle = DashStyle.Dot;
            using (var g = Graphics.FromImage(img))
            {
                int i = 0;
                foreach(var cizilceksehirler in sonuclar)
                {
                    if(sonuclar.IndexOf(cizilceksehirler) == sonuclar.Count - 1)
                    {
                        for (int index = 1; index < cizilceksehirler.gidilenPlakalar.Count; index++)
                        {

                            g.DrawLine(penred, new Point(cizilceksehirler.gidilenPlakalar[index - 1].x, cizilceksehirler.gidilenPlakalar[index - 1].y)
                                , new Point(cizilceksehirler.gidilenPlakalar[index].x - 10, cizilceksehirler.gidilenPlakalar[index].y - 10));

                        }
                        continue;
                    }
                   else if(a[i].SelectedIndex == 1)
                    {
                        for (int index = 1; index < cizilceksehirler.gidilenPlakalar.Count; index++)
                        {

                            g.DrawLine(pengreen, new Point(cizilceksehirler.gidilenPlakalar[index - 1].x, cizilceksehirler.gidilenPlakalar[index - 1].y)
                                , new Point(cizilceksehirler.gidilenPlakalar[index].x - 10, cizilceksehirler.gidilenPlakalar[index].y - 10));

                        }
                        i++;
                        continue;
                    }
                   
                    for (int index = 1; index < cizilceksehirler.gidilenPlakalar.Count; index++)
                    {

                        g.DrawLine(pen, new Point(cizilceksehirler.gidilenPlakalar[index-1].x, cizilceksehirler.gidilenPlakalar[index - 1].y)
                            , new Point(cizilceksehirler.gidilenPlakalar[index].x-10, cizilceksehirler.gidilenPlakalar[index].y-10));

                    }
                    i++;
                }
            }
            pictureBox2.Image = img;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}
