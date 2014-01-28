using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace PRATIKSIS
{
    public partial class frmMUSTERI : Form
    {
        DataSet ds = new DataSet();
        string sql = "SELECT * FROM MUSTERI";
        DbClass db = new DbClass();
        CurrencyManager cm;
        public frmMUSTERI()
        {
            InitializeComponent();
        }

        private void frmMUSTERI_Load(object sender, EventArgs e)
        {
            ds = db.Fill(sql, "MUSTERI");
            cm = this.BindingContext[ds.Tables["MUSTERI"]] as CurrencyManager;

            txtMUSTERI_REFNO.DataBindings.Add("Text", ds.Tables["MUSTERI"], "MUSTERI_REFNO");
            txtUNVANI.DataBindings.Add("Text", ds.Tables["MUSTERI"], "UNVANI");
            txtILGILI_KISI.DataBindings.Add("Text", ds.Tables["MUSTERI"], "ILGILI_KISI");
            txtSEHIR.DataBindings.Add("Text", ds.Tables["MUSTERI"], "SEHIR");
            txtPOSTA_KODU.DataBindings.Add("Text", ds.Tables["MUSTERI"], "POSTA_KODU");
            txtADRES.DataBindings.Add("Text", ds.Tables["MUSTERI"], "ADRES");
            txtTELEFON1.DataBindings.Add("Text", ds.Tables["MUSTERI"], "TELEFON1");
            txtTELEFON2.DataBindings.Add("Text", ds.Tables["MUSTERI"], "TELEFON2");
            txtTELEFON3.DataBindings.Add("Text", ds.Tables["MUSTERI"], "TELEFON3");
            txtFAX.DataBindings.Add("Text", ds.Tables["MUSTERI"], "FAX");
            txtE_POSTA.DataBindings.Add("Text", ds.Tables["MUSTERI"], "E_POSTA");
            txtWEB_ADRESI.DataBindings.Add("Text", ds.Tables["MUSTERI"], "WEB_ADRESI");
            txtACIKLAMA.DataBindings.Add("Text", ds.Tables["MUSTERI"], "ACIKLAMA");

            txtBORC.DataBindings.Add("Text", ds.Tables["MUSTERI"], "BORC");
            txtALACAK.DataBindings.Add("Text", ds.Tables["MUSTERI"], "ALACAK");
            txtBAKIYE.DataBindings.Add("Text", ds.Tables["MUSTERI"], "BAKIYE");
            txtVERGI_DAIRESI.DataBindings.Add("Text", ds.Tables["MUSTERI"], "VERGI_DAIRESI");
            txtVERGI_NUMARASI.DataBindings.Add("Text", ds.Tables["MUSTERI"], "VERGI_NUMARASI");
                        
        }

        private void button1_Click(object sender, EventArgs e)
        {// <<
            cm.Position = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {// <
            cm.Position--;

        }

        private void button3_Click(object sender, EventArgs e)
        {// >
            cm.Position++;
        }

        private void button4_Click(object sender, EventArgs e)
        {// >>
            cm.Position = cm.Count - 1;
        }

        private void button5_Click(object sender, EventArgs e)
        {//yeni
            cm.AddNew();
        }

        private void button6_Click(object sender, EventArgs e)
        {//kaydet
            cm.EndCurrentEdit();
        }

        private void button7_Click(object sender, EventArgs e)
        {//sil
            cm.RemoveAt(cm.Position);
        }

        private void button8_Click(object sender, EventArgs e)
        {//vazgeç
            cm.CancelCurrentEdit();
        }
    }
}
