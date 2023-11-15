using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        public FrmBlok()
        {
            InitializeComponent();
        }
        //Tıklanan(seçili) buttonu atmak için liste oluşturuyoruz.
        public List<Button> secilenButonlar = new List<Button>();

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmBlok_Load(object sender, EventArgs e)
        {

        }
      
        //Listedeki button için eğer tıklanan buttonsa arkaplan yeşil yap diğerlerini beyaz yap.
        public void SetOtherButtonColors(Button clickedButton)
        {
    
            foreach (Button control in flowLayoutPanel1.Controls.OfType<Button>())
            {
                if (control == clickedButton)
                    control.BackColor = Color.LightGreen;
                else
                    control.BackColor = Color.White;
            }
        }
        //Ekle butonuna basınca, seçilen buton varsa oluşturduğun klonlama metoduyla seçilen butonu kopyala
        //Seçilen butonu Form2'yi çalıştırıp flow panele ekle.
        public void btnEkle_Click(object sender, EventArgs e)
        {
            if (secilenButonlar.Count > 0)
            {
                Button secilenButton = CloneButton(secilenButonlar[0]); // Seçilen butonun kopyasını al
                Form2 form2 = Application.OpenForms["Form2"] as Form2;
                form2.AddButtonToFlowLayoutPanel(secilenButton); // Form2'deki FlowLayoutPanel'a kopyalanan butonu ekle
            }
        }
        //içine verilen buttonun özelliklerini kopyalıyoruz.
        private Button CloneButton(Button originalButton)
        {
            Button clonedButton = new Button();
            clonedButton.Text = originalButton.Text;
            clonedButton.Size = originalButton.Size;
            clonedButton.Location = originalButton.Location;
            clonedButton.BackColor = Color.CornflowerBlue;
            clonedButton.ForeColor = SystemColors.ButtonHighlight;

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


    }
}
