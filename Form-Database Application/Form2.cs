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
    public partial class Form2 : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["VeritabaniBaglantisi"].ConnectionString;
        public Form2()
        {
            InitializeComponent();
        }

        public void LoadMusteriDetay(int ID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT AD, Soyad, Telefon FROM Musteri WHERE ID = @ID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID", ID);

                    SqlDataReader reader = command.ExecuteReader();
                    listBox1.Items.Clear();
                    if (reader.Read())
                    {
                        // Müşteri bilgilerini ListBox'a ekleme
                        listBox1.Items.Add("Müşteri Adı: " + reader["AD"].ToString());
                        listBox1.Items.Add("Müşteri Soyadı: " + reader["Soyad"].ToString());
                        listBox1.Items.Add("Telefon: " + reader["Telefon"].ToString());
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Müşteri detayları yüklenirken bir hata oluştu: " + ex.Message);
                }
            }



        }
    }
}
