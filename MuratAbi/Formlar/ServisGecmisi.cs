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
    public partial class ServisGecmisi : Form
    {
        public ServisGecmisi()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        Veri veri = new Veri();
        void listele()
        {
            con = new SqlConnection(veri.Baglanti);
            con.Open();
            da = new SqlDataAdapter("Select ServisKayitNumarasi,Plaka,Tarih from Servis where Plaka LIKE('%" + textBox1.Text + "%') order by ServisKayitNumarasi desc", con);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "Servis");
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        void listele2()
        {
            con = new SqlConnection(veri.Baglanti);
            con.Open();
            da = new SqlDataAdapter("Select ServisKayitNumarasi,Plaka,Tarih from Servis where Tarih= '" + dateTimePicker1.Text + "' order by ServisKayitNumarasi desc ", con);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "Servis");
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void ServisGecmisi_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listele();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            listele2();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                ServisKayit servisKayit = new ServisKayit();
                servisKayit.Id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                servisKayit.Show();
            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
