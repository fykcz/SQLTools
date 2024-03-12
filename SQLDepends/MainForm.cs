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

namespace FYK.SQLTools.SQLDepends
{
    public partial class MainForm : Form
    {
        private SqlConnection sqlConnection;

        public MainForm()
        {
            InitializeComponent();
            connectToSQL.SetConnection(new SqlConnectionStringBuilder()
            {
                DataSource = "fykbuntu.westeurope.cloudapp.azure.com",
                UserID = "sa",
                IntegratedSecurity = false,
                Password = "Pas$w0rd."
            });
        }

        private void connectToSQL_ConnectionChanged(SQLToolsCommon.ConnectionChangedEventArg newConnectionInfo)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
            sqlConnection = new SqlConnection(newConnectionInfo.ConnectionInfo.ConnectionString);
            sqlConnection.Open();
            databaseTree.LoadDatabases(sqlConnection);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private void databaseTree_ItemSelected(SQLToolsCommon.ItemSelectedEventArgs itemInfo)
        {
            label1.Text = itemInfo.ItemInfo.Text;
            label2.Text = itemInfo.ItemInfo.Database;
            label3.Text = itemInfo.ItemInfo.Schema;
            label4.Text = itemInfo.ItemInfo.Name;
        }
    }
}
