using MuratAbi.Entities;
using MuratAbi.Formlar;
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

namespace MuratAbi
{
    public partial class CariKayit : Form
    {
        public CariKayit()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        public int Id = 0; Veri veri = new Veri();
        Cari c = new Cari();

        private void button1_Click(object sender, EventArgs e)
        {
            if (Id == 0)
            {

                SqlConnection con;
                con = new SqlConnection(veri.Baglanti);
                SqlCommand cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "insert into Cari(Ad,Soyad,Adres,Evtel,Istel,Ceptel)Values(" +
                    "'" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox6.Text + "','" + textBox5.Text + "','" + textBox4.Text + "')";
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Kaydedildi");
                c.Show();
                this.Hide();
            }
            else
            {
                SqlConnection con;
                con = new SqlConnection(veri.Baglanti);
                SqlCommand cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "UPDATE Cari SET Ad='" + textBox1.Text + "',Soyad='" + textBox2.Text + "',Adres='" + textBox3.Text + "',Evtel='" + textBox6.Text + "',Istel='" + textBox5.Text + "',Ceptel='" + textBox4.Text + "' where MusteriId=" + Id + "";


                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Kaydedildi");
                c.Show();
                this.Hide();
            }
        }
      public  string Plaka(string plaka)
        {
            int a = 0;
            string sonuc = ""; 
            for (int i = 0; i < plaka.Length; i++)
            {
                if(i==2)
                {
                    sonuc += " ";
                }
                if (i == 3 && a == 0)
                {
                    if (Char.IsNumber(plaka[i]))
                    {
                        sonuc += " ";
                        a++;
                    }
                }
                if (i == 4 && a == 0)
                {
                    if (Char.IsNumber(plaka[i]))
                    {
                        sonuc += " ";
                        a++;
                    }
                }
                if (i == 5 && a == 0)
                {
                    if (Char.IsNumber(plaka[i]))
                    {
                        sonuc += " ";
                        a++;
                    }
                }

                sonuc += plaka[i];
            }
            return sonuc;
        }
        void motorlistele()
        {

            SqlConnection con;
            con = new SqlConnection(veri.Baglanti);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select Plaka from Motor where CariId=" + Id + "";
            cmd.Connection = con;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string plaka = dr[0].ToString();
                listBox1.Items.Add(Plaka(plaka));
               

            }

            con.Close();
        }
        private void CariKayit_Load(object sender, EventArgs e)
        {
            if (Id > 0)
            {

                SqlConnection con;
                con = new SqlConnection(veri.Baglanti);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select * from Cari where MusteriId=" + Id + "";
                cmd.Connection = con;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    textBox1.Text = dr[1].ToString();
                    textBox2.Text = dr[2].ToString();
                    textBox3.Text = dr[3].ToString();
                    textBox4.Text = dr[6].ToString();
                    textBox5.Text = dr[5].ToString();
                    textBox6.Text = dr[4].ToString();

                }

                con.Close();

                listele2();
                motorlistele();

                panel2.Visible = true;
            }
        }
        public  void listele2 ()
            {
            SqlConnection con2;
            con2 = new SqlConnection(veri.Baglanti);
            SqlCommand cmd2 = new SqlCommand();
            cmd2.CommandText = "select * from Borc where MusteriId=" + Id + " order by Id desc";
            cmd2.Connection = con2;
            double toplam = 0;
            con2.Open();
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                string Aciklama = dr2[2].ToString();
                double Tutar = Convert.ToDouble(dr2[1]);
                int tur = Convert.ToInt32(dr2[4]); ;

                if (tur == 0)
                {
                    Tutar = Tutar * -1;
                    listView1.Items.Add(Tutar.ToString() + " :  " + Aciklama);
                    listView1.Items[listView1.Items.Count - 1].BackColor = Color.Red;

                }
                else
                {
                    Tutar = Tutar * 1;
                    listView1.Items.Add(Tutar.ToString() + " : " + Aciklama);
                    listView1.Items[listView1.Items.Count - 1].BackColor = Color.GreenYellow;

                }

                toplam = toplam + Tutar;


            }
            label7.Text = toplam.ToString();
            con2.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Borc b = new Borc();
            b.Id = Id;
            b.Show();
            this.Hide();
        }
    }
}
