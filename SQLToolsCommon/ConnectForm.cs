using System.Windows.Forms;

namespace FYK.SQLTools.SQLToolsCommon
{
    public partial class ConnectForm : Form
    {
        public ConnectForm()
        {
            InitializeComponent();
            authComboBox.SelectedIndex = 0;
        }
        public string Server { get { return serverTextBox.Text; } set { serverTextBox.Text = value; } }
        public bool SQLAuth { get { return authComboBox.SelectedIndex == 1; } set { authComboBox.SelectedIndex = (value ? 1 : 0); } }
        public string UserName { get { return userNameTextBox.Text; } set { userNameTextBox.Text = value; } }
        public string Password { get { return passwordTextBox.Text; } set { passwordTextBox.Text = value; } }

        private void authComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            userNameTextBox.Enabled = passwordTextBox.Enabled = (authComboBox.SelectedIndex == 1);
            EditData();
        }
        private void EditData()
        {
            connectButton.Enabled = (serverTextBox.Text.Length > 0) && ((authComboBox.SelectedIndex == 0) || ((authComboBox.SelectedIndex == 1) && (userNameTextBox.Text.Length > 0) && (passwordTextBox.Text.Length > 0)));
        }
        private void serverTextBox_TextChanged(object sender, System.EventArgs e)
        {
            EditData();
        }

        private void userNameTextBox_TextChanged(object sender, System.EventArgs e)
        {
            EditData();
        }

        private void passwordTextBox_TextChanged(object sender, System.EventArgs e)
        {
            EditData();
        }
    }
}
