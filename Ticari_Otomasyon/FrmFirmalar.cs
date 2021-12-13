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
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace Ticari_Otomasyon
{
    public partial class FrmFirmalar : Form
    {
        public FrmFirmalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void firmalistesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBL_FIRMALAR", bgl.baglanti()); // Select sorgusu ile verileri DataSet ya da DataTable'a Doldurmaktır.SqlAdapter Nesnesini Kullanmak için "Select" gerekir.
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }
        void sehirlistesi()
        {
            SqlCommand komut = new SqlCommand("Select SEHIR From TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader(); // Datadaki verileri oku ve çalıştır.
            while (dr.Read())
            {
                Cmbil.Properties.Items.Add(dr[0]); // 0. Index'ten itibaren illeri ekle.
            }
            bgl.baglanti().Close();
        }

        void carikodaciklamalar()
        {
            SqlCommand komut = new SqlCommand("Select FIRMAKOD1 from TBL_KODLAR", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader(); // Bir Veri kaynağından gelen verileri sırayla okumak için kullanılan geniş bir nesne kategorisidir.
            while (dr.Read())
            {
                RchKod1.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
        }


        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            firmalistesi();

            sehirlistesi();

            carikodaciklamalar();



        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var dgv = sender as DataGridView;
            if (dgv != null)
            {
                var row = dgv.CurrentRow;
                var id = row?.Cells["ID"].Value?.ToString();
                var ad = row?.Cells["AD"].Value?.ToString();
                var statu = row?.Cells["YETKILISTATU"].Value?.ToString();
                var adsoyad = row?.Cells["YETKILIADSOYAD"].Value?.ToString();
                var tc = row?.Cells["YETKILITC"].Value?.ToString();
                var sektor = row?.Cells["SEKTOR"].Value?.ToString();
                var telefon1 = row?.Cells["TELEFON1"].Value?.ToString();
                var telefon2 = row?.Cells["TELEFON2"].Value?.ToString();
                var telefon3 = row?.Cells["TELEFON3"].Value?.ToString();
                var maıl = row?.Cells["MAIL"].Value?.ToString();
                var fax = row?.Cells["FAX"].Value?.ToString();
                var ıl = row?.Cells["IL"].Value?.ToString();
                var ılce = row?.Cells["ILCE"].Value?.ToString();
                var vergıdaıre = row?.Cells["VERGIDAIRE"].Value?.ToString();
                var adres = row?.Cells["ADRES"].Value?.ToString();
                var ozelkod1 = row?.Cells["OZELKOD1"].Value?.ToString();
                var ozelkod2 = row?.Cells["OZELKOD2"].Value?.ToString();
                var ozelkod3 = row?.Cells["OZELKOD3"].Value?.ToString();

                Txtid.Text = id;
                txtAd.Text = ad;
                TxtYetkiliGorev.Text = statu;
                txtYetkili.Text = adsoyad;
                MskTC.Text = tc;
                txtSektor.Text = sektor;
                MskTelefon1.Text = telefon1;
                MskTelefon2.Text = telefon2;
                MskTelefon3.Text = telefon3;
                txtMail.Text = maıl;
                MskFax.Text = fax;
                Cmbil.Text = ıl;
                Cmbilce.Text = ılce;
                txtVergi.Text = vergıdaıre;
                RchAdres.Text = adres;
                TxtKod1.Text = ozelkod1;
                TxtKod2.Text = ozelkod2;
                TxtKod3.Text = ozelkod3;


            }
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_FIRMALAR (AD,YETKILISTATU,YETKILIADSOYAD,YETKILITC,SEKTOR,TELEFON1,TELEFON2,TELEFON3,MAIL,FAX,IL,ILCE,VERGIDAIRE,ADRES,OZELKOD1,OZELKOD2,OZELKOD3) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17)",  bgl.baglanti());

         

            komut.Parameters.AddWithValue("@P1", txtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtYetkiliGorev.Text);
            komut.Parameters.AddWithValue("@P3", txtYetkili.Text);
            komut.Parameters.AddWithValue("@P4", MskTC.Text);
            komut.Parameters.AddWithValue("@P5", txtSektor.Text);
            komut.Parameters.AddWithValue("@P6", MskTelefon1.Text);
            komut.Parameters.AddWithValue("@P7", MskTelefon2.Text);
            komut.Parameters.AddWithValue("@P8", MskTelefon3.Text);
            komut.Parameters.AddWithValue("@P9", txtMail.Text);
            komut.Parameters.AddWithValue("@P10", MskFax.Text);
            komut.Parameters.AddWithValue("@P11", Cmbil.Text);
            komut.Parameters.AddWithValue("@P12", Cmbilce.Text);
            komut.Parameters.AddWithValue("@P13", txtVergi.Text);
            komut.Parameters.AddWithValue("@P14", RchAdres.Text);
            komut.Parameters.AddWithValue("@P15", TxtKod1.Text);
            komut.Parameters.AddWithValue("@P16", TxtKod2.Text);
            komut.Parameters.AddWithValue("@P17", TxtKod3.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Sisteme Kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            firmalistesi();
            


        }

        private void groupControl5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            txtAd.Text = String.Empty;
            TxtYetkiliGorev.Text = String.Empty;
            txtYetkili.Text = String.Empty;
            MskTC.Text = String.Empty;
            txtSektor.Text = String.Empty;
            MskTelefon1.Text = String.Empty;
            MskTelefon2.Text = String.Empty;
            MskTelefon3.Text = String.Empty;
            txtMail.Text = String.Empty;
            MskFax.Text = String.Empty;
            Cmbil.Text = String.Empty;
            Cmbilce.Text = String.Empty;
            txtVergi.Text = String.Empty;
            RchAdres.Text = String.Empty;
            TxtKod1.Text = String.Empty;
            TxtKod2.Text = String.Empty;
            TxtKod3.Text = String.Empty;


        }

        private void Cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            {
                Cmbilce.Properties.Items.Clear(); // İlçelerde daha önce seçilmiş olan ilçeleri temizle.
                SqlCommand komut = new SqlCommand("Select ILCE from TBL_ILCELER where SEHIR=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", Cmbil.SelectedIndex + 1);
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    Cmbilce.Properties.Items.Add(dr[0]);
                }
                bgl.baglanti().Close();
            }
        }

        private void groupControl7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete From TBL_FIRMALAR where ID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            firmalistesi();
            MessageBox.Show("Firma listeden silindi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Hand);
            
            
            
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update TBL_FIRMALAR set AD=@P1,YETKILISTATU=@P2,YETKILIADSOYAD=@P3,YETKILITC=@P4,SEKTOR=@P5,TELEFON1=@P6,TELEFON2=@P7,TELEFON3=@P8,MAIL=@P9,IL=@P11,ILCE=@P12,FAX=@P10,VERGIDAIRE=@P13,ADRES=@P14," +
                "OZELKOD1=@P15,OZELKOD2=@P16,OZELKOD3=@P17 WHERE ID=@P18", bgl.baglanti());


            komut.Parameters.AddWithValue("@P1", txtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtYetkiliGorev.Text);
            komut.Parameters.AddWithValue("@P3", txtYetkili.Text);
            komut.Parameters.AddWithValue("@P4", MskTC.Text);
            komut.Parameters.AddWithValue("@P5", txtSektor.Text);
            komut.Parameters.AddWithValue("@P6", MskTelefon1.Text);
            komut.Parameters.AddWithValue("@P7", MskTelefon2.Text);
            komut.Parameters.AddWithValue("@P8", MskTelefon3.Text);
            komut.Parameters.AddWithValue("@P9", txtMail.Text);
            komut.Parameters.AddWithValue("@P10", MskFax.Text);
            komut.Parameters.AddWithValue("@P11", Cmbil.Text);
            komut.Parameters.AddWithValue("@P12", Cmbilce.Text);
            komut.Parameters.AddWithValue("@P13", txtVergi.Text);
            komut.Parameters.AddWithValue("@P14", RchAdres.Text);
            komut.Parameters.AddWithValue("@P15", TxtKod1.Text);
            komut.Parameters.AddWithValue("@P16", TxtKod2.Text);
            komut.Parameters.AddWithValue("@P17", TxtKod3.Text);
            komut.Parameters.AddWithValue("@P18", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Bilgileri Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            firmalistesi();
        }
    }
}
