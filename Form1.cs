using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PRATIKSIS
{
    public partial class Form1 : Form
    {
        DbClass db = new DbClass();
        DataSet dsurun = new DataSet();
        string sqlurun = "SELECT * FROM URUN";
        public Form1()
        {
            InitializeComponent();
        }

        private void ürünKartıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmURUN f1 = new frmURUN();
            f1.ShowDialog();
        }

        private void müşteriKartıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMUSTERI f2 = new frmMUSTERI();
            f2.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void xMLÇıktıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dsurun = db.Fill(sqlurun,"URUN");
            dsurun.DataSetName = "URUNLER";
            dsurun.WriteXml(Application.StartupPath+"/URUN.XML");
            System.Diagnostics.Process.Start(Application.StartupPath + "/URUN.XML");
        }

        private void textÇıktıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dsurun = db.Fill(sqlurun, "URUN");
           StreamWriter sw = new StreamWriter(Application.StartupPath + "/URUN.TXT",false,Encoding.Default);
            string satir="";
           for (int index = 0; index < dsurun.Tables["URUN"].Columns.Count; index++)
           {
               satir = satir + dsurun.Tables["URUN"].Columns[index].ColumnName + "|";
           }
           sw.WriteLine(satir);
           for (int i = 0; i < dsurun.Tables["URUN"].Rows.Count; i++)
           {
               satir = "";
               for (int index = 0; index < dsurun.Tables["URUN"].Columns.Count; index++)
               {
                   satir = satir + dsurun.Tables["URUN"].Rows[i][index].ToString() + "|";
               }
               sw.WriteLine(satir);
           }
           sw.Close();
           System.Diagnostics.Process.Start(Application.StartupPath + "/URUN.TXT");
        
        }

        private void ürünListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ürün listesi
            i = 0;
            dsurun = db.Fill(sqlurun, "URUN");
            printPreviewDialog1.ShowDialog();
        }
        int i;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            e.Graphics.DrawString("ÜRÜN ADI",
                   new Font("Arial", 15),
                  new SolidBrush(Color.Black), x, y);

            e.Graphics.DrawString("SATIŞ FİYATI",
                 new Font("Arial", 15),
                new SolidBrush(Color.Black), x + 500, y);

            float satiryuk = e.Graphics.MeasureString("Test",new Font("Arial",12)).Height;
            int sayfadakisatir = Convert.ToInt32(e.MarginBounds.Height / satiryuk);

            y = y + Convert.ToInt32(satiryuk);

            for (; i < dsurun.Tables["URUN"].Rows.Count; i++)
            {
                e.Graphics.DrawString(dsurun.Tables["URUN"].Rows[i]["ADI"].ToString(),
                     new Font("Arial", 12),
                    new SolidBrush(Color.Black), x, y);

                e.Graphics.DrawString(dsurun.Tables["URUN"].Rows[i]["KDVSIZ_SATIS_FIYATI"].ToString(),
                     new Font("Arial",12),               
                    new SolidBrush(Color.Black), x+500, y);

                y = y + Convert.ToInt32(satiryuk);

                if ((i + 1) % sayfadakisatir == 0)
                {
                  
                    i++;//yeni sayfada bir sonraki satırdan başlayarak yaz
                    e.HasMorePages = true; //yeni sayfada yaz
                    break; //döngüden çıkkı yeni sayfada yazabil
                }
            }
        }

        private void fiyatDeğiştirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFIYAT f2 = new frmFIYAT();
            f2.ShowDialog();
        }
    }
}
