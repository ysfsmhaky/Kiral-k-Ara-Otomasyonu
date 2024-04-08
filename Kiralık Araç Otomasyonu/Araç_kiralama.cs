using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace WindowsFormsApp1
{
    class Araç_kiralama
    {
        SqlConnection baglanti = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionString")); // Yapılandırma dosyasından bağlantı bilgisi getiriliyor.
        DataTable tablo;


        public List<string> getPlaka(SqlCommand komut, string sorgu)
        {
            List<string> plakalar = new List<string>();
            try // olası bir hataya karşılık try catch bloğu arasına alıyoruz.
            {
                if (this.baglanti.State == ConnectionState.Closed) // Bağlantı açık ise tekrar açmaya çalışmaması için gerekli kontrol sağlanıyor.
                {
                    baglanti.Open();
                }
                komut.Connection = baglanti;
                komut.CommandText = sorgu;
                SqlDataReader rd = komut.ExecuteReader(); // datareader ile eşitliyoruz
                while(rd.Read()) // readerin okuması bitene kadar while döngüsü çalışacak
                {
                    plakalar.Add(rd["plaka"].ToString()); // oluşturduğumuz plakalar listesine plakaları ekliyoruz ve return olarak listeyi veriyoruz.
                }
                baglanti.Close();
            }
            catch
            { }
            return plakalar;
        }
        public void ekle_kaldır_güncelle(SqlCommand komut, string sorgu) // Tek bir sınıf içinde ekleme,silme ve güncelleme işlemlerini yaptığımız kısım.
        {
            try // olası bir hataya karşılık try catch bloğu arasına alıyoruz.
            {
                if (this.baglanti.State == ConnectionState.Closed) // Bağlantı açık ise tekrar açmaya çalışmaması için gerekli kontrol sağlanıyor.
                {
                    baglanti.Open();
                }
                komut.Connection = baglanti;
                komut.CommandText = sorgu;
                komut.ExecuteNonQuery(); // gelen komutu execute edip bağlantıyı kapatıyoruz.
                baglanti.Close();
            }
            catch
            { }

        }
        public DataTable listele(SqlDataAdapter adtr, string sorgu) // Datagridview için oluşturduğumuz Datatable kısmı. geri dönüş olarak bir Datatable veriyor.
        {
            if (this.baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            tablo = new DataTable();
            adtr = new SqlDataAdapter(sorgu, baglanti);
            adtr.Fill(tablo);
            baglanti.Close();
            return tablo;


        }
    }
}
