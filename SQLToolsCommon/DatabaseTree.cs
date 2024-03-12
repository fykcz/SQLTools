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
    public partial class DatabaseTree : UserControl
    {
        [Description("Handler for ItemSelected event.")]
        public delegate void ItemSelectedHandler(ItemSelectedEventArgs itemInfo);

        [Description("Fired when treeview item is selected.")]
        public event ItemSelectedHandler ItemSelected;

        private SqlConnection connection;
        public DatabaseTree()
        {
            InitializeComponent();
        }

        public void LoadDatabases(SqlConnection sqlConnection)
        {
            if (sqlConnection?.State != ConnectionState.Open) return;
            connection = sqlConnection;

            using (var cmd = new SqlCommand())
            {
                cmd.Connection = sqlConnection;
                cmd.CommandText = "SELECT name FROM sys.databases WHERE HAS_DBACCESS(name) = 1 AND name NOT IN ('master', 'tempdb', 'model', 'msdb') ORDER BY name;";
                cmd.CommandType = CommandType.Text;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader[0].ToString();
                        var db = CreateNode(name, name, "", name, ObjectType.Database, 1);
                        db.Nodes.Add(CreateNode("Tables", name, "", "Tables", ObjectType.Folder, 0, true));
                        db.Nodes.Add(CreateNode("Views", name, "", "Views", ObjectType.Folder, 0, true));
                        db.Nodes.Add(CreateNode("Programmability", name, "", "Programmability", ObjectType.Folder, 0, true));

                        treeView.Nodes.Add(db);
                    }
                    reader.Close();
                }
            }
        }
        private TreeNode CreateNode(string text, string database, string schema, string name, string type, int imageIndex)
        {
            var rv = new TreeNode(text, imageIndex, imageIndex)
            {
                Tag = new ItemInfo() { Name = name, Text = text, Type = type, Database = database, Schema = schema }
            };
            return rv;
        }
        private TreeNode CreateNode(string text, string database, string schema, string name, string type, int imageIndex, bool addVirtualChild)
        {
            var rv = CreateNode(text, database, schema, name, type, imageIndex);
            if (addVirtualChild) rv.Nodes.Add(".");
            return rv;
        }

        private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            var n = e.Node;
            if (n.Nodes.Count == 1 && n.Nodes[0].Text == ".")
                n.Nodes.Clear();
            LoadNode(n);
        }

        private void LoadNode(TreeNode node)
        {
            var ni = (ItemInfo)node.Tag;
            switch (ni.Type)
            {
                case ObjectType.Database:
                    break;
                case ObjectType.Folder:
                    switch (ni.Name)
                    {
                        case "Tables":
                            LoadTables(node, ni.Database);
                            break;
                        case "Views":
                            LoadViews(node, ni.Database);
                            break;
                        case "Programmability":
                            LoadProgrammability(node, ni.Database);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    //node.Nodes.Add(CreateNode("Nic", "AniTady", "Co ja viem?", ObjectType.Folder, 0));
                    break;
            }
        }
        private void LoadTables(TreeNode node, string databaseName)
        {
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = $"SELECT TABLE_SCHEMA, TABLE_NAME FROM {databaseName}.INFORMATION_SCHEMA.TABLES " +
                    "WHERE TABLE_TYPE = 'BASE TABLE' " +
                    "ORDER BY TABLE_SCHEMA, TABLE_NAME";
                cmd.CommandType = CommandType.Text;
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        var tname = rd[0].ToString() + '.' + rd[1].ToString();
                        node.Nodes.Add(CreateNode(tname, databaseName, rd[0].ToString(), rd[1].ToString(), ObjectType.Table, 2));
                    }
                    rd.Close();
                }
            }
        }
        private void LoadViews(TreeNode node, string databaseName)
        {
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = $"SELECT TABLE_SCHEMA, TABLE_NAME FROM {databaseName}.INFORMATION_SCHEMA.TABLES " +
                    "WHERE TABLE_TYPE = 'VIEW' " +
                    "ORDER BY TABLE_SCHEMA, TABLE_NAME";
                cmd.CommandType = CommandType.Text;
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        var tname = rd[0].ToString() + '.' + rd[1].ToString();
                        node.Nodes.Add(CreateNode(tname, databaseName, rd[0].ToString(), rd[1].ToString(), ObjectType.View, 3));
                    }
                    rd.Close();
                }
            }
        }
        private void LoadProgrammability(TreeNode node, string databaseName)
        {
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = $"SELECT ROUTINE_SCHEMA, ROUTINE_NAME, ROUTINE_TYPE FROM {databaseName}.INFORMATION_SCHEMA.ROUTINES " +
                    "ORDER BY ROUTINE_SCHEMA, ROUTINE_NAME";
                cmd.CommandType = CommandType.Text;
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        var tname = rd[0].ToString() + '.' + rd[1].ToString();
                        node.Nodes.Add(CreateNode(tname, databaseName, rd[0].ToString(), rd[1].ToString(), ObjectType.Programmable, rd[2].ToString().Equals("FUNCTION") ? 5 : 4));
                    }
                    rd.Close();
                }
            }
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ItemSelected?.Invoke(new ItemSelectedEventArgs((ItemInfo)e.Node.Tag));
        }
    }
    public class ItemInfo
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Database { get; set; }
        public string Schema { get; set; }
    }
    public static class ObjectType
    {
        public const string Database = "database";
        public const string Folder = "folder";
        public const string Table = "table";
        public const string View = "view";
        public const string Programmable = "programmable";
    }
    public class ItemSelectedEventArgs : EventArgs
    {
        public ItemInfo ItemInfo{ get; private set; }
        public ItemSelectedEventArgs(ItemInfo info)
        {
            ItemInfo = info;
        }
    }
}
