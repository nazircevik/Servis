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
    public partial class ServisKayit : Form
    {
        public ServisKayit()
        {
            InitializeComponent();
        }
        public int Id = 0;
        bool ab = false;
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        int servisId = 0;
        private void button1_Click(object sender, EventArgs e)
        {

            listBox1.Items.Add(textBox2.Text + "  -> " + textBox1.Text);
            Parca parca = new Parca() { ParcaAciklama = textBox2.Text, Tutar = Convert.ToDouble(textBox1.Text), servisId = servisId };
            parcalar.Add(parca);

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        List<Parca> parcalar;

        private void ServisKayit_Load(object sender, EventArgs e)
        {
            Veri veri = new Veri();

            if (Id <= 0)
            {

                SqlConnection con = new SqlConnection();
                SqlCommand com = new SqlCommand();
                DataSet ds = new DataSet();
                parcalar = new List<Parca>();
                con = new SqlConnection(veri.Baglanti);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select top 1  ServisKayitNumarasi from Servis order by ServisKayitNumarasi desc";
                cmd.Connection = con;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    servisId = Convert.ToInt32(dr[0]);
                }
                if (servisId == 0)
                    servisId = 1;
                else
                    servisId = servisId + 1;
                con.Close();
            }
            if (Id > 0)
            {
                ab = true;
                parcalar = new List<Parca>();
                SqlConnection con2 = new SqlConnection();
                servisId = Id;
                con2 = new SqlConnection(veri.Baglanti);
                dateTimePicker1.Visible = false;
                button5.Visible = false;
                SqlCommand cmd2 = new SqlCommand();
                cmd2.CommandText = "select top 1  * from Servis where ServisKayitNumarasi=" + Id + "";
                cmd2.Connection = con2;
                con2.Open();
                SqlDataReader dr2 = cmd2.ExecuteReader();
                string a = "";
                while (dr2.Read())
                {
                    textBox3.Text = dr2[4].ToString();
                    a = dr2[4].ToString();
                    textBox11.Text = dr2[1].ToString();
                    textBox12.Text = dr2[2].ToString();
                    richTextBox1.Text = dr2[3].ToString();
                    label17.Text = dr2[5].ToString();
                    textBox13.Text = dr2[8].ToString();


                }
                con2.Close();
                SqlConnection con3 = new SqlConnection();
                servisId = Id;
                con3 = new SqlConnection(veri.Baglanti);

                SqlCommand cmd3 = new SqlCommand();
                cmd3.CommandText = "select  * from Parca where ServisId=" + Id + "";
                cmd3.Connection = con3;
                con3.Open();
                SqlDataReader dr3 = cmd3.ExecuteReader();
                while (dr3.Read())
                {
                    listBox1.Items.Add(dr3[1].ToString() + "  -> " + dr3[2].ToString());
                    Parca parca2 = new Parca();
                    parca2.ParcaAciklama = dr3[1].ToString();
                    parca2.Tutar = Convert.ToDouble(dr3[2]);
                    parca2.servisId = Id;
                    parca2.Id = Convert.ToInt32(dr3[0]);
                    parcalar.Add(parca2);
                }
                con3.Close();
                BilgileriGetir(a);

            }

        }
        void parcakaydet()
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (Id == 0)
            {
                Servis servis = new Servis();
                if (checkBox1.Checked)
                    servis.Bakim = 1;
                else
                    servis.Bakim = 0;

                servis.Benzin = textBox12.Text;
                servis.GelKm = textBox11.Text;
                string iscilik = textBox13.Text.Replace(",", ".");
                if (iscilik == null || iscilik == "")
                    iscilik = "0";
                servis.Iscilik = Convert.ToInt32(iscilik);
                servis.Tarih = DateTime.Now.ToShortDateString();
                servis.Yil = DateTime.Now.Year;
                servis.Plaka = textBox3.Text;

                Veri veri = new Veri();
                SqlConnection con;
                con = new SqlConnection(veri.Baglanti);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "insert into Servis (Gelkm,Benzin,Sikayet,Plaka,Tarih,Bakim,Yil,Iscilik) values " +
                    "('" + textBox11.Text + "','" + textBox12.Text + "','" + richTextBox1.Text + "','" + textBox3.Text + "','" + servis.Tarih + "'," + servis.Bakim + "," + servis.Yil + "," + servis.Iscilik + ") ";
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();



                for (int i = 0; i < parcalar.Count; i++)
                {
                    SqlCommand cmd2 = new SqlCommand();
                    cmd.CommandText = "insert into Parca (ParcaAciklama,Tutar,ServisId) values " +
                   "('" + parcalar[i].ParcaAciklama + "'," + parcalar[i].Tutar + "," + parcalar[i].servisId + ") ";
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();


                }
                con.Close();
                Id++;
                parcalar = new List<Parca>();
                MessageBox.Show("Kaydedildi");
            }
            else
            {


                Veri veri = new Veri();
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
                    parca2.servisId = Id;
                    parca2.Id = Convert.ToInt32(dr3[0]);
                    parcalar.Add(parca2);
                }
                con3.Close();
                Servis servis = new Servis();
                if (checkBox1.Checked)
                    servis.Bakim = 1;
                else
                    servis.Bakim = 0;

                servis.Benzin = textBox12.Text;
                servis.GelKm = textBox11.Text;
                string iscilik = textBox13.Text.Replace(",", ".");
                if (iscilik == null || iscilik == "")
                    iscilik = "0";
                servis.Iscilik = Convert.ToInt32(iscilik);
                servis.Tarih = DateTime.Now.ToShortDateString();
                servis.Yil = DateTime.Now.Year;
                servis.Plaka = textBox3.Text;

                SqlConnection con;
                con = new SqlConnection(veri.Baglanti);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Update Servis set Gelkm='" + servis.GelKm + "',Benzin='" + servis.Benzin + "',Sikayet='" + richTextBox1.Text + "',Iscilik=" + servis.Iscilik + "where ServisKayitNumarasi=" + servisId + "";

                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();


                for (int i = 0; i < parcalar.Count; i++)
                {
                    if (parcalar[i].Id == 0 || parcalar[i] == null)
                    {
                        SqlCommand cmd2 = new SqlCommand();
                        cmd.CommandText = "insert into Parca (ParcaAciklama,Tutar,ServisId) values " +
                       "('" + parcalar[i].ParcaAciklama + "'," + parcalar[i].Tutar + "," + parcalar[i].servisId + ") ";
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();

                    }
                }
                con.Close();
                if (ab)
                    parcalar = new List<Parca>();
                MessageBox.Show("KAYDEDİLDİ");
            }
        }
        void BilgileriGetir(string plaka)
        {
            Veri veri = new Veri();
            SqlConnection con;
            con = new SqlConnection(veri.Baglanti);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select top 1 * from Cari,Motor where Motor.Plaka LIKE('%" + plaka + "%') and Motor.CariId=Cari.MusteriId";
            cmd.Connection = con;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                textBox10.Text = dr[1].ToString();
                textBox9.Text = dr[2].ToString();
                textBox8.Text = dr[3].ToString();
                textBox7.Text = dr[6].ToString();
                textBox3.Text = dr[8].ToString();
                textBox4.Text = dr[9].ToString();
                textBox5.Text = dr[10].ToString();
                textBox6.Text = dr[11].ToString();


            }
            if (dr.HasRows == false)
            {
                MessageBox.Show("Bulunamadı");
                textBox10.Text = "";
                textBox9.Text = "";
                textBox8.Text = "";
                textBox7.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";

            }
            con.Close();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            BilgileriGetir(textBox3.Text);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            CiktiAlma ciktiAlma = new CiktiAlma();
            ciktiAlma.servisId = Id;
            ciktiAlma.Show();
        }
    }
}
