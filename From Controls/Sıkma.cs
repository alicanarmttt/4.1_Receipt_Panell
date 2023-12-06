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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ReceteMain.From_Controls
{
    public partial class Sıkma : UserControl
    {
        //Bağlantı kur.
        SqlConnection baglanti = new SqlConnection(@"Data Source=D15\SQLEXPRESS;Initial Catalog=Recete;Integrated Security=True");

        //Başka eventlerde kullanılacağı için double değişkenleri tanımla.
        private int defaultDevir;
        private double defaultDonusSure;
        private double defaultBeklemeSure;
        private double defaultSureDK;
        private double defaultSicaklik;
        private double minDevir;
        private double maxDevir;
        private double minDonusSure;
        private double maxDonusSure;
        private double minBeklemeSure;
        private double maxBeklemeSure;
        private double minSureDK;
        private double maxSureDK;
        private double maxSureSn;
        private double maxSicaklik;

        public int ID { get; set; }
        public Sıkma(int ID)
        {
            InitializeComponent();
            this.ID = ID;
        }
        private void Sıkma_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select defaultDevir,defaultDonusSure,defaultBeklemeSure,defaultSureDK,minDevir,maxDevir" +
                ",minDonusSure,maxDonusSure,minBeklemeSure,maxBeklemeSure,minSureDK,maxSureDK,maxSureSn from TblKitap1 where CommandID=@p1 AND isActive=1", baglanti);
            cmd.Parameters.AddWithValue("@p1", ID);
            baglanti.Open();

            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                //Geçici double yardımıyla 10 a böl. Textbox'a yaz.
                txtDevir.Text = rd["defaultDevir"].ToString();

                double defdonus = (double)rd["defaultDonusSure"];
                txtDonus.Text = (defdonus / 10.0).ToString();

                double defbekleme = (double)rd["defaultBeklemeSure"];
                txtBekleme.Text = (defbekleme / 10.0).ToString();

                //double defsure = (double)rd["defaultSicaklik"];
                //txtSure.Text = (defsure / 10.0).ToString();

                //Texte eklenen defaultlarını döngü dışına çıkarabilmek için kaydettik.  
                //Eğer geçersiz aralıkta ise defaultlarını kullanarak eski haline getireceğiz çünkü.

                defaultDevir = int.Parse(txtDevir.Text);
                defaultDonusSure = double.Parse(txtDonus.Text);
                defaultBeklemeSure = double.Parse(txtBekleme.Text);
                defaultSureDK = int.Parse(txtSure.Text);

                //Eğer 15 geldiyse textte .0 ekliyoruz.
                if (!txtDonus.Text.Contains("."))
                {
                    txtDonus.Text += ".0";
                }

                if (!txtBekleme.Text.Contains("."))
                {
                    txtBekleme.Text += ".0";
                }
                if (!txtSure.Text.Contains("."))
                {
                    txtSure.Text += ".0";
                }

                //min-max değerlerini de burada dolduruyoruz.

                minDevir = Convert.ToInt32(rd["minDevir"]);
                maxDevir = Convert.ToInt32(rd["maxDevir"]);
                minDonusSure = Convert.ToInt32(rd["minDonusSure"]);
                maxDonusSure = Convert.ToInt32(rd["maxDonusSure"]);
                minBeklemeSure = Convert.ToInt32(rd["minBeklemeSure"]);
                maxBeklemeSure = Convert.ToInt32(rd["maxBeklemeSure"]);
                minSureDK = Convert.ToInt32(rd["minSureDK"]);
                maxSureDK = Convert.ToInt32(rd["maxSureDK"]);
                maxSureSn = Convert.ToInt32(rd["maxSureSn"]);
                
            }
            baglanti.Close();

            //tooltip1 özellikleri
            toolTip1.ToolTipIcon = ToolTipIcon.Error;
            toolTip1.ToolTipTitle = "Hata!";
            toolTip1.AutomaticDelay = 200;
            toolTip1.AutoPopDelay = 3000;
        }
        //Veri girilip başka bir textboxa geçince tetikleniyor ve textbox'u kontrol ediyoruz.
        private void txtBox_LostFocus(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox textBox = sender as System.Windows.Forms.TextBox;

            //Virgül varsa nasıl düzenleyeceğiz.
            if (textBox.Text.Contains("."))
            {
                int index = textBox.Text.IndexOf(".");
                //Eğer XY,0 -> XY,0
                if (textBox.Text.EndsWith("0"))
                {
                    textBox.Text = textBox.Text;
                }
                //Eğer XY, -> XY,0
                if (textBox.Text.EndsWith("."))
                {
                    textBox.Text = textBox.Text.ToString() + "0";
                }
                //Eğer XY,Z -> XY,Z
                if (char.IsDigit(textBox.Text[textBox.Text.Length - 1]))
                {
                    textBox.Text = textBox.Text;
                }

            }

            //Eğer virgül yoksa
            else if (!textBox.Text.Contains("."))
            {
                textBox.Text = textBox.Text.ToString() + ".0";
            }

            //Hangi textboxu doldurmayı bitirdiysek. Formatı harici min maxlarına göre sınırlarını kontrol etmeliyiz.
            //Tooltip ile de kullanıcıya bildirip default değerleri geri döndürüyoruz.
            if (textBox != null)
            {
                double value;

                if (double.TryParse(textBox.Text, out value))
                {
                    // TextBox'tan alınan değer başarıyla bir decimal değere dönüştürüldü

                    switch (textBox.Name)
                    {
                        //case "txtDevir":
                        //    if (value < minDevir || value > maxDevir)
                        //    {
                        //        //MessageBox.Show(minDevir + " ile " + maxDevir + " arasında bir sayı giriniz.");
                        //        toolTip1.Show($"Geçersiz değer! {minDevir} ile {maxDevir} arasında bir sayı giriniz.", textBox, 0, -30, 3000);
                        //        textBox.Text = defaultDevir.ToString();
                        //        textBox.Focus(); // Odaklanmayı geri getir
                        //    }
                        //    break;

                        case "txtDonus":
                            if (value < minDonusSure || value > maxDonusSure)
                            {
                                //MessageBox.Show(minDonusSure + " ile " + maxDonusSure + " arasında bir sayı giriniz.");
                                toolTip1.Show($"Geçersiz değer! {minDonusSure} ile {maxDonusSure} arasında bir sayı giriniz.", textBox, 0, -30, 3000);
                                textBox.Text = defaultDonusSure.ToString() + ".0".ToString(); ;
                                textBox.Focus(); // Odaklanmayı geri getir
                            }

                            break;

                        case "txtBekleme":
                            if (value < minBeklemeSure || value > maxBeklemeSure)
                            {
                                //MessageBox.Show(minBeklemeSure + " ile " + maxBeklemeSure + " arasında bir sayı giriniz.");
                                toolTip1.Show($"Geçersiz değer! {minBeklemeSure} ile {maxBeklemeSure} arasında bir sayı giriniz.", textBox, 0, -30, 3000);
                                textBox.Text = defaultBeklemeSure.ToString() + ".0".ToString(); ;
                                textBox.Focus(); // Odaklanmayı geri getir
                            }
                            break;

                        case "txtSure":
                            if (value < minSureDK || value > maxSureDK)
                            {
                                //MessageBox.Show(minSureDK + " ile " + maxSureDK + " arasında bir sayı giriniz.");
                                toolTip1.Show($"Geçersiz değer! {minSureDK} ile {maxSureDK} arasında bir sayı giriniz.", textBox, 0, -30, 3000);
                                textBox.Text = defaultSureDK.ToString() + ".0".ToString(); ;
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
        //Devir integer şeklinde gözükecek
        private void txtBox_LostFocusTam(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox textBox = sender as System.Windows.Forms.TextBox;
            if (textBox != null)
            {

                double value;

                if (double.TryParse(textBox.Text, out value))
                {

                    switch (textBox.Name)
                    {
                        case "txtDevir":
                            if (value < minDevir || value > maxDevir)
                            {
                                //MessageBox.Show(minDevir + " ile " + maxDevir + " arasında bir sayı giriniz.");
                                toolTip1.Show($"Geçersiz değer! {minDevir} ile {maxDevir} arasında bir sayı giriniz.", textBox, 0, -30, 3000);
                                textBox.Text = defaultDevir.ToString();
                                textBox.Focus(); // Odaklanmayı geri getir
                            }
                            break;
                    }
                }

            }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //silme işlemi çalışabilsin.
            if (e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
                return;
            }

            // Eğer basılan tuş bir sayı değilse ve bir kontrol tuşu (Ctrl, Shift, vb.) değilse
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !(e.KeyChar == '.'))
            {
                // Girişi engelle
                e.Handled = true;
                return;
            }
            //Eğer tuş virgülse ve virgül metinde bulunuyorsa, ya da bir rakamsa ve virgül bulunuyorsa Sadece 1, basamak eklenebilsin.
            if (((e.KeyChar == '.') && (sender as System.Windows.Forms.TextBox).Text.Contains(".")) || (char.IsDigit(e.KeyChar) && (sender as System.Windows.Forms.TextBox).Text.Contains(".")))
            {
                int index = (sender as System.Windows.Forms.TextBox).Text.IndexOf(".");
                if (((sender as System.Windows.Forms.TextBox).Text.Length - 1) - index >= 1)
                {
                    e.Handled = true;
                }
            }
        }
        private void radioDonusYok_CheckedChanged(object sender, EventArgs e)
        {
            if (radioDonusYok.Checked)
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
