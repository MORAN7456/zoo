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
    public partial class всеоживотных : Form
    {
        private readonly string connString = @"Data Source=DB.db;Version=3;";
        public всеоживотных()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранные записи?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        int id = Convert.ToInt32(row.Cells["Idanimals"].Value);

                        // Выполняем удаление записи из базы данных
                        using (SQLiteConnection connection = new SQLiteConnection(connString))
                        {
                            connection.Open();
                            SQLiteCommand command = new SQLiteCommand("DELETE FROM Animall WHERE Idanimals = @Idanimals", connection);
                            command.Parameters.AddWithValue("@Idanimals", id);
                            command.ExecuteNonQuery();
                        }

                        // Удаляем выбранную строку из DataGridView
                        dataGridView1.Rows.Remove(row);
                    }

                    MessageBox.Show("Выбранные записи успешно удалены.", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Выделите записи, которые хотите удалить.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Создаем новую форму
            животные животные = new животные();

            // Открываем новую форму
            животные.Show();
        }

        private void всеоживотных_Load(object sender, EventArgs e)
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
    }
}
