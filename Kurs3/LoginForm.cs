using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BDclass;
using Microsoft.SqlServer.Server;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kurs3
{
    public partial class LoginForm : Form
    {
        int userId;
        string SQLconnect = "server = kurs; port=3306; userid=root; password=123456; database=kurs33; sslmode=none; charset=utf8 ";
        public LoginForm(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }
        public void Enter_Passenger()
        {
            string connectionString = "server=127.0.0.1;port=3306;" +
            "userid=root;password=123456;" +
            "database=kurs33;sslmode=none;charset=utf8;";
            MySqlConnection con1 = new MySqlConnection(connectionString);
            con1.Open();
            string query = "SELECT * FROM passengers WHERE login=@login and pass=@pass";
            MySqlCommand com = new MySqlCommand(query, con1);
            com.Parameters.AddWithValue("@login", loginField.Text);
            com.Parameters.AddWithValue("@pass", passField.Text);
            MySqlDataReader Dr = com.ExecuteReader();
                if (Dr.Read())
                {
                    WinUser mn = new WinUser(Convert.ToInt32(Dr["idpassengers"]));
                    this.Hide();
                    if (mn.ShowDialog() == DialogResult.OK)
                    {
                        Enter_Passenger();
                    }
                }
            else
            {
                MessageBox.Show("Такого логина или пароля не существует: \n\n1) Проверьте правильность ввода\n\n2) Обратитесь к администартору", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Dr.Close();
        }
        private void Enter_Admin()
        {
            string connectionString = "server=127.0.0.1;port=3306;" +
            "userid=root;password=123456;" +
            "database=kurs33;sslmode=none;charset=utf8;";
            MySqlConnection con1 = new MySqlConnection(connectionString);
            con1.Open();
            string query = "SELECT * FROM admin WHERE login=@login and pass=@pass";
            MySqlCommand com = new MySqlCommand(query, con1);
            com.Parameters.AddWithValue("@login", loginField.Text);
            com.Parameters.AddWithValue("@pass", passField.Text);
            MySqlDataReader Dr = com.ExecuteReader();

                if (Dr.Read())
                {
                    WinAdmin wa = new WinAdmin(userId, Convert.ToInt32(Dr["idadmin"]));
                    this.Hide();
                    if (wa.ShowDialog() == DialogResult.OK)
                    {
                        Enter_Passenger();
                    }
                }
                else
                {
                    MessageBox.Show("Такого логина или пароля не существует: \n\n1) Проверьте правильность ввода", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            Dr.Close();
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
            if(e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top  += e.Y - lastPoint.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void RegisterLabel_Click(object sender, EventArgs e)
        {
            this.Hide();
            SignupForm signupForm = new SignupForm();
            signupForm.Show();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {

            Enter_Passenger();

            //     string connectionString = "server=127.0.0.1;port=3306;" +
            //"userid=root;password=123456;" +
            //    "database=kurs33;sslmode=none;charset=utf8;";
            //     MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            //     mySqlConnection.Open();


            //     string loginUser = loginField.Text;
            //     string passUser = passField.Text;

            //     DB bd = new DB();
            //     MySqlDataAdapter adapter = new MySqlDataAdapter();

            //     DataTable table = new DataTable();

            //     MySqlCommand command = new MySqlCommand("SELECT * FROM passengers WHERE login = @uL AND pass = @uP", mySqlConnection);
            //     command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginUser;
            //     command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = passUser;

            //     adapter.SelectCommand = command;
            //     //adapter.Fill(table);
            //     MySqlDataReader Dr = command.ExecuteReader();
            //     //int count = Convert.ToInt32(command.ExecuteScalar());

            //     while (Dr.Read())
            //         if (Dr.HasRows == true)
            //         {
            //             WinUser wu = new WinUser(Convert.ToInt32(Dr["idpassengers"]));
            //             this.Hide();
            //             if (wu.ShowDialog() == DialogResult.OK)
            //             {
            //                 Enter_System();
            //             }
            //             //wu.Show();
            //             break;
            //         }
            //         else
            //         {
            //             MessageBox.Show("Такого логина или пароля не существует: \n\n1) Проверьте правильность ввода\n\n2) Обратитесь к администартору", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //         }

            //     //try
            //     //{
            //     //    if (count > 0)
            //     //    {
            //     //        this.Hide();
            //     //        WinUser winUser = new WinUser(id);
            //     //        winUser.Show();
            //     //    }
            //     //    else
            //     //    {
            //     //        MessageBox.Show("Ошибка авторизации: неправильный логин или пароль.");
            //     //    }
            //     //}

            //     //finally
            //     //{
            //     //    // Закрытие подключения после завершения попытки авторизации
            //     //    mySqlConnection.Close();
            //     //}


        }

        private void loginField_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Enter_Admin();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

