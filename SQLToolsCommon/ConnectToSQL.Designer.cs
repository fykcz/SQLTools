﻿
namespace FYK.SQLTools.SQLToolsCommon
{
    partial class ConnectToSQL
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.connectionString_Label = new System.Windows.Forms.Label();
            this.connect_Button = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.connectionString_Label);
            this.groupBox1.Controls.Add(this.connect_Button);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(493, 55);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection";
            // 
            // connectionString_Label
            // 
            this.connectionString_Label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.connectionString_Label.Location = new System.Drawing.Point(3, 24);
            this.connectionString_Label.Name = "connectionString_Label";
            this.connectionString_Label.Size = new System.Drawing.Size(446, 18);
            this.connectionString_Label.TabIndex = 1;
            this.connectionString_Label.Text = "Not connected ...";
            // 
            // connect_Button
            // 
            this.connect_Button.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.connect_Button.Location = new System.Drawing.Point(458, 19);
            this.connect_Button.Name = "connect_Button";
            this.connect_Button.Size = new System.Drawing.Size(29, 23);
            this.connect_Button.TabIndex = 0;
            this.connect_Button.Text = "...";
            this.connect_Button.UseVisualStyleBackColor = true;
            this.connect_Button.Click += new System.EventHandler(this.connect_Button_Click);
            // 
            // ConnectToSQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ConnectToSQL";
            this.Size = new System.Drawing.Size(493, 55);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button connect_Button;
        private System.Windows.Forms.Label connectionString_Label;
    }
}
