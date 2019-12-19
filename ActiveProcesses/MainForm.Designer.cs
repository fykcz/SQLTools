namespace FYK.SQLTools.ActiveProcesses
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.connectionLabel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.coloringCheckBox = new System.Windows.Forms.CheckBox();
            this.refreshButton = new System.Windows.Forms.Button();
            this.filterButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.spidTextBox = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.secNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.autoRefreshCheckBox = new System.Windows.Forms.CheckBox();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.countdownTimer = new System.Windows.Forms.Timer(this.components);
            this.withSelfLockedCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.secNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.connectButton);
            this.groupBox1.Controls.Add(this.connectionLabel);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1184, 51);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection";
            // 
            // connectButton
            // 
            this.connectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.connectButton.Location = new System.Drawing.Point(1147, 16);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(25, 23);
            this.connectButton.TabIndex = 1;
            this.connectButton.Text = "...";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // connectionLabel
            // 
            this.connectionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.connectionLabel.Location = new System.Drawing.Point(12, 21);
            this.connectionLabel.Name = "connectionLabel";
            this.connectionLabel.Size = new System.Drawing.Size(1129, 18);
            this.connectionLabel.TabIndex = 0;
            this.connectionLabel.Text = "Not connected ...";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.withSelfLockedCheckBox);
            this.groupBox2.Controls.Add(this.coloringCheckBox);
            this.groupBox2.Controls.Add(this.refreshButton);
            this.groupBox2.Controls.Add(this.filterButton);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.spidTextBox);
            this.groupBox2.Controls.Add(this.progressBar);
            this.groupBox2.Controls.Add(this.dataGridView);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.secNumericUpDown);
            this.groupBox2.Controls.Add(this.autoRefreshCheckBox);
            this.groupBox2.Location = new System.Drawing.Point(0, 57);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1184, 506);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Processes";
            // 
            // coloringCheckBox
            // 
            this.coloringCheckBox.AutoSize = true;
            this.coloringCheckBox.Location = new System.Drawing.Point(346, 20);
            this.coloringCheckBox.Name = "coloringCheckBox";
            this.coloringCheckBox.Size = new System.Drawing.Size(133, 17);
            this.coloringCheckBox.TabIndex = 9;
            this.coloringCheckBox.Text = "Highlight blocked rows";
            this.coloringCheckBox.UseVisualStyleBackColor = true;
            // 
            // refreshButton
            // 
            this.refreshButton.Image = global::FYK.SQLTools.ActiveProcesses.Properties.Resources.arrow_refresh;
            this.refreshButton.Location = new System.Drawing.Point(307, 17);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(23, 23);
            this.refreshButton.TabIndex = 8;
            this.refreshButton.UseVisualStyleBackColor = true;
            // 
            // filterButton
            // 
            this.filterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.filterButton.Enabled = false;
            this.filterButton.Location = new System.Drawing.Point(1096, 17);
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(75, 23);
            this.filterButton.TabIndex = 7;
            this.filterButton.Text = "Filter";
            this.filterButton.UseVisualStyleBackColor = true;
            this.filterButton.Click += new System.EventHandler(this.filterButton_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(993, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "SPID:";
            // 
            // spidTextBox
            // 
            this.spidTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.spidTextBox.Enabled = false;
            this.spidTextBox.Location = new System.Drawing.Point(1034, 19);
            this.spidTextBox.Name = "spidTextBox";
            this.spidTextBox.Size = new System.Drawing.Size(56, 20);
            this.spidTextBox.TabIndex = 5;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(201, 20);
            this.progressBar.MarqueeAnimationSpeed = 1;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 17);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 4;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToOrderColumns = true;
            this.dataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(15, 46);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.ShowCellErrors = false;
            this.dataGridView.ShowEditingIcon = false;
            this.dataGridView.ShowRowErrors = false;
            this.dataGridView.Size = new System.Drawing.Size(1157, 446);
            this.dataGridView.TabIndex = 3;
            this.dataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView_CellFormatting);
            this.dataGridView.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseDoubleClick);
            this.dataGridView.Sorted += new System.EventHandler(this.dataGridView_Sorted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(171, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "sec";
            // 
            // secNumericUpDown
            // 
            this.secNumericUpDown.Enabled = false;
            this.secNumericUpDown.Location = new System.Drawing.Point(119, 19);
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
            this.secNumericUpDown.TabIndex = 1;
            this.secNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // autoRefreshCheckBox
            // 
            this.autoRefreshCheckBox.AutoSize = true;
            this.autoRefreshCheckBox.Enabled = false;
            this.autoRefreshCheckBox.Location = new System.Drawing.Point(7, 20);
            this.autoRefreshCheckBox.Name = "autoRefreshCheckBox";
            this.autoRefreshCheckBox.Size = new System.Drawing.Size(106, 17);
            this.autoRefreshCheckBox.TabIndex = 0;
            this.autoRefreshCheckBox.Text = "Auto Refresh per";
            this.autoRefreshCheckBox.UseVisualStyleBackColor = true;
            this.autoRefreshCheckBox.CheckedChanged += new System.EventHandler(this.autoRefreshCheckBox_CheckedChanged);
            // 
            // refreshTimer
            // 
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // countdownTimer
            // 
            this.countdownTimer.Interval = 1000;
            this.countdownTimer.Tick += new System.EventHandler(this.countdownTimer_Tick);
            // 
            // withSelfLockedCheckBox
            // 
            this.withSelfLockedCheckBox.AutoSize = true;
            this.withSelfLockedCheckBox.Location = new System.Drawing.Point(485, 20);
            this.withSelfLockedCheckBox.Name = "withSelfLockedCheckBox";
            this.withSelfLockedCheckBox.Size = new System.Drawing.Size(102, 17);
            this.withSelfLockedCheckBox.TabIndex = 10;
            this.withSelfLockedCheckBox.Text = "With self-locked";
            this.withSelfLockedCheckBox.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 561);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 450);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Active Processes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.secNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Label connectionLabel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox autoRefreshCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown secNumericUpDown;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Timer countdownTimer;
        private System.Windows.Forms.Button filterButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox spidTextBox;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.CheckBox coloringCheckBox;
        private System.Windows.Forms.CheckBox withSelfLockedCheckBox;
    }
}