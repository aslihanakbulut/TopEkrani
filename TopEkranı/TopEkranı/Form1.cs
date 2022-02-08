using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TopEkranı
{
    public partial class Form1 : Form
    {
        int ekrandaki_top_sayisi = 0;
        List<RoundedButton> toplar = new List<RoundedButton>();


        Color[] renkler = {Color.Blue,Color.Red,Color.Purple,Color.Brown,Color.DimGray };


        int sayac = 0;
        public Form1()
        {
            InitializeComponent();
        }
        void yeni_top_ekle()
        {
            Random random = new Random();
            int konum_x = random.Next(100, this.Width-100);
            int konum_y = random.Next(100, this.Height-100);

            int aci = random.Next(0, 360);

            int renk_id = ekrandaki_top_sayisi; //sirayla farkli renkte top eklenmesi saglandi
            RoundedButton yeniTop = new RoundedButton(renkler[renk_id], konum_x, konum_y, (float)aci);
            toplar.Add(yeniTop);
            ekrandaki_top_sayisi++;
            this.Controls.Add(toplar.ElementAt(ekrandaki_top_sayisi-1));

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            yeni_top_ekle();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
            if(sayac>10*10) // her 10 saniyede bir top ekleme saglandi
            {
                sayac = 0;
                if(ekrandaki_top_sayisi<5) // ekrandaki top sayisi 5 olana kadar yeni top eklenir
                {
                   yeni_top_ekle();
                }
                
            }
            foreach (var top in toplar)
            {
                top.hareketlendir(this.Height, this.Width);
            }
        }
    }
}
