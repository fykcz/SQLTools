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

namespace FYK.SQLTools.SQLToolsCommon
{
    public partial class ConnectToSQL : UserControl
    {
        private SqlConnectionStringBuilder connectionInfo;

        [Description("Handler for ConnectionChanged event")]
        public delegate void ConnectionChangedHandler(ConnectionChangedEventArg newConnectionInfo);

        [Description("Fired when the connection information is changed")]
        public event ConnectionChangedHandler ConnectionChanged;

        public ConnectToSQL()
        {
            InitializeComponent();
            connectionInfo = null;
        }

        private void connect_Button_Click(object sender, EventArgs e)
        {
            using (var cf = new ConnectForm())
            {
                if (connectionInfo != null)
                {
                    cf.Server = connectionInfo.DataSource;
                    cf.SQLAuth = !connectionInfo.IntegratedSecurity;
                    if (cf.SQLAuth)
                    {
                        cf.UserName = connectionInfo.UserID;
                        cf.Password = connectionInfo.Password;
                    }
                }
                if (cf.ShowDialog() == DialogResult.Cancel) return;
                connectionInfo = new SqlConnectionStringBuilder
                {
                    DataSource = cf.Server,
                    IntegratedSecurity = !cf.SQLAuth
                };
                if (cf.SQLAuth)
                {
                    connectionInfo.UserID = cf.UserName;
                    connectionInfo.Password = "*****";
                }
                connectionString_Label.Text = connectionInfo.ConnectionString;
                if (cf.SQLAuth)
                {
                    connectionInfo.Password = cf.Password;
                }
                ConnectionChanged?.Invoke(new ConnectionChangedEventArg(connectionInfo));
            }
        }
    }
    public class ConnectionChangedEventArg : EventArgs
    {
        public SqlConnectionStringBuilder ConnectionInfo { get; private set; }
        public ConnectionChangedEventArg(SqlConnectionStringBuilder connectionStringBuilder)
        {
            ConnectionInfo = connectionStringBuilder;
        }
    }
}
