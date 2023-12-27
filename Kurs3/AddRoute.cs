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

namespace Kurs3
{
    public partial class AddRoute : Form
    {
        int idadmin;
        int userId;
        public AddRoute()
        {
            InitializeComponent();
          
        }

        private void AddRoute_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "server=127.0.0.1;port=3306;" +
            "userid=root;password=123456;" +
            "database=kurs33;sslmode=none;charset=utf8;";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();

            String sqlQuery = "INSERT INTO `flights`(`route`,`sending`,`arrival`,`price`)" +
                " VALUES (" +
                $" '{textBox3.Text}' , " +
                $" '{dateTimePicker1.Value.ToString("yyyy-MM-dd")}' , " +
                $" '{dateTimePicker2.Value.ToString("yyyy-MM-dd")}' , " +
                $" '{textBox4.Text}' " +
                ") ";
            MySqlCommand mySqlCommand = new MySqlCommand(sqlQuery, mySqlConnection);
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
            MessageBox.Show("Новая строчка успешно добавлена", "Сообщение об успешной операции", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;

            this.Hide();
            WinAdmin winAdmin = new WinAdmin(idadmin, userId);
            winAdmin.Show();

        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            WinAdmin adm = new WinAdmin(idadmin, userId);
            adm.Show();
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Red;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Black;
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
