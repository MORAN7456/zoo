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
    public partial class животные : Form
    {
        private readonly string connString = @"Data Source=DB.db;Version=3;";
        public животные()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string название_вида_животного = textBox1.Text.Trim();
            string суточное_потребление_корма = textBox2.Text.Trim();
            string семейство = textBox3.Text.Trim();
            string континент_обитания = textBox4.Text.Trim();
           

            // Проверяем, что поля логина и пароля не пустые
            if (название_вида_животного == "" || суточное_потребление_корма == "" || семейство == "" || континент_обитания == "")
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
                string insertQuery = $"INSERT INTO Animall (animal,dailyfeeding,family,habitat) VALUES ('{название_вида_животного}', '{суточное_потребление_корма}', '{семейство}', '{континент_обитания}');";
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
                textBox2.Text = "";
                textBox1.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
               
            }
        }

    }
}