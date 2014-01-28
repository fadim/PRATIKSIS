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
        da.FillSchema(ds, SchemaType.Source, tabloadi);//þemayý yükler, Güncellemelerde DataSetUpdate için gereklidir.
        da.Fill(ds, tabloadi);
        return ds;
    }

    public void DataSetUpdate(DataSet ds, string sql, string tabloadi)
    { //Insert, Update Delete için PK kullanýlýr
        OleDbDataAdapter da = new OleDbDataAdapter(sql, cnn);
        OleDbCommandBuilder cmd = new OleDbCommandBuilder(da);//insert update delete otomatik oluþturuluyor
        if (ds.HasChanges())
        {
            da.Update(ds, tabloadi);//DataSetteski bilgiler veritabanýna kalýcý bir þekilde iþleniyor
        }
       
    }

    public OleDbDataReader ExecuteReader(string sql)
    {   //ileri doðru salt okuma yapan bir satýr döndürür.
        OleDbCommand cmd = new OleDbCommand(sql,cnn);
        if (cnn.State == ConnectionState.Closed)
        {
            cnn.Open();
        }
        OleDbDataReader dr=cmd.ExecuteReader(CommandBehavior.CloseConnection);
        return dr; //dr conncection'u kitler. dr'nin iþi bittikten sonra kapatýlmasý gerek
                   //connection kitli iken diðer sorgularý çalýþtýramaz.
    }

    public void ExecuteNonQuery (string sql)
    {  //her türlü sorgu çalýþýr
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
    { //sadece tek deðer döndürür
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
