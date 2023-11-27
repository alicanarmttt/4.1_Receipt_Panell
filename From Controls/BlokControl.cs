using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace ReceteMain
{
    public partial class BlokControl : UserControl
    {
        //Bağlantı kur.
        SqlConnection baglanti = new SqlConnection(@"Data Source=D15\SQLEXPRESS;Initial Catalog=Recete;Integrated Security=True");

        //ID yi dışarıdan almak için yaratıyoruz.
        public int ID { get; set; }
        public BlokControl(int ID)
        {
            InitializeComponent();
            this.ID = ID;

        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Blok_Load(object sender, EventArgs e)
        {
            defaultValues();
        }

        // SQL'den ID'ye bakarak istediğimiz verileri okuyup textbox'a yazıyoruz.
        private void defaultValues()
        {
            try
            {
                SqlCommand kmt = new SqlCommand("select CommandName from TblKitap1 where CommandID=@p1 ", baglanti);
                kmt.Parameters.AddWithValue("@p1", ID);
                baglanti.Open();
                SqlDataReader rd = kmt.ExecuteReader();
                if (rd.Read())
                {
                    textBox1.Text = rd["CommandName"].ToString();
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
