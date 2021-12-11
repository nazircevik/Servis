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
    public partial class Borc : Form
    {
        public Borc()
        {
            InitializeComponent();
        }
        public int Id = 0;
        Veri veri = new Veri();
        private void button1_Click(object sender, EventArgs e)
        {try
            { 
            if(Id>0)
            {
                int tur;
                if(radioButton2.Checked)
                {
                    tur = 1;
                }
                else
                {
                    tur = 0;
                }
                SqlConnection con;
                con = new SqlConnection(veri.Baglanti);
                SqlCommand cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "insert into Borc(Tutar,Aciklama,MusteriId,Tur)Values("+Convert.ToDouble(textBox2.Text)+",'"+textBox1.Text+"',"+Id+","+tur+")";
                
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Kaydedildi");
                CariKayit c = new CariKayit();
                    c.Id = Id;
                    c.Show();
            this.Hide();
            }
            }
            catch
            {

            }
        }

        private void Borc_Load(object sender, EventArgs e)
        {
            radioButton1.Checked=true;
        }
    }
}
