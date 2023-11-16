using ReceteMain.From_Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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



        //Buttonu flow panelin içine ekliyoruz ve click eventi buttona atıyoruz. 

        public void AddButtonToFlowLayoutPanel(Button button)
        {
            button.Click += Button_Click;
            flowRecetePanel.Controls.Add(button);
            int buttonCount = flowRecetePanel.Controls.OfType<Button>().Count();
            txtKomutSay.Text = buttonCount.ToString();

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
             if(control.Width>130)
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
                    //btnKmtAc.Enabled = true;
                    anaPanel.Controls.Add(komutControl1);

                    break;
                case 48:
                case 49:
                case 50:
                    btn.BackColor = Color.LightGreen;
                    anaPanel.Controls.Clear();
                    BlokControl blokControl = new BlokControl(Convert.ToInt32(btn.Tag)); //UserControlü FrmBlok üzerindeki Tag==ID'sine göre çağırıyoruz.
                    anaPanel.Controls.Add(blokControl);
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

            // Kaldırılacak olan kontrolleri belirle
            foreach (Button control in flowRecetePanel.Controls.OfType<Button>())
            {
                if (control.BackColor == Color.LightGreen)
                {
                    controlsToRemove.Add(control);
                }
            }

            // Belirlenen kontrolleri kaldır
            foreach (Button controlToRemove in controlsToRemove)
            {
                flowRecetePanel.Controls.Remove(controlToRemove);
                int buttonCount = flowRecetePanel.Controls.OfType<Button>().Count();
                txtKomutSay.Text = buttonCount.ToString();
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
        //flowpanele control eklenmeden komut açılmıyor.
        private void flowRecetePanel_ControlAdded(object sender, ControlEventArgs e)
        {
            if (flowRecetePanel.Controls.Count > 0)
            {
                btnKomut.Enabled = true;
            }
        }
    }
}
