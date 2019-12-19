using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using FYK.SQLTools.SQLToolsCommon;

namespace FYK.SQLTools.ActiveProcesses
{
    public partial class MainForm : Form
    {
        #region Private attributes
        private SqlConnectionStringBuilder _connectionInfo;
        private SqlConnection _connection;
        private bool _preFill;
        DataGridViewColumn _sortedColumn;
        System.Windows.Forms.SortOrder _sortedDirection;
        #endregion

        #region Ctors
        public MainForm()
        {
            InitializeComponent();
            _connectionInfo = null;
        }
        #endregion

        #region Event handlers
        private void connectButton_Click(object sender, EventArgs e)
        {
            using (var cf = new ConnectForm())
            {
                if (_connectionInfo != null)
                {
                    cf.Server = _connectionInfo.DataSource;
                    cf.SQLAuth = !_connectionInfo.IntegratedSecurity;
                    if (cf.SQLAuth)
                    {
                        cf.UserName = _connectionInfo.UserID;
                        cf.Password = _connectionInfo.Password;
                    }
                }
                if (cf.ShowDialog() == DialogResult.Cancel) return;
                _connectionInfo = new SqlConnectionStringBuilder
                {
                    DataSource = cf.Server,
                    IntegratedSecurity = !cf.SQLAuth
                };
                if (cf.SQLAuth)
                {
                    _connectionInfo.UserID = cf.UserName;
                    _connectionInfo.Password = "*****";
                }
                connectionLabel.Text = _connectionInfo.ConnectionString;
                if (cf.SQLAuth)
                {
                    _connectionInfo.Password = cf.Password;
                }

                if (_connection != null && _connection.State == ConnectionState.Open)
                    _connection.Close();
                _connection = new SqlConnection(_connectionInfo.ConnectionString);
                _connection.Open();

                autoRefreshCheckBox.Enabled = secNumericUpDown.Enabled = true;
            }
        }

        private void autoRefreshCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            spidTextBox.Enabled = filterButton.Enabled = true;
            secNumericUpDown.Enabled = !autoRefreshCheckBox.Checked;
            connectButton.Enabled = !autoRefreshCheckBox.Checked;
            if (autoRefreshCheckBox.Checked)
            {
                refreshTimer.Interval = (int)secNumericUpDown.Value * 1000;
                RefreshStart();
            }
            else
            {
                RefreshStop();
            }
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            CountDownStop();
            int spid = -1;
            if (dataGridView.SelectedRows.Count > 0)
                spid = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
            int frIndex = dataGridView.FirstDisplayedScrollingRowIndex;
            Cursor = Cursors.WaitCursor;
            dataGridView.SuspendLayout();
            var cmd = Properties.Resources.SqlStatement;
            if (spidTextBox.Text.Length > 0)
            {
                cmd += "  AND [p].[spid] = " + spidTextBox.Text;
                frIndex = 0;
            }
            if (!withSelfLockedCheckBox.Checked)
            {
                cmd += " AND (p.blocked = 0 OR p.spid <> p.blocked)";
            }
            var scmd = new SqlCommand(cmd, _connection);
            var t1 = new DataTable();
            using (var da = new SqlDataAdapter(scmd))
                da.Fill(t1);
            dataGridView.DataSource = t1;
            foreach (DataGridViewColumn c in dataGridView.Columns)
                SetToolTipText(c);
            if (_sortedColumn == null)
                dataGridView.Sort(dataGridView.Columns[1], ListSortDirection.Descending);
            if (_sortedColumn != null)
            {
                dataGridView.Sort(dataGridView.Columns[_sortedColumn.Name], (_sortedDirection == System.Windows.Forms.SortOrder.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending);
            }
            if (spidTextBox.Text.Length == 0 && frIndex > -1)
                dataGridView.FirstDisplayedScrollingRowIndex = frIndex;

            if (spid != -1)
            {
                dataGridView.ClearSelection();
                foreach (DataGridViewRow r in dataGridView.Rows)
                    if (Convert.ToInt32(r.Cells[0].Value).Equals(spid))
                    {
                        r.Selected = true;
                    }
            }
            dataGridView.ResumeLayout();
            CountDownStart();
            Cursor = Cursors.Default;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            refreshTimer.Stop();
            if (_connection != null) _connection.Close();
        }

        private void countdownTimer_Tick(object sender, EventArgs e)
        {
            progressBar.Value--;
        }

