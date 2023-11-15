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
    public partial class KomutControl1 : UserControl
    {
        //Bağlantı kur.
        SqlConnection baglanti = new SqlConnection(@"Data Source=D15\SQLEXPRESS;Initial Catalog=Recete;Integrated Security=True");
        public int ID { get; set; }
        public KomutControl1(int ID)
        {
            InitializeComponent();
            this.ID = ID;
        }

        private void KomutControl1_Load(object sender, EventArgs e)
        {
            defaultValues();
        }
        // SQL'den ID'ye bakarak istediğimiz verileri okuyup textbox'a yazıyoruz.
        private void defaultValues()
        {
            try
            {

                SqlCommand kmt = new SqlCommand("select komut from TblRecete where KomutID=@p1 ", baglanti);
                kmt.Parameters.AddWithValue("@p1", ID);
                baglanti.Open();
                SqlDataReader rd = kmt.ExecuteReader();
                if (rd.Read())
                {
                    textBox1.Text = rd["komut"].ToString();
                }
                baglanti.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
