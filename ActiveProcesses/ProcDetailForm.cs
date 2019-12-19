using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace FYK.SQLTools.ActiveProcesses
{
    public partial class ProcDetailForm : Form
    {
        #region Private attributes
        private int _spid;
        private SqlCommand _command;
        private SqlCommand _inputBuffer;
        private SqlCommand _locks;
        private SqlCommand _blocked;
        #endregion

        #region Ctor
        public ProcDetailForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Event handlers
        private void autoRefreshCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            secNumericUpDown.Enabled = !autoRefreshCheckBox.Checked;
            if (autoRefreshCheckBox.Checked)
            {
                RefreshData();
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
            Cursor = Cursors.WaitCursor;
            CountDownStop();
            RefreshData();
            CountDownStart();
            Cursor = Cursors.Default;
        }

        private void countdownTimer_Tick(object sender, EventArgs e)
        {
            progressBar.Value--;
        }
        #endregion
        public void LoadDetail(int spid, SqlConnection sqlConnection, int refreshInterval)
        {
            _spid = spid;
            secNumericUpDown.Value = refreshInterval;
            var cmd = string.Format(Properties.Resources.SqlStatement + "  AND [p].[spid] = {0}", _spid);
            _command = new SqlCommand(cmd, sqlConnection);
            _inputBuffer = new SqlCommand(string.Format("SELECT * FROM sys.dm_exec_input_buffer ({0}, 0)", _spid), sqlConnection);
            _locks = new SqlCommand(string.Format("SET NOCOUNT on; DECLARE @lock TABLE(spid int, dbid int, objId int, indId int, Type char(4), resource nchar(32), Mode char(8), status char(6)); Insert into @lock exec sp_lock {0}; SELECT db_name(dbid) AS[Database], object_name(objid, dbid) AS[Object], indId, [Type], Resource, Mode, Status FROM @lock AS l;", _spid), sqlConnection);
            var blocking = string.Format(Properties.Resources.SqlStatement + " AND [p].[blocked] = {0}", _spid);
            _blocked = new SqlCommand(blocking, sqlConnection);

            RefreshData();
            //autoRefreshCheckBox.Checked = true;
        }

        #region Private methods
        private string LabelForProperty(string propertyName)
        {
            int max = 20;
            var rv = propertyName + ':';
            return rv.PadRight(max);
        }

        private void RefreshData()
        {
            Cursor = Cursors.WaitCursor;
            locksGridView.SuspendLayout();
            blockedDataGridView.SuspendLayout();

            var sb = new StringBuilder();

            using (var dr = _command.ExecuteReader())
            {
                detailTextBox.Text = "";
                if (dr.Read())
                {
                    for(var i = 0; i < dr.FieldCount; i++)
                    {
                        sb.Append(LabelForProperty(dr.GetName(i))).Append(Convert.ToString(dr.GetValue(i))).AppendLine();
                    }
                }
            }

            using(var dr = _inputBuffer.ExecuteReader())
            {
                if (dr.Read())
                {
                    for (var i = 0; i < dr.FieldCount; i++)
                    {
                        sb.Append(LabelForProperty(dr.GetName(i))).Append(Convert.ToString(dr.GetValue(i))).AppendLine();
                    }
                }
            }
            detailTextBox.Text = sb.ToString();
            detailTextBox.Select(0, 0);

            var t1 = new DataTable();
            using (var da = new SqlDataAdapter(_locks))
                da.Fill(t1);
            locksGridView.DataSource = t1;

            t1 = new DataTable();
            using(var da = new SqlDataAdapter(_blocked))
                da.Fill(t1);
            blockedDataGridView.DataSource = t1;
            foreach (DataGridViewColumn c in blockedDataGridView.Columns)
                MainForm.SetToolTipText(c);
            locksGridView.ResumeLayout();
            blockedDataGridView.ResumeLayout();
            Cursor = Cursors.Default;
        }

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
        #endregion
    }
}
