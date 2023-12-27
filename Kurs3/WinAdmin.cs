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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Kurs3
{
    public partial class WinAdmin : Form
    {
        int userId;
        int idadmin;

        public WinAdmin(int idadmin, int userId)
        {
            InitializeComponent();
            load_flights();
            this.idadmin = idadmin;
            this.userId = userId;
            get_info();
        }

        void get_info()
        {
            string connectionString = "server=127.0.0.1;port=3306;" +
            "userid=root;password=123456;" +
            "database=kurs33;sslmode=none;charset=utf8;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            //string queU = $"SELECT * FROM `kurs33`.`passengers` WHERE `idpassengers`=`{userId}`";
            string queU = $"SELECT * FROM `admin` WHERE `admin`.`idadmin`='{idadmin}'";
            MySqlCommand cmd = new MySqlCommand(queU, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //user data
                label1.Text = reader["name"].ToString();
            }
            conn.Close();
        }

        string search = "route";
        private void load_flights()
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
            dataAdmin.DataSource = table;
            dataAdmin.Columns[2].DefaultCellStyle.Format = "yyyy-MM-dd hh:mm:ss";
            dataAdmin.Columns[3].DefaultCellStyle.Format = "yyyy-MM-dd hh:mm:ss";
            dataAdmin.Columns[1].HeaderText = "Маршрут";
            dataAdmin.Columns[2].HeaderText = "Время вылета";
            dataAdmin.Columns[3].HeaderText = "Время прилета";
            dataAdmin.Columns[4].HeaderText = "Цена";
            dataAdmin.Columns[0].Visible = false;
        }

        //void get_data()
        //{
        //    string connectionString = "server=127.0.0.1;port=3306;" +
        //    "userid=root;password=123456;" +
        //    "database=kurs33;sslmode=none;charset=utf8;";
        //    MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
        //    mySqlConnection.Open();
        //    String sqlQuery = "SELECT `№_flight`," +
        //        "`sending`," +
        //        "`arrival`," +
        //        "`route`," +
        //        "`price` FROM `flights`";
        //    MySqlDataAdapter mySqlDataAdapter =
        //        new MySqlDataAdapter(sqlQuery, mySqlConnection);
        //    DataTable dataTable = new DataTable();
        //    mySqlDataAdapter.Fill(dataTable);
        //    dataAdmin.DataSource = dataTable;
        //    dataAdmin.Columns["№_flight"].Visible = false;
        //    mySqlConnection.Close();
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddRoute addRoute = new AddRoute();
            addRoute.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            EditRoute er = new EditRoute(userId);
            er.Show();
            //string connectionString = "server=127.0.0.1;port=3306;" +
            //"userid=root;password=123456;" +
            //"database=kurs33;sslmode=none;charset=utf8;";
            //MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            //mySqlConnection.Open();
            //String sqlQuery = "DELETE FROM `flights` WHERE FALSE ";
            //for (int i = 0; i < dataAdmin.SelectedRows.Count; i++)
            //{
            //    sqlQuery += "OR `№_flight`=" +
            //        $"'{dataAdmin.SelectedRows[i].Cells[0].Value}'";
            //}
            //MySqlCommand mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);
            //mySqlCommand.ExecuteNonQuery();
            //mySqlConnection.Close();
            //MessageBox.Show("Рейс успешно удален", "Сообщение об успешной операции", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //get_data();
        }

        private void dataAdmin_DoubleClick(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm(idadmin);
            loginForm.Show();
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            string connectionString = "server=127.0.0.1;port=3306;" +
            "userid=root;password=123456;" +
            "database=kurs33;sslmode=none;charset=utf8;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string cmdString = $"SELECT * FROM flights WHERE `{search}` LIKE '%{searchTextBox.Text}%'";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdString, connectionString);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataAdmin.DataSource = dataTable;
            dataAdmin.Columns["№_flight"].Visible = false;
            dataAdmin.Columns["route"].HeaderText = "Маршрут";
            dataAdmin.Columns["sending"].HeaderText = "Время вылета";
            dataAdmin.Columns["arrival"].HeaderText = "Время прилета";
            dataAdmin.Columns["price"].HeaderText = "Цена";
            conn.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Red;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Black;
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Red;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Black;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = "server=127.0.0.1;port=3306;" +
            "userid=root;password=123456;" +
            "database=kurs33;sslmode=none;charset=utf8;";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            String sqlQuery = "DELETE FROM `flights` WHERE FALSE ";
            for (int i = 0; i < dataAdmin.SelectedRows.Count; i++)
            {
                sqlQuery += "OR `№_flight`=" +
                    $"'{dataAdmin.SelectedRows[i].Cells[0].Value}'";
            }
            MySqlCommand mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            MessageBox.Show("Рейс успешно удален", "Сообщение об успешной операции", MessageBoxButtons.OK, MessageBoxIcon.Information);
            load_flights();
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
