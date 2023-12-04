using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace zoo
{
    public partial class Главная : Form
    {
        private readonly string connString = @"Data Source=DB.db;Version=3;";
        public Главная()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCourtshipData();
            LoadCourtshipData1();
        }

            private void LoadCourtshipData()
            {
                string query = "SELECT * FROM Courtship";
                using (SQLiteConnection connection = new SQLiteConnection(connString))
                {
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                    DataTable courtshipTable = new DataTable();
                    _ = adapter.Fill(courtshipTable);

                    // Set up DataGridView
                    dataGridView1.DataSource = courtshipTable;
                    dataGridView1.Columns["idhabitat"].Visible = false; // Hide this column
                    dataGridView1.Columns["complex"].HeaderText = "название комплекса";
                    dataGridView1.Columns["namberroom"].HeaderText = "номер помещения";
                    dataGridView1.Columns["water"].HeaderText = "водоем";
                    dataGridView1.Columns["heating"].HeaderText = "отопление";
                    dataGridView1.Columns["quantityanimals"].HeaderText = "кол-во животных в помещении";
                }
            }
            private void LoadCourtshipData1()
            {
                string query = "SELECT * FROM Animall";
                using (SQLiteConnection connection = new SQLiteConnection(connString))
                {
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                    DataTable Animall = new DataTable();
                    _ = adapter.Fill(Animall);

                    // добавляем столбы для datagridview, для их отображения
                    dataGridView1.DataSource = Animall;
                    dataGridView1.Columns["Idanimals"].Visible = false; // Прячем эту колонку
                    dataGridView1.Columns["animal"].HeaderText = "animal";
                    dataGridView1.Columns["dailyfeeding"].HeaderText = "dailyfeeding";
                    dataGridView1.Columns["family"].HeaderText = "family";
                    dataGridView1.Columns["habitat"].HeaderText = "habitat";

                }
            }
       

        private void button1_Click(object sender, EventArgs e)
        {
            всеоживотных все_о_животных = new всеоживотных(); // Создание новой формы
            все_о_животных.Show(); // Показ новой формы
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 нахождение_животных_в_зоопарке = new Form4();

            // Открываем новую форму
            нахождение_животных_в_зоопарке.Show();
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string query = "SELECT SUM(dailyfeeding) AS суточное_потребление FROM ZooData WHERE family = 'приматы'";

            using (SQLiteConnection connection = new SQLiteConnection(connString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                       dataGridView1.DataSource = dataTable;
                    }
                }
            }
        }
           

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM ZooData WHERE animal = 'карликовый гипопотам' AND water = 'нет'";

            using (SQLiteConnection connection = new SQLiteConnection(connString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string query = "SELECT SUM(quantityanimals) AS total_population FROM ZooData WHERE family = 'псовые'";

            using (SQLiteConnection connection = new SQLiteConnection(connString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string query = "SELECT a.animal AS animal1, b.animal AS animal2 " +
                           "FROM ZooData a " +
                           "JOIN ZooData b ON a.namberroom = b.namberroom " +
                           "WHERE a.animal != b.animal";

            using (SQLiteConnection connection = new SQLiteConnection(connString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
        }
    }
    
}

