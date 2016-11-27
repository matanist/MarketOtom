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
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private void AdminForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (Form siradakiForm in Application.OpenForms)
            {
                if (siradakiForm.Name == "Form1")
                {
                    siradakiForm.Show();
                }
               
            }
        }
        MarketDBEntities ent = new MarketDBEntities();
        private void AdminForm_Load(object sender, EventArgs e)
        {
            ListBoxDoldur("");

        }
        //public void ListBoxDoldur()
        //{
        //    var kullaniciListesi = ent.Kullanici.ToList();
        //    lstKullanicilar.DataSource = kullaniciListesi
        //        .Select(k =>
        //            new KullaniciView
        //            {
        //                id = k.id,
        //                KAdAdSoyadYetki = k.KullaniciAdi + " " + k.Ad + " " + k.Soyad + " " + (k.Yetki == 1 ? "Admin" : "Normal")
        //            }).ToList();
        //    lstKullanicilar.DisplayMember = "KAdAdSoyadYetki";
        //    lstKullanicilar.ValueMember = "id";
        //}
        public void ListBoxDoldur(string aranan)
        {
            var kullaniciListesi = ent.Kullanici.Where(k=>k.KullaniciAdi.Contains(aranan)||k.Ad.Contains(aranan)||k.Soyad.Contains(aranan)).ToList();
            lstKullanicilar.DataSource = kullaniciListesi
                .Select(k =>
                    new KullaniciView
                    {
                        id = k.id,
                        KAdAdSoyadYetki = k.KullaniciAdi + " " + k.Ad + " " + k.Soyad + " " + (k.Yetki == 1 ? "Admin" : "Normal")
                    }).ToList();
            lstKullanicilar.DisplayMember = "KAdAdSoyadYetki";
            lstKullanicilar.ValueMember = "id";
        }

        private void txbAra_TextChanged(object sender, EventArgs e)
        {
            var aranacakIfade = txbAra.Text;
            ListBoxDoldur(aranacakIfade);
            //
        }

        private void ekleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KullaniciEkleDuzenle yeniForm = new KullaniciEkleDuzenle();
            yeniForm.ShowDialog();//Açılan yeni pencere bu formun üzerine gelir ve yeni pencere kapanmadan bu pencereye müdahale edilemez.
           
        }

        private void AdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Form siradakiForm in Application.OpenForms)
            {
                
                if (siradakiForm.Name == "KullaniciEkleDuzenle")
                {
                    e.Cancel = true;
                }
            }
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstKullanicilar.SelectedIndex == -1) return;
            var seciliKullaniciID = Convert.ToInt32(lstKullanicilar.SelectedValue);
            var secim=MessageBox.Show("Emin misin","Uyarı!",MessageBoxButtons.OKCancel);
            if (secim==DialogResult.Cancel)
            {
                MessageBox.Show("Kullanıcı tarafından iptal edildi");
            }
            else
            {

                ent.Kullanici.Remove(ent.Kullanici.FirstOrDefault(k => k.id == seciliKullaniciID));
                MessageBox.Show(ent.SaveChanges()>0?"Kullanıcı silme işlemi başarılı":"Silinemedi");
                ListBoxDoldur("");



            }
        }

        private void duzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstKullanicilar.SelectedIndex == -1) return;
            var duzenlenecekKullanici = (KullaniciView)(lstKullanicilar.SelectedItem);
            VeriMerkezi.SeciliKullanici = duzenlenecekKullanici;
            KullaniciEkleDuzenle yeniForm = new KullaniciEkleDuzenle();
            yeniForm.Show();
            this.Hide();
            //MessageBox.Show(duzenlenecekKullanici.KAdAdSoyadYetki);
        }
    }
}
