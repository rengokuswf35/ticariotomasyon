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
    public partial class FrmUrunler : Form
    {
        public FrmUrunler()
        {
            InitializeComponent();
        }

        private void labelControl10_Click(object sender, EventArgs e)
        {

        }

        sqlbaglantisi bgl = new sqlbaglantisi();


        void listele()
        {
            DataTable dt = new DataTable(); // Datatable sınıfında "dt" isimli yeni bir nesne ürettim.
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBL_URUNLER", bgl.baglanti()); // Bağlantı adresimle ilişkilendirdim.
            da.Fill(dt); // DataAdaptor'un içini DataTable ile doldurdum.
            dataGridView1.DataSource = dt;
        }

        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            // Verileri Kaydetme

            SqlCommand komut = new SqlCommand("insert into TBL_URUNLER(URUNAD,MARKA,MODEL,YIL,ADET,ALISFIYAT,SATISFIYAT,DETAY) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());

            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtMarka.Text);
            komut.Parameters.AddWithValue("@p3", TxtModel.Text);
            komut.Parameters.AddWithValue("@p4", MskYil.Text);
            komut.Parameters.AddWithValue("@p5", int.Parse((NudAdet.Value).ToString())); // NemericUpDown Veritabanındaki formatı ne ise ona göre dönüşüm uygulanır.
            komut.Parameters.AddWithValue("@p6", decimal.Parse(TxtAlis.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(TxtSatis.Text));
            komut.Parameters.AddWithValue("@p8", RchDetay.Text);
            komut.ExecuteNonQuery(); // DML komutlarını gerçekleştirip sorguyu çalıştırıyor.
            bgl.baglanti().Close(); // Bağlantıyı kapat.
            MessageBox.Show("Ürün sisteme eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            txtAd.Text = String.Empty;
            txtMarka.Text = String.Empty;
            TxtModel.Text = String.Empty;
            MskYil.Text = String.Empty;
            NudAdet.Text = String.Empty;
            TxtAlis.Text = String.Empty;
            TxtSatis.Text = String.Empty;
            RchDetay.Text = String.Empty;

        }

        // Verileri Silme

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutsil = new SqlCommand("Delete From TBL_URUNLER where ID=@p1", bgl.baglanti());
            komutsil.Parameters.AddWithValue("@p1", Txtid.Text);
            komutsil.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            listele();
            
        }


        // Veri tabanındaki daha önceden girdiğimiz verileri güncelleyebilmemizi sağlar.

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_URUNLER set URUNAD=@P1,MARKA=@P2,MODEL=@P3,YIL=@P4,ADET=@P5,ALISFIYAT=@P6,SATISFIYAT=@P7,DETAY=@P8 where ID=@P9", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", txtAd.Text);
            komut.Parameters.AddWithValue("@P2", txtMarka.Text);
            komut.Parameters.AddWithValue("@P3", TxtModel.Text);
            komut.Parameters.AddWithValue("@P4", MskYil.Text);
            komut.Parameters.AddWithValue("@P5", int.Parse((NudAdet.Value).ToString())); // NemericUpDown Veritabanındaki formatı ne ise ona göre dönüşüm uygulanır.
            komut.Parameters.AddWithValue("@P6", decimal.Parse(TxtAlis.Text));
            komut.Parameters.AddWithValue("@P7", decimal.Parse(TxtSatis.Text));
            komut.Parameters.AddWithValue("@P8", RchDetay.Text);
            komut.Parameters.AddWithValue("@P9", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        // DataGridView ile veri tabanındaki maddelere tıklayarak verilerin kayıt bölümünde gözükmesini sağlar.

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {

            var dgv = sender as DataGridView;
            if (dgv != null)
            {
                var row = dgv.CurrentRow;
                var id = row?.Cells["ID"].Value?.ToString();
                var urunad = row?.Cells["URUNAD"].Value?.ToString();
                var marka = row?.Cells["MARKA"].Value?.ToString();
                var model = row?.Cells["MODEL"].Value?.ToString();
                var yil = row?.Cells["YIL"].Value?.ToString();
                var adet = row?.Cells["ADET"].Value?.ToString();
                var alis = row?.Cells["ALISFIYAT"].Value?.ToString();
                var satis= row?.Cells["SATISFIYAT"].Value?.ToString();
                var detay= row?.Cells["DETAY"].Value?.ToString();



                Txtid.Text = id;
                txtAd.Text = urunad;
                txtMarka.Text = marka;
                TxtModel.Text =model ;
                MskYil.Text =yil ;
                NudAdet.Text = adet;
                TxtAlis.Text =alis ;
                TxtSatis.Text =satis ;
                RchDetay.Text =detay ;

            }

           
        }

        // Kayıt kısmındaki verilerin temizlenmesini sağlar.
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            txtAd.Text = String.Empty;
            txtMarka.Text = String.Empty;
            TxtModel.Text = String.Empty;
            MskYil.Text = String.Empty;
            NudAdet.Text = String.Empty;
            TxtAlis.Text = String.Empty;
            TxtSatis.Text = String.Empty;
            RchDetay.Text = String.Empty;

        }
    }
}
