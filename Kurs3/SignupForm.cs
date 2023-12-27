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
using BDclass;

namespace Kurs3
{
    public partial class SignupForm : Form
    {
        int userId;
        MySqlConnection connection = new MySqlConnection("server=127.0.0.1;port=3306;username=root;password=123456;database=kurs33;sslmode=none;charset=utf8;");
        public SignupForm()
        {
            InitializeComponent();

        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void closeButton_MouseEnter(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.Red;
        }

        private void closeButton_MouseLeave(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.Black;
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
            lastPoint = new Point(e.X, e.Y);
        }

        private void ButtonRegister_Click(object sender, EventArgs e)
        {
            if (userNameField.Text == "")
            {
                MessageBox.Show("Enter full name");
                return;
            }
            if(phoneField.Text == "")
            {
                MessageBox.Show("Enter phone");
                return;
            }

            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `passengers` (`login`, `pass`, `name`, `phone`) VALUES(@login, @pass, @name, @phone)", db.GetConnection());

            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = loginField.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = passField.Text;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = userNameField.Text;
            command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = phoneField.Text;

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("succeful");
                this.Hide();
                LoginForm loginForm = new LoginForm(userId);
                loginForm.Show();
            }
            else
                MessageBox.Show("no");

            db.closeConnection();


        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm(userId);
            loginForm.Show();
        }

        // public Boolean checkUser()
        // {
        //     string connectionString = "server=127.0.0.1;port=3306;" +
        //"userid=root;password=123456;" +
        //    "database=kurs3;sslmode=none;charset=utf8;";
        //     MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
        //     mySqlConnection.Open();

        //     DB bd = new DB();
        //     MySqlDataAdapter adapter = new MySqlDataAdapter();

        //     DataTable table = new DataTable();

        //     MySqlCommand command = new MySqlCommand("SELECT * FROM users WHERE login = @uL");
        //     command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginField.Text;

        //     adapter.SelectCommand = command;
        //     adapter.Fill(table);


        //     if (table.Rows.Count > 0)
        //     {
        //         MessageBox.Show("login zanyat");
        //         return true;
        //     }
        //     else
        //         return false;



        //}
    }
}
