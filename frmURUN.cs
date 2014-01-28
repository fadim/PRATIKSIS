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
    public partial class frmURUN : Form
    {
        DataSet ds = new DataSet();
        string sql = "SELECT * FROM URUN";
        DbClass db = new DbClass();
        CurrencyManager cm;
        public frmURUN()
        {
            InitializeComponent();
        }

        private void frmURUN_Load(object sender, EventArgs e)
        {
            ds = db.Fill(sql, "URUN");
            DataSet ds1 = db.Fill("SELECT * FROM KATEGORI", "KATEGORI");
            cmbKATEGORI_REFNO.DisplayMember = "KATEGORI_ADI";
            cmbKATEGORI_REFNO.ValueMember = "KATEGORI_REFNO";
            cmbKATEGORI_REFNO.DataSource = ds1.Tables["KATEGORI"];

            DataSet ds2 = db.Fill("SELECT * FROM MUSTERI", "MUSTERI");
            cmbTEDARIKCI_REFNO.DisplayMember = "UNVANI";
            cmbTEDARIKCI_REFNO.ValueMember = "MUSTERI_REFNO";
            cmbTEDARIKCI_REFNO.DataSource = ds2.Tables["MUSTERI"];

            DataSet ds3 = db.Fill("SELECT * FROM KDV", "KDV");
            cmbALIS_KDV_ORANI.DisplayMember = "KDV_ORANI";
            cmbALIS_KDV_ORANI.ValueMember = "KDV_REFNO";
            cmbALIS_KDV_ORANI.DataSource = ds3.Tables["KDV"];

            cmbSATIS_KDV_ORANI.DisplayMember = "KDV_ORANI";
            cmbSATIS_KDV_ORANI.ValueMember = "KDV_REFNO";
            cmbSATIS_KDV_ORANI.DataSource = ds3.Tables["KDV"];
       
       

            cm = this.BindingContext[ds.Tables["URUN"]] as CurrencyManager;
            //veri bağlama
            txtADI.DataBindings.Add("Text", ds.Tables["URUN"], "ADI");
            txtBARKODU.DataBindings.Add("Text", ds.Tables["URUN"], "BARKODU");
            txtBIRIM1.DataBindings.Add("Text", ds.Tables["URUN"], "BIRIM1");
            txtBIRIM2.DataBindings.Add("Text", ds.Tables["URUN"], "BIRIM2");
            txtKDVSIZ_ALIS_FIYATI.DataBindings.Add("Text", ds.Tables["URUN"], "KDVSIZ_ALIS_FIYATI");
            txtKDVSIZ_SATIS_FIYATI.DataBindings.Add("Text", ds.Tables["URUN"], "KDVSIZ_SATIS_FIYATI");
            txtURUN_REFNO.DataBindings.Add("Text", ds.Tables["URUN"], "URUN_REFNO");
            cmbKATEGORI_REFNO.DataBindings.Add("SelectedValue", ds.Tables["URUN"], "KATEGORI_REFNO");
            cmbALT_KATEGORI_REFNO.DataBindings.Add("SelectedValue", ds.Tables["URUN"], "ALT_KATEGORI_REFNO");
            cmbTEDARIKCI_REFNO.DataBindings.Add("SelectedValue", ds.Tables["URUN"], "TEDARIKCI_REFNO");

            cm.PositionChanged += new EventHandler(cm_PositionChanged);
            cm_PositionChanged(null,null);

        }

        void cm_PositionChanged(object sender, EventArgs e)
        {
            label14.Text = (cm.Position + 1) + "/" + cm.Count;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            cm.Position = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cm.Position--;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            cm.Position++;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cm.Position = cm.Count - 1;
        }

        private void button5_Click(object sender, EventArgs e)
        {//yeni
            cm.AddNew();
        }

        private void button6_Click(object sender, EventArgs e)
        {//kaydet
            cm.EndCurrentEdit();
            db.DataSetUpdate(ds,sql,"URUN");
            MessageBox.Show("Kayıt Güncellendi.");
        }

        private void button7_Click(object sender, EventArgs e)
        {//sil
            cm.RemoveAt(cm.Position);
            db.DataSetUpdate(ds, sql, "URUN");
            MessageBox.Show("Kayıt Silindi.");
        }

        private void button8_Click(object sender, EventArgs e)
        {//vazgeç
            cm.CancelCurrentEdit();
        }

        private void cmbKATEGORI_REFNO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbKATEGORI_REFNO.SelectedIndex > -1)
            {
                string sql = "SELECT * FROM ALT_KATEGORI WHERE KATEGORI_REFNO=" +
                    cmbKATEGORI_REFNO.SelectedValue.ToString();
                DataSet ds1 = db.Fill(sql,"ALT_KATEGORI");
                cmbALT_KATEGORI_REFNO.DisplayMember = "ALT_KATEGORI_ADI";
                cmbALT_KATEGORI_REFNO.ValueMember = "ALT_KATEGORI_REFNO";
                cmbALT_KATEGORI_REFNO.DataSource = ds1.Tables["ALT_KATEGORI"];
                


            }
        }
    }
}
