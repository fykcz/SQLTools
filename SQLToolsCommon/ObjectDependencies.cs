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
    public partial class ObjectDependencies : UserControl
    {
        private SqlConnection connection;

        public ObjectDependencies()
        {
            InitializeComponent();
        }
        public void LoadDependencies(SqlConnection sqlConnection, ItemInfo nodeInfo)
        {
            if (sqlConnection?.State != ConnectionState.Open) return;
            connection = sqlConnection;

            dependenicesListView.Items.Clear();
            switch (nodeInfo.Type)
            {
                case ObjectType.Table:
                    LoadTable(nodeInfo);
                    break;
                case ObjectType.View:
                    break;
                case ObjectType.Programmable:
                    break;
            }
        }

        private void LoadTable(ItemInfo item)
        {
            var sqlCmd = "SELECT DISTINCT KCU{3}.TABLE_SCHEMA AS REFERENCED_TABLE_SCHEMA, " +
                "KCU{3}.TABLE_NAME AS REFERENCED_TABLE_NAME, " +
                "KCU1.CONSTRAINT_NAME AS FK_CONSTRAINT_NAME " +
                "FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS AS RC " +
                "INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KCU1 ON " +
                "KCU1.CONSTRAINT_CATALOG = RC.CONSTRAINT_CATALOG AND " +
                "KCU1.CONSTRAINT_SCHEMA = RC.CONSTRAINT_SCHEMA AND " +
                "KCU1.CONSTRAINT_NAME = RC.CONSTRAINT_NAME " +
                "INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KCU2 ON " +
                "KCU2.CONSTRAINT_CATALOG = RC.UNIQUE_CONSTRAINT_CATALOG AND " +
                "KCU2.CONSTRAINT_SCHEMA = RC.UNIQUE_CONSTRAINT_SCHEMA AND " +
                "KCU2.CONSTRAINT_NAME = RC.UNIQUE_CONSTRAINT_NAME AND " +
                "KCU2.ORDINAL_POSITION = KCU1.ORDINAL_POSITION " +
                "WHERE KCU{0}.TABLE_NAME = '{1}' AND KCU{0}.TABLE_SCHEMA = '{2}' " +
                "ORDER BY 1, 2, 3";
            // 0 - 1: z tabule ven; 2: do tabule
            // 1 - jmeno tabule
            // 2 - schema tabule

            using (var cmd = new SqlCommand() {
                CommandText = string.Format(sqlCmd, 1, item.Name, item.Schema, 2),
                Connection = connection,
                CommandType = CommandType.Text })
            {
                using (var sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        var ln = new ListViewItem();
                        var di = new DependInfo()
                        {
                            DependencyName = sdr[2].ToString(),
                            Direction = "Outbound",
                            Type = "Table",
                            Schema = sdr[0].ToString(),
                            Name = sdr[1].ToString()
                        };
                        ln.Text = "";
                        ln.ImageIndex = 2;
                    }
                }
            }
        }

        private void dependenicesListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void dependenicesListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            if (e.Header == directionColumnHeader || e.Header == typeColumnHeader)
            {
                e.DrawBackground();
                var imgRect = new Rectangle(e.Bounds.X, e.Bounds.Y, dependenicesListView.SmallImageList.ImageSize.Width, dependenicesListView.SmallImageList.ImageSize.Height);
                var di = (DependInfo)e.Item.Tag;
                e.Graphics.DrawImage(di.GetImageType(dependenicesListView.SmallImageList), imgRect);
            }
            else
            {
                e.DrawDefault = true;
            }
        }
    }
    internal class DependInfo
    {
        public string Direction { get; set; }
        public string Type { get; set; }
        public string Schema { get; set; }
        public string Name { get; set; }
        public string DependencyName { get; set; }

        public Image GetImageType(ImageList list) {
            return list.Images[Type];
        }
    }
}
