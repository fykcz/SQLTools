using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Astratex.ITTools.BCPGUI
{
    public partial class MainForm : Form
    {
        #region Private attributes
        private string activeAction;
        private BCPParams bcpParams;
        #endregion

        #region Ctor
        public MainForm()
        {
            InitializeComponent();
            bcpParams = new BCPParams();
            SetTooltips();
        }
        #endregion

        #region Private methods
        private void ManageCheckGroupBox(CheckBox chk, GroupBox grp)
        {
            // Make sure the CheckBox isn't in the GroupBox.
            // This will only happen the first time.
            if (chk.Parent == grp)
            {
                // Reparent the CheckBox so it's not in the GroupBox.
                grp.Controls.Remove(chk);
                grp.Parent.Controls.Add(chk);

                // Adjust the CheckBox's location.
                chk.Location = new Point(
                    chk.Left + grp.Left,
                    chk.Top + grp.Top);

                // Move the CheckBox to the top of the stacking order.
                chk.BringToFront();
            }

            // Enable or disable the GroupBox.
            grp.Enabled = chk.Checked;
            chk.CheckedChanged += delegate (object o, EventArgs e)
            {
                grp.Enabled = chk.Checked;
            };
        }

        private void ChangeAction(RadioButton checkedButton)
        {
            sourceTextBox.Multiline = false;
            sourceTextBox.Size = new Size(srcTgtGroupBox.Width - (sourceTextBox.Location.X * 2), 30);
            sourceTextBox.Text = "";

            srcTgtGroupBox.Text = $"2. {checkedButton.Tag.ToString()}";

            dataFileGroupBox.Enabled = false;
            toolTip.SetToolTip(sourceTextBox, $"Is the name of the destination table/view when importing data into SQL Server (in),{Environment.NewLine}and the source table/view when exporting data from SQL Server (out).");
            bcpParams.Action = activeAction = checkedButton.Text;

            switch (checkedButton.Text)
            {
                case "In":
                    dataFileGroupBox.Enabled = true;
                    break;
                case "Out":
                    dataFileGroupBox.Enabled = true;
                    break;
                case "Format":
                    fileFormatCheckBox.Checked = true;
                    break;
                case "Queryout":
                    sourceTextBox.Multiline = true;
                    sourceTextBox.Size = new Size(srcTgtGroupBox.Width - (sourceTextBox.Location.X * 2), srcTgtGroupBox.Height - sourceTextBox.Location.Y - 6);
                    dataFileGroupBox.Enabled = true;
                    toolTip.SetToolTip(sourceTextBox, $"Is a Transact-SQL query that returns a result set.");
                    break;
                default:
                    break;
            }
        }

        private void SetTooltips()
        {
            var tip = $"-f format_file{Environment.NewLine}Specifies the full path of a format file.";
            toolTip.SetToolTip(fileFormatCheckBox, tip);
            toolTip.SetToolTip(formatFileTextBox, tip);
            toolTip.SetToolTip(formatXMLCheckBox, $"-x{Environment.NewLine}Used with the format and - f format_file options, generates an XML - based format file instead of the default non - XML format file.");
            tip = $"-a packet_size{Environment.NewLine}Specifies the number of bytes, per network packet, sent to and from the server.{Environment.NewLine}packet_size can be from 4096 to 65535 bytes; the default is 4096.";
            toolTip.SetToolTip(packetSizeCheckBox, tip);
            toolTip.SetToolTip(packetSizeNumericUpDown, tip);
            tip = $"-b batch_size{Environment.NewLine}Specifies the number of rows per batch of imported data.{Environment.NewLine}Each batch is imported and logged as a separate transaction that imports the whole batch before being committed.{Environment.NewLine}By default, all the rows in the data file are imported as one batch.{Environment.NewLine}To distribute the rows among multiple batches, specify a batch_size that is smaller than the number of rows in the data file.{Environment.NewLine}If the transaction for any batch fails, only insertions from the current batch are rolled back.{Environment.NewLine}Batches already imported by committed transactions are unaffected by a later failure.";
            toolTip.SetToolTip(batchSizeCheckBox, tip);
            toolTip.SetToolTip(batchSizeNumericUpDown, tip);
            toolTip.SetToolTip(serverTextBox, $"-S server_name [\\instance_name]{Environment.NewLine}Specifies the instance of SQL Server to which to connect.{Environment.NewLine}If no server is specified, the bcp utility connects to the default instance of SQL Server on the local computer.");
            toolTip.SetToolTip(databaseTextBox, $"-d database_name{Environment.NewLine}Specifies the database to connect to.{Environment.NewLine}By default, bcp.exe connects to the user's default database.");
            toolTip.SetToolTip(authTypeComboBox, $"SQL Authentication{Environment.NewLine}\tUse SQL login to connect to SQL server.{Environment.NewLine}Windows Integrated Security -T{Environment.NewLine}\tSpecifies that the bcp utility connects to SQL Server with a trusted connection using integrated security.{Environment.NewLine}Azure Active Directory -G{Environment.NewLine}\tUser be authenticated using Azure Active Directory authentication.");
            toolTip.SetToolTip(passwordTextBox, $"-P password{Environment.NewLine}Specifies the password for the login ID.");
            toolTip.SetToolTip(loginTextBox, $"-U login_id{Environment.NewLine}Specifies the login ID used to connect to SQL Server.");
            tip = $"-L last_row{Environment.NewLine}Specifies the number of the last row to export from a table or import from a data file{Environment.NewLine}This parameter requires a value greater than (>) 0 but less than (<) or equal to (=) the number of the last row.{Environment.NewLine}In the absence of this parameter, the default is the last row of the file.";
            toolTip.SetToolTip(lastRowCheckBox, tip);
            toolTip.SetToolTip(lastRowNumericUpDown, tip);
            tip = $"-F first_row{Environment.NewLine}Specifies the number of the first row to export from a table or import from a data file.{Environment.NewLine}This parameter requires a value greater than (>) 0 but less than (<) or equal to (=) the total number rows.{Environment.NewLine}In the absence of this parameter, the default is the first row of the file.";
            toolTip.SetToolTip(firstRowCheckBox, tip);
            toolTip.SetToolTip(firstRowNumericUpDown, tip);
            toolTip.SetToolTip(inRadioButton, "in copies from a file into the database table or view.");
            toolTip.SetToolTip(outRadioButton, "out copies from the database table or view to a file.");
            toolTip.SetToolTip(queryoutRadioButton, "queryout copies from a query and must be specified only when bulk copying data from a query.");
            toolTip.SetToolTip(formatRadioButton, "format creates a format file based on the option specified (-n, -c, -w, or -N) and the table or view delimiters.");
            toolTip.SetToolTip(dataFileTextBox, $"Is the full path of the data file.{Environment.NewLine}When data is bulk imported into SQL Server, the data file contains the data to be copied into the specified table or view.{Environment.NewLine}When data is bulk exported from SQL Server, the data file contains the data copied from the table or view.");
            tip = $"-e err_file{Environment.NewLine}Specifies the full path of an error file used to store any rows that the bcp utility cannot transfer from the file to the database.";
            toolTip.SetToolTip(errorFileCheckBox, tip);
            toolTip.SetToolTip(errFileTextBox, tip);
            tip = $"-i input_file{Environment.NewLine}Specifies the name of a response file, containing the responses to the command prompt questions for each data field when a bulk copy is being performed using interactive mode (-n, -c, -w, or - N not specified).";
            toolTip.SetToolTip(inputFileCheckBox, tip);
            toolTip.SetToolTip(inputFileTextBox, tip);
            tip = $"-o output_file{Environment.NewLine}Specifies the name of a file that receives output redirected from the command prompt.";
            toolTip.SetToolTip(outputFileCheckBox, tip);
            toolTip.SetToolTip(outputFileTextBox, tip);
            tip = $"-c Performs the operation using a character data type.{Environment.NewLine}\tThis option does not prompt for each field; it uses char as the storage type, without prefixes and with \\t (tab character) as the field separator and \\r\\n (newline character) as the row terminator.{Environment.NewLine}";
            tip += $"-w Performs the bulk-copy operation using Unicode characters.{Environment.NewLine}\tThis option does not prompt for each field; it uses nchar as the storage type, no prefixes, \\t (tab character) as the field separator, and \\n (newline character) as the row terminator.{Environment.NewLine}";
            tip += $"-n Performs the bulk-copy operation using the native (database) data types of the data.{Environment.NewLine}\tThis option does not prompt for each field; it uses the native values.{Environment.NewLine}";
            tip += $"-N Performs the bulk-copy operation using the native (database) data types of the data for noncharacter data, and Unicode characters for character data.";
            toolTip.SetToolTip(formatComboBox, tip);
            tip = $"-C {{ ACP | OEM | RAW | code_page }}{Environment.NewLine}Specifies the code page of the data in the data file.{Environment.NewLine}code_page is relevant only if the data contains char, varchar, or text columns with character values greater than 127 or less than 32.{Environment.NewLine}";
            tip += $"\tACP - ANSI/Microsoft Windows (ISO 1252){Environment.NewLine}";
            tip += $"\tOEM - Default code page used by the client{Environment.NewLine}";
            tip += $"\tRAW - No conversion from one code page to another occurs{Environment.NewLine}";
            tip += $"\tcode_page - Specific code page number";
            toolTip.SetToolTip(codePageCheckBox, tip);
            toolTip.SetToolTip(codePageComboBox, tip);
            tip = $"-V (80 | 90 | 100 | 110 | 120 | 130 ){Environment.NewLine}Performs the bulk-copy operation using data types from an earlier version of SQL Server.{Environment.NewLine}This option does not prompt for each field; it uses the default values.{Environment.NewLine}";
            tip += $"\t80 = SQL Server 2000 (8.x){Environment.NewLine}";
            tip += $"\t90 = SQL Server 2005 (9.x){Environment.NewLine}";
            tip += $"\t100 = SQL Server 2008 and SQL Server 2008 R2{Environment.NewLine}";
            tip += $"\t110 = SQL Server 2012 (11.x){Environment.NewLine}";
            tip += $"\t120 = SQL Server 2014 (12.x){Environment.NewLine}";
            tip += $"\t130 = SQL Server 2016 (13.x){Environment.NewLine}";
            toolTip.SetToolTip(dataTypeVersionCheckBox, tip);
            toolTip.SetToolTip(dataTypeComboBox, tip);
            tip = $"-r row_term{Environment.NewLine}Specifies the row terminator. The default is \\n (newline character).";
            toolTip.SetToolTip(rowTerminatorCheckBox, tip);
            toolTip.SetToolTip(rowTerminatorTextBox, tip);
            tip = $"-t field_term{Environment.NewLine}Specifies the field terminator. The default is \\t (tab character).";
            toolTip.SetToolTip(fieldTerminatorCheckBox, tip);
            toolTip.SetToolTip(fieldTerminatorTextBox, tip);
            toolTip.SetToolTip(identityCheckBox, $"-E{Environment.NewLine}Specifies that identity value or values in the imported data file are to be used for the identity column.{Environment.NewLine}If -E is not given, the identity values for this column in the data file being imported are ignored,{Environment.NewLine}and SQL Server automatically assigns unique values based on the seed and increment values specified during table creation.");
            toolTip.SetToolTip(keepNullCheckBox, $"-k{Environment.NewLine}Specifies that empty columns should retain a null value during the operation,{Environment.NewLine}rather than have any default values for the columns inserted.");
            toolTip.SetToolTip(quotedIdentifierCheckBox, $"-q{Environment.NewLine}Executes the SET QUOTED_IDENTIFIERS ON statement in the connection between the bcp utility and an instance of SQL Server.");
            toolTip.SetToolTip(regionalFormatCheckBox, $"-R{Environment.NewLine}Specifies that currency, date, and time data is bulk copied into SQL Server{Environment.NewLine}using the regional format defined for the locale setting of the client computer.");
        }

        private int FindItem(ComboBox.ObjectCollection items, string value)
        {
            if (value == null) return -1;
            var xLen = value.Length;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ToString().StartsWith(value)) return i;
            }
            return -1;
        }

        private void SetNumericUpDown(NumericUpDown numericUpDown, long value, CheckBox checkBox, bool withValue)
        {
            checkBox.Checked = withValue;
            if (withValue)
                numericUpDown.Value = (value < numericUpDown.Minimum ? numericUpDown.Minimum : (value > numericUpDown.Maximum ? numericUpDown.Maximum : value));
            else
                numericUpDown.Value = numericUpDown.Minimum;

        }

        private void SetDefaultValues()
        {
            inRadioButton.Checked = true;
            sourceTextBox.Text = "";
            dataFileTextBox.Text = "";
            fileFormatCheckBox.Checked = false;
            formatFileTextBox.Text = "";
            formatXMLCheckBox.Checked = false;
            serverTextBox.Text = databaseTextBox.Text = "";
            authTypeComboBox.SelectedIndex = 0;
            loginTextBox.Text = passwordTextBox.Text = "";

            SetNumericUpDown(packetSizeNumericUpDown, 0, packetSizeCheckBox, false);
            SetNumericUpDown(batchSizeNumericUpDown, 0, batchSizeCheckBox, false);
            SetNumericUpDown(firstRowNumericUpDown, 0, firstRowCheckBox, false);
            SetNumericUpDown(lastRowNumericUpDown, 0, lastRowCheckBox, false);
            SetNumericUpDown(maxErrorsNumericUpDown, 0, maxErrorsCheckBox, false);

            errorFileCheckBox.Checked = inputFileCheckBox.Checked = outputFileCheckBox.Checked = false;
            errFileTextBox.Text = inputFileTextBox.Text = outputFileTextBox.Text = "";

            formatComboBox.SelectedIndex = codePageComboBox.SelectedIndex = dataTypeComboBox.SelectedIndex = 0;
            codePageCheckBox.Checked = dataTypeVersionCheckBox.Checked = rowTerminatorCheckBox.Checked =
                fieldTerminatorCheckBox.Checked = identityCheckBox.Checked = keepNullCheckBox.Checked =
                quotedIdentifierCheckBox.Checked = regionalFormatCheckBox.Checked = false;
            rowTerminatorTextBox.Text = fieldTerminatorTextBox.Text = "";

            orderHintCheckBox.Checked = tablockHintCheckBox.Checked = checkConstraintsHintCheckBox.Checked = fireTriggersHintCheckBox.Checked = false;
            SetNumericUpDown(rowsPerBatchHintNumericUpDown, 0, rowsPerBatchHintCheckBox, false);
            SetNumericUpDown(kiloPerBatchHintNumericUpDown, 0, kiloPerBatchHintCheckBox, false);

            
        }
        #endregion

        #region Event handlers
        private void Action_CheckedChanged(object sender, EventArgs e)
        {
            ChangeAction(((RadioButton)sender));
        }

        private void dataFileBrowseButton_Click(object sender, EventArgs e)
        {
            FileDialog fileDialog = null;
            if ((activeAction == "Out") || (activeAction == "Queryout"))
            {
                fileDialog = new SaveFileDialog
                {
                    CheckFileExists = false
                };
            }
            else
            {
                fileDialog = new OpenFileDialog
                {
                    CheckFileExists = true
                };
            }

            fileDialog.Title = srcTgtGroupBox.Text;
            fileDialog.Filter = "Data files (*.dat)|*.dat|All files (*.*)|*.*";
            fileDialog.FilterIndex = 1;
            fileDialog.ShowHelp = false;
            fileDialog.ValidateNames = true;
            if (fileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            dataFileTextBox.Text = fileDialog.FileName;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            formatFileGroupBox.Enabled = fileFormatCheckBox.Checked;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            ChangeAction(inRadioButton);
            ManageCheckGroupBox(fileFormatCheckBox, formatFileGroupBox);
            SetDefaultValues();
        }

        private void formatFileBrowseButton_Click(object sender, EventArgs e)
        {
            FileDialog fileDialog = null;
            if (activeAction == "Format")
            {
                fileDialog = new SaveFileDialog
                {
                    CheckFileExists = false
                };
            }
            else
            {
                fileDialog = new OpenFileDialog
                {
                    CheckFileExists = true
                };
            }

            fileDialog.Title = srcTgtGroupBox.Text;
            fileDialog.Filter = "Standard format file (*.fmt)|*.fmt|XML format file (*.xml)|*.xml|All files (*.*)|*.*";
            fileDialog.FilterIndex = 2;
            fileDialog.ShowHelp = false;
            fileDialog.ValidateNames = true;
            if (fileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            formatFileTextBox.Text = fileDialog.FileName;
        }

        private void authTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            loginLabel.Enabled = loginTextBox.Enabled = passwordLabel.Enabled = passwordTextBox.Enabled =
                !authTypeComboBox.SelectedItem.ToString().Equals("Windows Integrated Security");
            if (authTypeComboBox.SelectedItem.ToString().Equals("Windows Integrated Security"))
                loginTextBox.Text = passwordTextBox.Text = "";
            bcpParams.Authentication = authTypeComboBox.SelectedItem.ToString();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            codePageComboBox.Items.AddRange(new object[] {
                "ACP - ANSI/Microsoft Windows (ISO 1252)",
                "OEM - Default code page used by the client",
                "RAW - No conversion from one code page to another occurs",
                "037 - IBM EBCDIC US-Canada",
                "437 - OEM United States",
                "500 - IBM EBCDIC International",
                "708 - Arabic (ASMO 708)",
                "709 - Arabic (ASMO-449+, BCON V4)",
                "710 - Arabic - Transparent Arabic",
                "720 - Arabic (Transparent ASMO)",
                "737 - OEM Greek (formerly 437G)",
                "775 - OEM Baltic",
                "850 - OEM Multilingual Latin 1",
                "852 - OEM Latin 2",
                "855 - OEM Cyrillic (primarily Russian)",
                "857 - OEM Turkish",
                "858 - OEM Multilingual Latin 1 + Euro symbol",
                "860 - OEM Portuguese",
                "861 - OEM Icelandic",
                "862 - OEM Hebrew",
                "863 - OEM French Canadian",
                "864 - OEM Arabic",
                "865 - OEM Nordic",
                "866 - OEM Russian",
                "869 - OEM Modern Greek",
                "870 - IBM EBCDIC Multilingual/ROECE (Latin 2)",
                "874 - ANSI/OEM Thai (ISO 8859-11)",
                "875 - IBM EBCDIC Greek Modern",
                "932 - ANSI/OEM Japanese",
                "936 - ANSI/OEM Simplified Chinese (PRC, Singapore)",
                "949 - ANSI/OEM Korean (Unified Hangul Code)",
                "950 - ANSI/OEM Traditional Chinese (Taiwan",
                "1026 - IBM EBCDIC Turkish (Latin 5)",
                "1047 - IBM EBCDIC Latin 1/Open System",
                "1140 - IBM EBCDIC US-Canada (037 + Euro symbol)",
                "1141 - IBM EBCDIC Germany (20273 + Euro symbol)",
                "1142 - IBM EBCDIC Denmark-Norway (20277 + Euro symbol)",
                "1143 - IBM EBCDIC Finland-Sweden (20278 + Euro symbol)",
                "1144 - IBM EBCDIC Italy (20280 + Euro symbol)",
                "1145 - IBM EBCDIC Latin America-Spain (20284 + Euro symbol)",
                "1146 - IBM EBCDIC United Kingdom (20285 + Euro symbol)",
                "1147 - IBM EBCDIC France (20297 + Euro symbol)",
                "1148 - IBM EBCDIC International (500 + Euro symbol)",
                "1149 - IBM EBCDIC Icelandic (20871 + Euro symbol)",
                "1200 - Unicode UTF-16, little endian byte order (BMP of ISO 10646)",
                "1201 - Unicode UTF-16, big endian byte order",
                "1250 - ANSI Central European",
                "1251 - ANSI Cyrillic",
                "1252 - ANSI Latin 1",
                "1253 - ANSI Greek",
                "1254 - ANSI Turkish",
                "1255 - ANSI Hebrew",
                "1256 - ANSI Arabic",
                "1257 - ANSI Baltic",
                "1258 - ANSI/OEM Vietnamese",
                "1361 - Korean (Johab)",
                "10000 - MAC Roman",
                "10001 - Japanese (Mac)",
                "10002 - MAC Traditional Chinese (Big5)",
                "10003 - Korean (Mac)",
                "10004 - Arabic (Mac)",
                "10005 - Hebrew (Mac)",
                "10006 - Greek (Mac)",
                "10007 - Cyrillic (Mac)",
                "10008 - MAC Simplified Chinese (GB 2312)",
                "10010 - Romanian (Mac)",
                "10017 - Ukrainian (Mac)",
                "10021 - Thai (Mac)",
                "10029 - MAC Latin 2",
                "10079 - Icelandic (Mac)",
                "10081 - Turkish (Mac)",
                "10082 - Croatian (Mac)",
                "12000 - Unicode UTF-32, little endian byte order",
                "12001 - Unicode UTF-32, big endian byte order",
                "20000 - CNS Taiwan",
                "20001 - TCA Taiwan",
                "20002 - Eten Taiwan",
                "20003 - IBM5550 Taiwan",
                "20004 - TeleText Taiwan",
                "20005 - Wang Taiwan",
                "20105 - IA5 (IRV International Alphabet No. 5, 7-bit)",
                "20106 - IA5 German (7-bit)",
                "20107 - IA5 Swedish (7-bit)",
                "20108 - IA5 Norwegian (7-bit)",
                "20127 - US-ASCII (7-bit)",
                "20261 - T.61",
                "20269 - ISO 6937 Non-Spacing Accent",
                "20273 - IBM EBCDIC Germany",
                "20277 - IBM EBCDIC Denmark-Norway",
                "20278 - IBM EBCDIC Finland-Sweden",
                "20280 - IBM EBCDIC Italy",
                "20284 - IBM EBCDIC Latin America-Spain",
                "20285 - IBM EBCDIC United Kingdom",
                "20290 - IBM EBCDIC Japanese Katakana Extended",
                "20297 - IBM EBCDIC France",
                "20420 - IBM EBCDIC Arabic",
                "20423 - IBM EBCDIC Greek",
                "20424 - IBM EBCDIC Hebrew",
                "20833 - IBM EBCDIC Korean Extended",
                "20838 - IBM EBCDIC Thai",
                "20866 - Russian (KOI8-R)",
                "20871 - IBM EBCDIC Icelandic",
                "20880 - IBM EBCDIC Cyrillic Russian",
                "20905 - IBM EBCDIC Turkish",
                "20924 - IBM EBCDIC Latin 1/Open System (1047 + Euro symbol)",
                "20932 - Japanese (JIS 0208-1990 and 0212-1990)",
                "20936 - Simplified Chinese (GB2312)",
                "20949 - Korean Wansung",
                "21025 - IBM EBCDIC Cyrillic Serbian-Bulgarian",
                "21027 - (deprecated)",
                "21866 - Ukrainian (KOI8-U)",
                "28591 - ISO 8859-1 Latin 1",
                "28592 - ISO 8859-2 Central European",
                "28593 - ISO 8859-3 Latin 3",
                "28594 - ISO 8859-4 Baltic",
                "28595 - ISO 8859-5 Cyrillic",
                "28596 - ISO 8859-6 Arabic",
                "28597 - ISO 8859-7 Greek",
                "28598 - ISO 8859-8 Hebrew",
                "28599 - ISO 8859-9 Turkish",
                "28603 - ISO 8859-13 Estonian",
                "28605 - ISO 8859-15 Latin 9",
                "29001 - Europa 3",
                "38598 - ISO 8859-8 Hebrew",
                "50220 - ISO 2022 Japanese with no halfwidth Katakana",
                "50221 - ISO 2022 Japanese with halfwidth Katakana",
                "50222 - ISO 2022 Japanese JIS X 0201-1989",
                "50225 - ISO 2022 Korean",
                "50227 - ISO 2022 Simplified Chinese",
                "50229 - ISO 2022 Traditional Chinese",
                "50930 - EBCDIC Japanese (Katakana) Extended",
                "50931 - EBCDIC US-Canada and Japanese",
                "50933 - EBCDIC Korean Extended and Korean",
                "50935 - EBCDIC Simplified Chinese Extended and Simplified Chinese",
                "50936 - EBCDIC Simplified Chinese",
                "50937 - EBCDIC US-Canada and Traditional Chinese",
                "50939 - EBCDIC Japanese (Latin) Extended and Japanese",
                "51932 - EUC Japanese",
                "51936 - EUC Simplified Chinese",
                "51949 - EUC Korean",
                "51950 - EUC Traditional Chinese",
                "52936 - HZ-GB2312 Simplified Chinese",
                "54936 - GB18030 Simplified Chinese (4 byte)",
                "57002 - ISCII Devanagari",
                "57003 - ISCII Bangla",
                "57004 - ISCII Tamil",
                "57005 - ISCII Telugu",
                "57006 - ISCII Assamese",
                "57007 - ISCII Odia",
                "57008 - ISCII Kannada",
                "57009 - ISCII Malayalam",
                "57010 - ISCII Gujarati",
                "57011 - ISCII Punjabi",
                "65000 - Unicode (UTF-7)",
                "65001 - Unicode (UTF-8)" });
            authTypeComboBox.SelectedIndex = 0;
            formatComboBox.SelectedIndex = 0;

        }

        private void packetSizeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bcpParams.WithPacketSize = packetSizeNumericUpDown.Enabled = packetSizeCheckBox.Checked;
        }

        private void batchSizeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bcpParams.WithBatchSize = batchSizeNumericUpDown.Enabled = batchSizeCheckBox.Checked;
        }

        private void firstRowCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bcpParams.WithFirstRow = firstRowNumericUpDown.Enabled = firstRowCheckBox.Checked;
        }

        private void lastRowCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bcpParams.WithLastRow = lastRowNumericUpDown.Enabled = lastRowCheckBox.Checked;
        }

        private void errorFileCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bcpParams.WithErrorFile = errFileTextBox.Enabled = errFileBrowseButton.Enabled = errorFileCheckBox.Checked;
        }

        private void inputFileCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bcpParams.WithInputFile = inputFileTextBox.Enabled = inputFileBrowseButton.Enabled = inputFileCheckBox.Checked;
        }

        private void outputFileCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bcpParams.WithOutputFile = outputFileTextBox.Enabled = outputFileBrowseButton.Enabled = outputFileCheckBox.Checked;
        }

        private void codePageCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bcpParams.WithCodePage = codePageComboBox.Enabled = codePageCheckBox.Checked;
        }

        private void dataTypeVersionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bcpParams.WithDataTypeVersion = dataTypeComboBox.Enabled = dataTypeVersionCheckBox.Checked;
        }

        private void formatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var opt = ((string)formatComboBox.SelectedItem).Substring(1, 1);
            var ind = "cw".IndexOf(opt);
            fieldTerminatorCheckBox.Enabled = rowTerminatorCheckBox.Enabled = (ind > -1);
            bcpParams.Format = opt;
        }

        private void rowTerminatorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bcpParams.WithRowTerminator = rowTerminatorTextBox.Enabled = rowTerminatorCheckBox.Checked;
        }

        private void rowTerminatorCheckBox_EnabledChanged(object sender, EventArgs e)
        {
            if (!rowTerminatorCheckBox.Enabled)
            {
                rowTerminatorTextBox.Text = "";
                rowTerminatorCheckBox.Checked = false;
            }
        }

        private void fieldTerminatorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bcpParams.WithFieldTerminator = fieldTerminatorTextBox.Enabled = fieldTerminatorCheckBox.Checked;
        }

        private void fieldTerminatorCheckBox_EnabledChanged(object sender, EventArgs e)
        {
            if (!fieldTerminatorCheckBox.Enabled)
            {
                fieldTerminatorTextBox.Text = "";
                fieldTerminatorCheckBox.Checked = false;
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            sb.Append("bcp.exe ");
            if (activeAction.Equals("Queryout"))
                sb.Append($"\"{sourceTextBox.Text}\"");
            else
                sb.Append(sourceTextBox.Text);
            sb.Append($" {activeAction.ToLower()} ");

            if (activeAction.Equals("Format"))
                sb.Append("nul ");
            else
                sb.Append($"\"{dataFileTextBox.Text}\" ");

            if (fileFormatCheckBox.Checked)
            {
                sb.Append($"-f\"{formatFileTextBox.Text}\" ");
                if (formatXMLCheckBox.Checked)
                    sb.Append("-x ");
            }

            if (serverTextBox.Text != "")
                sb.Append($"-S{serverTextBox.Text} ");

            if (databaseTextBox.Text != "")
                sb.Append($"-d{databaseTextBox.Text} ");

            switch (authTypeComboBox.SelectedIndex)
            {
                case 0:
                    sb.Append($"-U{loginTextBox.Text} -P{passwordTextBox.Text} ");
                    break;
                case 1:
                    sb.Append("-T ");
                    break;
                case 2:
                    sb.Append("-G ");
                    break;
            }

            if (packetSizeCheckBox.Checked) sb.Append($"-a{packetSizeNumericUpDown.Value} ");
            if (batchSizeCheckBox.Checked) sb.Append($"-b{batchSizeNumericUpDown.Value} ");
            if (firstRowCheckBox.Checked) sb.Append($"-F{firstRowNumericUpDown.Value} ");
            if (lastRowCheckBox.Checked) sb.Append($"-L{lastRowNumericUpDown.Value} ");

            if (errorFileCheckBox.Checked && errFileTextBox.Text != "") sb.Append($"-e\"{errFileTextBox.Text}\" ");
            if (inputFileCheckBox.Checked && inputFileTextBox.Text != "") sb.Append($"-i\"{inputFileTextBox.Text}\" ");
            if (outputFileCheckBox.Checked && outputFileTextBox.Text != "") sb.Append($"-o\"{outputFileTextBox.Text}\" ");

            var opt = ((string)formatComboBox.SelectedItem).Substring(1, 1);
            sb.Append($"-{opt} ");

            if (codePageCheckBox.Checked)
            {
                var cp = Regex.Replace((string)codePageComboBox.SelectedItem, @"^(?<codepage>[0-9A-Z]*) - .*$", "${codepage}");
                sb.Append($"-C{cp} ");
            }

            if (dataTypeVersionCheckBox.Checked)
            {
                var ver = Regex.Replace((string)dataTypeComboBox.SelectedItem, @"^(?<version>[0-9]*) - .*$", "${version}");
                sb.Append($"-V{ver} ");
            }

            if (rowTerminatorCheckBox.Checked && rowTerminatorTextBox.Text != "") sb.Append($"-r{rowTerminatorTextBox.Text} ");
            if (fieldTerminatorCheckBox.Checked && fieldTerminatorTextBox.Text != "") sb.Append($" -t{fieldTerminatorTextBox.Text} ");
            if (identityCheckBox.Checked) sb.Append("-E ");
            if (keepNullCheckBox.Checked) sb.Append("-k ");
            if (quotedIdentifierCheckBox.Checked) sb.Append("-q ");
            if (regionalFormatCheckBox.Checked) sb.Append("-R ");

            if (maxErrorsCheckBox.Checked) sb.Append($"-m{maxErrorsNumericUpDown.Value} ");

            if (orderHintCheckBox.Checked || rowsPerBatchHintCheckBox.Checked ||
                kiloPerBatchHintCheckBox.Checked || tablockHintCheckBox.Checked ||
                checkConstraintsHintCheckBox.Checked || fireTriggersHintCheckBox.Checked)
            {
                var pars = new List<string>();

                if (orderHintCheckBox.Checked)
                    pars.Add($"ORDER({orderHintTextBox.Text})");
                if (rowsPerBatchHintCheckBox.Checked)
                    pars.Add($"ROWS_PER_BATCH={rowsPerBatchHintNumericUpDown.Value}");
                if (kiloPerBatchHintCheckBox.Checked)
                    pars.Add($"KILOBYTES_PER_BATCH={kiloPerBatchHintNumericUpDown.Value}");
                if (tablockHintCheckBox.Checked) pars.Add("TABLOCK");
                if (checkConstraintsHintCheckBox.Checked) pars.Add("CHECK_CONSTRAINTS");
                if (fireTriggersHintCheckBox.Checked) pars.Add("FIRE_TRIGGERS");

                sb.Append($"-h \"{String.Join(", ", pars.ToArray())}\"");
            }

            Clipboard.SetText(sb.ToString());
            MessageBox.Show(sb.ToString(), Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void maxErrorsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bcpParams.WithMaxErrors = maxErrorsNumericUpDown.Enabled = maxErrorsCheckBox.Checked;
        }

        private void orderHintCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bcpParams.WithHintOrder = orderHintTextBox.Enabled = orderHintCheckBox.Checked;
        }

        private void rowsPerBatchHintCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bcpParams.WithHintRowsPerBatch = rowsPerBatchHintNumericUpDown.Enabled = rowsPerBatchHintCheckBox.Checked;
        }

        private void kiloPerBatchHintCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bcpParams.WithHintKilobytesPerBatch = kiloPerBatchHintNumericUpDown.Enabled = kiloPerBatchHintCheckBox.Checked;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            using (var fsb = new SaveFileDialog())
            {
                fsb.Filter = "BCP GUI Settings (*.bcpj)|*.bcpj|JSON Files (*.json)|*.json|All Files (*.*)|*.*";
                fsb.FilterIndex = 1;
                fsb.Title = Text;
                fsb.ShowHelp = false;
                fsb.OverwritePrompt = true;
                if (fsb.ShowDialog() == DialogResult.Cancel) return;

                var output = JsonConvert.SerializeObject(bcpParams);
                using (var sw = new System.IO.StreamWriter(fsb.FileName))
                {
                    sw.Write(output);
                    sw.Flush();
                    sw.Close();
                }
            }
        }

        private void sourceTextBox_TextChanged(object sender, EventArgs e)
        {
            bcpParams.SourceOrTarget = sourceTextBox.Text;
        }

        private void errFileBrowseButton_Click(object sender, EventArgs e)
        {
            using (var fob = new SaveFileDialog())
            {
                fob.Filter = "Error files (*.err)|*.err|All Files (*.*)|*.*";
                fob.FilterIndex = 1;
                fob.Title = Text;
                fob.ShowHelp = false;
                fob.OverwritePrompt = true;
                if (fob.ShowDialog() == DialogResult.Cancel) return;
                errFileTextBox.Text = fob.FileName;
            }
        }

        private void inputFileBrowseButton_Click(object sender, EventArgs e)
        {
            using (var fob = new OpenFileDialog())
            {
                fob.Filter = "Input files (*.inp)|*.inp|All Files (*.*)|*.*";
                fob.FilterIndex = 1;
                fob.Title = Text;
                fob.ShowHelp = false;
                fob.Multiselect = false;
                fob.CheckFileExists = true;
                if (fob.ShowDialog() == DialogResult.Cancel) return;
                inputFileTextBox.Text = fob.FileName;
            }
        }

        private void outputFileBrowseButton_Click(object sender, EventArgs e)
        {
            using (var fob = new SaveFileDialog())
            {
                fob.Filter = "Output files (*.out)|*.out|All Files (*.*)|*.*";
                fob.FilterIndex = 1;
                fob.Title = Text;
                fob.ShowHelp = false;
                fob.OverwritePrompt = true;
                if (fob.ShowDialog() == DialogResult.Cancel) return;
                outputFileTextBox.Text = fob.FileName;
            }
        }

        private void dataFileTextBox_TextChanged(object sender, EventArgs e)
        {
            bcpParams.DataFile = dataFileTextBox.Text;
        }

        private void formatXMLCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bcpParams.WithXMLFormatFile = formatXMLCheckBox.Checked;
        }

        private void serverTextBox_TextChanged(object sender, EventArgs e)
        {
            bcpParams.Server = serverTextBox.Text;
        }

        private void databaseTextBox_TextChanged(object sender, EventArgs e)
        {
            bcpParams.Database = databaseTextBox.Text;
        }

        private void loginTextBox_TextChanged(object sender, EventArgs e)
        {
            bcpParams.Login = loginTextBox.Text;
        }

        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {
            bcpParams.Password = passwordTextBox.Text;
        }

        private void packetSizeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            bcpParams.PacketSize = (long)packetSizeNumericUpDown.Value;
        }

        private void batchSizeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            bcpParams.BatchSize = (long)batchSizeNumericUpDown.Value;
        }

        private void firstRowNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            bcpParams.FirstRow = (long)firstRowNumericUpDown.Value;
        }

        private void lastRowNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            bcpParams.LastRow = (long)lastRowNumericUpDown.Value;
        }

        private void maxErrorsNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            bcpParams.MaxErrors = (long)maxErrorsNumericUpDown.Value;
        }

        private void errFileTextBox_TextChanged(object sender, EventArgs e)
        {
            bcpParams.ErrorFile = errFileTextBox.Text;
        }

        private void inputFileTextBox_TextChanged(object sender, EventArgs e)
        {
            bcpParams.InputFile = inputFileTextBox.Text;
        }

        private void outputFileTextBox_TextChanged(object sender, EventArgs e)
        {
            bcpParams.OutputFile = outputFileTextBox.Text;
        }

        private void codePageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bcpParams.CodePage = (string)codePageComboBox.SelectedItem;
        }

        private void dataTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bcpParams.DataTypeVersion = Convert.ToInt32(Regex.Replace((string)dataTypeComboBox.SelectedItem, @"^(?<version>[0-9]*) - .*$", "${version}"));
        }

        private void rowTerminatorTextBox_TextChanged(object sender, EventArgs e)
        {
            bcpParams.RowTerminator = rowTerminatorTextBox.Text;
        }

        private void fieldTerminatorTextBox_TextChanged(object sender, EventArgs e)
        {
            bcpParams.FieldTerminator = fieldTerminatorTextBox.Text;
        }

        private void identityCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bcpParams.WithImportIdentity = identityCheckBox.Checked;
        }

        private void keepNullCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bcpParams.WithKeepNull = keepNullCheckBox.Checked;
        }

        private void quotedIdentifierCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bcpParams.WithQuotedIdentifiers = quotedIdentifierCheckBox.Checked;
        }

        private void regionalFormatCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bcpParams.WithRegionalFormat = regionalFormatCheckBox.Checked;
        }

        private void orderHintTextBox_TextChanged(object sender, EventArgs e)
        {
            bcpParams.HintOrder = orderHintTextBox.Text;
        }

        private void rowsPerBatchHintNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            bcpParams.HintRowsPerBatch = (long)rowsPerBatchHintNumericUpDown.Value;
        }

        private void kiloPerBatchHintNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            bcpParams.HintKilobytesPerBatch = (long)kiloPerBatchHintNumericUpDown.Value;
        }

        private void tablockHintCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bcpParams.WithHintTablock = tablockHintCheckBox.Checked;
        }

        private void checkConstraintsHintCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bcpParams.WithHintCheckContraints = checkConstraintsHintCheckBox.Checked;
        }

        private void fireTriggersHintCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bcpParams.WithHintFireTriggers = fireTriggersHintCheckBox.Checked;
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            var pars = "";

            using (var fob = new OpenFileDialog())
            {
                fob.Filter = "BCP GUI Settings (*.bcpj)|*.bcpj|JSON Files (*.json)|*.json|All Files (*.*)|*.*";
                fob.FilterIndex = 1;
                fob.Title = Text;
                fob.ShowHelp = false;
                fob.Multiselect = false;
                fob.CheckFileExists = true;
                if (fob.ShowDialog() == DialogResult.Cancel) return;

                pars = System.IO.File.ReadAllText(fob.FileName);
            }
            SetDefaultValues();
            var bPars = JsonConvert.DeserializeObject<BCPParams>(pars);

            switch (bPars.Action)
            {
                case "In": inRadioButton.Checked = true; break;
                case "Out": outRadioButton.Checked = true; break;
                case "Format": formatRadioButton.Checked = true; break;
                case "Queryout": queryoutRadioButton.Checked = true; break;
            }

            sourceTextBox.Text = bPars.SourceOrTarget;
            dataFileTextBox.Text = bPars.DataFile;
            fileFormatCheckBox.Checked = bPars.WithFormatFile;
            formatFileTextBox.Text = bPars.FormatFile;
            formatXMLCheckBox.Checked = bPars.WithXMLFormatFile;
            serverTextBox.Text = bPars.Server;
            databaseTextBox.Text = bPars.Database;
            authTypeComboBox.SelectedIndex = authTypeComboBox.Items.IndexOf(bPars.Authentication);
            loginTextBox.Text = bPars.Login;
            passwordTextBox.Text = bPars.Password;
            SetNumericUpDown(packetSizeNumericUpDown, bPars.PacketSize, packetSizeCheckBox, bPars.WithPacketSize);
            SetNumericUpDown(batchSizeNumericUpDown, bPars.BatchSize, batchSizeCheckBox, bPars.WithBatchSize);
            SetNumericUpDown(firstRowNumericUpDown, bPars.FirstRow, firstRowCheckBox, bPars.WithFirstRow);
            SetNumericUpDown(lastRowNumericUpDown, bPars.LastRow, lastRowCheckBox, bPars.WithLastRow);
            SetNumericUpDown(maxErrorsNumericUpDown, bPars.MaxErrors, maxErrorsCheckBox, bPars.WithMaxErrors);
            errorFileCheckBox.Checked = bPars.WithErrorFile;
            errFileTextBox.Text = bPars.ErrorFile;
            inputFileCheckBox.Checked = bPars.WithInputFile;
            inputFileTextBox.Text = bPars.InputFile;
            outputFileCheckBox.Checked = bPars.WithOutputFile;
            outputFileTextBox.Text = bPars.OutputFile;

            var idx = FindItem(formatComboBox.Items, bPars.Format);
            if (idx != -1) formatComboBox.SelectedIndex = idx;

            codePageCheckBox.Checked = bPars.WithCodePage;
            idx = FindItem(codePageComboBox.Items, bPars.CodePage);
            if (idx != -1) codePageComboBox.SelectedIndex = idx;

            dataTypeVersionCheckBox.Checked = bPars.WithDataTypeVersion;
            idx = FindItem(dataTypeComboBox.Items, bPars.DataTypeVersion.ToString());
            if (idx != -1) dataTypeComboBox.SelectedIndex = idx;

            rowTerminatorCheckBox.Checked = bPars.WithRowTerminator;
            rowTerminatorTextBox.Text = bPars.RowTerminator;
            fieldTerminatorCheckBox.Checked = bPars.WithFieldTerminator;
            fieldTerminatorTextBox.Text = bPars.FieldTerminator;
            identityCheckBox.Checked = bPars.WithImportIdentity;
            keepNullCheckBox.Checked = bPars.WithKeepNull;
            quotedIdentifierCheckBox.Checked = bPars.WithQuotedIdentifiers;
            regionalFormatCheckBox.Checked = bPars.WithRegionalFormat;

            orderHintCheckBox.Checked = bPars.WithHintOrder;
            orderHintTextBox.Text = bPars.HintOrder;
            SetNumericUpDown(rowsPerBatchHintNumericUpDown, bPars.HintRowsPerBatch, rowsPerBatchHintCheckBox, bPars.WithHintRowsPerBatch);
            SetNumericUpDown(kiloPerBatchHintNumericUpDown, bPars.HintKilobytesPerBatch, kiloPerBatchHintCheckBox, bPars.WithHintKilobytesPerBatch);
            tablockHintCheckBox.Checked = bPars.WithHintTablock;
            checkConstraintsHintCheckBox.Checked = bPars.WithHintCheckContraints;
            fireTriggersHintCheckBox.Checked = bPars.WithHintFireTriggers;
        }
        private void resetButton_Click(object sender, EventArgs e)
        {
            SetDefaultValues();
        }
        #endregion
    }
}
