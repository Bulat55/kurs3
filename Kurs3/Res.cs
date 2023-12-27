using Microsoft.SqlServer.Server;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kurs3
{
    public partial class Res : Form
    {
        object id;
        int userId;
        public Res(object id, int userId)
        {
            InitializeComponent();
            get_data();
            this.id = id;
            this.userId = userId;
        }
        void get_data()
        {
            string connectionString = "server=127.0.0.1;port=3306;" +
            "userid=root;password=123456;" +
            "database=kurs33;sslmode=none;charset=utf8;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string query = $"SELECT * FROM `flights`  WHERE `№_flight`='{id}' ";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                label1.Text = read["route"].ToString();
                label2.Text = read["sending"].ToString();
                label3.Text = read["arrival"].ToString();
                label4.Text = read["price"].ToString();
                textBox1.Text = read["nop"].ToString();
            }
            conn.Close();
        }
        private void Res_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "server=127.0.0.1;port=3306;" +
            "userid=root;password=123456;" +
            "database=kurs33;sslmode=none;charset=utf8;";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            string query = $"SELECT * FROM  passengers";


            MySqlCommand command = new MySqlCommand("INSERT INTO tickets (route,sending,arrival,price, nop) values (@route,@sending,@arrival,@price, @nop)");
            command.Connection = mySqlConnection;
            //command.Parameters.AddWithValue("name", );
            command.Parameters.AddWithValue("route", label1.Text);
            command.Parameters.AddWithValue("sending", label2.Text);
            command.Parameters.AddWithValue("arrival", label3.Text);
            command.Parameters.AddWithValue("price", label4.Text);
            command.Parameters.AddWithValue("nop", textBox1.Text);


            command.ExecuteNonQuery();
            mySqlConnection.Close();
            MessageBox.Show("Билет успешно забронирован");
        }
    }
}
