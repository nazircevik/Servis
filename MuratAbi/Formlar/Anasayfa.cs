using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuratAbi.Formlar
{
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ServisKayit s = new ServisKayit();
            s.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CariKayit c = new CariKayit();
            c.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Cari c2 = new Cari();
            c2.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MotorKayit m = new MotorKayit();
            m.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MotorBilgileri m = new MotorBilgileri();
            m.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServisGecmisi s = new ServisGecmisi();
            s.Show();
        }
    }
}
