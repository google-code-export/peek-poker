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
            this.panel1 = new System.Windows.Forms.Panel();
            this.hexBox = new Be.Windows.Forms.HexBox();
            this.label7 = new System.Windows.Forms.Label();
            this.newPeekButton = new System.Windows.Forms.Button();
            this.pokeButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.peekLengthTextBox = new System.Windows.Forms.TextBox();
            this.peekButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.PeekPokeAddressTextBox = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Transparent;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusStripLabel,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 426);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(507, 24);
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
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(122, 19);
            this.toolStripStatusLabel1.Text = "Revision 6 (Unofficial)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "IP Address:";
            // 
            // ipAddressTextBox
            // 
            this.ipAddressTextBox.Location = new System.Drawing.Point(99, 27);
            this.ipAddressTextBox.Name = "ipAddressTextBox";
            this.ipAddressTextBox.Size = new System.Drawing.Size(116, 22);
            this.ipAddressTextBox.TabIndex = 1;
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(233, 25);
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
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(0, 58);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(508, 363);
            this.tabControl1.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(500, 335);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Peek & Poke";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.hexBox);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.newPeekButton);
            this.panel1.Controls.Add(this.pokeButton);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.peekLengthTextBox);
            this.panel1.Controls.Add(this.peekButton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.PeekPokeAddressTextBox);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(8, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(668, 326);
            this.panel1.TabIndex = 12;
            // 
            // hexBox
            // 
            this.hexBox.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hexBox.LineInfoForeColor = System.Drawing.Color.Empty;
            this.hexBox.LineInfoVisible = true;
            this.hexBox.Location = new System.Drawing.Point(0, 77);
            this.hexBox.Name = "hexBox";
            this.hexBox.SelectionBackColor = System.Drawing.Color.DodgerBlue;
            this.hexBox.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexBox.Size = new System.Drawing.Size(487, 246);
            this.hexBox.TabIndex = 15;
            this.hexBox.UseFixedBytesPerLine = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Courier New", 10F);
            this.label7.Location = new System.Drawing.Point(83, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(392, 17);
            this.label7.TabIndex = 13;
            this.label7.Text = "00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F ";
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
            // pokeButton
            // 
            this.pokeButton.Location = new System.Drawing.Point(337, 1);
            this.pokeButton.Name = "pokeButton";
            this.pokeButton.Size = new System.Drawing.Size(87, 27);
            this.pokeButton.TabIndex = 7;
            this.pokeButton.Text = "Poke";
            this.pokeButton.UseVisualStyleBackColor = true;
            this.pokeButton.Click += new System.EventHandler(this.PokeButtonClick);
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
            // PeekPokeAddressTextBox
            // 
            this.PeekPokeAddressTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.PeekPokeAddressTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.PeekPokeAddressTextBox.Location = new System.Drawing.Point(99, 3);
            this.PeekPokeAddressTextBox.Name = "PeekPokeAddressTextBox";
            this.PeekPokeAddressTextBox.Size = new System.Drawing.Size(116, 22);
            this.PeekPokeAddressTextBox.TabIndex = 2;
            this.PeekPokeAddressTextBox.Leave += new System.EventHandler(this.FixTheAddresses);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
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
            this.tabPage3.Size = new System.Drawing.Size(500, 335);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Search Range";
            // 
            // dumpLengthTextBoxReadOnly
            // 
            this.dumpLengthTextBoxReadOnly.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.dumpLengthTextBoxReadOnly.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.dumpLengthTextBoxReadOnly.Location = new System.Drawing.Point(378, 6);
            this.dumpLengthTextBoxReadOnly.Name = "dumpLengthTextBoxReadOnly";
            this.dumpLengthTextBoxReadOnly.ReadOnly = true;
            this.dumpLengthTextBoxReadOnly.Size = new System.Drawing.Size(70, 22);
            this.dumpLengthTextBoxReadOnly.TabIndex = 14;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(269, 9);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(106, 15);
            this.label15.TabIndex = 13;
            this.label15.Text = "or Dump Length:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(258, 118);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(222, 30);
            this.label14.TabIndex = 12;
            this.label14.Text = "*NB: The larger the range the longer \r\nthe search will take";
            // 
            // searchRangeResultListView
            // 
            this.searchRangeResultListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.numberColumn,
            this.offsetHeader});
            this.searchRangeResultListView.FullRowSelect = true;
            this.searchRangeResultListView.GridLines = true;
            this.searchRangeResultListView.Location = new System.Drawing.Point(23, 86);
            this.searchRangeResultListView.Name = "searchRangeResultListView";
            this.searchRangeResultListView.Size = new System.Drawing.Size(201, 229);
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
            this.label13.Location = new System.Drawing.Point(9, 49);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(74, 15);
            this.label13.TabIndex = 10;
            this.label13.Text = "Search for :";
            // 
            // searchRangeValueTextBox
            // 
            this.searchRangeValueTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.searchRangeValueTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.searchRangeValueTextBox.Location = new System.Drawing.Point(89, 46);
            this.searchRangeValueTextBox.Name = "searchRangeValueTextBox";
            this.searchRangeValueTextBox.Size = new System.Drawing.Size(296, 22);
            this.searchRangeValueTextBox.TabIndex = 8;
            // 
            // searchRangeButton
            // 
            this.searchRangeButton.Location = new System.Drawing.Point(392, 31);
            this.searchRangeButton.Name = "searchRangeButton";
            this.searchRangeButton.Size = new System.Drawing.Size(88, 50);
            this.searchRangeButton.TabIndex = 9;
            this.searchRangeButton.Text = "Search Hex Value";
            this.searchRangeButton.UseVisualStyleBackColor = true;
            this.searchRangeButton.Click += new System.EventHandler(this.SearchRangeButtonClick);
            // 
            // endRangeAddressTextBox
            // 
            this.endRangeAddressTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.endRangeAddressTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.endRangeAddressTextBox.Location = new System.Drawing.Point(181, 6);
            this.endRangeAddressTextBox.Name = "endRangeAddressTextBox";
            this.endRangeAddressTextBox.Size = new System.Drawing.Size(82, 22);
            this.endRangeAddressTextBox.TabIndex = 7;
            this.endRangeAddressTextBox.Leave += new System.EventHandler(this.FixTheAddresses);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(143, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 15);
            this.label12.TabIndex = 6;
            this.label12.Text = "End:";
            // 
            // startRangeAddressTextBox
            // 
            this.startRangeAddressTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.startRangeAddressTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.startRangeAddressTextBox.Location = new System.Drawing.Point(51, 6);
            this.startRangeAddressTextBox.Name = "startRangeAddressTextBox";
            this.startRangeAddressTextBox.Size = new System.Drawing.Size(86, 22);
            this.startRangeAddressTextBox.TabIndex = 5;
            this.startRangeAddressTextBox.Leave += new System.EventHandler(this.FixTheAddresses);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "Start:";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.textBox3);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(500, 335);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Calculator";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(205, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = ">>";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(94, 33);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Decimal:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(287, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 15);
            this.label5.TabIndex = 5;
            this.label5.Text = "Hex:";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(327, 32);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 22);
            this.textBox3.TabIndex = 4;
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 18);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.aboutToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(507, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.aboutToolStripMenuItem.Text = "Select Game";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem1.Text = "About";
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(500, 335);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Dumping";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 24);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(500, 335);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Credit";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(507, 450);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ipAddressTextBox);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Peek Poker";
            this.Load += new System.EventHandler(this.Form1Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.Button pokeButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button newPeekButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox peekLengthTextBox;
        private System.Windows.Forms.Button peekButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PeekPokeAddressTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage tabPage2;
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
        private Be.Windows.Forms.HexBox hexBox;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;

    }
}

