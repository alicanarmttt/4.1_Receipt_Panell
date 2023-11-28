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

namespace ReceteMain.From_Controls
{
    public partial class Yikama : UserControl
    {
        //Bağlantı kur.
        SqlConnection baglanti = new SqlConnection(@"Data Source=D15\SQLEXPRESS;Initial Catalog=Recete;Integrated Security=True");
        private int defaultDevir;
        private int defaultDonusSure;
        private int defaultBeklemeSure;
        private int defaultSureDK;
        private int defaultSicaklik;
        private int minDevir;
        private int maxDevir;
        private int minDonusSure;
        private int maxDonusSure;
        private int minBeklemeSure;
        private int maxBeklemeSure;
        private int minSureDK;
        private int maxSureDK;
        private int maxSureSn;
        private int maxSicaklik;
        //ID yi dışarıdan almak için yaratıyoruz.
        public int ID { get; set; }
        public Yikama(int ID)
        {
            InitializeComponent();
            this.ID = ID;
        }

        private void Yıkama_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select defaultDevir,defaultDonusSure,defaultBeklemeSure,defaultSureDK,defaultSicaklik,minDevir,maxDevir" +
                ",minDonusSure,maxDonusSure,minBeklemeSure,maxBeklemeSure,minSureDK,maxSureDK,maxSureSn,maxSicaklik from TblKitap1 where CommandID=@p1 AND isActive=1", baglanti);
            cmd.Parameters.AddWithValue("@p1", ID);
            baglanti.Open();

            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                
                txtDevir.Text = rd["defaultDevir"].ToString();
                txtDonus.Text = rd["defaultDonusSure"].ToString();
                txtBekleme.Text = rd["defaultBeklemeSure"].ToString();
                txtSure.Text = rd["defaultSureDK"].ToString();
                txtSıcaklık.Text = rd["defaultSicaklik"].ToString();

                defaultDevir = int.Parse(txtDevir.Text);
                defaultDonusSure = int.Parse(txtDonus.Text);
                defaultBeklemeSure = int.Parse(txtBekleme.Text);
                defaultSureDK = int.Parse(txtSure.Text);
                defaultSicaklik = int.Parse(txtSıcaklık.Text);

                minDevir = Convert.ToInt32(rd["minDevir"]);
                 maxDevir = Convert.ToInt32(rd["maxDevir"]);
                 minDonusSure = Convert.ToInt32(rd["minDonusSure"]);
                 maxDonusSure = Convert.ToInt32(rd["maxDonusSure"]);
                 minBeklemeSure = Convert.ToInt32(rd["minBeklemeSure"]);
                 maxBeklemeSure = Convert.ToInt32(rd["maxBeklemeSure"]);
                 minSureDK = Convert.ToInt32(rd["minSureDK"]);
                 maxSureDK = Convert.ToInt32(rd["maxSureDK"]);
                 maxSureSn = Convert.ToInt32(rd["maxSureSn"]);
                 maxSicaklik = Convert.ToInt32(rd["maxSicaklik"]);
            }
            baglanti.Close();
        }
        private void txtBox_LostFocus(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox != null)
            {
                decimal value;

                if (decimal.TryParse(textBox.Text, out value))
                {
                    // TextBox'tan alınan değer başarıyla bir decimal değere dönüştürüldü

                    switch (textBox.Name)
                    {
                        case "txtDevir":
                            if ( value < minDevir || value > maxDevir)
                            {
                                MessageBox.Show(minDevir+" ile " + maxDevir + " arasında bir sayı giriniz.");
                                textBox.Text = defaultDevir.ToString(); 
                                textBox.Focus(); // Odaklanmayı geri getir
                            }
                            break;

                        case "txtDonus":
                            if (value < minDonusSure || value > maxDonusSure)
                            {
                                MessageBox.Show(minDonusSure + " ile " + maxDonusSure + " arasında bir sayı giriniz.");
                                textBox.Text = defaultDonusSure.ToString();
                                textBox.Focus(); // Odaklanmayı geri getir
                            }
                            
                            break;

                        case "txtBekleme":
                            if (value < minBeklemeSure || value > maxBeklemeSure)
                            {
                                MessageBox.Show(minBeklemeSure + " ile " + maxBeklemeSure + " arasında bir sayı giriniz.");
                                textBox.Text = defaultBeklemeSure.ToString();
                                textBox.Focus(); // Odaklanmayı geri getir
                            }
                            break;

                        case "txtSure":
                            if (value < minSureDK || value > maxSureDK)
                            {
                                MessageBox.Show(minSureDK + " ile " + maxSureDK + " arasında bir sayı giriniz.");
                                textBox.Text = defaultSureDK.ToString();
                                textBox.Focus(); // Odaklanmayı geri getir
                            }
                            break;

                        case "txtSıcaklık":
                            if ( value > maxSicaklik)
                            {
                                MessageBox.Show(maxSicaklik + "'dan az bir sayı giriniz.");
                                textBox.Text = defaultSicaklik.ToString();
                                textBox.Focus(); // Odaklanmayı geri getir
                            }
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Geçersiz değer! Sayısal bir değer giriniz.");
                    textBox.Focus(); // Odaklanmayı geri getir
                }
            }


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                panel1.Visible = false; 
            }
            else
            {
                panel1.Visible = true;
            }
        }
    }
}
