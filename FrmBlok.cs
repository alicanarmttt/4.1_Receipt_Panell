using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReceteMain
{
    public partial class FrmBlok : Form
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=D15\SQLEXPRESS;Initial Catalog=Recete;Integrated Security=True");
        public static FrmBlok instance;
        Form2 frm2 = Form2.Instance;
        public FrmBlok()
        {
            InitializeComponent();
            instance = this;
        }
        //Tıklanan(seçili) buttonu atmak için liste oluşturuyoruz.
        public List<Button> secilenButonlar = new List<Button>();

        private void btnIptal_Click(object sender, EventArgs e)
        {
            Form2 frm2 = Form2.Instance;
            frm2.UpdateButtonsEnabilityBlok(true);
            Form2.statikBlokEkleBtn.Enabled = true;
            frm2.flowuAktifYap(true);
            this.Close();
            frm2.yesilButtonuAc();
            


        }
        
        private void FrmBlok_Load(object sender, EventArgs e)
        {
            Form2 frm2 = Form2.Instance;
            if(frm2.flowReceteCount()==0)
            {
                btnUstEkle.Visible= false;
            }
            
            //Başlangıçta üste ekleme kapalı.

            //btnUstEkle.Enabled = false;

            //Tablodan komutları döngüyle oluşturuyoruz. 
            SqlCommand kmt1 = new SqlCommand("select * from TblReceteFull where [Komut ID] >47 and AKTİF=1", baglanti);
            baglanti.Open();
            SqlDataReader rd1 = kmt1.ExecuteReader();
            while (rd1.Read())
            {
                // Her bir kayıt için bir buton oluştur
                Button button = new Button();
                button.Text = rd1["Komut"].ToString(); // Buton adını veritabanından alınan değerle ayarla
                button.Tag = rd1["Komut ID"]; // Butonun Tag özelliğini veritabanından alınan değerle ayarla,
                button.Size = new Size(175, 50);
                button.BackColor = Color.White;
                button.ForeColor = Color.Black;
                button.Font = new Font("Microsoft Sans Serif", 12);
                button.Click += button_Click;
                flowLayoutPanel1.Controls.Add(button);
            }
            baglanti.Close();
            flowLayoutPanel1.Controls[0].BackColor = Color.LightGreen;
            secilenButonlar.Add((Button)flowLayoutPanel1.Controls[0]);
        }
      
        //Listedeki button için eğer tıklanan buttonsa arkaplan yeşil yap diğerlerini beyaz yap.
        public void SetOtherButtonColors(Button button) 
        { 
    
            foreach (Button control in flowLayoutPanel1.Controls.OfType<Button>())
            {
                if (control == button)
                    control.BackColor = Color.LightGreen;
                else
                    control.BackColor = Color.White;
            }
        }
        //Ekle butonuna basınca, seçilen buton varsa oluşturduğun klonlama metoduyla seçilen butonu kopyala
        //Seçilen butonu Form2'yi çalıştırıp flow panele ekle.  
        public void btnEkle_Click(object sender, EventArgs e)
        {
            Form2 frm2 = Form2.Instance;
            frm2.renkSıfırla();
            if (secilenButonlar.Count > 0)
            {
                Button secilenButton = CloneButton(secilenButonlar[0]); // Seçilen butonun kopyasını al
                Form2 form2 = Application.OpenForms["Form2"] as Form2;
                form2.AddButtonToFlowLayoutPanel(secilenButton,form2.yesilIndex); // Form2'deki FlowLayoutPanel'a kopyalanan butonu ekle
            }
            frm2.UpdateButtonsEnability(true);
            Form2.statikBlokEkleBtn.Enabled = true;
            this.Close();
            
        }
        //butonu üste ekle
        private void btnUstEkle_Click(object sender, EventArgs e)
        {
            Form2 frm2 = Form2.Instance;
            frm2.renkSıfırla();
            if (secilenButonlar.Count > 0)
            {
                Button secilenButton = CloneButton(secilenButonlar[0]); // Seçilen butonun kopyasını al
                Form2 form2 = Application.OpenForms["Form2"] as Form2;
                form2.AddButtonToFlowLayoutPanelTOP(secilenButton, form2.yesilIndex); // Form2'deki FlowLayoutPanel'a kopyalanan butonu ekle
            }
            frm2.UpdateButtonsEnability(true);
            Form2.statikBlokEkleBtn.Enabled = true;
            this.Close();
        }
        //içine verilen buttonun özelliklerini kopyalıyoruz.
        private Button CloneButton(Button originalButton)
        {
            Button clonedButton = new Button();
            clonedButton.Text = originalButton.Text;
            clonedButton.Size = originalButton.Size;
            clonedButton.Location = originalButton.Location;
            clonedButton.BackColor = Color.LightGreen;
            clonedButton.ForeColor = originalButton.ForeColor;
            clonedButton.TextAlign = ContentAlignment.MiddleLeft;
            clonedButton.Tag = originalButton.Tag;
            return clonedButton;
        }

        //Blok buttonlarının click eventini oluşturuyoruz.
        //Seçilen buttonun rengi ayarlanacak.
        // secilenButonlar listesi temizlenip, tıklaan buton eklenecek.
        
        private void button_Click(object sender, EventArgs e)
        {
            secilenButonlar.Clear();
            Button btn = sender as Button;
            SetOtherButtonColors(btn);
            secilenButonlar.Add(btn);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmBlok_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm2.UpdateButtonsEnabilityBlok(true);
        }
    }
}
