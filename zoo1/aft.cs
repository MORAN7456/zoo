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
    public partial class aft : Form
    {
        private readonly string connString = @"Data Source=DB.db;Version=3;";
        public aft()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Получаем логин и пароль из текстовых полей и удаляем пробелы в начале и конце строк
            string имя = textBox1.Text.Trim();
            string пароль = textBox2.Text.Trim();
           

            // Проверяем, что поля логина и пароля не пустые
            if (имя == "" || пароль == "")
            {
                _ = MessageBox.Show("Заполните все поля");
                return;
            }

            // Формируем запрос на проверку наличия пользователя с таким же логином в базе данных
            string checkQuery = $"SELECT COUNT(*) FROM User WHERE name='{имя}' AND pass='{пароль}'";

            // Создаем новое подключение к базе данных SQLite
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                conn.Open();

                using (SQLiteCommand checkCmd = new SQLiteCommand(checkQuery, conn))
                {
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    // Проверяем, что пользователь с таким логином уже не зарегистрирован в базе данных
                    if (count > 0)
                    {
                        _ = MessageBox.Show("Пользователь с таким логином или почтой уже зарегистрирован");
                        return;
                    }
                }

                // Формируем запрос на добавление нового пользователя в базу данных
                string insertQuery = $"INSERT INTO User (name, pass) VALUES ('{имя}', '{пароль}');";

                // Создаем новый объект команды SQL с запросом на добавление нового пользователя в базу данных
                using (SQLiteCommand insertCmd = new SQLiteCommand(insertQuery, conn))
                {
                    // Выполняем запрос на добавление нового пользователя в базу данных
                    _ = insertCmd.ExecuteNonQuery();
                }
            }

        MessageBox.Show("Регистрация прошла успешно для пользователя: " +  "Успех");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Получаем логин и пароль из текстовых полей и удаляем пробелы в начале и конце строк
            
            string имя = textBox4.Text.Trim();
            string пароль = textBox5.Text.Trim();

            // Проверяем, что поля логина и пароля не пустые
            if (имя == "" || пароль == "")
            {
                _ = MessageBox.Show("Заполните все поля");
                return;
            }

            // Формируем запрос на проверку наличия пользователя с таким же логином и паролем в базе данных
            string checkQuery = $"SELECT COUNT(*) FROM User WHERE name='{имя}' AND pass='{пароль}'";

            // Создаем новое подключение к базе данных SQLite
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                conn.Open();

                using (SQLiteCommand checkCmd = new SQLiteCommand(checkQuery, conn))
                {
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    // Проверяем, что пользователь с таким логином и паролем существует в базе данных
                    if (count == 0)
                    {
                        _ = MessageBox.Show("Неверный логин или пароль");
                        return;
                    }
                }

                // Выводим сообщение об успешной авторизации пользователя
                _ = MessageBox.Show("Вы успешно авторизовались");

                // Создаем новое окно
                Главная otherWindow = new Главная();

                // Скрываем текущее окно
                this.Hide();

                // Отображаем новое окно
                otherWindow.Show();
            }
        }
    }
}
