using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class frmAnasayfa : Form
    {
        Araç_kiralama arac_kiralama = new Araç_kiralama();
        public frmAnasayfa()
        {
            InitializeComponent();
        }
        private void frmAnasayfa_Load(object sender, EventArgs e) // uygulamamız açıldığı zaman gerçekleşecek kısım
        {
            this.Size = new Size(600, 450);
            this.panel1.Location = this.panel2.Location = this.panel3.Location = this.panel4.Location = this.panel5.Location = new Point(215, 100); // tüm panellerimizin location değerini sabitliyoruz 
            this.label1.Location = new Point((this.Size.Width - this.label1.Size.Width) / 2, 10); // Başlığımızı ise ortalıyoruz.
        }

        private void frmAnasayfa_Resize(object sender, EventArgs e) // formun boyutu değiştiği zaman gerçekleşecek kısım
        {
            this.label1.Location = new Point((this.Size.Width - this.label1.Size.Width) / 2, 10); // başlığımızı formun boyutuna göre tekrar ortalıyoruz.
            this.CenterToScreen(); // centerToScreen metodu ile formumuzu ekrana göre ortalıyoruz.
        }
        private void switchPanels(int switchCode) // Paneller arası geçiş yaptığımız kısım.Gelen int değerindeki veriyi switch ile değiştiriyoruz.
        {
            this.label38.Visible = false; // İşlem yapmak için sol taraftaki kısımdan seçim yapmamızı gösteren labeli gizliyoruz.
            switch (switchCode)
            {
                case 1:
                    this.Size = new Size(535, 445); // Panel değişimine göre ana formumuzun boyutunu ayarlıyoruz.
                    this.panel1.Visible = true; // aktif olan panelin visible ve enabled seçeneğini true değerine getiriyoruz.
                    this.panel1.Enabled = true;
                    this.panel2.Visible = false; // diğer değerleri de false yapıp sadece 1.panelin gözükmesini sağlıyoruz.
                    this.panel2.Enabled = false;
                    this.panel3.Visible = false;
                    this.panel3.Enabled = false;
                    this.panel4.Visible = false;
                    this.panel4.Enabled = false;
                    this.panel5.Visible = false;
                    this.panel5.Enabled = false;
                    break;
                case 2:
                    this.Size = new Size(1190, 645);
                    this.panel1.Visible = false;
                    this.panel1.Enabled = false;
                    this.panel2.Visible = true;
                    this.panel2.Enabled = true;
                    this.panel3.Visible = false;
                    this.panel3.Enabled = false;
                    this.panel4.Visible = false;
                    this.panel4.Enabled = false;
                    this.panel5.Visible = false;
                    this.panel5.Enabled = false;
                    break;
                case 3:
                    this.Size = new Size(550, 635);
                    this.panel1.Visible = false;
                    this.panel1.Enabled = false;
                    this.panel2.Visible = false;
                    this.panel2.Enabled = false;
                    this.panel3.Visible = true;
                    this.panel3.Enabled = true;
                    this.panel4.Visible = false;
                    this.panel4.Enabled = false;
                    this.panel5.Visible = false;
                    this.panel5.Enabled = false;
                    break;
                case 4:
                    this.Size = new Size(1190, 645);
                    this.panel1.Visible = false;
                    this.panel1.Enabled = false;
                    this.panel2.Visible = false;
                    this.panel2.Enabled = false;
                    this.panel3.Visible = false;
                    this.panel3.Enabled = false;
                    this.panel4.Visible = true;
                    this.panel4.Enabled = true;
                    this.panel5.Visible = false;
                    this.panel5.Enabled = false;
                    break;
                case 5:
                    this.Size = new Size(1272, 680);
                    this.panel1.Visible = false;
                    this.panel1.Enabled = false;
                    this.panel2.Visible = false;
                    this.panel2.Enabled = false;
                    this.panel3.Visible = false;
                    this.panel3.Enabled = false;
                    this.panel4.Visible = false;
                    this.panel4.Enabled = false;
                    clearSozlesme();
                    refreshPlaka();
                    refreshSozlesmeList();
                    this.panel5.Visible = true;
                    this.panel5.Enabled = true;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switchPanels(1); // 1.Panele geçiş
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            switchPanels(2); // 2.Panele geçiş
            refreshMusteriList(); // Müşteri listesine ait datagridview'deki verileri yeniliyoruz.
            clearMusteriList(); // 2.Panele ait textbox üzerindeki yazıları sıfırlıyoruz.
            comboBox1.SelectedIndex = 0; // 2.Panele ait combobox değerinin indexini 0'a çekiyoruz, panel her açıldığında ilk seçenek seçili şekilde geliyor.
        }

        private void button3_Click(object sender, EventArgs e)
        {
            switchPanels(3); // 3.Panele geçiş
            clearCarRegList(); // 3.Panele ait textbox üzerindeki yazıları sıfırlıyoruz.

        }

        private void button5_Click(object sender, EventArgs e)
        {
            switchPanels(5); // 5.Panele geçiş
        }
        private void frmAnasayfa_FormClosing(object sender, FormClosingEventArgs e) // Uygulama kapatılırken uyarı mesajı gönderir.Hayır cevaı verilirse uygulama kapatılmaz.
        {
            if (MessageBox.Show("Uygulamayı kapatmak istediğine emin misin?", "Sistem", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes) // Hayır seçeneği seçilirse return çeviriyoruz, böylelikle form kapanmıyor.
            {
                e.Cancel = true;
                return;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            switchPanels(4); // 4.Panele geçiş.
            refreshCarList(); // Araç listesine ait datagridview'deki verileri yeniliyoruz.
            clearCarList(); // 4.Paneldeki textbox değerlerini sıfırlıyoruz.
            comboBox2.SelectedIndex = 0; // 4.Panele ait combobox değerinin indexini 0'a çekiyoruz, panel her açıldığında ilk seçenek seçili şekilde geliyor.
        }

        // Müşteri Ekleme Paneli
        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "0 Olmadan giriniz.") // 1.Paneldeki telefon numarası girdiğimiz textbox kutusunun kutuya tıkladığımız zaman silmemizi sağlıyor.
            {                                          // Eğer textbox'daki yazı '0 Olmadan giriniz.'e eşit ise değeri sıfırlıyoruz.
                textBox3.Text = "";
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text)) // 1.Paneldeki telefon numarası girdiğimiz kısım.Textbox'dan çıkış yaptığımızda tekrar '0 Olmadan giriniz.' yazısını gösteriyor.Tabi bir numara girilmemişse bu işlem gerçekleşiyor.
            {
                textBox3.Text = "0 Olmadan giriniz.";
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); // Sadece rakam girilmesini istediğimiz textboxlara kontrol sağlıyoruz.
        }                                                                       // Eğer girilen değer bir rakam değilse işlem yapılmamasını sağlıyor.

        private void button8_Click(object sender, EventArgs e)
        {
            clearMusteriEkle(); // Müşteri eklediğimiz kısımdaki textbox değerlerini sıfırlıyor.
            MessageBox.Show("Değerler temizlendi.");
        }

        private void clearMusteriEkle()
        {
            foreach (Control item in this.panel1.Controls) // Sadece 1.panel olduğu için foreach döngüsünü 1.panelin üzerinden yapıyoruz.
            {
                if (item is TextBox && item.Text != "0 Olmadan giriniz.") // eğer Control itemi bir textbox ise textbox'daki yazı ve '0 Olmadan giriniz.' değilse değeir sıfırlıyoruz.
                {
                    item.Text = "";
                }
            }
        }
        private void button7_Click(object sender, EventArgs e) // Müşteri eklediğimiz buton
        {
            int errorCode = 0; // Yeni bir int değerinde değişken oluşturuyoruz, böylelikle hatamızın neyden kaynaklı olduğunu kullanıcıya daha rahat gösterebiliriz.
            foreach (Control item in this.panel1.Controls) // Tekrar 1.panel üzerinde foreach döngüsü oluşturuyoruz.
            {
                if (item is TextBox && string.IsNullOrEmpty(item.Text)) // eğer control itemi bir textbox ise ve boş bir değer ise errorCode değerini 1 yapıyoruz.
                {
                    errorCode = 1;
                }
            }
            if (textBox1.Text.Length < 11) // textbox1 değerinin uzunluğu 11'den küçük ise errorCode değerini 2 yapıyoruz. TC Kimlik numarası 11 rakamdan oluşuyor.
            {
                errorCode = 2;
            }
            if (textBox3.Text.Length < 10) // telefon numarası girdiğimiz kısma 0 olmadan giriniz demiştik.0 Olmadan 10 rakamdan oluşan bir değer girilmesi gerekiyor
            {
                errorCode = 3; // eğer girilen değer 10'dan küçükse errorCode değerini 3 yapıyoruz
            }
            switch (errorCode) // Switch ile errorCode değişkenindeki değeri kontrol ediyoruz.
            {
                case 1: // eğer errorCode değeri 1 ise 
                    MessageBox.Show("Boş değer girilemez.");
                    break;
                case 2: // eğer errorCode değeri 2 ise
                    MessageBox.Show("TC Kimlik numarası 11 rakamdan oluşmalıdır.");
                    break;
                case 3: // eğer errorCode değeri 3 ise
                    MessageBox.Show("Telefon numarası 10 rakamdan oluşmalıdır.");
                    break;
            }
            if (errorCode > 0) return; // burda ise errorCode değeri 0'dan büyükse return çektiriyoruz çünkü yukarda errorCode değerimize 1,2 ve 3 değerlerini atadığında bir hata meydana gelmiş oluyor.

            string cümle = "INSERT INTO müşteri(adsoyad,tc,adres,telefon,email) VALUES(@adsoyad,@tc,@adres,@telefon,@email)"; // Insert Into komutu ile bir komut oluşturuyoruz
            SqlCommand komut2 = new SqlCommand();
            komut2.Parameters.AddWithValue("@tc", textBox1.Text); // textbox değerlerimizi parametrelerimize eşitliyoruz.
            komut2.Parameters.AddWithValue("@adsoyad", textBox2.Text);
            komut2.Parameters.AddWithValue("@telefon", textBox3.Text);
            komut2.Parameters.AddWithValue("@adres", textBox4.Text);
            komut2.Parameters.AddWithValue("@email", textBox5.Text);
            arac_kiralama.ekle_kaldır_güncelle(komut2, cümle); // önceden oluşturduğumuz sınıfımızdaki metodu çağırıp komutu gönderiyoruz.
            clearMusteriEkle(); // komut gönderildikten sonra textbox değerlerimizi tekrar temizliyoruz.
            MessageBox.Show("Yeni müşteri başarıyla eklendi !");
        }

        private void pictureBox1_Click(object sender, EventArgs e) // 1.Paneli kapatma butonu
        {
            clearMusteriEkle(); // 1.Paneli gizlediğimizde paneldeki textbox değerlerini temizlememiz gerekiyor.
            this.panel1.Visible = false; // 1.Panelin visible değerini false yapıp görünmemesini sağlıyoruz.
            this.panel1.Enabled = false; // 1.Panelin enabled değerini false yapıp deaktif olmasını sağlıyoruz.
            this.Size = new Size(600, 450); // formumuzun boyutunu tekrar default haline getiriyoruz.
            this.label38.Visible = true; // soldan işlem yapmamızı seçmemizi isteyen labeli tekrar görünür hale getiriyoruz.
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            // 1.Paneli gizlemek için oluşturduğumuz butonun üzerine geldiğimizde resminin değişmesini sağlıyoruz.
            var image = (System.Drawing.Bitmap)new ResourceManager(typeof(Otomasyon.Properties.Resources)).GetObject("navi_close_enter");
            pictureBox1.Image = image;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            // üzerinden çıktığımızda ise normal haline gelmesini sağlıyoruz.
            var image = (System.Drawing.Bitmap)new ResourceManager(typeof(Otomasyon.Properties.Resources)).GetObject("navi_close_normal");
            pictureBox1.Image = image;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            // tıkladığımızda ise tekrar değişmesini sağlıyoruz.
            var image = (System.Drawing.Bitmap)new ResourceManager(typeof(Otomasyon.Properties.Resources)).GetObject("navi_close_press");
            pictureBox1.Image = image;
        }
        // Müşteri Ekleme Paneli



        // Müşteri Listeleme Paneli


        private void clearMusteriList()
        {
            foreach (Control item in this.panel2.Controls) // 2.Paneldeki textbox değerlerini sıfırlıyoruz, o yüzden foreach döngüsü panel2'nin controls kısmında oluyor.
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }
        private void refreshMusteriList() // Datagridview verilerini yeniliyoruz.
        {
            string cümle = "SELECT * FROM müşteri";
            SqlDataAdapter adtr2 = new SqlDataAdapter();

            dataGridView1.DataSource = arac_kiralama.listele(adtr2, cümle); // önceden oluşturduğumuz sınıftaki listele metodunu çağırıyoruz.
            dataGridView1.Columns[0].HeaderText = "AD SOYAD"; // return olarak bir datatable çevirdiği için datagridview'in datasource'sini listele metoduna eşitliyoruz.
            dataGridView1.Columns[1].HeaderText = "TC";
            dataGridView1.Columns[2].HeaderText = "ADRES";
            dataGridView1.Columns[3].HeaderText = "TELEFON";
            dataGridView1.Columns[4].HeaderText = "E-MAİL";
        }
        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            // 2.Paneli gizlemek için oluşturduğumuz butonun üzerine geldiğimizde resminin değişmesini sağlıyoruz.
            var image = (System.Drawing.Bitmap)new ResourceManager(typeof(Otomasyon.Properties.Resources)).GetObject("navi_close_enter");
            pictureBox2.Image = image;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            // üzerinden çıktığımızda ise normal haline gelmesini sağlıyoruz.
            var image = (System.Drawing.Bitmap)new ResourceManager(typeof(Otomasyon.Properties.Resources)).GetObject("navi_close_normal");
            pictureBox2.Image = image;
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            // tıkladığımızda ise tekrar değişmesini sağlıyoruz.
            var image = (System.Drawing.Bitmap)new ResourceManager(typeof(Otomasyon.Properties.Resources)).GetObject("navi_close_press");
            pictureBox2.Image = image;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.panel2.Visible = false; // 2.Panelin visible ve enabled değerini false yapıyoruz ve gizliyoruz.
            this.panel2.Enabled = false;
            this.clearMusteriList(); // 2.Paneldeki textbox değerlerini temizliyoruz.
            this.Size = new Size(600, 450); // formumuzun boyutunu default hale getiriyoruz.
            this.label38.Visible = true; // işlem yapmamızı söyleyen labeli de görünür hale getiriyoruz.
        }

        private void textBox11_TextChanged(object sender, EventArgs e) // datagridview'de arama yaptığımız kısım
        {
            string searchValue = ""; // yeni bir string değeri oluşturuyoruz.
            switch (comboBox1.SelectedIndex) // combobox'daki indeks değişimini switch ile kontrol ediyoruz.
            {
                case 0:
                    searchValue = "tc"; // eğer 0.indeks yani ilk seçenek seçiliyse tc'ye göre aramasını sağlıyoruz.
                    break;
                case 1:
                    searchValue = "adsoyad"; // eğer 1.indeks(2.seçenek) seçiliyse isim - soyisim'e göre aramasını sağlıyoruz.
                    break;
                case 2:
                    searchValue = "email"; // eğer 2.indeks(3.seçenek) seçiliyse email'e göre aramasını sağlıyoruz.
                    break;
                case 3:
                    searchValue = "telefon"; // eğer 3.indeks(4.seçenek) seçiliyse telefon numarasına göre aramasını sağlıyoruz.
                    break;
            }
            if (!string.IsNullOrEmpty(textBox11.Text)) // eğer arama kutusundaki değer boş bir değer değil ise arama sağlıyoruz.Boş bir bağlantı açmak gereksiz.
            {
                string cümle = "select * from müşteri where " + searchValue + " like '%" + textBox11.Text + "%'"; // bu komutumuzda like değeri kullanıyoruz çünkü benzeyen bir değer arıyoruz
                SqlDataAdapter adtr2 = new SqlDataAdapter(); // eğer "+ searchValue + " = 'örnek text' direkt olarak textbox'daki değere eşit veriyi gösterir.

                dataGridView1.DataSource = arac_kiralama.listele(adtr2, cümle); // sınıfımızdan listele metodunu tekrar datagridview sourcesine eşitliyoruz.
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e) // datagridview'deki bir hücreye çift tıklandığında datagridview'deki değerleri textbox'lara atamamızı sağlayan kısım
        {
            if (this.panel2.Visible) // panel2'nin visible değeri true ise bu işlem gerçekleşecek.
            {
                DataGridViewRow satır = dataGridView1.CurrentRow; // seçili satırı çağırıyoruz.
                textBox9.Text = satır.Cells[0].Value.ToString(); // toString ile gelen değeri string haline çeviriyoruz.
                textBox10.Text = satır.Cells[1].Value.ToString();
                textBox8.Text = satır.Cells[3].Value.ToString();
                textBox7.Text = satır.Cells[2].Value.ToString();
                textBox6.Text = satır.Cells[4].Value.ToString();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null) // eğer boş bir değer ise güncelleme sağlanmayacak şekilde kontrol yaptırıyoruz.
            {
                string cümle = "update müşteri set adsoyad=@adsoyad,telefon=@telefon,adres=@adres,email=@email where tc=@tc"; // update komutumuzu oluşturuyoruz ve tcye göre update yaptırıyoruz.
                SqlCommand komut2 = new SqlCommand();

                komut2.Parameters.AddWithValue("@adsoyad", textBox9.Text); // textbox değerlerimizi parametlere eşitliyoruz.
                komut2.Parameters.AddWithValue("@tc", textBox10.Text);
                komut2.Parameters.AddWithValue("@adres", textBox7.Text);
                komut2.Parameters.AddWithValue("@telefon", textBox8.Text);
                komut2.Parameters.AddWithValue("@email", textBox6.Text);
                arac_kiralama.ekle_kaldır_güncelle(komut2, cümle); // sınıfımızdaki diğer metodu kullanıp komutu execute ediyoruz.
                clearMusteriList(); // textbox değerlerimizi temizliyoruz.
                refreshMusteriList(); // datagridview'i yeniliyoruz.
            }
            else // Boş bir değer için hata mesajı gösteriyoruz.
            {
                MessageBox.Show("Boş bir değer güncellenemez !");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            clearMusteriList();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null) // eğer boş bir değer ise güncelleme sağlanmayacak şekilde kontrol yaptırıyoruz.
            {
                DataGridViewRow satır = dataGridView1.CurrentRow; // seçili satırı çağırıyoruz.
                string cümle = " DELETE FROM müşteri WHERE tc ='" + satır.Cells[1].Value.ToString() + "'"; // delete komutu oluşturup tc'ye göre sildiriyoruz bu sefer like kullanmadık çünkü direkt olarak eşit veriyi değiştirmek istiyoruz ona benzeyeni değil.
                SqlCommand komut2 = new SqlCommand();
                MessageBox.Show(satır.Cells[0].Value.ToString() + " İsimli müşteri veri tabanından silindi.");
                arac_kiralama.ekle_kaldır_güncelle(komut2, cümle);
                refreshMusteriList(); // datagridview'i yeniliyoruz.
            }
            else // Boş bir değer için hata mesajı gösteriyoruz.
            {
                MessageBox.Show("Boş değer silinemez !");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox11.Enabled = true;
        }
        // Müşteri Listeleme Paneli



        // Araç Kayıt Paneli

        private void clearCarRegList() // 3.paneldeki değerleri temizliyoruz.
        {
            foreach (Control item in this.panel3.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            // bu sefer resim seçeneği olduğu için label25'i gizliyoruz picturebox'un ise imagelocation değerini null yapıyoruz.
            label25.Visible = false; // böylelikle fotoğraf değeride temizlenmiş oluyor.
            this.pictureBox4.ImageLocation = null;
        }
        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            // 3.Paneli gizlemek için oluşturduğumuz butonun üzerine geldiğimizde resminin değişmesini sağlıyoruz.
            var image = (System.Drawing.Bitmap)new ResourceManager(typeof(Otomasyon.Properties.Resources)).GetObject("navi_close_enter");
            pictureBox3.Image = image;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            // üzerinden çıktığımızda ise normal haline gelmesini sağlıyoruz.
            var image = (System.Drawing.Bitmap)new ResourceManager(typeof(Otomasyon.Properties.Resources)).GetObject("navi_close_normal");
            pictureBox3.Image = image;
        }

        private void pictureBox3_MouseClick(object sender, MouseEventArgs e)
        {
            // tıkladığımızda ise tekrar değişmesini sağlıyoruz.
            var image = (System.Drawing.Bitmap)new ResourceManager(typeof(Otomasyon.Properties.Resources)).GetObject("navi_close_press");
            pictureBox3.Image = image;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.panel3.Visible = false; // 3.Panelimizin visible ve enabled değerini false yapıp deaktif hale getiriyoruz.
            this.panel3.Enabled = false;
            this.Size = new Size(600, 450); // formumuzun boyutunu default hale getiriyoruz.
            this.label38.Visible = true; // işlem yapmamızı gösteren labelin'de visible değerini true yapıp görünür hale getiriyoruz.
        }

        private void button14_Click(object sender, EventArgs e) // openFileDialog butonu
        {
            openFileDialog1.FileName = null; // openFileDialog'umuzun filename değerini null yaptığımızda resim seçme kısmı açıldığında altta herhngi bir yazı gözükmemesini sağlamış oluyoruz.
            openFileDialog1.ShowDialog(); // showDialog metodu ile resim seçme ekranımızı açtırıyoruz.
            if (!string.IsNullOrEmpty(openFileDialog1.FileName)) // eğer seçilen değer boş bir değer değil ise bu işlem gerçekleşiyor.
            {
                pictureBox4.ImageLocation = openFileDialog1.FileName; // picturebox'un imagelocation değerine openfiledialog'daki dosya ismini atıyoruz böylelikle seçtiğimiz resim gözüküyor.
                textBox20.Text = pictureBox4.ImageLocation; // textbox değerimize de seçtiğimiz resmin yolunu atıyoruz.
                label25.Visible = true; // seçilen resim yazısını görünür hale getiriyoruz.
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            clearCarRegList(); // araç kaydı yaptığımız kısımdaki textboxları temizliyoruz.
        }

        private void button13_Click(object sender, EventArgs e)
        {
            bool isError = false; // yeni bir bool değeri oluşturuyoruz. Eğer foreach döngüsü içinde hata mesajı verdirtirsek oluşan hatalar kadar mesaj alırız
            foreach (Control item in Controls) // o yüzden bool değeri oluşturup tek bir hata mesajına düşürüyoruz.
            {
                if (item is TextBox && string.IsNullOrEmpty(item.Text))
                {
                    isError = true;
                }
            }
            if (string.IsNullOrEmpty(pictureBox4.ImageLocation))
            {
                isError = true;
            }
            if (isError) // eğer isError değeri true ise hata mesajı gönderip return çeviriyoruz böylelikle boş bir bağlantı açıp hataya düşürmeyecek.
            {
                MessageBox.Show("Boş değer girilemez.");
                return;
            }
            string cümle = "insert into araç(plaka,marka,seri,yil,renk,km,yakit,kiraucreti,resim,tarih,durumu) values(@plaka,@marka,@seri,@yil,@renk,@km,@yakit,@kiraucreti,@resim,@tarih,@durumu)";
            SqlCommand komut2 = new SqlCommand();
            komut2.Parameters.AddWithValue("@plaka", textBox16.Text); // textbox değerlerimizi parametrelerimizle eşitliyoruz.
            komut2.Parameters.AddWithValue("@marka", textBox15.Text);
            komut2.Parameters.AddWithValue("@seri", string.IsNullOrEmpty(textBox14.Text) ? "Belirsiz" : textBox14.Text); // eğer textbox14'deki text boş bir değer ise Belirsiz değerini atıyoruz değil ise tekrar textbox14'deki değeri çekiyoruz.
            komut2.Parameters.AddWithValue("@yil", textBox13.Text);
            komut2.Parameters.AddWithValue("@renk", textBox12.Text);
            komut2.Parameters.AddWithValue("@km", textBox17.Text);
            komut2.Parameters.AddWithValue("@yakit", textBox18.Text);
            komut2.Parameters.AddWithValue("@kiraucreti", int.Parse(textBox19.Text)); // kira ücreti bir int değerinde olduğu için textbox'daki değeri de int'e çeviriyoruz.
            komut2.Parameters.AddWithValue("@resim", pictureBox4.ImageLocation);
            komut2.Parameters.AddWithValue("@tarih", DateTime.Now.ToString()); // DateTime.Now'dan ise şu an ki tarihin stringe çevirilmiş halini alıyoruz.
            komut2.Parameters.AddWithValue("@durumu", "Boş Durumda");
            arac_kiralama.ekle_kaldır_güncelle(komut2, cümle); // sınıfımızdaki diğer metodumuza komutumuzu gönderiyoruz.
            clearCarRegList(); // araç kayıt panelindeki textboxları temizliyoruz.
            MessageBox.Show("Yeni araç başarıyla eklendi.");
        }

        private void textBox19_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); // textbox'a girilen değerin rakam olup olmadığını kontrol ediyoruz. Eğer bir rakam değilse işlem yaptırtmıyoruz.
        }

        private void textBox17_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); // textbox'a girilen değerin rakam olup olmadığını kontrol ediyoruz. Eğer bir rakam değilse işlem yaptırtmıyoruz.
        }

        private void textBox13_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); // textbox'a girilen değerin rakam olup olmadığını kontrol ediyoruz. Eğer bir rakam değilse işlem yaptırtmıyoruz.
        }
        // Araç Kayıt Paneli



        // Araç Listeleme Paneli


        private void clearCarList()
        {
            foreach (Control item in this.panel4.Controls) // 4.Paneldeki değerleri temizlememiz için 4.panelin controls kısmında foreach döngüsü oluşturuyoruz.
            {
                if (item is TextBox) // eğer control itemi bir textbox ise değeri temizliyoruz.
                {
                    item.Text = "";
                }
            }
            label37.Visible = false; // resim kısmını da temizleyeceğimiz için label37'yi gizliyoruz picturebox'un imagelocation değerini ise null yapıyoruz böylelikle resimlerde temizlenmiş oluyor.
            this.pictureBox6.ImageLocation = null;
            button18.Enabled = false; // resim ekleme butonumuzun enabled değerini false yapıp deaktif hale getiriyoruz.
        }
        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            // 4.Paneli gizlemek için oluşturduğumuz butonun üzerine geldiğimizde resminin değişmesini sağlıyoruz.
            var image = (System.Drawing.Bitmap)new ResourceManager(typeof(Otomasyon.Properties.Resources)).GetObject("navi_close_enter");
            pictureBox5.Image = image;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            // üzerinden çıktığımızda ise normal haline gelmesini sağlıyoruz.
            var image = (System.Drawing.Bitmap)new ResourceManager(typeof(Otomasyon.Properties.Resources)).GetObject("navi_close_normal");
            pictureBox5.Image = image;
        }

        private void pictureBox5_MouseClick(object sender, MouseEventArgs e)
        {
            // tıkladığımızda ise tekrar değişmesini sağlıyoruz.
            var image = (System.Drawing.Bitmap)new ResourceManager(typeof(Otomasyon.Properties.Resources)).GetObject("navi_close_press");
            pictureBox5.Image = image;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.panel4.Visible = false; // 4.Panelimizin visible ve enabled değerini false yapıp deaktif hale getiriyoruz.
            this.panel4.Enabled = false;
            this.Size = new Size(600, 450); // formumuzun boyutunu default hale getiriyoruz.
            this.label38.Visible = true; // işlem yapmamızı gösteren label'i görünür hale getiriyoruz.
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e) // datagridview'deki bir hücrete çift tıklandığında gerçekleşecek işlem
        {
            DataGridViewRow satır = dataGridView2.CurrentRow; // seçili satırı çağırıyoruz
            textBox26.Text = satır.Cells["plaka"].Value.ToString(); // seçili satırdaki değerlerimizi string'e çevirip textboxlarımıza atıyoruz.
            textBox25.Text = satır.Cells["marka"].Value.ToString();
            textBox24.Text = satır.Cells["seri"].Value.ToString();
            textBox23.Text = satır.Cells["yil"].Value.ToString();
            textBox22.Text = satır.Cells["renk"].Value.ToString();
            textBox27.Text = satır.Cells["km"].Value.ToString();
            textBox28.Text = satır.Cells["yakit"].Value.ToString();
            textBox29.Text = satır.Cells["kiraucreti"].Value.ToString();
            pictureBox6.ImageLocation = satır.Cells["resim"].Value.ToString();
            if (!string.IsNullOrEmpty(pictureBox6.ImageLocation)) // eğer seçtiğimiz veri herhangi bir resme sahipse label37'yi görünür hale getirip textbox'umuza picturebox'daki imagelocation değerini atıyoruz.
            {
                textBox30.Text = pictureBox6.ImageLocation;
                label37.Visible = true;
            }
            else // eğer picturebox'daki imagelocation değerimiz boş bir değe rise textbox'umuza Resim Yok yazdırıp label37'yi gizliyoruz.Çünkü bir resim yok
            {
                textBox30.Text = "Resim Yok";
                label37.Visible = false;
            }
            button18.Enabled = true; // button18'i aktif hale getiriyoruz.
        }
        private void refreshCarList() // Datagridview'deki değerlerimizi tekrar yüklüyoruz böylelikle yenilenmiş oluyor
        {
            string cümle = "SELECT * FROM araç";
            SqlDataAdapter adtr2 = new SqlDataAdapter();
            dataGridView2.DataSource = arac_kiralama.listele(adtr2, cümle); // listele metodumuz bir datatable returnu çevirdiği için datagridview'in sourcesine eşitliyoruz.
            dataGridView2.Columns[0].HeaderText = "Plaka";
            dataGridView2.Columns[1].HeaderText = "Marka";
            dataGridView2.Columns[2].HeaderText = "Seri";
            dataGridView2.Columns[3].HeaderText = "Yıl";
            dataGridView2.Columns[4].HeaderText = "Renk";
            dataGridView2.Columns[5].HeaderText = "Kilometre";
            dataGridView2.Columns[6].HeaderText = "Yakıt Türü";
            dataGridView2.Columns[7].HeaderText = "Kira Ücreti";
            dataGridView2.Columns[8].HeaderText = "Resim";
            dataGridView2.Columns[9].HeaderText = "Tarih";
            dataGridView2.Columns[10].HeaderText = "Durum";
        }

        private void button15_Click(object sender, EventArgs e) // silme butonumuz
        {
            if (dataGridView2.CurrentRow != null) // boş bir değer olup olmadığının kontrolünü sağlıyoruz.
            {
                DataGridViewRow satır = dataGridView2.CurrentRow; // seçili satırdaki veriyi çekiyoruz.
                string cümle = " delete from araç where plaka ='" + satır.Cells["plaka"].Value.ToString() + "'"; // plakaya göre araç silme komutumuz.
                SqlCommand komut2 = new SqlCommand();
                arac_kiralama.ekle_kaldır_güncelle(komut2, cümle); // sınıfımızdaki metoda oluşturduğumuz komutu gönderiyoruz.
                refreshCarList(); // datagridview'deki verilerimizi yeniliyoruz.
                clearCarList(); // textboxlarımızı temizliyoruz.
            }
            else // eğer boş bir değer ise hata mesajı verdiriyoruz.
            {
                MessageBox.Show("Boş değer silinemez !");
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            clearCarList();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow != null) // boş bir değer olup olmadığının kontrolünü sağlıyoruz.
            {
                string cümle = "update araç set marka=@marka,seri=@seri,yil=@yil,renk=@renk,km=@km,yakit=@yakit,kiraucreti=@kiraucreti,resim=@resim where plaka=@plaka";
                SqlCommand komut2 = new SqlCommand(); // plakaya göre update komutu oluşturuyoruz. Plaka değerimiz değiştirilmediği için en uygun değer bu

                komut2.Parameters.AddWithValue("@plaka", textBox26.Text); // textbox'daki verilerimizi parametrelerimizle eşitliyoruz.
                komut2.Parameters.AddWithValue("@marka", textBox25.Text);
                komut2.Parameters.AddWithValue("@seri", string.IsNullOrEmpty(textBox24.Text) ? "Belirsiz" : textBox24.Text); // textbox24'deki değer boş bir değer ise Belirsiz değerini atıyoruz, değil ise textbox24'deki değeri tekrar çekiyoruz.
                komut2.Parameters.AddWithValue("@yil", textBox23.Text);
                komut2.Parameters.AddWithValue("@renk", textBox22.Text);
                komut2.Parameters.AddWithValue("@km", textBox27.Text);
                komut2.Parameters.AddWithValue("@yakit", textBox28.Text);
                komut2.Parameters.AddWithValue("@kiraucreti", textBox29.Text);
                komut2.Parameters.AddWithValue("@resim", pictureBox6.ImageLocation);
                komut2.Parameters.AddWithValue("@tarih", DateTime.Now.ToString()); // DateTime.Now'dan şu an ki tarihin string halini çekiyoruz.
                MessageBox.Show(textBox26.Text + " plakaya sahip araç güncellendi!"); // textbox26'da araç plakası olduğu için o değeri çekip mesaj gönderiyoruz.
                arac_kiralama.ekle_kaldır_güncelle(komut2, cümle); // sınıfımızdaki metoda komutumuzu gönderiyoruz.
                refreshCarList(); // datagridview'deki verilerimizi tekrar yüklüyoruz.
                clearCarList(); // textbox değerlerimizi temizliyoruz.
            }
            else // eğer seçili satır boş ise bu mesajı gönderiyoruz.
            {
                MessageBox.Show("Boş bir değer güncellenemez !");
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = null; // openFileDialog'umuzun filename değerini null yaptığımızda resim seçme kısmı açıldığında altta herhngi bir yazı gözükmemesini sağlamış oluyoruz.
            openFileDialog1.ShowDialog(); // showdialog ile resim seçme kısmını açıyoruz.
            if (!string.IsNullOrEmpty(openFileDialog1.FileName)) // eğer filename değeri boş bir değer değil ise işlem gerçekleşiyor.
            {
                pictureBox6.ImageLocation = openFileDialog1.FileName; // picturebox'umuzun imagelocation değerine resim seçme kısmındaki dosyanın yolunu atıyoruz.
                textBox30.Text = pictureBox6.ImageLocation; // textboxumuza da aynı şekilde o yolu atıyoruz.
                label37.Visible = true; // seçili resim labelini görünür hale getiriyoruz.
            }
        }

        private void textBox29_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); // textbox'a girilen değerin rakam olup olmadığını kontrol ediyoruz. Eğer bir rakam değilse işlem yaptırtmıyoruz.
        }

        private void textBox27_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); // textbox'a girilen değerin rakam olup olmadığını kontrol ediyoruz. Eğer bir rakam değilse işlem yaptırtmıyoruz.
        }

        private void textBox21_TextChanged(object sender, EventArgs e) // arama yaptığımız kısım
        {
            string searchValue = ""; // yeni bir string değeri oluşturuyoruz.
            switch (comboBox2.SelectedIndex) // comboboxumuzun indeks değerini switch ile kontrol ediyoruz
            {
                case 0:// eğer 0.indeks(1.seçenek) seçiliyse oluşturulan string değerine plaka değerini atıyoruz.
                    searchValue = "plaka";
                    break;
                case 1:// eğer 1.indeks(2.seçenek) seçiliyse oluşturulan string değerine marka değerini atıyoruz.
                    searchValue = "marka";
                    break;
                case 2:// eğer 2.indeks(3.seçenek) seçiliyse oluşturulan string değerine seri değerini atıyoruz.
                    searchValue = "seri";
                    break;
                case 3:// eğer 3.indeks(4.seçenek) seçiliyse oluşturulan string değerine yil değerini atıyoruz.
                    searchValue = "yil";
                    break;
                case 4:// eğer 4.indeks(5.seçenek) seçiliyse oluşturulan string değerine renk değerini atıyoruz.
                    searchValue = "renk";
                    break;
                case 5:// eğer 5.indeks(6.seçenek) seçiliyse oluşturulan string değerine yakit değerini atıyoruz.
                    searchValue = "yakit";
                    break;
                case 6:// eğer 6.indeks(7.seçenek) seçiliyse oluşturulan string değerine kiraucreti değerini atıyoruz.
                    searchValue = "kiraucreti";
                    break;
                case 7:// eğer 7.indeks(8.seçenek) seçiliyse oluşturulan string değerine durumu değerini atıyoruz.
                    searchValue = "durumu";
                    break;
            }
            string komut = "select * from araç where " + searchValue + " like '%" + textBox21.Text + "%'"; // komutumuzda tekrar like kullanıyoruz çünkü benzer bir değeri aratıyoruz.
            if (searchValue == "kiraucreti" || searchValue == "yil") // ama kira ücreti ve yılı like ile aratmamız saçma olur o yüzden kontrol sağlatıyoruz
            {
                komut = "select * from araç where " + searchValue + " >= " + textBox21.Text; // eğer kira ücreti veya yıla göre aratıyorsak  >= ifadesini kullanıyoruz girilen değer büyük veya eşit ise sonuçlar ekrana geliyor.
            }
            if (!string.IsNullOrEmpty(textBox21.Text)) // eğer arama kutusundaki değer boş bir değer değil ise arama sağlıyoruz.Boş bir bağlantı açmak gereksiz.
            {
                SqlDataAdapter adtr2 = new SqlDataAdapter();

                dataGridView2.DataSource = arac_kiralama.listele(adtr2, komut); // sınıfımızdaki listele metodunu kullanarak datagridview'imizin sourcesini listeleye eşitliyoruz.
            }
        }
        // Sözleşme
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            clearSozlesme();
            this.panel5.Enabled = false;
            this.panel5.Visible = false;
            this.Size = new Size(600, 450);
            this.label38.Visible = true;
        }
        private void pictureBox8_MouseEnter(object sender, EventArgs e)
        {
            // 4.Paneli gizlemek için oluşturduğumuz butonun üzerine geldiğimizde resminin değişmesini sağlıyoruz.
            var image = (System.Drawing.Bitmap)new ResourceManager(typeof(Otomasyon.Properties.Resources)).GetObject("navi_close_enter");
            pictureBox8.Image = image;
        }

        private void pictureBox8_MouseLeave(object sender, EventArgs e)
        {
            // üzerinden çıktığımızda ise normal haline gelmesini sağlıyoruz.
            var image = (System.Drawing.Bitmap)new ResourceManager(typeof(Otomasyon.Properties.Resources)).GetObject("navi_close_normal");
            pictureBox8.Image = image;
        }

        private void pictureBox8_MouseClick(object sender, MouseEventArgs e)
        {
            // tıkladığımızda ise tekrar değişmesini sağlıyoruz.
            var image = (System.Drawing.Bitmap)new ResourceManager(typeof(Otomasyon.Properties.Resources)).GetObject("navi_close_press");
            pictureBox8.Image = image;
        }

        private void clearSozlesme()
        {
            foreach (Control item in this.panel5.Controls) // 4.Paneldeki değerleri temizlememiz için 4.panelin controls kısmında foreach döngüsü oluşturuyoruz.
            {
                if (item is TextBox) // eğer control itemi bir textbox ise değeri temizliyoruz.
                {
                    item.Text = "";
                }
            }
            dateTimePicker1.ResetText(); // datetimepickerleri resetliyoruz.
            dateTimePicker2.ResetText();
            dateTimePicker3.ResetText();
            comboBox3.SelectedIndex = 0;
            if (comboBox4.Items.Count > 0)
                comboBox4.SelectedIndex = 0;
            if (comboBox5.Items.Count > 0)
                comboBox5.SelectedIndex = 0;
        }
        private void refreshPlaka()
        {
            comboBox4.Items.Clear(); // boş durumda olan araçları listeleyip comboboxumuza ekliyoruz.
            string command = "SELECT * FROM araç where durumu = 'Boş Durumda'";
            SqlCommand komut2 = new SqlCommand();
            List<string> plakalar = arac_kiralama.getPlaka(komut2, command);
            for (int i = 0; i < plakalar.Count; i++)
            {
                comboBox4.Items.Add(plakalar[i]);
            }
            if (comboBox4.Items.Count > 0)
                comboBox4.SelectedIndex = 0;
            if (comboBox5.Items.Count > 0)
                comboBox5.SelectedIndex = 0;
        }

        private void refreshSozlesmeList()
        {
            string cümle = "SELECT * FROM sozlesme";
            SqlDataAdapter adtr2 = new SqlDataAdapter();
            dataGridView3.DataSource = arac_kiralama.listele(adtr2, cümle); // listele metodumuz bir datatable returnu çevirdiği için datagridview'in sourcesine eşitliyoruz.
            dataGridView3.Columns[0].HeaderText = "TC";
            dataGridView3.Columns[1].HeaderText = "Isim - Soyisim";
            dataGridView3.Columns[2].HeaderText = "Telefon";
            dataGridView3.Columns[3].HeaderText = "Ehliyet No";
            dataGridView3.Columns[4].HeaderText = "Ehliyet Tarihi";
            dataGridView3.Columns[5].HeaderText = "Kiralanan Şehir";
            dataGridView3.Columns[6].HeaderText = "Plaka";
            dataGridView3.Columns[7].HeaderText = "Kira Şekli";
            dataGridView3.Columns[8].HeaderText = "Kira Ücreti";
            dataGridView3.Columns[9].HeaderText = "Kiralama Tarihi";
            dataGridView3.Columns[10].HeaderText = "Teslim Tarihi";
            dataGridView3.Columns[11].HeaderText = "İndirim Oranı";
            dataGridView3.Columns[12].HeaderText = "Ödenen Tutar";
        }
        private void button21_Click(object sender, EventArgs e)
        {
            clearSozlesme();
        }

        private void textBox31_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); // textbox'a girilen değerin rakam olup olmadığını kontrol ediyoruz. Eğer bir rakam değilse işlem yaptırtmıyoruz.
        }

        private void textBox31_TextChanged(object sender, EventArgs e)
        {
            if (textBox31.Text.StartsWith("0")) // kira ücreti veya indirim 0 ile başlarsa sildiriyoruz.
            {
                textBox31.Text = "";
            }
            if (textBox41.Text.StartsWith("0"))
            {
                textBox41.Text = "";
            }
            if (!string.IsNullOrEmpty(textBox41.Text) && !string.IsNullOrEmpty(textBox31.Text)) // kira ücreti ve indirim boş değermi diye kontrol ediyoruz
            {
                if (int.Parse(textBox31.Text) > 100) // eğer indirim değeri 100den büyükse 100e sabitliyoruz.
                {
                    textBox31.Text = "100";
                }
                if (int.Parse(textBox31.Text) < 1) // eğer indirim değeri 1den küçükse 1e sabitliyoruz.
                {
                    textBox31.Text = "1";
                }
                int kiralamaucreti = int.Parse(textBox41.Text); // kira ücretini int değişkenine atıyoruz.
                double indirim = double.Parse((double.Parse(textBox31.Text) / 100).ToString()); // indirim verisini 100e bölüyoruz noktalı bir sayı elde edebiliriz o yüzden double türüne dönüştürüyoruz.
                textBox44.Text = (kiralamaucreti - (kiralamaucreti * indirim)).ToString(); // güncel tutarımızı belirliyoruz.
            }
            else
            {
                textBox44.Text = "";
            }
        }

        private void textBox41_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); // textbox'a girilen değerin rakam olup olmadığını kontrol ediyoruz. Eğer bir rakam değilse işlem yaptırtmıyoruz.
        }

        private void textBox40_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); // textbox'a girilen değerin rakam olup olmadığını kontrol ediyoruz. Eğer bir rakam değilse işlem yaptırtmıyoruz.

        }

        private void textBox34_KeyPress(object sender, KeyPressEventArgs e)
        {
            // bu textboxa sadece harf girilmesini sağlatıyoruz
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar);
            if ((int)e.KeyChar == 32) // boşluk bırakmayı engelletiyoruz.
            {
                e.Handled = true;
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            int errorCode = 0; // Yeni bir int değerinde değişken oluşturuyoruz, böylelikle hatamızın neyden kaynaklı olduğunu kullanıcıya daha rahat gösterebiliriz.
            foreach (Control item in this.panel5.Controls) // Tekrar 1.panel üzerinde foreach döngüsü oluşturuyoruz.
            {
                if (item is TextBox && item.Name != "textBox35" && string.IsNullOrEmpty(item.Text)) // eğer control itemi bir textbox ise ve boş bir değer ise errorCode değerini 1 yapıyoruz.
                {
                    errorCode = 1;
                }
            }
            if (textBox40.Text.Length < 11) // textbox1 değerinin uzunluğu 11'den küçük ise errorCode değerini 2 yapıyoruz. TC Kimlik numarası 11 rakamdan oluşuyor.
            {
                errorCode = 2;
            }
            if (textBox38.Text.Length < 10) // telefon numarası girdiğimiz kısma 0 olmadan giriniz demiştik.0 Olmadan 10 rakamdan oluşan bir değer girilmesi gerekiyor
            {
                errorCode = 3; // eğer girilen değer 10'dan küçükse errorCode değerini 3 yapıyoruz
            }
            if (comboBox4.SelectedItem == null)
            {
                errorCode = 4; // Plaka değeri boş ise
            }
            switch (errorCode) // Switch ile errorCode değişkenindeki değeri kontrol ediyoruz.
            {
                case 1: // eğer errorCode değeri 1 ise 
                    MessageBox.Show("Boş değer girilemez.");
                    break;
                case 2: // eğer errorCode değeri 2 ise
                    MessageBox.Show("TC Kimlik numarası 11 rakamdan oluşmalıdır.");
                    break;
                case 3: // eğer errorCode değeri 3 ise
                    MessageBox.Show("Telefon numarası 10 rakamdan oluşmalıdır.");
                    break;
                case 4:
                    MessageBox.Show("Araç plakası seçilmedi !");
                    break;
            }
            if (errorCode > 0) return; // burda ise errorCode değeri 0'dan büyükse return çektiriyoruz çünkü yukarda errorCode değerimize 1,2 ve 3 değerlerini atadığında bir hata meydana gelmiş oluyor.

            string cümle = "INSERT INTO sozlesme(tc,adsoyad,telefon,ehliyetno,ehliyettarihi,kiralanansehir,plaka,kirasekli,kiraucreti,kiralamatarihi,kirabitistarihi,indirimorani,odenentutar) VALUES(@tc,@adsoyad,@telefon,@ehliyetno,@ehliyettarihi,@kiralanansehir,@plaka,@kirasekli,@kiraucreti,@kiralamatarihi,@kirabitistarihi,@indirimorani,@odenentutar)"; // Insert Into komutu ile bir komut oluşturuyoruz
            SqlCommand komut2 = new SqlCommand();
            komut2.Parameters.AddWithValue("@tc", textBox40.Text); // textbox değerlerimizi parametrelerimize eşitliyoruz.
            komut2.Parameters.AddWithValue("@adsoyad", textBox39.Text);
            komut2.Parameters.AddWithValue("@telefon", textBox38.Text);
            komut2.Parameters.AddWithValue("@ehliyetno", textBox37.Text);
            komut2.Parameters.AddWithValue("@ehliyettarihi", dateTimePicker1.Value.ToShortDateString());
            komut2.Parameters.AddWithValue("@kiralanansehir", textBox34.Text);
            komut2.Parameters.AddWithValue("@plaka", comboBox4.SelectedItem.ToString());
            komut2.Parameters.AddWithValue("@kirasekli", comboBox5.SelectedItem.ToString());
            komut2.Parameters.AddWithValue("@kiraucreti", int.Parse(textBox41.Text));
            komut2.Parameters.AddWithValue("@kiralamatarihi", dateTimePicker2.Value.ToShortDateString());
            komut2.Parameters.AddWithValue("@kirabitistarihi", dateTimePicker3.Value.ToShortDateString());
            komut2.Parameters.AddWithValue("@indirimorani", int.Parse(textBox31.Text));
            komut2.Parameters.AddWithValue("@odenentutar", double.Parse(textBox44.Text));
            arac_kiralama.ekle_kaldır_güncelle(komut2, cümle); // önceden oluşturduğumuz sınıfımızdaki metodu çağırıp komutu gönderiyoruz.
            string updateStatus = "UPDATE araç SET durumu = 'Dolu' where plaka = '" + comboBox4.SelectedItem.ToString() + "'"; // update komutu ile kiralanan aracın durumunu dolu hale getiriyoruz.
            comboBox4.Items.Remove(comboBox4.SelectedItem);
            SqlCommand statusCommand = new SqlCommand();
            arac_kiralama.ekle_kaldır_güncelle(statusCommand, updateStatus);
            clearSozlesme(); // komut gönderildikten sonra textbox değerlerimizi tekrar temizliyoruz.
            refreshSozlesmeList();
            refreshPlaka();

            MessageBox.Show("Araç kiralama başarılı !");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (dataGridView3.CurrentRow != null) // eğer boş bir değer ise güncelleme sağlanmayacak şekilde kontrol yaptırıyoruz.
            {
                DataGridViewRow satır = dataGridView3.CurrentRow; // seçili satırı çağırıyoruz.
                string updateStatus = "UPDATE araç SET durumu = 'Boş Durumda' where plaka = '" + satır.Cells[6].Value.ToString() + "'";
                string cümle = " DELETE FROM sozlesme WHERE plaka ='" + satır.Cells[6].Value.ToString() + "'"; // delete komutu oluşturup plakaya göre sildiriyoruz bu sefer like kullanmadık çünkü direkt olarak eşit veriyi değiştirmek istiyoruz ona benzeyeni değil.
                SqlCommand komut2 = new SqlCommand();
                SqlCommand komut3 = new SqlCommand();
                MessageBox.Show(satır.Cells[6].Value.ToString() + " plakaya sahip aracın sözleşmesi iptal edildi.");
                arac_kiralama.ekle_kaldır_güncelle(komut2, cümle);
                arac_kiralama.ekle_kaldır_güncelle(komut3, updateStatus);
                clearSozlesme(); // komut gönderildikten sonra textbox değerlerimizi tekrar temizliyoruz.
                refreshSozlesmeList();
                refreshPlaka();
            }
            else // Boş bir değer için hata mesajı gösteriyoruz.
            {
                MessageBox.Show("Boş değer silinemez !");
            }
        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBox4.Items.Clear();
            DataGridViewRow satır = dataGridView3.CurrentRow; // seçili satırı çağırıyoruz.

            textBox40.Text = satır.Cells[0].Value.ToString();
            textBox39.Text = satır.Cells[1].Value.ToString();
            textBox38.Text = satır.Cells[2].Value.ToString();
            textBox37.Text = satır.Cells[3].Value.ToString();
            dateTimePicker1.Value = Convert.ToDateTime(satır.Cells[4].Value);
            textBox34.Text = satır.Cells[5].Value.ToString();
            comboBox4.Items.Add(satır.Cells[6].Value);
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedItem = satır.Cells[7].Value;
            textBox41.Text = satır.Cells[8].Value.ToString();
            dateTimePicker2.Value = Convert.ToDateTime(satır.Cells[9].Value);
            dateTimePicker3.Value = Convert.ToDateTime(satır.Cells[10].Value);
            textBox31.Text = satır.Cells[11].Value.ToString();
            textBox44.Text = satır.Cells[12].Value.ToString();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (dataGridView3.CurrentRow != null) // boş bir değer olup olmadığının kontrolünü sağlıyoruz.
            {
                string cümle = "Update sozlesme set tc=@tc,adsoyad=@adsoyad,telefon=@telefon,ehliyetno=@ehliyetno,ehliyettarihi=@ehliyettarihi,kiralanansehir=@kiralanansehir,kirasekli=@kirasekli,kiraucreti=@kiraucreti,kiralamatarihi=@kiralamatarihi,kirabitistarihi=@kirabitistarihi,indirimorani=@indirimorani,odenentutar=@odenentutar where plaka=@plaka";
                SqlCommand komut2 = new SqlCommand(); // plakaya göre update komutu oluşturuyoruz. Plaka değerimiz değiştirilmediği için en uygun değer bu

                komut2.Parameters.AddWithValue("@tc", textBox40.Text); // textbox değerlerimizi parametrelerimize eşitliyoruz.
                komut2.Parameters.AddWithValue("@adsoyad", textBox39.Text);
                komut2.Parameters.AddWithValue("@telefon", textBox38.Text);
                komut2.Parameters.AddWithValue("@ehliyetno", textBox37.Text);
                komut2.Parameters.AddWithValue("@ehliyettarihi", dateTimePicker1.Value.ToShortDateString());
                komut2.Parameters.AddWithValue("@kiralanansehir", textBox34.Text);
                komut2.Parameters.AddWithValue("@plaka", comboBox4.SelectedItem.ToString());
                komut2.Parameters.AddWithValue("@kirasekli", comboBox5.SelectedItem.ToString());
                komut2.Parameters.AddWithValue("@kiraucreti", int.Parse(textBox41.Text));
                komut2.Parameters.AddWithValue("@kiralamatarihi", dateTimePicker2.Value.ToShortDateString());
                komut2.Parameters.AddWithValue("@kirabitistarihi", dateTimePicker3.Value.ToShortDateString());
                komut2.Parameters.AddWithValue("@indirimorani", int.Parse(textBox31.Text));
                komut2.Parameters.AddWithValue("@odenentutar", double.Parse(textBox44.Text));
                MessageBox.Show(comboBox4.SelectedItem.ToString() + " plakaya sahip araç güncellendi!");
                arac_kiralama.ekle_kaldır_güncelle(komut2, cümle); // sınıfımızdaki metoda komutumuzu gönderiyoruz.
                clearSozlesme(); // komut gönderildikten sonra textbox değerlerimizi tekrar temizliyoruz.
                refreshSozlesmeList();
                refreshPlaka();
            }
            else // eğer seçili satır boş ise bu mesajı gönderiyoruz.
            {
                MessageBox.Show("Boş bir değer güncellenemez !");
            }
        }

        private void textBox35_TextChanged(object sender, EventArgs e)
        {
            string searchValue = ""; // yeni bir string değeri oluşturuyoruz.
            switch (comboBox3.SelectedIndex) // comboboxumuzun indeks değerini switch ile kontrol ediyoruz
            {
                case 0:// eğer 0.indeks(1.seçenek) seçiliyse oluşturulan string değerine plaka değerini atıyoruz.
                    searchValue = "plaka";
                    break;
                case 1:// eğer 1.indeks(2.seçenek) seçiliyse oluşturulan string değerine marka değerini atıyoruz.
                    searchValue = "marka";
                    break;
                case 2:// eğer 2.indeks(3.seçenek) seçiliyse oluşturulan string değerine kiraucreti değerini atıyoruz.
                    searchValue = "kiraucreti";
                    break;
                case 3:// eğer 3.indeks(4.seçenek) seçiliyse oluşturulan string değerine telefon değerini atıyoruz.
                    searchValue = "telefon";
                    break;
                case 4:// eğer 4.indeks(5.seçenek) seçiliyse oluşturulan string değerine kiralanansehir değerini atıyoruz.
                    searchValue = "kiralanansehir";
                    break;
            }
            string komut = "select * from sozlesme where " + searchValue + " like '%" + textBox35.Text + "%'"; // komutumuzda tekrar like kullanıyoruz çünkü benzer bir değeri aratıyoruz.
            if (searchValue == "kiraucreti") // ama kira ücreti ve yılı like ile aratmamız saçma olur o yüzden kontrol sağlatıyoruz
            {
                komut = "select * from sozlesme where " + searchValue + " >= " + textBox35.Text; // eğer kira ücreti veya yıla göre aratıyorsak  >= ifadesini kullanıyoruz girilen değer büyük veya eşit ise sonuçlar ekrana geliyor.
            }
            if (!string.IsNullOrEmpty(textBox35.Text)) // eğer arama kutusundaki değer boş bir değer değil ise arama sağlıyoruz.Boş bir bağlantı açmak gereksiz.
            {
                SqlDataAdapter adtr2 = new SqlDataAdapter();

                dataGridView3.DataSource = arac_kiralama.listele(adtr2, komut); // sınıfımızdaki listele metodunu kullanarak datagridview'imizin sourcesini listeleye eşitliyoruz.
            }
            else
            {
                komut = "select * from sozlesme";
                SqlDataAdapter adtr2 = new SqlDataAdapter();

                dataGridView3.DataSource = arac_kiralama.listele(adtr2, komut); // sınıfımızdaki listele metodunu kullanarak datagridview'imizin sourcesini listeleye eşitliyoruz.
            }
        }
    }
}
