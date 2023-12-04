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
    public partial class Form5 : Form
    {
        private readonly string connString = @"Data Source=DB.db;Version=3;";
        public Form5()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string название_комплекса = comboBox1.Text.Trim();
            string номер_помещения = comboBox2.Text.Trim();
            string водоем = comboBox3.SelectedItem?.ToString().Trim();
            string отопление = comboBox4.Text.Trim();
            string колво_животных_в_помещений = comboBox5.Text.Trim();
           

            // Проверяем, что поля логина и пароля не пустые
            if (название_комплекса == "" || номер_помещения == "" || водоем == "" || отопление == "" || колво_животных_в_помещений == "")
            {
                _ = MessageBox.Show("Заполните все поля");
                return;
            }

            // Добавляем сообщение с подтверждением операции
            DialogResult confirmationResult = MessageBox.Show("Вы точно хотите сохранить местоположение?", "Подтверждение", MessageBoxButtons.YesNo);
            if (confirmationResult == DialogResult.No)
            {
                return;
            }

            // Создаем новое подключение к базе данных SQLite
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                conn.Open();
                string insertQuery = $"INSERT INTO Courtship (complex,namberroom,water,heating,quantityanimals) VALUES ('{название_комплекса}', '{номер_помещения}', '{водоем}', '{отопление}' , '{колво_животных_в_помещений}');";
                using (SQLiteCommand insertCmd = new SQLiteCommand(insertQuery, conn))
                {
                    // Выполняем запрос на добавление нового пользователя в базу данных
                    _ = insertCmd.ExecuteNonQuery();
                }
            }

            // Выводим окно с подтверждением операции
            DialogResult result = MessageBox.Show("местоположение успешно занесено в базу данных. Хотите выполнить еще одну операцию?", "Подтверждение", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // Очистить поля ввода для выполнения новой операции
                
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
                comboBox3.SelectedIndex = -1;
                comboBox4.SelectedIndex = -1;
                comboBox5.SelectedIndex = -1;
            }
        }

    }
}

