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
using System.Configuration;
using System.IO;
using ClosedXML.Excel;

namespace Form_Database_Application
{
    public partial class Form1 : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["VeritabaniBaglantisi"].ConnectionString;
        public Form1()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.Form1_Load);
        }

        private void LoadData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlDataAdapter musteriAdapter = new SqlDataAdapter("SELECT * FROM Musteri", connection);
                DataTable musteriTable = new DataTable();
                musteriAdapter.Fill(musteriTable);
                dataGridView1.DataSource = musteriTable;

                comboBox1.DataSource = musteriTable;
                comboBox1.DisplayMember = "Ad";
                comboBox1.ValueMember = "Id";



                SqlDataAdapter siparisAdapter = new SqlDataAdapter("SELECT * FROM Siparis", connection);
                DataTable siparisTable = new DataTable();
                siparisAdapter.Fill(siparisTable);
                dataGridView2.DataSource = siparisTable;
            }
        }




        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
            dateTimePicker1.Value = DateTime.Now;
            RoundButton(button1);
            RoundButton(button2);
            RoundButton(button3);
            RoundButton(button4);
            RoundButton(button5);
            RoundButton(button6);
            RoundButton(button7);
            RoundButton(button8);
            RoundButton(button9);
            RoundButton(button10);
            RoundButton(button11);
            RoundButton(button12);
            RoundButton(button13);
            RoundButton(button14);
            StyleDataGridView(dataGridView1);
            StyleDataGridView(dataGridView2);
            CreateCustomerCard("Ahmet Yılmaz", "555-1234567");
            CreateCustomerCard("Mehmet Demir", "555-2345678");
            CustomizeForm();
       
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Lütfen tüm müşteri bilgilerini doldurun.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {

                MessageBox.Show("Müşteri başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Müşteri eklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            string ad = textBox1.Text;
            string soyad = textBox2.Text;
            string telefon = textBox3.Text;


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Musteri (Ad, Soyad, Telefon) VALUES (@Ad, @Soyad, @Telefon)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Ad", ad);
                    command.Parameters.AddWithValue("@Soyad", soyad);
                    command.Parameters.AddWithValue("@Telefon", telefon);
                    command.ExecuteNonQuery();
                }
            }

            LoadData();
            ClearMusteriFields();



        }

        private void ClearMusteriFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null ||
       string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Lütfen tüm sipariş bilgilerini doldurun.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {

                MessageBox.Show("Sipariş başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sipariş eklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }





            int musteriId = (int)comboBox1.SelectedValue;
            string urunAdi = textBox4.Text;
            DateTime siparisTarihi = dateTimePicker1.Value;


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Siparis (MusteriId, SiparisTarihi, UrunAdi) VALUES (@MusteriId, @SiparisTarihi, @UrunAdi)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MusteriId", musteriId);
                    command.Parameters.AddWithValue("@SiparisTarihi", siparisTarihi);
                    command.Parameters.AddWithValue("@UrunAdi", urunAdi);
                    command.ExecuteNonQuery();
                }
            }

            LoadData();
            ClearSiparisFields();






        }

        private void ClearSiparisFields()
        {
            textBox4.Clear();
            comboBox1.SelectedIndex = -1;
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {

                int Id = (int)dataGridView2.SelectedRows[0].Cells["Id"].Value;


                string urunAdi = textBox4.Text;

                DateTime siparisTarihi = dateTimePicker1.Value;


                int musteriId = (int)comboBox1.SelectedValue;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string updateSiparisQuery = "UPDATE Siparis SET UrunAdi = @UrunAdi, SiparisTarihi = @SiparisTarihi, MusteriID = @MusteriID WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(updateSiparisQuery, connection))
                    {
                        command.Parameters.AddWithValue("@UrunAdi", urunAdi);
                        command.Parameters.AddWithValue("@SiparisTarihi", siparisTarihi);
                        command.Parameters.AddWithValue("@MusteriID", musteriId);
                        command.Parameters.AddWithValue("@Id", Id);
                        command.ExecuteNonQuery();
                    }
                }

                LoadData();
                MessageBox.Show("Sipariş güncellendi.");
            }
            else
            {
                MessageBox.Show("Lütfen güncellemek için bir sipariş seçin.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue != null)
            {
                int musteriId = (int)comboBox1.SelectedValue;
                string musteriAdi = textBox1.Text;
                string musteriSoyadi = textBox2.Text;
                string musteriTelefonu = textBox3.Text;


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Musteri SET Ad = @Ad, Soyad = @Soyad, Telefon = @Telefon WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", musteriId);
                        command.Parameters.AddWithValue("@Ad", musteriAdi);
                        command.Parameters.AddWithValue("@Soyad", musteriSoyadi);
                        command.Parameters.AddWithValue("@Telefon", musteriTelefonu);
                        command.ExecuteNonQuery();
                    }
                }

                LoadData();
                MessageBox.Show("Müşteri bilgileri güncellendi.");
            }

            else
            {
                MessageBox.Show("Lütfen güncellemek için bir müşteri seçin.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                int musteriId = (int)comboBox1.SelectedValue;


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string checkSiparisQuery = "SELECT COUNT(*) FROM Siparis WHERE MusteriId = @MusteriId";
                    using (SqlCommand command = new SqlCommand(checkSiparisQuery, connection))
                    {
                        command.Parameters.AddWithValue("@MusteriId", musteriId);
                        int siparisCount = (int)command.ExecuteScalar();

                        if (siparisCount > 0)
                        {
                            MessageBox.Show("Bu müşteriye ait siparişler var. Öncelikle siparişleri silin.");
                            return;
                        }
                    }


                    string deleteMusteriQuery = "DELETE FROM Musteri WHERE Id = @MusteriId";
                    using (SqlCommand command = new SqlCommand(deleteMusteriQuery, connection))
                    {
                        command.Parameters.AddWithValue("@MusteriId", musteriId);
                        command.ExecuteNonQuery();
                    }
                }

                LoadData();
                MessageBox.Show("Müşteri silindi.");
            }
            else
            {
                MessageBox.Show("Lütfen silmek için bir müşteri seçin.");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {

                int ID = (int)dataGridView2.SelectedRows[0].Cells["Id"].Value;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string deleteSiparisQuery = "DELETE FROM Siparis WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(deleteSiparisQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ID", ID);
                        command.ExecuteNonQuery();
                    }
                }

                LoadData();
                MessageBox.Show("Sipariş silindi.");
            }
            else
            {
                MessageBox.Show("Lütfen silmek için bir sipariş seçin.");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string aramaTerimi = textBox5.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string searchQuery = "SELECT * FROM Musteri WHERE Ad LIKE @aramaTerimi OR Soyad LIKE @aramaTerimi";

                using (SqlCommand command = new SqlCommand(searchQuery, connection))
                {
                    command.Parameters.AddWithValue("@aramaTerimi", "%" + aramaTerimi + "%"); // Arama terimini parametre olarak ekle
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);


                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string aramaTerimi = textBox6.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string searchQuery = "SELECT * FROM Siparis WHERE UrunAdi LIKE @aramaTerimi";

                using (SqlCommand command = new SqlCommand(searchQuery, connection))
                {
                    command.Parameters.AddWithValue("@aramaTerimi", "%" + aramaTerimi + "%");
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);


                    dataGridView2.DataSource = dt;
                }
            }
        }

        private void LoadMusteriData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Musteri";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox5.Text))
            {
                LoadMusteriData();
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID"].Value);
                Form2 detayForm = new Form2();
                detayForm.LoadMusteriDetay(ID);
                detayForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Lütfen bir müşteri seçin.");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int siparisId = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["ID"].Value);
                Form3 detayForm = new Form3();
                detayForm.loadsıparıs(siparisId);
                detayForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Lütfen bir sipariş seçin.");
            }
        }

        private void Exportbothexcel()
        {
            var dataGrids = new List<DataGridView> { dataGridView1, dataGridView2 };
            var sheetNames = new List<string> { "Musteri", "Siparis" };

            using (var workbook = new XLWorkbook())
            {
                for (int i = 0; i < dataGrids.Count; i++)
                {
                    var dataGrid = dataGrids[i];
                    var sheet = workbook.Worksheets.Add(sheetNames[i]);

                    // Başlıkları ekle
                    for (int j = 0; j < dataGrid.Columns.Count; j++)
                    {
                        sheet.Cell(1, j + 1).Value = dataGrid.Columns[j].HeaderText;
                    }

                    // Verileri ekle
                    for (int j = 0; j < dataGrid.Rows.Count; j++)
                    {
                        if (dataGrid.Rows[j].Cells[0].Value != null) // Boş satır kontrolü
                        {
                            for (int k = 0; k < dataGrid.Columns.Count; k++)
                            {
                                var cellValue = dataGrid.Rows[j].Cells[k].Value;
                                if (cellValue != null)
                                {
                                    sheet.Cell(j + 2, k + 1).Value = cellValue.ToString();
                                }
                            }
                        }
                    }
                }

                // Excel dosyasını kaydet
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    Title = "Export to Excel"
                };
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                }
            }

        }


        public void ImportExcelToDatabase(string filePath)
        {
            // Veritabanı bağlantısını açıyoruz
            string connectionString = "Server=DESKTOP-E3R2KG9;Database=proje;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var workbook = new XLWorkbook(filePath))
                {
                    // Müşteri sayfasını okuyoruz
                    var customerSheet = workbook.Worksheet("Musteri");
                    foreach (var row in customerSheet.RowsUsed().Skip(1)) // Başlıkları atlıyoruz
                    {
                        string name = row.Cell(1).GetValue<string>();
                        string surname = row.Cell(2).GetValue<string>();
                        string phone = row.Cell(3).GetValue<string>();

                        // Müşteriyi veritabanına ekle
                        var customerCmd = new SqlCommand("INSERT INTO Musteri (Ad, Soyad, Telefon) VALUES (@Ad, @Soyad, @Telefon)", connection);
                        customerCmd.Parameters.AddWithValue("@Ad", name);
                        customerCmd.Parameters.AddWithValue("@Soyad", surname);
                        customerCmd.Parameters.AddWithValue("@Telefon", phone);
                        customerCmd.ExecuteNonQuery();
                    }

                    // Sipariş sayfasını okuyoruz
                    var orderSheet = workbook.Worksheet("Siparis");
                    foreach (var row in orderSheet.RowsUsed().Skip(1)) // Başlıkları atlıyoruz
                    {
                        int musteriID = row.Cell(1).GetValue<int>();
                        string productName = row.Cell(3).GetValue<string>();

                        // Tarih değerini Excel hücresinden alıp DateTime formatına dönüştürme
                        DateTime siparisTarihi;
                        if (DateTime.TryParse(row.Cell(2).GetValue<string>(), out siparisTarihi))
                        {
                            // Siparişi veritabanına ekle
                            var orderCmd = new SqlCommand("INSERT INTO Siparis (MusteriID, SiparisTarihi, UrunAdi) VALUES (@MusteriID, @SiparisTarihi, @UrunAdi)", connection);
                            orderCmd.Parameters.AddWithValue("@MusteriID", musteriID);
                            orderCmd.Parameters.AddWithValue("@SiparisTarihi", siparisTarihi);
                            orderCmd.Parameters.AddWithValue("@UrunAdi", productName);
                            orderCmd.ExecuteNonQuery();
                        }
                        else
                        {
                            MessageBox.Show("Geçersiz tarih formatı. Satır atlandı.");
                        }
                    }
                }
                MessageBox.Show("Excel verileri başarıyla içeri aktarıldı.");
            }
        }











        private void button11_Click(object sender, EventArgs e)
        {
            Exportbothexcel();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xlsx";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    ImportExcelToDatabase(filePath);
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Tüm müşterileri silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string connectionString = "Server=DESKTOP-E3R2KG9;Database=proje;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                   
                    SqlCommand deleteOrdersCmd = new SqlCommand("DELETE FROM Siparis", connection);
                    deleteOrdersCmd.ExecuteNonQuery();

                
                    SqlCommand deleteCustomersCmd = new SqlCommand("DELETE FROM Musteri", connection);
                    deleteCustomersCmd.ExecuteNonQuery();
                }
                MessageBox.Show("Tüm müşteriler ve siparişler başarıyla silindi.");

                // DataGridView güncelle
                LoadData();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                // Koyu tema
                this.BackColor = Color.FromArgb(45, 45, 48); // Koyu gri arka plan
                dataGridView1.BackgroundColor = Color.FromArgb(30, 30, 30); // Datagrid koyu
                label1.ForeColor = Color.White; // Metin beyaz
                                                // Diğer bileşenlerin renklerini ayarlayın
            }
            else
            {
                // Açık tema
                this.BackColor = Color.White;
                dataGridView1.BackgroundColor = Color.White;
                label1.ForeColor = Color.Black;
                // Diğer bileşenlerin renklerini ayarlayın
            }
        }

        private void RoundButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.FromArgb(100, 149, 237); // Özel bir mavi tonu
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Yuvarlak kenar ayarı için GraphicsPath ile bir elips tanımlıyoruz
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, btn.Width, btn.Height);
            btn.Region = new Region(path);
        }


        private void StyleDataGridView(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(70, 70, 70);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(235, 235, 235);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(50, 120, 200);
        }


        private void CreateCustomerCard(string customerName, string phone)
        {
            Panel card = new Panel();
            card.Size = new Size(200, 100);
            card.BackColor = Color.FromArgb(240, 248, 255);
            Label nameLabel = new Label() { Text = customerName, Location = new Point(10, 10) };
            Label phoneLabel = new Label() { Text = "Telefon: " + phone, Location = new Point(10, 40) };
            card.Controls.Add(nameLabel);
            card.Controls.Add(phoneLabel);
            flowLayoutPanel1.Controls.Add(card);
        }

        private void CustomizeForm()
        {
            
            this.BackColor = Color.FromArgb(240, 248, 255); // Açık mavi tonunda bir renk

          
            this.FormBorderStyle = FormBorderStyle.FixedSingle;  // Başlık çubuğuna sabit form
            this.ControlBox = false; // Kapatma, minimize ve büyütme butonlarını kaldır

            // Başlık için renk ve font düzenlemesi
            this.Text = "Müşteri ve Sipariş Yönetimi";  // Form başlığı
            this.Font = new Font("Segoe UI", 12, FontStyle.Bold);  // Yazı tipi ve büyüklüğü
        }




    }
}
