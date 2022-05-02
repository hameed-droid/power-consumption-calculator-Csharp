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

namespace PowerCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.devicesTableTableAdapter.Fill(this.devicesDataSet3.DevicesTable);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'devicesDataSet3.DevicesTable' table. You can move, or remove it, as needed.
            //  this.devicesTableTableAdapter.Fill(this.devicesDataSet3.DevicesTable);
            Sum();
            Average();

        }

        public void Average()
        {
            int Sum = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                Sum += Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
            Sum = Sum / (dataGridView1.Rows.Count-1);
            tbAverage.Text = Sum + "";
        }

        public void Sum()
        {
            /*  tbSum.Text = (from DataGridViewRow row in dataGridView1.Rows
                                 where row.Cells[1].FormattedValue.ToString() != string.Empty
                                 select Convert.ToInt32(row.Cells[1].FormattedValue)).Sum().ToString();
          */
            int Sum = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                Sum += Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
            tbSum.Text = Sum+"";
         }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            SqlConnection connection;
            connection = new SqlConnection();
            connection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB;
                                            Initial Catalog=Devices;
                                            Integrated Security=true";
            SqlCommand command = new SqlCommand();
            command.Connection = connection;

                String device = tbDevice.Text;
                int power = Convert.ToInt32(tbPower.Text);
                command.CommandText = "Insert into DevicesTable(Device,Power) values('" + device + "','" + power + "');";

            
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            devicesTableTableAdapter.Fill(devicesDataSet3.DevicesTable);
            Sum();
            Average();

        }

       
      

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection connection;
            connection = new SqlConnection();
            connection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB;
                                            Initial Catalog=Devices;
                                            Integrated Security=true";
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            
            String device = tbDevice.Text;
            int power = Convert.ToInt32(tbPower.Text);
            command.CommandText = "Delete From DevicesTable Where Device='"+device+"' AND Power='"+power+"';";
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            devicesTableTableAdapter.Fill(devicesDataSet3.DevicesTable);
            Sum();
            Average();
        }
    }
}
