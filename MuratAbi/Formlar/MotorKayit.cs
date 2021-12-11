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

namespace MuratAbi
{
    public partial class MotorKayit : Form
    {
        public MotorKayit()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con;
            con = new SqlConnection(veri.Baglanti);
            SqlCommand cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "insert into Motor(Plaka,Marka,Model,Renk,CariId)Values(" +
                "'" + textBox1.Text.ToUpper().Replace(" ","") + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "'," + dataGridView1.SelectedRows[0].Cells[0].Value + ")";
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kaydedildi");
        }

        private void MotorKayit_Load(object sender, EventArgs e)
        {
            listele(textBox5.Text);
        }
        SqlConnection con = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand com = new SqlCommand();
        DataSet ds = new DataSet();
        Veri veri = new Veri();
        void listele(string text)
        {

            con = new SqlConnection(veri.Baglanti);
            da = new SqlDataAdapter("Select MusteriId,Ad,Soyad From Cari where Ad LIKE('%" + text + "%')", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "Cari");
            dataGridView1.DataSource = ds.Tables["Cari"];
            con.Close();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            listele(textBox5.Text);

        }
    }
}
