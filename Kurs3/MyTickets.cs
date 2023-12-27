using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kurs3
{
    public partial class MyTickets : Form
    {
        int userId;
        public MyTickets()
        {
            InitializeComponent();
            load_flights();

        }
        public void load_flights()
        {
            string connectionString = "server=127.0.0.1;port=3306;" +
            "userid=root;password=123456;" +
            "database=kurs33;sslmode=none;charset=utf8;";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            string query = $"SELECT * FROM  tickets";
            MySqlCommand command = new MySqlCommand(query, mySqlConnection);

            adapter.SelectCommand = command;
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            dataGridView1.Columns[2].DefaultCellStyle.Format = "yyyy-MM-dd hh:mm:ss";
            dataGridView1.Columns[3].DefaultCellStyle.Format = "yyyy-MM-dd hh:mm:ss";
            dataGridView1.Columns[1].HeaderText = "Маршрут";
            dataGridView1.Columns[2].HeaderText = "Время отлета";
            dataGridView1.Columns[3].HeaderText = "Время прилета";
            dataGridView1.Columns[4].HeaderText = "Цена";
            dataGridView1.Columns[5].Visible = false;/*HeaderText = "Количество мест";*/
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[0].Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            WinUser wu = new WinUser(userId);
            wu.Show();
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Red;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Black;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "server=127.0.0.1;port=3306;" +
            "userid=root;password=123456;" +
            "database=kurs33;sslmode=none;charset=utf8;";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            String sqlQuery = "DELETE FROM `tickets` WHERE FALSE ";
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                sqlQuery += "OR `idtickets`=" +
                    $"'{dataGridView1.SelectedRows[i].Cells[0].Value}'";
            }
            MySqlCommand mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            MessageBox.Show("Бронь успешно удалена", "Сообщение об успешной операции", MessageBoxButtons.OK, MessageBoxIcon.Information);
            load_flights();
        }
        Point lastPoint;
        private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
