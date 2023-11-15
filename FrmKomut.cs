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

namespace ReceteMain
{
    public partial class FrmKomut : Form
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=D15\SQLEXPRESS;Initial Catalog=Recete;Integrated Security=True");
        //Tıklanan(seçili) buttonu atmak için liste oluşturuyoruz.
        private List<Button> secilenButonlar = new List<Button>();

        public FrmKomut()
        {
            InitializeComponent();
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Listedeki button için eğer tıklanan buttonsa arkaplan yeşil yap diğerlerini beyaz yap.
        private void SetOtherButtonColors(Button clickedButton)
        {
            // flowLayoutPanel1 içindeki tüm butonları kontrol et

            foreach (Button control in flowLayoutPanel1.Controls.OfType<Button>())
            {
                if (control == clickedButton)
                    control.BackColor = Color.LightGreen;
                else
                    control.BackColor = Color.White;
            }
        }
       
        //Listedeki buttonun özelliklerini kopyalıyoruz.
        private Button CloneButton(Button originalButton)
        {
            Button clonedButton = new Button();
            clonedButton.Text = originalButton.Text;
            clonedButton.Size = originalButton.Size;
            clonedButton.Location = originalButton.Location;
            clonedButton.BackColor = SystemColors.InactiveCaption;
            clonedButton.ForeColor = SystemColors.ControlText;

            clonedButton.Tag = originalButton.Tag;
            // Yeni butona click event handler ekliyoruz.


            return clonedButton;
        }

        private void button_Click(object sender, EventArgs e)
        {
            secilenButonlar.Clear();
            Button btn = sender as Button;
            SetOtherButtonColors(btn);
            secilenButonlar.Add(btn);
        }

        //Ekle butonuna basınca, seçilen buton varsa oluşturduğun klonlama metoduyla seçilen butonu kopyala
        //Seçilen butonu Form2'yi çalıştırıp flow panele ekle.
        private void btnEkle_Click_1(object sender, EventArgs e)
        {
            if (secilenButonlar.Count > 0)
            {
                Button secilenButton = CloneButton(secilenButonlar[0]); // Seçilen butonun kopyasını al
                Form2 form2 = Application.OpenForms["Form2"] as Form2;
                form2.AddButtonToFlowLayoutPanel(secilenButton); // Form2'deki FlowLayoutPanel'a kopyalanan butonu ekle

            }
        }

        private void FrmKomut_Load(object sender, EventArgs e)
        {
            //başlangıçta her buttonun görünürlüğünü false ayarlıyoruz.
            foreach (Button button in flowLayoutPanel1.Controls.OfType<Button>())
            {
                button.Visible = false;
            }

            SqlCommand kmt = new SqlCommand("select KomutID, Aktif from TblRecete", baglanti);
            baglanti.Open();
            SqlDataReader rd = kmt.ExecuteReader();
            
            while (rd.Read())
            {
                int komutID = Convert.ToInt32(rd["KomutID"]);
                bool aktiflik = Convert.ToBoolean(rd["Aktif"]);
                //button tagi var mı ya da tagiyle KomutID si eşleşiyor mu kontrol et.
                Button targetButton = flowLayoutPanel1.Controls.OfType<Button>().FirstOrDefault(b => b.Tag != null && Convert.ToInt32(b.Tag) == komutID);

                if (targetButton != null)
                {
                    // Butonun görünürlüğünü ayarla
                    targetButton.Visible = true;
                }
                
            }
            baglanti.Close();
        }
    }
}
