using Microsoft.SqlServer.Server;
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
using System.Xml.Linq;
using static System.Windows.Forms.DataFormats;

namespace Kurs3
{
    public partial class EditRoute : Form
    {
        int userId;
        int idadmin;
        public EditRoute(int userId)
        {
            this.userId = userId;
            InitializeComponent();
            load_data();
        }
        
        void load_data()
        {
            string connectionString = "server=127.0.0.1;port=3306;" +
            "userid=root;password=123456;" +
            "database=kurs33;sslmode=none;charset=utf8;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string queU = $"SELECT * FROM `flights` WHERE `№_flight`=@userId";
            MySqlCommand cmd = new MySqlCommand(queU, conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                //user data

                textBox1.Text = reader["route"].ToString();
                textBox2.Text = ((DateTime)reader["sending"]).ToString("yyyy-MM-dd HH:mm:ss");
                textBox3.Text = ((DateTime)reader["arrival"]).ToString("yyyy-MM-dd HH:mm:ss");
                textBox4.Text = reader["price"].ToString();

            }
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "server=127.0.0.1;port=3306;" +
            "userid=root;password=123456;" +
            "database=kurs33;sslmode=none;charset=utf8;";
            DialogResult result = MessageBox.Show("Подтвердите действие", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                MySqlConnection con = new MySqlConnection(connectionString);
                con.Open();
                string que = $"UPDATE `flights` " +
                    $"SET" +
                    $" `route` = '{textBox1.Text}' , " +
                    $" `sending` = '{textBox2.Text}' , " +
                    $" `arrival` = '{textBox3.Text}' , " +
                    $" `price` = '{textBox4.Text}' " +
                    $"  WHERE `№_flight` = '{userId}' ";
                MySqlCommand cmd = new MySqlCommand(que, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Изменено успешно", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                //WinAdmin wa = new WinAdmin(userId, idadmin);
                //wa.Show();            
            }
            else if (result == DialogResult.No)
            {
                MessageBox.Show("Изменения отменены", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            WinAdmin wa = new WinAdmin(userId, idadmin);
            wa.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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
