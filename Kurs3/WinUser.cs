using Microsoft.SqlServer.Server;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Kurs3
{

    public partial class WinUser : Form
    {
        MySqlConnection connection = new MySqlConnection("server=kurs;port=3306;username=root;password=123456;database=kurs33;sslmode=none;charset=utf8;");
        int userId;
        public event EventHandler DataUpdated;
        public WinUser(int userId)
        {
            InitializeComponent();
            load_Tickets();
            this.userId = userId;
            get_info();
            get_reis();
        }
        void get_reis()
        {
            string connectionString = "server=127.0.0.1;port=3306;" +
            "userid=root;password=123456;" +
            "database=kurs33;sslmode=none;charset=utf8;";
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();
            String que = $"SELECT * FROM `flights`";
            MySqlDataAdapter msda = new MySqlDataAdapter(que, con);
            DataTable table = new DataTable();
            msda.Fill(table);
            dataTickets.DataSource = table;
            dataTickets.Columns["№_flight"].Visible = false;
            con.Close();
        }
        void get_info()
        {
            string connectionString = "server=127.0.0.1;port=3306;" +
            "userid=root;password=123456;" +
            "database=kurs33;sslmode=none;charset=utf8;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            //string queU = $"SELECT * FROM `kurs33`.`passengers` WHERE `idpassengers`=`{userId}`";
            string queU = $"SELECT * FROM `passengers` WHERE `passengers`.`idpassengers`='{userId}'";
            MySqlCommand cmd = new MySqlCommand(queU, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //user data
                name.Text = reader["name"].ToString();
                textBox4.Text = reader["phone"].ToString();
                textBox1.Text = reader["login"].ToString();
                textBox2.Text = reader["pass"].ToString();
                textBox3.Text = reader["name"].ToString();

            }
            conn.Close();
        }
        public void load_Tickets()
        {
            string connectionString = "server=127.0.0.1;port=3306;" +
            "userid=root;password=123456;" +
            "database=kurs33;sslmode=none;charset=utf8;";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();
            string query = $"SELECT * FROM  flights";
            MySqlCommand command = new MySqlCommand(query, mySqlConnection);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            dataTickets.DataSource = table;
            dataTickets.Columns[2].DefaultCellStyle.Format = "yyyy-MM-dd hh:mm:ss";
            dataTickets.Columns[3].DefaultCellStyle.Format = "yyyy-MM-dd hh:mm:ss";
            dataTickets.Columns[1].HeaderText = "Маршрут";
            dataTickets.Columns[2].HeaderText = "Время отлета";
            dataTickets.Columns[3].HeaderText = "Время прилета";
            dataTickets.Columns[4].HeaderText = "Цена";
            dataTickets.Columns[0].Visible = false;
        }
        private bool updateData()
        {
            string connectionString = "server=127.0.0.1;port=3306;" +
            "userid=root;password=123456;" +
            "database=kurs33;sslmode=none;charset=utf8;";
            DialogResult result = MessageBox.Show("Вы действительно хотите изменить аккаунт?", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                MySqlConnection con = new MySqlConnection(connectionString);
                con.Open();
                string que = $"UPDATE `passengers` " +
                    $"SET" +
                    $" `name` = '{textBox3.Text}' , " +
                    $" `phone` = '{textBox4.Text}' , " +
                    $" `login` = '{textBox1.Text}' , " +
                    $" `pass` = '{textBox2.Text}' " +
                    $"  WHERE `idpassengers` = '{userId}' ";
                MySqlCommand cmd = new MySqlCommand(que, con);
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Изменено успешно\n Войдите заново ", "Успешно изменено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    LoginForm lf = new LoginForm(userId);
                    lf.Show();
                    return true;
                }
            }
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            updateData();
        }

        private void WinUser_Load(object sender, EventArgs e)
        {

        }

        private void resBut_Click(object sender, EventArgs e)
        {
            string connectionString = "server=127.0.0.1;port=3306;" +
            "userid=root;password=123456;" +
            "database=kurs33;sslmode=none;charset=utf8;";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();
            string query = $"SELECT * FROM  passengers";
            MySqlCommand command = new MySqlCommand("INSERT INTO tickets (route,sending,arrival,price) values (@route,@sending,@arrival,@price)");
            command.Connection = mySqlConnection;
            command.Parameters.AddWithValue("route", label1.Text);
            command.Parameters.AddWithValue("sending", label10.Text);
            command.Parameters.AddWithValue("arrival", label11.Text);
            command.Parameters.AddWithValue("price", label12.Text);
            command.ExecuteNonQuery();
            mySqlConnection.Close();
            MessageBox.Show("Билет успешно забронирован");
        }
        private void label8_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm(userId);
            loginForm.Show();
        }

        private void dataTickets_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            {
                label17.Text = dataTickets.Columns[1].HeaderText = "Маршрут";
                label16.Text = dataTickets.Columns[2].HeaderText = "Время отлета";
                label15.Text = dataTickets.Columns[3].HeaderText = "Время прилета";
                label14.Text = dataTickets.Columns[4].HeaderText = "Цена";

                label1.Text = dataTickets.CurrentRow.Cells[1].Value.ToString();
                label12.Text = dataTickets.CurrentRow.Cells[4].Value.ToString();

                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataTickets.Rows[e.RowIndex];
                    if (row.Cells["sending"].Value != null)
                    {
                        DateTime date = (DateTime)row.Cells["sending"].Value;
                        label10.Text = date.ToShortDateString();
                        label10.Text = date.ToString("yyyy-MM-dd hh:mm:ss");
                    }
                }
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataTickets.Rows[e.RowIndex];
                    if (row.Cells["arrival"].Value != null)
                    {
                        DateTime date = (DateTime)row.Cells["arrival"].Value;
                        label11.Text = date.ToShortDateString();
                        label11.Text = date.ToString("yyyy-MM-dd hh:mm:ss");
                    }
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            MyTickets myTickets = new MyTickets();
            myTickets.Show();
        }

        //private void search_TextChanged(object sender, EventArgs e)
        //{
        //    string connectionString = "server=127.0.0.1;port=3306;" +
        //    "userid=root;password=123456;" +
        //    "database=kurs33;sslmode=none;charset=utf8;";
        //    MySqlConnection conn = new MySqlConnection(connectionString);
        //    conn.Open();
        //    string cmdString = $"SELECT * FROM flights WHERE `{search}` LIKE '%{search.Text}%'";
        //    MySqlDataAdapter adapter = new MySqlDataAdapter(cmdString, connectionString);
        //    DataTable dataTable = new DataTable();
        //    adapter.Fill(dataTable);
        //    dataTickets.DataSource = dataTable;
        //    dataTickets.Columns["№_flight"].Visible = false;
        //    dataTickets.Columns["route"].HeaderText = "Маршрут";
        //    dataTickets.Columns["sending"].HeaderText = "Время вылета";
        //    dataTickets.Columns["arrival"].HeaderText = "Время прилета";
        //    dataTickets.Columns["price"].HeaderText = "Цена";
        //    conn.Close();
        //}

        private void label13_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataTickets_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Res pick = new Res(dataTickets.SelectedRows[0].Cells[0].Value, userId);
            //this.Hide();
            //if (pick.ShowDialog() == DialogResult.OK)
            //{
            //    load_Tickets();
            //}
            //pick.Show();
        }

        private void label13_MouseEnter(object sender, EventArgs e)
        {
            label13.ForeColor = Color.Red;
        }

        private void label13_MouseLeave(object sender, EventArgs e)
        {
            label13.ForeColor = Color.Black;
        }

        private void label8_MouseEnter(object sender, EventArgs e)
        {
            label8.ForeColor = Color.Red;
        }

        private void label8_MouseLeave(object sender, EventArgs e)
        {
            label8.ForeColor = Color.Black;
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
    }
}
