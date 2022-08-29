using LiveCharts;
using LiveCharts.Wpf;
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
    public partial class ParaTakip : Form
    {
        private List<Parca> parcalar;

        public ParaTakip()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        Func<ChartPoint, string> labelPoint = chartPoint =>
                       string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
        private void button1_Click(object sender, EventArgs e)
        {
            int iscilik = paras2.Where(x => x.Tarih>=Convert.ToDateTime(dateTimePicker1.Text) && x.Tarih <= Convert.ToDateTime(dateTimePicker2.Text)).Select(x => x.Iscilik).Sum();
            int parca2 = paras2.Where(x => x.Tarih >= Convert.ToDateTime(dateTimePicker1.Text) && x.Tarih <= Convert.ToDateTime(dateTimePicker2.Text)).Select(x => x.Parca).Sum();

            textBox1.Text = iscilik.ToString();
            textBox2.Text = parca2.ToString();
            textBox3.Text = (iscilik + parca2).ToString();
            pieChart1.Series = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "İşçilik",
                    Values = new ChartValues<double> {iscilik},
                    PushOut = 15,
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                new PieSeries
                {
                    Title = "Parça",
                    Values = new ChartValues<double> {parca2},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
            };


        }
        List<Para> paras2 = new List<Para>();

        private void ParaTakip_Load(object sender, EventArgs e)
        {
           
           

            pieChart1.LegendLocation = LegendLocation.Bottom;
            paras2 = new List<Para>();
            Veri veri = new Veri();
                List<Para> paras = new List<Para>();
            List<Parca> Parcalar = new List<Parca>();

            SqlConnection con = new SqlConnection();
                SqlCommand com = new SqlCommand();
                DataSet ds = new DataSet();
                parcalar = new List<Parca>();
                con = new SqlConnection(veri.Baglanti);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select   ServisKayitNumarasi,Iscilik,Tarih from Servis order by ServisKayitNumarasi desc";
                cmd.Connection = con;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                Para para = new Para();
                para.ServisNumarasi = Convert.ToInt32(dr[0]);
                para.Iscilik = Convert.ToInt32(dr[1]);
                para.Tarih = Convert.ToDateTime(dr[2]);
                paras.Add(para);
                }
                con.Close();

                cmd.CommandText = "select   * from Parca order by Id desc";
            con.Open();
            SqlDataReader dr2 = cmd.ExecuteReader();
            while (dr2.Read())
            {
                Parca parca = new Parca();
                parca.Id =  Convert.ToInt32(dr2[0]);
                parca.ParcaAciklama = (dr2[1]).ToString();
                parca.Tutar = Convert.ToInt32(dr2[2]);
                parca.servisId = Convert.ToInt32(dr2[3]);
                parca.Adet = Convert.ToInt32(dr2[4]);
                Parcalar.Add(parca);
            }
            con.Close();




            foreach (var item in paras)
            {
                var p = Parcalar.Where(x=>x.servisId==item.ServisNumarasi);
                double toplam=0;
                foreach (var item2 in p)
                {
                    toplam += (item2.Tutar * item2.Adet);
                }
                item.Parca = Convert.ToInt32(toplam);
                paras2.Add(item);
            }

                int iscilik= paras2.Select(x => x.Iscilik).Sum();
                int parca2 = paras2.Select(x => x.Parca).Sum();

            textBox1.Text = iscilik.ToString();
            textBox2.Text = parca2.ToString();
            textBox3.Text = (iscilik+parca2).ToString();
            pieChart1.Series = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "İşçilik",
                    Values = new ChartValues<double> {iscilik},
                    PushOut = 15,
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                new PieSeries
                {
                    Title = "Parça",
                    Values = new ChartValues<double> {parca2},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
            };

        }

        private void pieChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
    }
}
