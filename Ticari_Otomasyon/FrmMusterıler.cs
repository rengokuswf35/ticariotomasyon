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
    public partial class FrmMusterıler : Form
    {
        public FrmMusterıler()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBL_MUSTERILER", bgl.baglanti());
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
        private void FrmMusterıler_Load(object sender, EventArgs e)
        {
            listele();

            sehirlistesi();
        }

        private void Cmbil_SelectedIndexChanged(object sender, EventArgs e)
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

        // Dışarıdan Girilen Verileri Veri Tabanına Kaydetme
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_MUSTERILER (AD,SOYAD,TELEFON,TELEFON2,TC,MAIL,IL,ILCE,ADRES,VERGIDAIRE) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTelefon1.Text);
            komut.Parameters.AddWithValue("@p4", MskTelefon2.Text);
            komut.Parameters.AddWithValue("@p5", MskTC.Text);
            komut.Parameters.AddWithValue("@p6", txtMail.Text);
            komut.Parameters.AddWithValue("@p7", Cmbil.Text);
            komut.Parameters.AddWithValue("@p8", Cmbilce.Text);
            komut.Parameters.AddWithValue("@p9", txtVergi.Text);
            komut.Parameters.AddWithValue("@p10", RchAdres.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri Sisteme Eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();


        }

        // DataGridView ile veri tabanındaki maddelere tıklayarak verilerin kayıt bölümünde gözükmesini sağlar.

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var dgv = sender as DataGridView;
            if (dgv != null)
            {
                var row = dgv.CurrentRow;
                var id = row?.Cells["ID"].Value?.ToString();
                var ad = row?.Cells["AD"].Value?.ToString();
                var soyad = row?.Cells["SOYAD"].Value?.ToString();
                var telefon1 = row?.Cells["TELEFON"].Value?.ToString();
                var telefon2 = row?.Cells["TELEFON2"].Value?.ToString();
                var tc = row?.Cells["TC"].Value?.ToString();
                var maıl = row?.Cells["MAIL"].Value?.ToString();
                var ıl = row?.Cells["IL"].Value?.ToString();
                var vergıdaıre = row?.Cells["VERGIDAIRE"].Value?.ToString();
                var adres = row?.Cells["ADRES"].Value?.ToString();

                Txtid.Text = id;
                txtAd.Text = ad;
                txtSoyad.Text = soyad;
                MskTelefon1.Text = telefon1;
                MskTelefon2.Text = telefon2;
                MskTC.Text = tc;
                txtMail.Text = maıl;
                txtVergi.Text = vergıdaıre;
                RchAdres.Text = adres;
            }
        }

        // Kayıt kısmındaki verilerin temizlenmesini sağlar.

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            txtAd.Text = String.Empty;
            txtSoyad.Text = String.Empty;
            MskTelefon1.Text = String.Empty;
            MskTelefon2.Text = String.Empty;
            MskTC.Text = String.Empty;
            txtMail.Text = String.Empty;
            txtVergi.Text = String.Empty;
            RchAdres.Text = String.Empty;
        }

        // Veri tabanında istediğimiz verileri silebilmemizi sağlar.

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete from TBL_MUSTERILER where ID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

        }

        // Veri tabanındaki daha önceden girdiğimiz verileri güncelleyebilmemizi sağlar.
        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_MUSTERILER set AD=@P1,SOYAD=@P2,TELEFON=@P3,TELEFON2=@P4,TC=@P5,MAIL=@P6,IL=@P7,ILCE=@P8,VERGIDAIRE=@P9,ADRES=@P10 where ID=@P11", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTelefon1.Text);
            komut.Parameters.AddWithValue("@p4", MskTelefon2.Text);
            komut.Parameters.AddWithValue("@p5", MskTC.Text);
            komut.Parameters.AddWithValue("@p6", txtMail.Text);
            komut.Parameters.AddWithValue("@p7", Cmbil.Text);
            komut.Parameters.AddWithValue("@p8", Cmbilce.Text);
            komut.Parameters.AddWithValue("@p9", txtVergi.Text);
            komut.Parameters.AddWithValue("@p10", RchAdres.Text);
            komut.Parameters.AddWithValue("@p11", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }
    }
}
