using ReceteMain.From_Controls;
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
        public Form2()
        {
            _obj = this;
            InitializeComponent();
            IsMdiContainer = true;

        }
        public FlowLayoutPanel GetFlowRecetePanel()
        {
            return flowRecetePanel;
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
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {

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
            
        }
        //flowpaneldeki her kontrole baktığımda rengi green olanın indexini alıyoruz.
        //yoksa da toplam button sayısını integer olarak alıyoruz.
        private int GetIndexOfGreenControl(Button button)
        {
            foreach (Control control in flowRecetePanel.Controls)
            {
                if (control.BackColor == Color.LightGreen && flowRecetePanel.Controls.IndexOf(control)!=0)
                {
                    return flowRecetePanel.Controls.IndexOf(control);
                }
                else if (control.BackColor == Color.LightGreen && flowRecetePanel.Controls.IndexOf(control) == 0)
                {
                    return flowRecetePanel.Controls.IndexOf(control)+1;
                }
            }
            return 0;

        }
        
        //Seçili buton varsa altına ekleme yapıyoruz. Ve Buttonun click eventini atıyoruz.
        public void AddButtonToFlowLayoutPanel(Button button)
        {   
            //KOMUT EKLEMEK İSTİYORSAK

            if (Convert.ToInt32(button.Tag) < 48)
            {
                button.Click += Button_Click;
                int targetIndex = GetIndexOfGreenControl(button);
                                      
                if (targetIndex > 0)  //Eğer yeşil button varsa.
                {
                    flowRecetePanel.Controls.Add((button));
                    flowRecetePanel.Controls.SetChildIndex(button, targetIndex + 1);
                }
                if (targetIndex == 0) //Eğer seçili yeşil buton yoksa.

                {
                    int targetIndexNew = flowRecetePanel.Controls.Count;
                    flowRecetePanel.Controls.Add((button));
                    flowRecetePanel.Controls.SetChildIndex(button, targetIndexNew + 1);
                }
            }
            
            //BLOK EKLEMEK İSTİYORSAK

            if (Convert.ToInt32(button.Tag) > 47)
            {   
                button.Click += Button_Click;
                //Yeşil button varsa indexini integer olarak getirir. Yeşil button ilk elemansa +1 eklenir. ()
                //Yeşil seçili değilse 0 getirir. 
                int targetIndex = GetIndexOfGreenControl(button);
                if (targetIndex > 0) //Eğer içeride yeşil bir button seçiliyse
                    
                {
                    for (int i = targetIndex + 1; i < flowRecetePanel.Controls.Count; i++)
                    {
                        if (Convert.ToInt32((flowRecetePanel.Controls[i]).Tag) > 47) //Blok ile karşılaşırsa bunu yap 
                            {
                                //flowpanelin i. indexindeki butto komutsa oraya ekle.
                                flowRecetePanel.Controls.Add((button));
                                flowRecetePanel.Controls.SetChildIndex(button, i);
                                break;
                            }
                        
                        else if(i==flowRecetePanel.Controls.Count-1) //Eğer i tamsayısı son buttona da bakmış ise ulaşmış ise demekki Blok bulamamıştır. 
                                                                     // Yani Flow Panelin son elemanı komut ise 
                        {
                            Control last = flowRecetePanel.Controls[flowRecetePanel.Controls.Count - 1];
                            if (Convert.ToInt32(last.Tag) < 48) //flow un son elemanı komut ise bunu yap.
                            {
                                flowRecetePanel.Controls.Add((button));
                                flowRecetePanel.Controls.SetChildIndex(button, i+1);
                                break;
                            }
                        }
                    }
                }
                if (targetIndex == 0) //Eğer seçili yeşil buton yoksa.
                {

                    int targetIndexNew = flowRecetePanel.Controls.Count;
                    flowRecetePanel.Controls.Add((button));
                    flowRecetePanel.Controls.SetChildIndex(button, targetIndexNew + 1);
                }
                
            }
        }
        //Seçili buton varsa üstüne ekleme yapıyoruz. Ve buttonun click eventini atıyoruz.
        public void AddButtonToFlowLayoutPanelTOP(Button button)
        {
            button.Click += Button_Click;
            // yeşil olan butonun indexini al.
            int targetIndex = GetIndexOfGreenControl(button);
            //butonu eklerken indexini yeşile göre seç.
            flowRecetePanel.Controls.Add((button));
            flowRecetePanel.Controls.SetChildIndex(button, targetIndex - 1);
     
        }

        //click eventimizi Button taggine göre tüm taglere ekliyoruz.
        //click event==Blok Control adındaki usercontrolü anaPanele eklemek.
        private void Button_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;
            //tıklanan butonu clickedButton olarak tut.
            Button clickedButton = sender as Button;
            //Tıklanan butonun indexini al.
            int clickedIndex = flowRecetePanel.Controls.IndexOf(clickedButton);

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
                    btnSil.Enabled = true; ;

                    break;
                case 48:
                case 49:
                case 50:
                    btn.BackColor = Color.LightGreen;
                    // Eğer ilk Blok altına komut eklenmişse o Blok silinmesin.
                    //ilk elema yeşilse indexini bir fazla aldığımız için -1 yazıyoruz.
                    if (GetIndexOfGreenControl(btn)-1 == 0 && flowRecetePanel.Controls.Count > 1)
                    {
                        btnSil.Enabled = false;
                    }
                    anaPanel.Controls.Clear();
                    BlokControl blokControl = new BlokControl(Convert.ToInt32(btn.Tag)); //UserControlü FrmBlok üzerindeki Tag==ID'sine göre çağırıyoruz.
                    anaPanel.Controls.Add(blokControl);
                    btnKomut.Enabled = false;
                    break;
                default:
                    break;
            }

        }

        private void btnBlok_Click_1(object sender, EventArgs e)
        {
            FrmBlok frmb = new FrmBlok();
            FormGetir(frmb);
        }

        private void btnKomut_Click_1(object sender, EventArgs e)
        {
            FrmKomut frmk = new FrmKomut();
            FormGetir(frmk);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            //silinecek olanı listele.
            List<Button> controlsToRemove = new List<Button>();
            int kacıncıEleman = 0;
            // Kaldırılacak olan kontrolü bulup listeye al
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
            //Yeşil olan kontrol eğere Bloksa
            if (Convert.ToInt32(flowRecetePanel.Controls[kacıncıEleman - 1].Tag) > 47)
            {
                //Son button Komut ise böyle yap
                if (Convert.ToInt32(flowRecetePanel.Controls[flowRecetePanel.Controls.Count-1].Tag) < 48)
                {
                    List<Button> topluSilineceklerList = new List<Button>();
                    for (int i = kacıncıEleman - 1; i < flowRecetePanel.Controls.Count; i++)
                    {
                        
                        Button mevcutButton = (Button)flowRecetePanel.Controls[i];
                        topluSilineceklerList.Add(mevcutButton);
                        
                    }
                    foreach (Button silinecekButton in topluSilineceklerList)
                    {
                        flowRecetePanel.Controls.Remove(silinecekButton);
                    }

                }
                    //Son komut Blok ise böyle yap. 
                if (Convert.ToInt32(flowRecetePanel.Controls[flowRecetePanel.Controls.Count - 1 ].Tag) > 47)
                {


                    List<Button> topluSilineceklerList = new List<Button>();
                    //Yeşil buttonun bir sonraki buttonundan başlayarak Komut buttonuna denk gelene kadar i yi arttırak buttonları sil.
                    //i elemanın indekisini temsil ediyor. Çünkü KacıncıIndex=5 bir çıkarsa ise o button 4. index içindedir.
                    for (int i = kacıncıEleman - 1; i < flowRecetePanel.Controls.Count + 1; i++)
                    {
                        
                        Button mevcutButton = (Button)flowRecetePanel.Controls[i];
                        topluSilineceklerList.Add(mevcutButton);

                        //Eğer bir sonraki eleman Blok ise ondan önceki elemanı kaldır ve döngüyü durdur.
                        if (Convert.ToInt32(flowRecetePanel.Controls[i + 1].Tag) > 47)
                        {
                            Button mevcutSonButton1 = (Button)flowRecetePanel.Controls[i];
                            topluSilineceklerList.Add(mevcutSonButton1);
                            break;
                        }

                    }
                    foreach (Button silinecekButton in topluSilineceklerList)
                    {
                        flowRecetePanel.Controls.Remove(silinecekButton);
                    }
                }
            }
            //Yeşil Eleman Komutsa
            else if (Convert.ToInt32(flowRecetePanel.Controls[kacıncıEleman - 1].Tag) < 48)
            {
                // Listedeki kontrolü kaldır.
                foreach (Button controlToRemove in controlsToRemove)
                {
                    flowRecetePanel.Controls.Remove(controlToRemove);

                }
                controlsToRemove.Clear();
            }
            else
            {
                //Eğer seçili kontrol yoksa, son kontrolü kaldır.
                if (controlsToRemove.Count == 0 && flowRecetePanel.Controls.Count > 0)
                {
                    Control lastButton = flowRecetePanel.Controls[flowRecetePanel.Controls.Count - 1];
                    flowRecetePanel.Controls.Remove(lastButton);
                }
            }
            
        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Form1'i göster
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void btnKmtAc_Click(object sender, EventArgs e)
        {

        }
        //Flowpanele Blok eklenmeden komut açılmıyor.
        public void flowRecetePanel_ControlAdded(object sender, ControlEventArgs e)
        {
            if (flowRecetePanel.Controls.Count > 0)
            {
                btnKomut.Enabled = true;
            }
            int buttonCount = flowRecetePanel.Controls.OfType<Button>().Count();
            txtKomutSay.Text = buttonCount.ToString();
        }

        public void flowRecetePanel_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (flowRecetePanel.Controls.Count == 0)
            {
                btnKomut.Enabled = false;
            }
            int buttonCount = flowRecetePanel.Controls.OfType<Button>().Count();
            txtKomutSay.Text = buttonCount.ToString();
        }
        //Buttona tıklanırsa renkler orijinale döner.
        private void btnSecımKaldır_Click(object sender, EventArgs e)
        {
            btnKomut.Enabled = true;
            btnSil.Enabled = true;  
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
        }
    }
}
