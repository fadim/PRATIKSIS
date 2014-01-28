using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.OleDb;
using System.Windows.Forms;

public class DbClass
{

    string cnnstr = "Provider=Microsoft.Jet.OLEDB.4.0;Mode=Share Deny None;Data Source=" +
    Application.StartupPath + "/App_Data/PRATIK.mdb;User ID=Admin;Password=;";

    public  OleDbConnection cnn = new OleDbConnection();

    public DbClass()
    {
        cnn.ConnectionString = cnnstr;
    }

    public DataSet Fill(string sql, string tabloadi)
    {
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(sql, cnn);
        da.FillSchema(ds, SchemaType.Source, tabloadi);//�emay� y�kler, G�ncellemelerde DataSetUpdate i�in gereklidir.
        da.Fill(ds, tabloadi);
        return ds;
    }

    public void DataSetUpdate(DataSet ds, string sql, string tabloadi)
    { //Insert, Update Delete i�in PK kullan�l�r
        OleDbDataAdapter da = new OleDbDataAdapter(sql, cnn);
        OleDbCommandBuilder cmd = new OleDbCommandBuilder(da);//insert update delete otomatik olu�turuluyor
        if (ds.HasChanges())
        {
            da.Update(ds, tabloadi);//DataSetteski bilgiler veritaban�na kal�c� bir �ekilde i�leniyor
        }
       
    }

    public OleDbDataReader ExecuteReader(string sql)
    {   //ileri do�ru salt okuma yapan bir sat�r d�nd�r�r.
        OleDbCommand cmd = new OleDbCommand(sql,cnn);
        if (cnn.State == ConnectionState.Closed)
        {
            cnn.Open();
        }
        OleDbDataReader dr=cmd.ExecuteReader(CommandBehavior.CloseConnection);
        return dr; //dr conncection'u kitler. dr'nin i�i bittikten sonra kapat�lmas� gerek
                   //connection kitli iken di�er sorgular� �al��t�ramaz.
    }

    public void ExecuteNonQuery (string sql)
    {  //her t�rl� sorgu �al���r
        OleDbCommand cmd = new OleDbCommand(sql, cnn);
        if (cnn.State == ConnectionState.Closed)
        {
            cnn.Open();
        }
        try
        {
            cmd.ExecuteNonQuery();
        }
        finally
        {
            cnn.Close();
        }
       
    }

    public  object ExecuteScalar(string sql)
    { //sadece tek de�er d�nd�r�r
        object obj;
        OleDbCommand cmd = new OleDbCommand(sql, cnn);
        if (cnn.State == ConnectionState.Closed)
        {
            cnn.Open();
        }
        try
        {
             obj=cmd.ExecuteScalar();
        }
        finally
        {
            cnn.Close();
        }

        return obj;

    }

   
}