        private void dataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1) return;
            //autoRefreshCheckBox.Checked = false;
            var spid = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells[0].Value);
            using (var pdf = new ProcDetailForm())
            {
                pdf.LoadDetail(spid, _connection, Convert.ToInt32(secNumericUpDown.Value));
                pdf.ShowDialog();
            }
        }

        private void filterButton_Click(object sender, EventArgs e)
        {
            RefreshStop();
            autoRefreshCheckBox.Checked = true;
            RefreshStart();
        }

        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (!coloringCheckBox.Checked) return;
            DataGridViewRow row = dataGridView.Rows[e.RowIndex];
            if (Convert.ToInt32(row.Cells[1].Value) > 0)
                row.DefaultCellStyle.BackColor = Color.OrangeRed;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (_preFill) connectButton.PerformClick();
        }

        private void dataGridView_Sorted(object sender, EventArgs e)
        {
            if (dataGridView.SortedColumn != null)
            {
                _sortedColumn = dataGridView.SortedColumn;
                _sortedDirection = dataGridView.SortOrder;
            }
        }
        #endregion

        #region Private methods
        private void RefreshStart()
        {
            progressBar.Minimum = 0;
            progressBar.Maximum = (int)secNumericUpDown.Value;
            progressBar.Value = (int)secNumericUpDown.Value;
            refreshTimer_Tick(null, null);
            refreshTimer.Start();
        }

        private void RefreshStop()
        {
            refreshTimer.Stop();
            CountDownStop();
        }

        private void CountDownStart()
        {
            progressBar.Value = (int)secNumericUpDown.Value;
            countdownTimer.Start();
        }

        private void CountDownStop()
        {
            countdownTimer.Stop();
            progressBar.Value = 0;
        }

        public static void SetToolTipText(DataGridViewColumn column)
        {
            switch (column.Name)
            {
                case "spid":
                    column.ToolTipText = "SQL Server session ID";
                    break;
                case "kpid":
                    column.ToolTipText = "Windows thread ID";
                    break;
                case "blocked":
                    column.ToolTipText = "ID of the session that is blocking the request";
                    break;
                case "waittime":
                    column.ToolTipText = "Current wait time in milliseconds";
                    break;
                case "lastwaittype":
                    column.ToolTipText = "A string indicating the name of the last or current wait type";
                    break;
                case "waitresource":
                    column.ToolTipText = "Textual representation of a lock resource";
                    break;
                case "dbid":
                    column.ToolTipText = "ID of the database currently being used by the process";
                    break;
                case "uid":
                    column.ToolTipText = "ID of the user that executed the command";
                    break;
                case "cpu":
                    column.ToolTipText = "Cumulative CPU time for the process";
                    break;
                case "physical_io":
                    column.ToolTipText = "Cumulative disk reads and writes for the process";
                    break;
                case "memusage":
                    column.ToolTipText = "Number of pages in the procedure cache that are currently allocated to this process";
                    break;
                case "login_time":
                    column.ToolTipText = "Time at which a client process logged into the server";
                    break;
                case "last_batch":
                    column.ToolTipText = "Last time a client process executed a remote stored procedure call or an EXECUTE statement";
                    break;
                case "ecid":
                    column.ToolTipText = "Execution context ID used to uniquely identify the subthreads operating on behalf of a single process";
                    break;
                case "open_tran":
                    column.ToolTipText = "Number of open transactions for the process";
                    break;
                case "status":
                    column.ToolTipText = "Process ID status:\n" +
                        "dormant = SQL Server is resetting the session;\n" +
                        "running = The session is running one or more batches;\n" +
                        "background = The session is running a background task, such as deadlock detection\n" +
                        "rollback = The session has a transaction rollback in process\n" +
                        "pending = The session is waiting for a worker thread to become available\n" +
                        "runnable = The task in the session is in the runnable queue of a scheduler while waiting to get a time quantum\n" +
                        "spinloop = The task in the session is waiting for a spinlock to become free\n" +
                        "suspended = The session is waiting for an event, such as I/O, to complete";
                    break;
/*
                case "sid":
                    column.ToolTipText = "Globally unique identifier (GUID) for the user";
                    break;
*/
                case "hostname":
                    column.ToolTipText = "Name of the workstation";
                    break;
                case "program_name":
                    column.ToolTipText = "Name of the application program";
                    break;
                case "hostprocess":
                    column.ToolTipText = "Workstation process ID number";
                    break;
                case "cmd":
                    column.ToolTipText = "Command currently being executed";
                    break;
                case "nt_domain":
                    column.ToolTipText = "Windows domain for the client, if using Windows Authentication, or a trusted connection";
                    break;
                case "nt_username":
                    column.ToolTipText = "Windows user name for the process, if using Windows Authentication, or a trusted connection";
                    break;
                case "net_address":
                    column.ToolTipText = "Assigned unique identifier for the network adapter on the workstation of each user";
                    break;
                case "net_library":
                    column.ToolTipText = "Column in which the client's network library is stored";
                    break;
                case "loginame":
                    column.ToolTipText = "Login name";
                    break;
                case "stmt_start":
                    column.ToolTipText = "Starting offset of the current SQL statement for the specified sql_handle";
                    break;
                case "stmt_end":
                    column.ToolTipText = "Ending offset of the current SQL statement for the specified sql_handle";
                    break;
                case "database":
                    column.ToolTipText = "Name of the database currently being used by the process";
                    break;

            }
        }
        #endregion
    }
}
