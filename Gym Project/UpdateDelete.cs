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

namespace Gym_Project
{
    public partial class UpdateDelete : Form
    {
        public UpdateDelete()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Salary\Documents\GymDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void populate()
        {
            conn.Open();
            string query = "select * from MemberTbl";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, conn);
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder();
            var dataset = new DataSet();
            sqlDataAdapter.Fill(dataset);
            MemberGridView.DataSource = dataset.Tables[0];
            conn.Close();
        }

        private void UpdateDelete_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
        int key = 0;
        private void MemberGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow selectedRow = MemberGridView.Rows[e.RowIndex];

                if (selectedRow != null)
                {
                    // Access cell values with null checks
                    key = Convert.ToInt32(selectedRow.Cells["MName"].Value.ToString());
                    NameTb.Text = selectedRow.Cells["MName"].Value.ToString();
                    PhoneTb.Text = selectedRow.Cells["MPhone"].Value.ToString();
                    GenderCb.Text = selectedRow.Cells["MGen"].Value.ToString();
                    AgeTb.Text = selectedRow.Cells["MAge"].Value.ToString();
                    AmountTb.Text = selectedRow.Cells["MAmount"].Value.ToString();
                    ScheduleCb.Text = selectedRow.Cells["MSchedule"].Value.ToString();
                }
                else
                {
                    MessageBox.Show("Try again :)");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddMember addMember = new AddMember();
            addMember.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(key == 0)
            {
                MessageBox.Show("Select The Member To Be Deleted");
            }
            else
            {
                try
                {
                    conn.Open();
                    string query = "delete from MemberTbl where MId = " + key + ";";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Member Deleted Successfully");
                    conn.Close();
                    populate();
                }catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
