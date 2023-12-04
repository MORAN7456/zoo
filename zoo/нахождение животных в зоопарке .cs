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
    public partial class Form4 : Form
    {
        private readonly string Courtship = @"Data Source=DB.db;Version=3;";
        public Form4()
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
                        int id = Convert.ToInt32(row.Cells["idhabitat"].Value);

                        // Выполняем удаление записи из базы данных
                        using (SQLiteConnection connection = new SQLiteConnection(Courtship))
                        {
                            connection.Open();
                            SQLiteCommand command = new SQLiteCommand("DELETE FROM Courtship WHERE idhabitat = @idhabitat", connection);
                            command.Parameters.AddWithValue("@idhabitat", id);
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
            string query = "SELECT * FROM Courtship";
            using (SQLiteConnection connection = new SQLiteConnection(Courtship))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                DataTable Courtship = new DataTable();
                _ = adapter.Fill(Courtship);

                // добавляем столбы для datagridview, для их отображения
                dataGridView1.DataSource = Courtship;
                dataGridView1.Columns["idhabitat"].Visible = false; // Прячем эту колонку
                dataGridView1.Columns["complex"].HeaderText = "название комплекса";
                dataGridView1.Columns["namberroom"].HeaderText = "номер помещения";
                dataGridView1.Columns["water"].HeaderText = "водоем";
                dataGridView1.Columns["heating"].HeaderText = "отопление";
                dataGridView1.Columns["quantityanimals"].HeaderText = "кол-во животных в помещении";

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Создаем новую форму
            Form5 помещения = new Form5();

            // Открываем новую форму
            помещения.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Courtship";
            using (SQLiteConnection connection = new SQLiteConnection(Courtship))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                DataTable Courtship = new DataTable();
                _ = adapter.Fill(Courtship);

                // добавляем столбы для datagridview, для их отображения
                dataGridView1.DataSource = Courtship;
                dataGridView1.Columns["idhabitat"].Visible = false; // Прячем эту колонку
                dataGridView1.Columns["complex"].HeaderText = "название комплекса";
                dataGridView1.Columns["namberroom"].HeaderText = "номер помещения";
                dataGridView1.Columns["water"].HeaderText = "водоем";
                dataGridView1.Columns["heating"].HeaderText = "отопление";
                dataGridView1.Columns["quantityanimals"].HeaderText = "кол-во животных в помещении";
               
            }
        }
    }
}

