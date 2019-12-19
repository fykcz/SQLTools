namespace FYK.SQLTools.ActiveProcesses
{
    partial class ProcDetailForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcDetailForm));
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.countdownTimer = new System.Windows.Forms.Timer(this.components);
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.secNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.autoRefreshCheckBox = new System.Windows.Forms.CheckBox();
            this.detailTextBox = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.detailTabPage = new System.Windows.Forms.TabPage();
            this.locksTabPage = new System.Windows.Forms.TabPage();
            this.locksGridView = new System.Windows.Forms.DataGridView();
            this.blockedTabPage = new System.Windows.Forms.TabPage();
            this.blockedDataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.secNumericUpDown)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.detailTabPage.SuspendLayout();
            this.locksTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.locksGridView)).BeginInit();
            this.blockedTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blockedDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // refreshTimer
            // 
            this.refreshTimer.Interval = 1000;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // countdownTimer
            // 
            this.countdownTimer.Interval = 1000;
            this.countdownTimer.Tick += new System.EventHandler(this.countdownTimer_Tick);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(206, 12);
            this.progressBar.MarqueeAnimationSpeed = 1;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 17);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(176, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "sec";
            // 
            // secNumericUpDown
            // 
            this.secNumericUpDown.Location = new System.Drawing.Point(124, 11);
            this.secNumericUpDown.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.secNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.secNumericUpDown.Name = "secNumericUpDown";
            this.secNumericUpDown.Size = new System.Drawing.Size(50, 20);
            this.secNumericUpDown.TabIndex = 6;
            this.secNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // autoRefreshCheckBox
            // 
            this.autoRefreshCheckBox.AutoSize = true;
            this.autoRefreshCheckBox.Location = new System.Drawing.Point(12, 12);
            this.autoRefreshCheckBox.Name = "autoRefreshCheckBox";
            this.autoRefreshCheckBox.Size = new System.Drawing.Size(106, 17);
            this.autoRefreshCheckBox.TabIndex = 5;
            this.autoRefreshCheckBox.Text = "Auto Refresh per";
            this.autoRefreshCheckBox.UseVisualStyleBackColor = true;
            this.autoRefreshCheckBox.CheckedChanged += new System.EventHandler(this.autoRefreshCheckBox_CheckedChanged);
            // 
            // detailTextBox
            // 
            this.detailTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.detailTextBox.Location = new System.Drawing.Point(3, 3);
            this.detailTextBox.Multiline = true;
            this.detailTextBox.Name = "detailTextBox";
            this.detailTextBox.ReadOnly = true;
            this.detailTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.detailTextBox.Size = new System.Drawing.Size(964, 489);
            this.detailTextBox.TabIndex = 9;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.detailTabPage);
            this.tabControl1.Controls.Add(this.locksTabPage);
            this.tabControl1.Controls.Add(this.blockedTabPage);
            this.tabControl1.Location = new System.Drawing.Point(5, 37);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(978, 521);
            this.tabControl1.TabIndex = 10;
            // 
            // detailTabPage
            // 
            this.detailTabPage.Controls.Add(this.detailTextBox);
            this.detailTabPage.Location = new System.Drawing.Point(4, 22);
            this.detailTabPage.Name = "detailTabPage";
            this.detailTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.detailTabPage.Size = new System.Drawing.Size(970, 495);
            this.detailTabPage.TabIndex = 0;
            this.detailTabPage.Text = "Detail";
            this.detailTabPage.UseVisualStyleBackColor = true;
            // 
            // locksTabPage
            // 
            this.locksTabPage.Controls.Add(this.locksGridView);
            this.locksTabPage.Location = new System.Drawing.Point(4, 22);
            this.locksTabPage.Name = "locksTabPage";
            this.locksTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.locksTabPage.Size = new System.Drawing.Size(970, 495);
            this.locksTabPage.TabIndex = 1;
            this.locksTabPage.Text = "Locks";
            this.locksTabPage.UseVisualStyleBackColor = true;
            // 
            // locksGridView
            // 
            this.locksGridView.AllowUserToAddRows = false;
            this.locksGridView.AllowUserToDeleteRows = false;
            this.locksGridView.AllowUserToOrderColumns = true;
            this.locksGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.locksGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.locksGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.locksGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.locksGridView.Location = new System.Drawing.Point(3, 3);
            this.locksGridView.MultiSelect = false;
            this.locksGridView.Name = "locksGridView";
            this.locksGridView.ReadOnly = true;
            this.locksGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.locksGridView.ShowCellErrors = false;
            this.locksGridView.ShowEditingIcon = false;
            this.locksGridView.ShowRowErrors = false;
            this.locksGridView.Size = new System.Drawing.Size(964, 489);
            this.locksGridView.TabIndex = 4;
            // 
            // blockedTabPage
            // 
            this.blockedTabPage.Controls.Add(this.blockedDataGridView);
            this.blockedTabPage.Location = new System.Drawing.Point(4, 22);
            this.blockedTabPage.Name = "blockedTabPage";
            this.blockedTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.blockedTabPage.Size = new System.Drawing.Size(970, 495);
            this.blockedTabPage.TabIndex = 2;
            this.blockedTabPage.Text = "Blocked";
            this.blockedTabPage.UseVisualStyleBackColor = true;
            // 
            // blockedDataGridView
            // 
            this.blockedDataGridView.AllowUserToAddRows = false;
            this.blockedDataGridView.AllowUserToDeleteRows = false;
            this.blockedDataGridView.AllowUserToOrderColumns = true;
            this.blockedDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.blockedDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.blockedDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.blockedDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blockedDataGridView.Location = new System.Drawing.Point(3, 3);
            this.blockedDataGridView.MultiSelect = false;
            this.blockedDataGridView.Name = "blockedDataGridView";
            this.blockedDataGridView.ReadOnly = true;
            this.blockedDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.blockedDataGridView.ShowCellErrors = false;
            this.blockedDataGridView.ShowEditingIcon = false;
            this.blockedDataGridView.ShowRowErrors = false;
            this.blockedDataGridView.Size = new System.Drawing.Size(964, 489);
            this.blockedDataGridView.TabIndex = 5;
            // 
            // ProcDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.secNumericUpDown);
            this.Controls.Add(this.autoRefreshCheckBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "ProcDetailForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Process Detail";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.secNumericUpDown)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.detailTabPage.ResumeLayout(false);
            this.detailTabPage.PerformLayout();
            this.locksTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.locksGridView)).EndInit();
            this.blockedTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.blockedDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.Timer countdownTimer;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown secNumericUpDown;
        private System.Windows.Forms.CheckBox autoRefreshCheckBox;
        private System.Windows.Forms.TextBox detailTextBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage detailTabPage;
        private System.Windows.Forms.TabPage locksTabPage;
        private System.Windows.Forms.DataGridView locksGridView;
        private System.Windows.Forms.TabPage blockedTabPage;
        private System.Windows.Forms.DataGridView blockedDataGridView;
    }
}