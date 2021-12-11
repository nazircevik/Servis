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
    public partial class MotorBilgileri : Form
    {
        public MotorBilgileri()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
            da = new SqlDataAdapter("Select * from Motor where Plaka LIKE('%" + textBox2.Text + "%')", con);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "Motor");
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void MotorBilgileri_Load(object sender, EventArgs e)
        {
           

            listele();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[5].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            da.Update(ds, "Motor");
            MessageBox.Show("Kayıt güncellendi");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            listele();

        }
    }
}
