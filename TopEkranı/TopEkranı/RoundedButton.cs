using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace TopEkranı
{

    class RoundedButton : Button
    {
        private int borderSize = 0;
        private int borderRadius = 30;
        private float hareket_acisi = 0;
        private Color borderColor = Color.Transparent;

        public int BorderSize
        {
            get { return borderSize; }
            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }

        public int BorderRadius
        {
            get { return borderRadius; }
            set
            {
                borderRadius = value;
                this.Invalidate();
            }
        }

        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }


        public Color BackgroundColor
        {
            get { return this.BackColor; }
            set { this.BackColor = value; }
        }
        public RoundedButton(Color renk,int x, int y, float hareket_acisi)
        {

            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(60, 60);
            this.BackColor = renk;
            this.ForeColor = Color.White;
            this.Resize += new EventHandler(Button_Resize);
            this.Text = "";

            this.Location = new Point(x, y);
            this.hareket_acisi = hareket_acisi;
            this.Show();
        }
        private GraphicsPath GetFigurePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);


            Rectangle rectSurface = this.ClientRectangle;
            Rectangle rectBorder = Rectangle.Inflate(rectSurface, -borderSize, -borderSize);
            int smoothSize = 2;
            if (borderSize > 0)
                smoothSize = borderSize;

            if (borderRadius > 2)
            {
                using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadius - borderSize))
                using (Pen penSurface = new Pen(this.Parent.BackColor, smoothSize))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                    this.Region = new Region(pathSurface);

                    pevent.Graphics.DrawPath(penSurface, pathSurface);


                    if (borderSize >= 1)

                        pevent.Graphics.DrawPath(penBorder, pathBorder);
                }
            }

        }


        private void Button_Resize(object sender, EventArgs e)
        {
            if (borderRadius > this.Height)
                borderRadius = this.Height;
        }


        public void hareketlendir(int form_height, int form_width)
        {
            float hareket_hizi = 10;
            int yeni_x =  (int)(hareket_hizi * Math.Cos(hareket_acisi / 180 * 3.14)) + this.Location.X ;
            int yeni_y = (int)(hareket_hizi * Math.Sin(hareket_acisi / 180 * 3.14)) + this.Location.Y;


            if(yeni_y<1) //yukarı
            {
                hareket_acisi = hareket_acisi*-1;
                if(hareket_acisi<0)
                {
                    hareket_acisi += 360;
                }
            }
            if(yeni_y>form_height-120)  //asagi
            {
                hareket_acisi = hareket_acisi*-1;
                if (hareket_acisi < 0)
                {
                    hareket_acisi += 360;
                }
            }
            
            
            //sağ ve soldan gecis
            if(yeni_x<-60 && Math.Abs(hareket_acisi)>90)
            {
                yeni_x = form_width - 60;
            }
            if(yeni_x> form_width && Math.Abs(hareket_acisi) < 90)
            {
                yeni_x = -60;
            }

            this.Location = new Point(yeni_x, yeni_y);
        }

    }

}
