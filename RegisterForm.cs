using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExampleSqlApp
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();

            UserNameField.Text = "Введите имя";
            UserNameField.ForeColor = Color.Gray;

            UserSurnameField.Text = "Введите фамилию";
            UserSurnameField.ForeColor = Color.Gray;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        Point lastPoint;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point();
        }

        private void UserNameField_Enter(object sender, EventArgs e)
        {
            if (UserNameField.Text == "Введите имя")
            {
                UserNameField.Text = "";
                UserNameField.ForeColor = Color.Black;
            }
        }

        private void UserNameField_Leave(object sender, EventArgs e)
        {
            if (UserNameField.Text == "")
            {
                UserNameField.Text = "Введите имя";
                UserNameField.ForeColor = Color.Gray;
            }
        }

        private void UserSurnameField_Enter(object sender, EventArgs e)
        {
            if (UserSurnameField.Text == "Введите фамилию")
            {
                UserSurnameField.Text = "";
                UserSurnameField.ForeColor = Color.Black;
            }

        }

        private void UserSurnameField_Leave(object sender, EventArgs e)
        {
            if (UserSurnameField.Text == "")
            {
                UserSurnameField.Text = "Введите фамилию";
                UserSurnameField.ForeColor = Color.Gray;
            }
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if (UserNameField.Text == "Введите имя")
            {
                MessageBox.Show("Введите имя");
                return;
            }
            if (UserSurnameField.Text == "Введите фамилию")
            {
                MessageBox.Show("Введите имя");
                return;
            }

            if (isUserExists())
                return;

                DB dB = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`login`, `pass`, `name`, `surname`) VALUES (@login, @pass, @name, @surname)", dB.getConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = LoginField.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = PassField.Text;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = UserNameField.Text;
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = UserSurnameField.Text;

            dB.openConnection();

            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("Аккаунт был создан");
            else
                MessageBox.Show("Аккаунт не был создан");


            dB.closeConnection();
        }

        public Boolean isUserExists()
        {
            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE 'login' = @ul", db.getConnection());
            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = LoginField.Text;


            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой логин уже создан, введите другой логин");
                return true;
            }
               
           else
            return false;
        }

        private void registerlabel_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }
    }
}      
    

