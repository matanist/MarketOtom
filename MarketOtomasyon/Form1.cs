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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        MarketDBEntities ent = new MarketDBEntities();
        private void btnGiris_Click(object sender, EventArgs e)
        {
            var kullaniciAdi = txbKullaniciAdi.Text.Trim();
            var sifre = txbSifre.Text.Trim();
            Kullanici seciliKullanici= ent.Kullanici.FirstOrDefault(k=> k.KullaniciAdi==kullaniciAdi && k.Sifre==sifre);
            if (seciliKullanici!=null)
            {
                //Kullanıcı bilgileri doğru girdi. Admin ise onu admin formuna gönder.
                if (seciliKullanici.Yetki==0)
                {
                    //Normal kullanıcı
                    UrunForm yeniUrunForm = new UrunForm();
                    yeniUrunForm.Show();
                    this.Hide();
                }
                else
                {
                    //Admin kullanıcı
                    AdminForm yeniAdminForm = new AdminForm();
                    yeniAdminForm.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Yanlış kullanıcı adı veya şifre");
            }

        }
    }
}
