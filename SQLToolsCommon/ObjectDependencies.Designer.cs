
namespace FYK.SQLTools.SQLToolsCommon
{
    partial class ObjectDependencies
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObjectDependencies));
            this.dependenicesListView = new System.Windows.Forms.ListView();
            this.directionColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.typeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.schemaColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.dependColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // dependenicesListView
            // 
            this.dependenicesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.directionColumnHeader,
            this.typeColumnHeader,
            this.schemaColumnHeader,
            this.nameColumnHeader,
            this.dependColumnHeader});
            this.dependenicesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dependenicesListView.HideSelection = false;
            this.dependenicesListView.Location = new System.Drawing.Point(0, 0);
            this.dependenicesListView.Name = "dependenicesListView";
            this.dependenicesListView.OwnerDraw = true;
            this.dependenicesListView.Size = new System.Drawing.Size(561, 320);
            this.dependenicesListView.SmallImageList = this.imageList;
            this.dependenicesListView.TabIndex = 0;
            this.dependenicesListView.UseCompatibleStateImageBehavior = false;
            this.dependenicesListView.View = System.Windows.Forms.View.Details;
            this.dependenicesListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.dependenicesListView_DrawColumnHeader);
            this.dependenicesListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.dependenicesListView_DrawSubItem);
            // 
            // directionColumnHeader
            // 
            this.directionColumnHeader.Text = "<-->";
            this.directionColumnHeader.Width = 40;
            // 
            // typeColumnHeader
            // 
            this.typeColumnHeader.Text = "Type";
            this.typeColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.typeColumnHeader.Width = 40;
            // 
            // schemaColumnHeader
            // 
            this.schemaColumnHeader.Text = "Schema";
            // 
            // nameColumnHeader
            // 
            this.nameColumnHeader.Text = "Name";
            this.nameColumnHeader.Width = 200;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Inbound");
            this.imageList.Images.SetKeyName(1, "Outbound");
            this.imageList.Images.SetKeyName(2, "Table");
            this.imageList.Images.SetKeyName(3, "View");
            this.imageList.Images.SetKeyName(4, "Procedure");
            this.imageList.Images.SetKeyName(5, "Function");
            this.imageList.Images.SetKeyName(6, "Key");
            this.imageList.Images.SetKeyName(7, "Link");
            // 
            // dependColumnHeader
            // 
            this.dependColumnHeader.Text = "Depend";
            this.dependColumnHeader.Width = 300;
            // 
            // ObjectDependencies
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dependenicesListView);
            this.Name = "ObjectDependencies";
            this.Size = new System.Drawing.Size(561, 320);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView dependenicesListView;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ColumnHeader directionColumnHeader;
        private System.Windows.Forms.ColumnHeader typeColumnHeader;
        private System.Windows.Forms.ColumnHeader schemaColumnHeader;
        private System.Windows.Forms.ColumnHeader nameColumnHeader;
        private System.Windows.Forms.ColumnHeader dependColumnHeader;
    }
}
