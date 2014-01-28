using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PRATIKSIS
{
    public partial class frmFIYAT : Form
    {
        DbClass db = new DbClass();
        DataSet ds = new DataSet();
        string sql = "SELECT ADI,KDVSIZ_SATIS_FIYATI FROM URUN";
       
        public frmFIYAT()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int yuzde = Convert.ToInt32(maskedTextBox1.Text);
            string sql1 = "UPDATE URUN SET KDVSIZ_SATIS_FIYATI=KDVSIZ_SATIS_FIYATI+KDVSIZ_SATIS_FIYATI*"+yuzde+"/100";
            db.ExecuteNonQuery(sql1);
            ds = db.Fill(sql1,"URUN");
            dataGridView1.DataSource = ds.Tables["URUN"];
        
        }

        private void frmFIYAT_Load(object sender, EventArgs e)
        {
            ds = db.Fill(sql,"URUN");
            dataGridView1.DataSource=ds.Tables["URUN"];
        }

    
    }
}
