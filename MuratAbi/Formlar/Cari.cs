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
    public partial class Cari : Form
    {
        public Cari()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        SqlConnection con = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand com = new SqlCommand();
        DataSet ds = new DataSet();
        Veri veri = new Veri();

        void listele(string text)
        {

            con = new SqlConnection(veri.Baglanti);
            da = new SqlDataAdapter("Select * From Cari where Ad LIKE('%" +text+ "%')", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "Cari");
            dataGridView1.DataSource = ds.Tables["Cari"];
            con.Close();

        }
        private void Cari_Load(object sender, EventArgs e)
        {
            listele(textBox1.Text);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listele(textBox1.Text);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try { 
            CariKayit cariKayit = new CariKayit();
            cariKayit.Id = Convert.ToInt32( dataGridView1.SelectedRows[0].Cells[0].Value);
            cariKayit.Show();
            }
            catch
            {

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
