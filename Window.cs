using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;


namespace DataTable
{
    public partial class Window : Form
    {

        private SqlDataReader reader;
        private System.Data.DataTable table;
        private SqlConnection connection;

        public Window()
        {
            InitializeComponent();
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
        }

        private void ShowClick(object sender, EventArgs e)
        {
            try
            {
                SqlCommand command = new SqlCommand(textCommand.Text, connection);
                dataGridView.DataSource = null;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                System.Data.DataTable table = new System.Data.DataTable();
                int line = 0;

                while (reader.Read())
                {
                    if (line == 0)
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            table.Columns.Add(reader.GetName(i));
                        }

                        line++;
                    }

                    DataRow row = table.NewRow();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row[i] = reader[i];
                    }

                    table.Rows.Add(row);
                }

                dataGridView.DataSource = table;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Wrong request syntax");
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }

                if (reader != null)
                {
                    reader.Close();
                }
            }
            
        }
    }
}
