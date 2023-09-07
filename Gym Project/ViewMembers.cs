using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Gym_Project
{
    public partial class ViewMembers : Form
    {
        public ViewMembers()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Salary\Documents\GymDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void filterByName()
        {
            conn.Open();
            string query = "select * from MemberTbl where MName='" + SearchMember.Text + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);
            MemberGridView.DataSource = ds.Tables[0];
            conn.Close();
        }
        private void populate()
        {
            conn.Open();
            string query = "select * from MemberTbl";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query,conn);
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder();
            var dataset = new DataSet();
            sqlDataAdapter.Fill(dataset);
            MemberGridView.DataSource = dataset.Tables[0];
            conn.Close();
        }
        private void ViewMembers_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            filterByName();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            populate();
        }
    }
}
