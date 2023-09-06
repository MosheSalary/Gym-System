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
    public partial class AddMember : Form
    {
        public AddMember()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Salary\Documents\GymDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void AddMember_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (NameTb.Text == "" || PhoneTb.Text == "" || AmountTb.Text == "" || AgeTb.Text == "")
            {
                MessageBox.Show("Missing Some Information");
            }
            else
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO MemberTbl (MName, MPhone, MGen, MAge, MAmount, MSchedule) VALUES (@Name, @Phone, @Gender, @Age, @Amount, @Schedule)";
                    SqlCommand sqlCommand = new SqlCommand(query, conn);
                    sqlCommand.Parameters.AddWithValue("@Name", NameTb.Text);
                    sqlCommand.Parameters.AddWithValue("@Phone", PhoneTb.Text);
                    sqlCommand.Parameters.AddWithValue("@Gender", GenderCb.SelectedItem.ToString());
                    sqlCommand.Parameters.AddWithValue("@Age", AgeTb.Text);
                    sqlCommand.Parameters.AddWithValue("@Amount", AmountTb.Text);
                    sqlCommand.Parameters.AddWithValue("@Schedule", ScheduleCb.SelectedItem.ToString());

                    sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Member Successfully Added :)");
                    NameTb.Text = "";
                    PhoneTb.Text = "";
                    AmountTb.Text = "";
                    AgeTb.Text = "";
                    GenderCb.Text = string.Empty;
                    ScheduleCb.Text = string.Empty;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NameTb.Text = "";
            PhoneTb.Text = "";
            AmountTb.Text = "";
            AgeTb.Text = "";
            GenderCb.Text = string.Empty;
            ScheduleCb.Text = string.Empty;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}