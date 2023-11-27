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
    public partial class Isitma : UserControl
    {
        //Bağlantı kur.
        SqlConnection baglanti = new SqlConnection(@"Data Source=D15\SQLEXPRESS;Initial Catalog=Recete;Integrated Security=True");
        public int ID { get; set; }
        public Isitma()
        {
            InitializeComponent();
            this.ID = ID;
        }

        private void Isitma_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select defaultDevir,defaultDonusSure,defaultBeklemeSure,defaultSureDK,defaultSicaklik," +
            "minDevir,maxDevir,minDonusSure,MaxDonusSure,minBeklemeSure,maxBeklemeSure,maxSureDK,maxSureSN,maxSicaklik from TblKitap1 where isActive=1 AND CommandID=1",baglanti);
            cmd.Parameters.AddWithValue("@p1", ID);
            baglanti.Open();

        }
    }
}
