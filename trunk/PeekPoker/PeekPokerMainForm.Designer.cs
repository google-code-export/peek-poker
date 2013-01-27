namespace PeekPoker
{
    partial class PeekPokerMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PeekPokerMainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.pluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHideOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultXbox360IPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.ipAddressTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.mainGroupBox = new System.Windows.Forms.GroupBox();
            this.displayOutsideParentBox = new System.Windows.Forms.CheckBox();
            this.pluginInfoButton = new System.Windows.Forms.Button();
            this.converterButton = new System.Windows.Forms.Button();
            this.SearchButton = new System.Windows.Forms.Button();
            this.dumpButton = new System.Windows.Forms.Button();
            this.peekNpokeButton = new System.Windows.Forms.Button();
            this.showHideOptionsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.mainGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pluginsToolStripMenuItem,
            this.showHideOptionsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(838, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // pluginsToolStripMenuItem
            // 
            this.pluginsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pluginsToolStripMenuItem.Image")));
            this.pluginsToolStripMenuItem.Name = "pluginsToolStripMenuItem";
            this.pluginsToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.pluginsToolStripMenuItem.Text = "Plugins";
            // 
            // showHideOptionsToolStripMenuItem
            // 
            this.showHideOptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backgroundImageToolStripMenuItem,
            this.defaultXbox360IPToolStripMenuItem,
            this.showHideOptionsToolStripMenuItem1});
            this.showHideOptionsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("showHideOptionsToolStripMenuItem.Image")));
            this.showHideOptionsToolStripMenuItem.Name = "showHideOptionsToolStripMenuItem";
            this.showHideOptionsToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.showHideOptionsToolStripMenuItem.Text = "Settings";
            // 
            // backgroundImageToolStripMenuItem
            // 
            this.backgroundImageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectToolStripMenuItem,
            this.clearToolStripMenuItem});
            this.backgroundImageToolStripMenuItem.Name = "backgroundImageToolStripMenuItem";
            this.backgroundImageToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.backgroundImageToolStripMenuItem.Text = "Background Image";
            // 
            // selectToolStripMenuItem
            // 
            this.selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            this.selectToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.selectToolStripMenuItem.Text = "Select..";
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            // 
            // defaultXbox360IPToolStripMenuItem
            // 
            this.defaultXbox360IPToolStripMenuItem.Name = "defaultXbox360IPToolStripMenuItem";
            this.defaultXbox360IPToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.defaultXbox360IPToolStripMenuItem.Text = "Default Xbox360 IP";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aboutToolStripMenuItem.Image")));
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem1Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 24);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 456);
            this.splitter1.TabIndex = 15;
            this.splitter1.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusStripLabel,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel1,
            this.StatusProgressBar});
            this.statusStrip1.Location = new System.Drawing.Point(3, 456);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(835, 24);
            this.statusStrip1.TabIndex = 17;
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
            this.toolStripStatusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(91, 19);
            this.toolStripStatusLabel1.Text = "Revision 8.0.0.3";
            // 
            // StatusProgressBar
            // 
            this.StatusProgressBar.Name = "StatusProgressBar";
            this.StatusProgressBar.Size = new System.Drawing.Size(100, 18);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(835, 79);
            this.panel1.TabIndex = 18;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.mainGroupBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(3, 103);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(193, 353);
            this.panel2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.connectButton);
            this.groupBox1.Controls.Add(this.ipAddressTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(8, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(169, 100);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // connectButton
            // 
            this.connectButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.connectButton.Location = new System.Drawing.Point(3, 49);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(163, 27);
            this.connectButton.TabIndex = 6;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.ConnectButtonClick);
            // 
            // ipAddressTextBox
            // 
            this.ipAddressTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.ipAddressTextBox.Location = new System.Drawing.Point(3, 29);
            this.ipAddressTextBox.Name = "ipAddressTextBox";
            this.ipAddressTextBox.Size = new System.Drawing.Size(163, 22);
            this.ipAddressTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(3, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "IP Address:";
            // 
            // mainGroupBox
            // 
            this.mainGroupBox.Controls.Add(this.displayOutsideParentBox);
            this.mainGroupBox.Controls.Add(this.pluginInfoButton);
            this.mainGroupBox.Controls.Add(this.converterButton);
            this.mainGroupBox.Controls.Add(this.SearchButton);
            this.mainGroupBox.Controls.Add(this.dumpButton);
            this.mainGroupBox.Controls.Add(this.peekNpokeButton);
            this.mainGroupBox.Enabled = false;
            this.mainGroupBox.Location = new System.Drawing.Point(8, 111);
            this.mainGroupBox.Name = "mainGroupBox";
            this.mainGroupBox.Size = new System.Drawing.Size(172, 213);
            this.mainGroupBox.TabIndex = 22;
            this.mainGroupBox.TabStop = false;
            this.mainGroupBox.Text = "Selection Options";
            // 
            // displayOutsideParentBox
            // 
            this.displayOutsideParentBox.AutoSize = true;
            this.displayOutsideParentBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.displayOutsideParentBox.Location = new System.Drawing.Point(3, 181);
            this.displayOutsideParentBox.Name = "displayOutsideParentBox";
            this.displayOutsideParentBox.Size = new System.Drawing.Size(166, 17);
            this.displayOutsideParentBox.TabIndex = 14;
            this.displayOutsideParentBox.Text = "Display outside Parent?";
            this.displayOutsideParentBox.UseVisualStyleBackColor = true;
            // 
            // pluginInfoButton
            // 
            this.pluginInfoButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.pluginInfoButton.Enabled = false;
            this.pluginInfoButton.Location = new System.Drawing.Point(3, 148);
            this.pluginInfoButton.Name = "pluginInfoButton";
            this.pluginInfoButton.Size = new System.Drawing.Size(166, 33);
            this.pluginInfoButton.TabIndex = 13;
            this.pluginInfoButton.Text = "Plugin Info";
            this.pluginInfoButton.UseVisualStyleBackColor = true;
            this.pluginInfoButton.Click += new System.EventHandler(this.pluginInfoButton_Click);
            // 
            // converterButton
            // 
            this.converterButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.converterButton.Location = new System.Drawing.Point(3, 115);
            this.converterButton.Name = "converterButton";
            this.converterButton.Size = new System.Drawing.Size(166, 33);
            this.converterButton.TabIndex = 12;
            this.converterButton.Text = "Converter";
            this.converterButton.UseVisualStyleBackColor = true;
            this.converterButton.Click += new System.EventHandler(this.converterButton_Click);
            // 
            // SearchButton
            // 
            this.SearchButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.SearchButton.Location = new System.Drawing.Point(3, 82);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(166, 33);
            this.SearchButton.TabIndex = 11;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButtonClick);
            // 
            // dumpButton
            // 
            this.dumpButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.dumpButton.Location = new System.Drawing.Point(3, 49);
            this.dumpButton.Name = "dumpButton";
            this.dumpButton.Size = new System.Drawing.Size(166, 33);
            this.dumpButton.TabIndex = 10;
            this.dumpButton.Text = "Dump";
            this.dumpButton.UseVisualStyleBackColor = true;
            this.dumpButton.Click += new System.EventHandler(this.dumpButton_Click);
            // 
            // peekNpokeButton
            // 
            this.peekNpokeButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.peekNpokeButton.Location = new System.Drawing.Point(3, 16);
            this.peekNpokeButton.Name = "peekNpokeButton";
            this.peekNpokeButton.Size = new System.Drawing.Size(166, 33);
            this.peekNpokeButton.TabIndex = 9;
            this.peekNpokeButton.Text = "Peek && Poke";
            this.peekNpokeButton.UseVisualStyleBackColor = true;
            this.peekNpokeButton.Click += new System.EventHandler(this.peekNpokeButton_Click);
            // 
            // showHideOptionsToolStripMenuItem1
            // 
            this.showHideOptionsToolStripMenuItem1.Name = "showHideOptionsToolStripMenuItem1";
            this.showHideOptionsToolStripMenuItem1.Size = new System.Drawing.Size(178, 22);
            this.showHideOptionsToolStripMenuItem1.Text = "Show/Hide Options";
            this.showHideOptionsToolStripMenuItem1.Click += new System.EventHandler(this.showHideOptionsToolStripMenuItem_Click);
            // 
            // PeekPokerMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(838, 480);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PeekPokerMainForm";
            this.Text = "Peek Poker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
            this.Load += new System.EventHandler(this.Form1Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.mainGroupBox.ResumeLayout(false);
            this.mainGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolTip toolTips;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ToolStripMenuItem pluginsToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        internal System.Windows.Forms.ToolStripProgressBar StatusProgressBar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem showHideOptionsToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.TextBox ipAddressTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox mainGroupBox;
        private System.Windows.Forms.CheckBox displayOutsideParentBox;
        private System.Windows.Forms.Button pluginInfoButton;
        private System.Windows.Forms.Button converterButton;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Button dumpButton;
        private System.Windows.Forms.Button peekNpokeButton;
        private System.Windows.Forms.ToolStripMenuItem backgroundImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultXbox360IPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHideOptionsToolStripMenuItem1;

    }
}

