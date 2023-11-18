using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SQLQueryApp:
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void ExecuteQueryButton_Click(object sender, EventArgs e)
        {
            string serverIp = ServerIpTextBox.Text;
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;
            string query = QueryTextBox.Text;

            string connectionString = $"Data Source={serverIp};Initial Catalog=HC;User ID={username};Password={password};";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    // �M�ŵ��G��r��
                    ResultTextBox.Clear();

                    while (reader.Read())
                    {
                        // �v��Ū���d�ߵ��G�A����ܦb���G��r�ؤ�
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            ResultTextBox.AppendText(reader[i].ToString() + "\t");
                        }
                        ResultTextBox.AppendText(Environment.NewLine);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"�d�߰��楢�ѡG{ex.Message}", "���~", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // �p�G�ϥΪ̫��UEnter��A����d��
        private void QueryTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ExecuteQueryButton_Click(sender, e);
            }
        }
    }
}