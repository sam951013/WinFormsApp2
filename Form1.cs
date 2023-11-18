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

                    // 清空結果文字框
                    ResultTextBox.Clear();

                    while (reader.Read())
                    {
                        // 逐行讀取查詢結果，並顯示在結果文字框中
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
                MessageBox.Show($"查詢執行失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 如果使用者按下Enter鍵，執行查詢
        private void QueryTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ExecuteQueryButton_Click(sender, e);
            }
        }
    }
}