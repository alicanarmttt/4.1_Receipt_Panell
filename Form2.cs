﻿using ReceteMain.From_Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReceteMain
{
    public partial class Form2 : Form
    {

        //Form2 içinde tanımlanan bir olay

        //Bu iki blok kapanırsa ileri butonu aktif olsun.
        
        static Form2 _obj;
        //Instance yaratıyoruz.
        public static Form2 Instance
        {
            get
            {
                if (_obj == null)
                    _obj = new Form2();
                return _obj;
            }
        }
        //ileri butonunu static hale getiriyoruz
        public static Button statikIleriButon;
        public static Button statikBlokEkleBtn;
        public static Button statikKomutEkleBtn;
        public static Button statikSilBtn;
        public Form2()
        {
            _obj = this;
            InitializeComponent();
            IsMdiContainer = true;

        }
        
        //Btn görünürlüğü.
        public void UpdateButtonsEnability(bool isVisible)
        {
            btnSil.Enabled = isVisible;
            btnBlok.Enabled = isVisible;
        }

        public void UpdateButtonsEnabilityBlok(bool isVisible)
        {
            btnSil.Enabled = isVisible;
            btnKomut.Enabled = isVisible;
        }
        
        //Bu metod içine verilen formu anaPanele control olarak ekler.
        public void FormGetir(Form Frm)
        {
            try
            {
                anaPanel.Controls.Clear();
                Frm.MdiParent = this;
                Frm.FormBorderStyle = FormBorderStyle.None;
                Frm.TopLevel = false;
                Frm.Dock = DockStyle.Fill;
                anaPanel.Controls.Add(Frm);
                Frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata form Ana Panele eklenemedi: " + ex.Message);
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {

            statikIleriButon = btnIleri;
            statikBlokEkleBtn = btnBlok;
            statikKomutEkleBtn = btnKomut;
            statikSilBtn = btnSil;
            //Button (Toplam Komut) sayısını göster.
            int buttonCount = flowRecetePanel.Controls.OfType<Button>().Count();
            txtKomutSay.Text = buttonCount.ToString();

            //Form2 yüklendiğinde flowda hiç button yoksa Komut eklenemesin.
            if (flowRecetePanel.Controls.OfType<Button>().Count() == 0)
            {
                btnKomut.Enabled = false;
            }
            else
            {
                btnKomut.Enabled = true;
            }
            if (flowRecetePanel.Controls.Count == 0)
            {
                btnSil.Enabled = false;
            }
        }
       

        //ALTA BUTTON EKLEME METODU
        public void AddButtonToFlowLayoutPanel(Button button, int targetIndex)
        {

            //KOMUT EKLEMEK İSTİYORSAK

            if (Convert.ToInt32(button.Tag) < 48)
            {
                button.Click += Button_Click;

            
                    flowRecetePanel.Controls.Add((button));
                    flowRecetePanel.Controls.SetChildIndex(button, targetIndex + 1);
                
            }

            //BLOK EKLEMEK İSTİYORSAK

            if (Convert.ToInt32(button.Tag) > 47)
            {
                button.Click += Button_Click;
                

                //EĞER FLOWDA BUTTON YOKSA
                if (flowRecetePanel.Controls.Count == 0)
                {
                    flowRecetePanel.Controls.Add((button));

                }
                //EĞER FLOWDA BUTTON VARSA
                else if (flowRecetePanel.Controls.Count > 0)
                {

                    for (int i = targetIndex; i < flowRecetePanel.Controls.Count; i++)
                    {
                        //Son elemana kadar taradıysak. Blok bulamamışız demektir.
                        if (i == flowRecetePanel.Controls.Count - 1)

                        {
                            Control last = flowRecetePanel.Controls[flowRecetePanel.Controls.Count - 1];
                            if (Convert.ToInt32(last.Tag) < 48) //flow un son elemanı komut ise bunu yap.
                            {
                                flowRecetePanel.Controls.Add((button));
                                flowRecetePanel.Controls.SetChildIndex(button, i + 1);
                                break;
                            }
                            else if(Convert.ToInt32(last.Tag) > 47)//flow un son elemanı blok ise bunu yap.
                            {
                                flowRecetePanel.Controls.Add((button));
                                flowRecetePanel.Controls.SetChildIndex(button, i + 1);
                                break;
                            }
                        }
                        //Blok ile karşılaşırsa bunu yap 
                        //flowpanelin bir sonraki indexindeki button Bloksa, oraya ekle.
                        else if (Convert.ToInt32((flowRecetePanel.Controls[i+1]).Tag) > 47) 
                        {
                            
                            flowRecetePanel.Controls.Add((button));
                            flowRecetePanel.Controls.SetChildIndex(button, i + 1);
                            break;
                        }

                    }
                }

            }
        }
        //Seçili buton varsa üstüne ekleme yapıyoruz. Ve buttonun click eventini atıyoruz.
        public void AddButtonToFlowLayoutPanelTOP(Button button, int targetIndex)
        {
            button.Click += Button_Click;
            
            //butonu eklerken indexini yeşile göre seç.
            flowRecetePanel.Controls.Add((button));
            flowRecetePanel.Controls.SetChildIndex(button, targetIndex - 1);

        }
        
        //click eventimizi Button taggine göre tüm taglere ekliyoruz.
        //click event==Blok Control adındaki usercontrolü anaPanele eklemek.
        public void Button_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;
  
            // Child form kapanırken ana formdaki buttonları güncelle
            UpdateButtonsEnability(true);
            UpdateButtonsEnabilityBlok(true);

            //her butonu kontrol edip tıklanmayan butonların rengini orijinaline çeviriyoruz.
            foreach (Button control in flowRecetePanel.Controls.OfType<Button>())
            {
                if (control.Width > 130)
                {
                    control.BackColor = Color.CornflowerBlue;
                }
                else
                {
                    control.BackColor = SystemColors.InactiveCaption;
                }
            }
            //Oluşan butonun tag'ine bakarak çağırılacak kontrolü anaPanel'e ekliyoruz.
            switch (Convert.ToInt32(btn.Tag))
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                    btn.BackColor = Color.LightGreen;
                    anaPanel.Controls.Clear();
                    KomutControl1 komutControl1 = new KomutControl1(Convert.ToInt32(btn.Tag)); //UserControlü FrmKomut üzerindeki Tag==ID'sine göre çağırıyoruz.
                    anaPanel.Controls.Add(komutControl1);
                    btnKomut.Enabled = true;
                    btnBlok.Enabled = false;
                    btnSil.Enabled = true; ;
                    break;

                case 48:
                case 49:
                case 50:
                    btn.BackColor = Color.LightGreen;
                    anaPanel.Controls.Clear();
                    BlokControl blokControl = new BlokControl(Convert.ToInt32(btn.Tag)); //UserControlü FrmBlok üzerindeki Tag==ID'sine göre çağırıyoruz.
                    anaPanel.Controls.Add(blokControl); 
                    btnBlok.Enabled= true;
                    break;
                default:
                    break;
            }

        }
        //Blok ve komut ekleme elemanlarında her türlü button inaktiftir.
        private void btnBlok_Click_1(object sender, EventArgs e)
        {
            
            FrmBlok frmb = new FrmBlok();
            FormGetir(frmb);
            UpdateButtonsEnabilityBlok(false);
            foreach (Control control in flowRecetePanel.Controls)
            {
                control.Enabled = false;
            }
            btnBlok.Enabled=false;
            //btnIleri.Enabled = false;
        }

        private void btnKomut_Click_1(object sender, EventArgs e)
        {
            FrmKomut frmk = new FrmKomut();
            FormGetir(frmk);
            UpdateButtonsEnability(false);
            foreach (Control control in flowRecetePanel.Controls)
            {
                control.Enabled = false;
            }
            btnKomut.Enabled = false;
            //btnIleri.Enabled = false;
        }

        //SİLME BUTONU
        private void btnSil_Click(object sender, EventArgs e)
        {
            anaPanel.Controls.Clear();
            //Silinecek olanı tutacağımız listeyi oluştur.
            List<Button> controlsToRemove = new List<Button>();
            int kacıncıEleman = 0;

            // Kaldırılacak olan kontrolü bulup listeye al.
            foreach (Button control in flowRecetePanel.Controls.OfType<Button>())
            {
                //counter ile kaçıncı index olduğunu buluyoruz.
                kacıncıEleman++;
                if (control.BackColor == Color.LightGreen)
                {
                    controlsToRemove.Add(control);
                    break;
                }
            }

            // Silinecek butonla ilgili 2 Olasılık var.

            //1-Yeşil (SEÇİLİ) button BLOK İSE: 
            if (Convert.ToInt32(flowRecetePanel.Controls[kacıncıEleman - 1].Tag) > 47)
            {

                List<Button> topluSilineceklerList = new List<Button>();
                for (int i = kacıncıEleman - 1; i < flowRecetePanel.Controls.Count; i++)
                {

                    Button mevcutButton = (Button)flowRecetePanel.Controls[i];
                    topluSilineceklerList.Add(mevcutButton);
                    //Eğer eklediğimiz son buttondan sonraki button blok türünde ise döngüyü durduruyoruz. 
                    if (flowRecetePanel.Controls.Count - 1 > i)
                    {
                        if (Convert.ToInt32(flowRecetePanel.Controls[i+1].Tag) > 47)
                        {
                            //Seçili butonu bir sonraki yapıyoruz.
                            flowRecetePanel.Controls[i + 1].BackColor = Color.LightGreen;
                            break;
                        }   
                    }
                    if(i==flowRecetePanel.Controls.Count-1 && i!=0)
                    {
                        flowRecetePanel.Controls[kacıncıEleman-2].BackColor = Color.LightGreen;
                        break;
                    }
                }
                
                //Listeye eklenenleri tek tek siliyoruz.
                foreach (Button silinecekButton in topluSilineceklerList)
                {
                    flowRecetePanel.Controls.Remove(silinecekButton);
                }
                yesilButtonuAc();
            }

            //2-Yeşil (SEÇİLİ) button KOMUT İSE: 
            else if (Convert.ToInt32(flowRecetePanel.Controls[kacıncıEleman - 1].Tag) < 48)
            {
                // Listedeki kontrolü kaldır.
                foreach (Button controlToRemove in controlsToRemove)
                {
                    flowRecetePanel.Controls.Remove(controlToRemove);
                }

                //Eğer varsa seçili butonu bir sonraki yapıyoruz.
                if (flowRecetePanel.Controls[kacıncıEleman - 2] != null)
                {
                    flowRecetePanel.Controls[kacıncıEleman - 2].BackColor = Color.LightGreen;
                    yesilButtonuAc();
                    controlsToRemove.Clear();
                }
            }
            if (flowRecetePanel.Controls.Count == 0)
            {
                btnSil.Enabled = false;
            }
       
        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Form1'i göster
            Form1 form1 = new Form1();
            form1.Show();
        }
      
        //Flowpanele Blok eklenmeden komut açılmıyor.
        public void flowRecetePanel_ControlAdded(object sender, ControlEventArgs e)
        {

            //Toplam komut sayma işlemi
            int buttonCount = flowRecetePanel.Controls.OfType<Button>().Count();
            txtKomutSay.Text = buttonCount.ToString();
            //Flow a button eklendiğinde yeşil olan paneli aç.
            yesilButtonuAc();
            flowuAktifYap(true);
        }

        public void flowRecetePanel_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (flowRecetePanel.Controls.Count == 0)
            {
                btnKomut.Enabled = false;
                btnBlok.Enabled = true;
                
            }
            //Toplam komut sayma işlemi
            int buttonCount = flowRecetePanel.Controls.OfType<Button>().Count();
            txtKomutSay.Text = buttonCount.ToString();
            //yesilButtonuAc();
        }
       
        public int yesilIndex { get; set; }
        public void renkSıfırla()
        {
            foreach (Button control in flowRecetePanel.Controls.OfType<Button>())
            {
                if (control.BackColor == Color.LightGreen)
                {
                    yesilIndex = flowRecetePanel.Controls.GetChildIndex(control);
                    

                }
                if (control.Width > 130)
                {
                    control.BackColor = Color.CornflowerBlue;

                }

                else
                {

                    control.BackColor = SystemColors.InactiveCaption;
                }
            }

        }
        public void flowuAktifYap(bool aktiflik)
        {
            foreach (Control control in flowRecetePanel.Controls)
            {
                control.Enabled = aktiflik;
            }
        }

        private void btnIleri_Click(object sender, EventArgs e)
        {
            //flowuAktifYap(true);
            //anaPanel.Controls.Clear();
            //btnKomut.Enabled = true;
            //btnSil.Enabled=true;
        }
        //Yeşil buttonu ekrana aç
        private void yesilButtonuAc()
        {
            foreach (Button button in flowRecetePanel.Controls.OfType<Button>())
            {
                if (button.BackColor == Color.LightGreen)
                {
                    button.Click += Button_Click;
                    button.PerformClick();
                }
            }
        }

        private void anaPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            
        }
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
