using MuratAbi.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuratAbi.Formlar
{
    public partial class CiktiAlma : Form
    {
        public CiktiAlma()
        {
            InitializeComponent();
        }
        List<Parca> parcalar;
        Servis servis;
        Musteri cari;
        Motor motor;
        public int servisId = 0;
        private void CiktiAlma_Load(object sender, EventArgs e)
        {
            Veri veri = new Veri();
            parcalar = new List<Parca>();
            SqlConnection con3 = new SqlConnection();

            con3 = new SqlConnection(veri.Baglanti);

            SqlCommand cmd3 = new SqlCommand();
            cmd3.CommandText = "select  * from Parca where ServisId=" + servisId + "";
            cmd3.Connection = con3;
            con3.Open();
            SqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {

                Parca parca2 = new Parca();
                parca2.ParcaAciklama = dr3[1].ToString();
                parca2.Tutar = Convert.ToDouble(dr3[2]);
                parca2.servisId = servisId;
                parca2.Adet= Convert.ToInt32(dr3[4]);
                parca2.Id = Convert.ToInt32(dr3[0]);
                parcalar.Add(parca2);
            }
            con3.Close();
            CariKayit cariKayit = new CariKayit();

            SqlCommand cmd = new SqlCommand();
             servis = new Servis();
             cari = new Musteri();
             motor = new Motor();
            cmd.CommandText = "select * from Cari,Motor,Servis where Motor.CariId=Cari.MusteriId and Servis.ServisKayitNumarasi=" + servisId + " and Servis.Plaka=Motor.Plaka";
            cmd.Connection = con3;
            con3.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cari.Ad = dr[1].ToString();
                cari.Soyad = dr[2].ToString();
                cari.Adres = dr[3].ToString();
                cari.CepTelefonu = dr[6].ToString();
                motor.Plaka= cariKayit.Plaka(dr[8].ToString());
                motor.Marka = dr[9].ToString();
                motor.Model = dr[10].ToString();
                motor.Renk = dr[11].ToString();
                servis.GelKm= dr[14].ToString();
                servis.Benzin = dr[15].ToString();
                servis.Iscilik = Convert.ToDouble(dr[21]);
                servis.Tarih = dr[18].ToString();
                servis.Not = dr[16].ToString();

            }
            con3.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult pdr = printDialog1.ShowDialog();
            if (pdr == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Yazı fontumu ve çizgi çizmek için fırçamı ve kalem nesnemi oluşturdum
            Font myFont = new Font("Calibri", 28);
            Font myFont2 = new Font("Calibri", 28);
            SolidBrush sbrush = new SolidBrush(Color.Black);
            Pen myPen = new Pen(Color.Black);

            //logo için
            e.Graphics.DrawImage(Properties.Resources.tvs, 100, 20);

            //Bu kısımda sipariş formu yazısını ve çizgileri yazdırıyorum
            StringFormat myStringFormat = new StringFormat();
            myStringFormat.Alignment = StringAlignment.Far;
            myFont2 = new Font("Calibri", 12, FontStyle.Bold);
            e.Graphics.DrawString(DateTime.Now.ToShortDateString(), myFont2, sbrush, 620,30 );

            e.Graphics.DrawLine(myPen, 120, 180, 750, 180);
            e.Graphics.DrawString("SERVİS KAYIT FORMU", myFont, sbrush, 230, 200);

            e.Graphics.DrawString("Plaka   : " , myFont2, sbrush, 470, 260);
            e.Graphics.DrawString(motor.Plaka, myFont2, sbrush, 540, 260);

            e.Graphics.DrawString("Marka : " ,myFont2, sbrush, 470, 290);
            e.Graphics.DrawString(motor.Marka, myFont2, sbrush, 540, 290);

            e.Graphics.DrawString("Model : " , myFont2, sbrush, 470, 320);
            e.Graphics.DrawString( motor.Model, myFont2, sbrush, 540, 320);

            e.Graphics.DrawString("Renk    : " + motor.Renk, myFont2, sbrush, 470, 350);

            e.Graphics.DrawString("Ad                    : " + cari.Ad, myFont2, sbrush, 120, 260);
            e.Graphics.DrawString("Soyad              : " + cari.Soyad, myFont2, sbrush, 120, 290);
            e.Graphics.DrawString("CepTelefonu : " + cari.CepTelefonu, myFont2, sbrush,120, 320);
            e.Graphics.DrawString("Km                    : " + servis.GelKm, myFont2, sbrush, 120, 350);
            e.Graphics.DrawString("Geliş Tarihi     : " + servis.Tarih, myFont2, sbrush, 120, 380);

            e.Graphics.DrawString("İş Emri  : " + servis.Not, myFont2, sbrush, 120, 470);



            e.Graphics.DrawLine(myPen, 120, 550, 750, 550);

            myFont = new Font("Calibri", 12, FontStyle.Bold);
          
            e.Graphics.DrawString("PARÇA AÇIKLAMA ", myFont, sbrush, 140, 558);
      
            e.Graphics.DrawString("Fiyat", myFont, sbrush, 640, 558);
            e.Graphics.DrawString("Adet", myFont, sbrush, 700, 558);


            e.Graphics.DrawLine(myPen, 120, 583, 750, 583);

            int y = 590;

   

            decimal gTotal = 0;

            foreach (var item in parcalar)
            {
                e.Graphics.DrawString(item.ParcaAciklama, myFont, sbrush, 120, y);
                decimal bFiyat = Convert.ToDecimal(item.Tutar);
            
                gTotal += (bFiyat*item.Adet);
                e.Graphics.DrawString(bFiyat.ToString("c"), myFont, sbrush, 680, y, myStringFormat);
                e.Graphics.DrawString(item.Adet.ToString(), myFont, sbrush, 740, y, myStringFormat);

                y += 20;

            }

            e.Graphics.DrawLine(myPen, 120, y, 750, y);
            e.Graphics.DrawString("Parça Toplamı = "+gTotal.ToString("c"), myFont, sbrush, 700, y + 10, myStringFormat);
            e.Graphics.DrawString("İşcilik Toplamı = " + servis.Iscilik.ToString("c"), myFont, sbrush, 700, y + 30, myStringFormat);
            e.Graphics.DrawString("Genel Toplamı = " +(Convert.ToDouble(gTotal) + servis.Iscilik).ToString("c"), myFont, sbrush, 700, y + 50, myStringFormat);
            e.Graphics.DrawString("İMZA ", myFont, sbrush, 700, y +150, myStringFormat);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();

        }
    }
}
