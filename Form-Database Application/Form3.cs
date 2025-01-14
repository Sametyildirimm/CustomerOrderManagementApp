using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Form_Database_Application
{
    public partial class Form3 : Form
    {

        private string connectionString = ConfigurationManager.ConnectionStrings["VeritabaniBaglantisi"].ConnectionString;
        public Form3()
        {
            InitializeComponent();
        }

        public void loadsıparıs(int siparisId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT SiparisTarihi, UrunAdi FROM Siparis WHERE ID = @SiparisID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@SiparisID", siparisId);

                    SqlDataReader reader = command.ExecuteReader();
                    listBox1.Items.Clear();  // Eski verileri temizleyin
                    if (reader.Read())
                    {
                        // Sipariş bilgilerini ListBox'a ekleme
                        listBox1.Items.Add("Sipariş Tarihi: " + reader["SiparisTarihi"].ToString());
                        listBox1.Items.Add("Ürün Adı: " + reader["UrunAdi"].ToString());
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sipariş detayları yüklenirken bir hata oluştu: " + ex.Message);
                }
            }
        }

    }
}
