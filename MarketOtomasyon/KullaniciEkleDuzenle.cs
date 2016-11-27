using MarketOtomasyon.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarketOtomasyon
{
    public partial class KullaniciEkleDuzenle : Form
    {
        public KullaniciEkleDuzenle()
        {
            InitializeComponent();
        }

        private void KullaniciEkleDuzenle_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (Form siradakiForm in Application.OpenForms)
            {
                if (siradakiForm.Name=="AdminForm")
                {
                    siradakiForm.Show();
                }
            }
            VeriMerkezi.SeciliKullanici = null;
        }
        MarketDBEntities ent = new MarketDBEntities();
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            //todo:Butonun text'inde Kaydet yazıyorsa yeni kullanıcı oluştursun. Aşağıdaki kodlar çalışsın. Ancak Düzenle yazıyorsa var olan kullanıcının (VeriMerkezindeki) özelliklerini veritabanından değiştirsin.
            Kullanici yeniKullanici = new Kullanici();

            if (ent.Kullanici.FirstOrDefault(k=>k.KullaniciAdi==txbKullaniciAdi.Text)!=null)
            {
                MessageBox.Show("Bu kullanıcı adını kullanamazsınız");
                return;
            }
            if (string.IsNullOrEmpty(txbAd.Text)||string.IsNullOrEmpty(txbSoyad.Text)||string.IsNullOrEmpty(txbKullaniciAdi.Text)||string.IsNullOrEmpty(txbSifre.Text))
            {
                MessageBox.Show("Bu alanlar boş geçilemez");
                return;
            }
            yeniKullanici.Ad = txbAd.Text;
            yeniKullanici.Soyad = txbSoyad.Text;
            yeniKullanici.KullaniciAdi = txbKullaniciAdi.Text;
            yeniKullanici.Sifre = txbSifre.Text;
            yeniKullanici.Yetki = chkAdmin.Checked ?Convert.ToInt16(1) :Convert.ToInt16(0);
            ent.Kullanici.Add(yeniKullanici);
            MessageBox.Show(ent.SaveChanges()>0?"Yeni Kullanıcı Başarı ile eklendi":"Bir sorun oluştu");

            foreach (Form siradakiForm in Application.OpenForms)
            {
                if (siradakiForm.Name=="AdminForm")
                {
                    (siradakiForm as AdminForm).ListBoxDoldur("");
                }
            }

        }

        private void KullaniciEkleDuzenle_Load(object sender, EventArgs e)
        {
            if (VeriMerkezi.SeciliKullanici == null)
            {
                //bu durumda admin yeni kullanıcı eklemek istemiş...
            }
            else
            {
                //bu durumda admin var olan bir kullanıcıyı değiştirmek istemiş.
                var duzenlenecekKullanici = ent.Kullanici.FirstOrDefault(k => k.id == VeriMerkezi.SeciliKullanici.id);
                txbAd.Text = duzenlenecekKullanici.Ad;
                txbSoyad.Text = duzenlenecekKullanici.Soyad;
                txbKullaniciAdi.Text = duzenlenecekKullanici.KullaniciAdi;
                txbSifre.Text = duzenlenecekKullanici.Sifre;
                chkAdmin.Checked = duzenlenecekKullanici.Yetki == 1 ? true : false;
                btnKaydet.Text = "Düzenle"; //butonun yazısına göre işlem yaptıracağımız için burada adını değiştiriyorum. Böylece butonda Düzenle yazıyorsa var olan bir kullanıcının özelliğini değiştirmemiz gerekiyor.
            }
        }
    }
}
