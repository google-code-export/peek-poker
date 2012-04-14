namespace PeekPoker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.ipAddressTextBox = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pokeValueTextBox = new System.Windows.Forms.TextBox();
            this.pokeButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.pokeAddressTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.peekResultTextBox = new System.Windows.Forms.RichTextBox();
            this.newPeekButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.peekLengthTextBox = new System.Windows.Forms.TextBox();
            this.peekButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.peekAddressTextBox = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label16 = new System.Windows.Forms.Label();
            this.dumpLengthTextBoxReadOnly = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.searchRangeResultListView = new System.Windows.Forms.ListView();
            this.numberColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.offsetHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label13 = new System.Windows.Forms.Label();
            this.searchRangeValueTextBox = new System.Windows.Forms.TextBox();
            this.searchRangeButton = new System.Windows.Forms.Button();
            this.endRangeAddressTextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.startRangeAddressTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Transparent;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusStripLabel,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 408);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(693, 24);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusStripLabel
            // 
            this.statusStripLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.statusStripLabel.Name = "statusStripLabel";
            this.statusStripLabel.Size = new System.Drawing.Size(30, 19);
            this.statusStripLabel.Text = "Idle";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabel2.IsLink = true;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(118, 19);
            this.toolStripStatusLabel2.Text = "www.360haven.com";
            this.toolStripStatusLabel2.Click += new System.EventHandler(this.ToolStripStatusLabel2Click);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(60, 19);
            this.toolStripStatusLabel1.Text = "Revision 5";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "IP Address:";
            // 
            // ipAddressTextBox
            // 
            this.ipAddressTextBox.Location = new System.Drawing.Point(112, 14);
            this.ipAddressTextBox.Name = "ipAddressTextBox";
            this.ipAddressTextBox.Size = new System.Drawing.Size(116, 22);
            this.ipAddressTextBox.TabIndex = 1;
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(246, 12);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(87, 27);
            this.connectButton.TabIndex = 6;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.ConnectButtonClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 42);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(690, 363);
            this.tabControl1.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(682, 335);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Peek & Poke";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.pokeValueTextBox);
            this.panel2.Controls.Add(this.pokeButton);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.pokeAddressTextBox);
            this.panel2.Enabled = false;
            this.panel2.Location = new System.Drawing.Point(347, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(331, 326);
            this.panel2.TabIndex = 13;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(-1, 82);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 240);
            this.label10.TabIndex = 15;
            this.label10.Text = "0x00\r\n0x10\r\n0x20\r\n0x30\r\n0x40\r\n0x50\r\n0x60\r\n0x70\r\n0x80\r\n0x90\r\n0xA0\r\n0xB0\r\n0xC0\r\n0xD" +
    "0\r\n0xE0\r\n0xF0\r\n";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(41, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(265, 15);
            this.label8.TabIndex = 14;
            this.label8.Text = " 000102030405060708090A0B0C0D0E0F";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(157, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "Value:";
            // 
            // pokeValueTextBox
            // 
            this.pokeValueTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pokeValueTextBox.Location = new System.Drawing.Point(44, 82);
            this.pokeValueTextBox.Multiline = true;
            this.pokeValueTextBox.Name = "pokeValueTextBox";
            this.pokeValueTextBox.Size = new System.Drawing.Size(262, 240);
            this.pokeValueTextBox.TabIndex = 9;
            // 
            // pokeButton
            // 
            this.pokeButton.Location = new System.Drawing.Point(233, 1);
            this.pokeButton.Name = "pokeButton";
            this.pokeButton.Size = new System.Drawing.Size(87, 27);
            this.pokeButton.TabIndex = 7;
            this.pokeButton.Text = "Poke";
            this.pokeButton.UseVisualStyleBackColor = true;
            this.pokeButton.Click += new System.EventHandler(this.PokeButtonClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 15);
            this.label5.TabIndex = 5;
            this.label5.Text = "Address:";
            // 
            // pokeAddressTextBox
            // 
            this.pokeAddressTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.pokeAddressTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.pokeAddressTextBox.Location = new System.Drawing.Point(99, 3);
            this.pokeAddressTextBox.Name = "pokeAddressTextBox";
            this.pokeAddressTextBox.Size = new System.Drawing.Size(116, 22);
            this.pokeAddressTextBox.TabIndex = 4;
            this.pokeAddressTextBox.Leave += new System.EventHandler(this.FixTheAddresses);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.peekResultTextBox);
            this.panel1.Controls.Add(this.newPeekButton);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.peekLengthTextBox);
            this.panel1.Controls.Add(this.peekButton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.peekAddressTextBox);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(8, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(333, 326);
            this.panel1.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 82);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 240);
            this.label9.TabIndex = 14;
            this.label9.Text = "0x00\r\n0x10\r\n0x20\r\n0x30\r\n0x40\r\n0x50\r\n0x60\r\n0x70\r\n0x80\r\n0x90\r\n0xA0\r\n0xB0\r\n0xC0\r\n0xD" +
    "0\r\n0xE0\r\n0xF0\r\n";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(45, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(265, 15);
            this.label7.TabIndex = 13;
            this.label7.Text = " 000102030405060708090A0B0C0D0E0F";
            // 
            // peekResultTextBox
            // 
            this.peekResultTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.peekResultTextBox.Location = new System.Drawing.Point(48, 82);
            this.peekResultTextBox.Name = "peekResultTextBox";
            this.peekResultTextBox.Size = new System.Drawing.Size(262, 243);
            this.peekResultTextBox.TabIndex = 6;
            this.peekResultTextBox.Text = "";
            // 
            // newPeekButton
            // 
            this.newPeekButton.Location = new System.Drawing.Point(233, 31);
            this.newPeekButton.Name = "newPeekButton";
            this.newPeekButton.Size = new System.Drawing.Size(87, 27);
            this.newPeekButton.TabIndex = 11;
            this.newPeekButton.Text = "New";
            this.newPeekButton.UseVisualStyleBackColor = true;
            this.newPeekButton.Click += new System.EventHandler(this.NewPeekButtonClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "Length:";
            // 
            // peekLengthTextBox
            // 
            this.peekLengthTextBox.Location = new System.Drawing.Point(99, 33);
            this.peekLengthTextBox.Name = "peekLengthTextBox";
            this.peekLengthTextBox.Size = new System.Drawing.Size(116, 22);
            this.peekLengthTextBox.TabIndex = 3;
            this.peekLengthTextBox.Leave += new System.EventHandler(this.FixTheAddresses);
            // 
            // peekButton
            // 
            this.peekButton.Location = new System.Drawing.Point(233, 1);
            this.peekButton.Name = "peekButton";
            this.peekButton.Size = new System.Drawing.Size(87, 27);
            this.peekButton.TabIndex = 7;
            this.peekButton.Text = "Peek";
            this.peekButton.UseVisualStyleBackColor = true;
            this.peekButton.Click += new System.EventHandler(this.PeekButtonClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Address:";
            // 
            // peekAddressTextBox
            // 
            this.peekAddressTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.peekAddressTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.peekAddressTextBox.Location = new System.Drawing.Point(99, 3);
            this.peekAddressTextBox.Name = "peekAddressTextBox";
            this.peekAddressTextBox.Size = new System.Drawing.Size(116, 22);
            this.peekAddressTextBox.TabIndex = 2;
            this.peekAddressTextBox.Leave += new System.EventHandler(this.FixTheAddresses);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tabPage3.Controls.Add(this.label16);
            this.tabPage3.Controls.Add(this.dumpLengthTextBoxReadOnly);
            this.tabPage3.Controls.Add(this.label15);
            this.tabPage3.Controls.Add(this.label14);
            this.tabPage3.Controls.Add(this.searchRangeResultListView);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.searchRangeValueTextBox);
            this.tabPage3.Controls.Add(this.searchRangeButton);
            this.tabPage3.Controls.Add(this.endRangeAddressTextBox);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.startRangeAddressTextBox);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(682, 335);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Search Range";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(48, 28);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(216, 30);
            this.label16.TabIndex = 15;
            this.label16.Text = "Example:\r\n0xC0000000 <--To--> 0xC000FFFF";
            // 
            // dumpLengthTextBoxReadOnly
            // 
            this.dumpLengthTextBoxReadOnly.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.dumpLengthTextBoxReadOnly.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.dumpLengthTextBoxReadOnly.Location = new System.Drawing.Point(122, 94);
            this.dumpLengthTextBoxReadOnly.Name = "dumpLengthTextBoxReadOnly";
            this.dumpLengthTextBoxReadOnly.ReadOnly = true;
            this.dumpLengthTextBoxReadOnly.Size = new System.Drawing.Size(98, 22);
            this.dumpLengthTextBoxReadOnly.TabIndex = 14;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(26, 97);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(90, 15);
            this.label15.TabIndex = 13;
            this.label15.Text = "Dump Length:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(48, 200);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(337, 15);
            this.label14.TabIndex = 12;
            this.label14.Text = "*NB: The larger the range the longer the search will take";
            // 
            // searchRangeResultListView
            // 
            this.searchRangeResultListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.numberColumn,
            this.offsetHeader});
            this.searchRangeResultListView.FullRowSelect = true;
            this.searchRangeResultListView.GridLines = true;
            this.searchRangeResultListView.Location = new System.Drawing.Point(478, 9);
            this.searchRangeResultListView.Name = "searchRangeResultListView";
            this.searchRangeResultListView.Size = new System.Drawing.Size(201, 320);
            this.searchRangeResultListView.TabIndex = 11;
            this.searchRangeResultListView.UseCompatibleStateImageBehavior = false;
            this.searchRangeResultListView.View = System.Windows.Forms.View.Details;
            // 
            // numberColumn
            // 
            this.numberColumn.Text = "Number";
            this.numberColumn.Width = 58;
            // 
            // offsetHeader
            // 
            this.offsetHeader.Text = "Offset";
            this.offsetHeader.Width = 128;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(42, 122);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(74, 15);
            this.label13.TabIndex = 10;
            this.label13.Text = "Search for :";
            // 
            // searchRangeValueTextBox
            // 
            this.searchRangeValueTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.searchRangeValueTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.searchRangeValueTextBox.Location = new System.Drawing.Point(122, 119);
            this.searchRangeValueTextBox.Name = "searchRangeValueTextBox";
            this.searchRangeValueTextBox.Size = new System.Drawing.Size(296, 22);
            this.searchRangeValueTextBox.TabIndex = 8;
            // 
            // searchRangeButton
            // 
            this.searchRangeButton.Location = new System.Drawing.Point(302, 147);
            this.searchRangeButton.Name = "searchRangeButton";
            this.searchRangeButton.Size = new System.Drawing.Size(88, 50);
            this.searchRangeButton.TabIndex = 9;
            this.searchRangeButton.Text = "Search Range";
            this.searchRangeButton.UseVisualStyleBackColor = true;
            this.searchRangeButton.Click += new System.EventHandler(this.SearchRangeButtonClick);
            // 
            // endRangeAddressTextBox
            // 
            this.endRangeAddressTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.endRangeAddressTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.endRangeAddressTextBox.Location = new System.Drawing.Point(285, 66);
            this.endRangeAddressTextBox.Name = "endRangeAddressTextBox";
            this.endRangeAddressTextBox.Size = new System.Drawing.Size(98, 22);
            this.endRangeAddressTextBox.TabIndex = 7;
            this.endRangeAddressTextBox.Leave += new System.EventHandler(this.FixTheAddresses);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(226, 73);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 15);
            this.label12.TabIndex = 6;
            this.label12.Text = "≥ and ≤";
            // 
            // startRangeAddressTextBox
            // 
            this.startRangeAddressTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.startRangeAddressTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.startRangeAddressTextBox.Location = new System.Drawing.Point(122, 66);
            this.startRangeAddressTextBox.Name = "startRangeAddressTextBox";
            this.startRangeAddressTextBox.Size = new System.Drawing.Size(98, 22);
            this.startRangeAddressTextBox.TabIndex = 5;
            this.startRangeAddressTextBox.Leave += new System.EventHandler(this.FixTheAddresses);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "Value Within:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(682, 335);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Calculator";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(230, 161);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(251, 15);
            this.label11.TabIndex = 0;
            this.label11.Text = "I don\'t have time to implement this yet :P";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(693, 432);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ipAddressTextBox);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Peek Poker";
            this.Load += new System.EventHandler(this.Form1Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ipAddressTextBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox pokeValueTextBox;
        private System.Windows.Forms.Button pokeButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox pokeAddressTextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button newPeekButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox peekLengthTextBox;
        private System.Windows.Forms.Button peekButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox peekAddressTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox peekResultTextBox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ListView searchRangeResultListView;
        private System.Windows.Forms.ColumnHeader numberColumn;
        private System.Windows.Forms.ColumnHeader offsetHeader;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox searchRangeValueTextBox;
        private System.Windows.Forms.Button searchRangeButton;
        private System.Windows.Forms.TextBox endRangeAddressTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox startRangeAddressTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox dumpLengthTextBoxReadOnly;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;

    }
}

