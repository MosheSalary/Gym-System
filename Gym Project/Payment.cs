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
    public partial class Payment : Form
    {
        public Payment()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Salary\Documents\GymDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void FillName()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select Mname from MemberTbl", conn);
            SqlDataReader readata;
            readata = cmd.ExecuteReader();
            DataTable dt = new DataTable(); 
            dt.Columns.Add("Mname", typeof(string));
            dt.Load(readata);
            NameCb.ValueMember = "MName";
            NameCb.DataSource = dt;
            conn.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MainForm mainform = new MainForm();
            mainform.Show();
            this.Hide();
        }
        private void filterByName()
        {
            conn.Open();
            string query = "select * from PaymentTabl where PMember='"+SearchName.Text+"'";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);
            PaymentGridView.DataSource = ds.Tables[0];
            conn.Close();
        }
        private void populate()
        {
            conn.Open();
            string query = "select * from PaymentTabl";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);
            PaymentGridView.DataSource = ds.Tables[0];
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //NameCb.Text = "";
            AmountTb.Text = "";
            
        }

        private void Payment_Load(object sender, EventArgs e)
        {
            FillName();
            populate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(NameCb.Text == "" || AmountTb.Text == "")
            {
                MessageBox.Show("Missing Some Information");
            }
            else
            {
                string payperiode = DatePicker.Value.Month.ToString() + DatePicker.Value.Year.ToString();
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select count (*) from PaymentTabl where PMember='" + NameCb.SelectedValue.ToString() + "' and PMonth ='" + payperiode + "'", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    MessageBox.Show("Already paid for this month :)");
                }
                else
                {
                    string query = "insert into PaymentTabl values ('" + payperiode + "','" + NameCb.SelectedValue.ToString() + "','" + AmountTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Amount paid successfully :)");
                }
                conn.Close();
                populate();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            filterByName();
            SearchName.Text = string.Empty;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            populate();
        }
    }
}
