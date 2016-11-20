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
        }
        MarketDBEntities ent = new MarketDBEntities();
        private void btnKaydet_Click(object sender, EventArgs e)
        {
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
    }
}
