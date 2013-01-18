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
            this.label2 = new System.Windows.Forms.Label();
            this.ipAddressTextBox = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.pluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.pluginInfoButton = new System.Windows.Forms.Button();
            this.converterButton = new System.Windows.Forms.Button();
            this.SearchButton = new System.Windows.Forms.Button();
            this.dumpButton = new System.Windows.Forms.Button();
            this.peekNpokeButton = new System.Windows.Forms.Button();
            this.displayOutsideParentBox = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "IP Address:";
            // 
            // ipAddressTextBox
            // 
            this.ipAddressTextBox.Location = new System.Drawing.Point(80, 6);
            this.ipAddressTextBox.Name = "ipAddressTextBox";
            this.ipAddressTextBox.Size = new System.Drawing.Size(116, 22);
            this.ipAddressTextBox.TabIndex = 1;
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(202, 3);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(87, 27);
            this.connectButton.TabIndex = 6;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.ConnectButtonClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pluginsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(702, 24);
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
            this.splitter1.Size = new System.Drawing.Size(3, 282);
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
            this.statusStrip1.Location = new System.Drawing.Point(3, 282);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(699, 24);
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
            this.toolStripStatusLabel1.Text = "Revision 8.0.0.0";
            // 
            // StatusProgressBar
            // 
            this.StatusProgressBar.Name = "StatusProgressBar";
            this.StatusProgressBar.Size = new System.Drawing.Size(100, 18);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.ipAddressTextBox);
            this.panel1.Controls.Add(this.connectButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(699, 97);
            this.panel1.TabIndex = 18;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.displayOutsideParentBox);
            this.groupBox5.Controls.Add(this.pluginInfoButton);
            this.groupBox5.Controls.Add(this.converterButton);
            this.groupBox5.Controls.Add(this.SearchButton);
            this.groupBox5.Controls.Add(this.dumpButton);
            this.groupBox5.Controls.Add(this.peekNpokeButton);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox5.Location = new System.Drawing.Point(0, 41);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(697, 54);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Selection Options";
            // 
            // pluginInfoButton
            // 
            this.pluginInfoButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.pluginInfoButton.Location = new System.Drawing.Point(391, 18);
            this.pluginInfoButton.Name = "pluginInfoButton";
            this.pluginInfoButton.Size = new System.Drawing.Size(119, 33);
            this.pluginInfoButton.TabIndex = 13;
            this.pluginInfoButton.Text = "Plugin Info";
            this.pluginInfoButton.UseVisualStyleBackColor = true;
            this.pluginInfoButton.Click += new System.EventHandler(this.pluginInfoButton_Click);
            // 
            // converterButton
            // 
            this.converterButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.converterButton.Location = new System.Drawing.Point(294, 18);
            this.converterButton.Name = "converterButton";
            this.converterButton.Size = new System.Drawing.Size(97, 33);
            this.converterButton.TabIndex = 12;
            this.converterButton.Text = "Converter";
            this.converterButton.UseVisualStyleBackColor = true;
            this.converterButton.Click += new System.EventHandler(this.converterButton_Click);
            // 
            // SearchButton
            // 
            this.SearchButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.SearchButton.Location = new System.Drawing.Point(197, 18);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(97, 33);
            this.SearchButton.TabIndex = 11;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButtonClick);
            // 
            // dumpButton
            // 
            this.dumpButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.dumpButton.Location = new System.Drawing.Point(100, 18);
            this.dumpButton.Name = "dumpButton";
            this.dumpButton.Size = new System.Drawing.Size(97, 33);
            this.dumpButton.TabIndex = 10;
            this.dumpButton.Text = "Dump";
            this.dumpButton.UseVisualStyleBackColor = true;
            this.dumpButton.Click += new System.EventHandler(this.dumpButton_Click);
            // 
            // peekNpokeButton
            // 
            this.peekNpokeButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.peekNpokeButton.Location = new System.Drawing.Point(3, 18);
            this.peekNpokeButton.Name = "peekNpokeButton";
            this.peekNpokeButton.Size = new System.Drawing.Size(97, 33);
            this.peekNpokeButton.TabIndex = 9;
            this.peekNpokeButton.Text = "Peek && Poke";
            this.peekNpokeButton.UseVisualStyleBackColor = true;
            this.peekNpokeButton.Click += new System.EventHandler(this.peekNpokeButton_Click);
            // 
            // displayOutsideParentBox
            // 
            this.displayOutsideParentBox.AutoSize = true;
            this.displayOutsideParentBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.displayOutsideParentBox.Location = new System.Drawing.Point(510, 18);
            this.displayOutsideParentBox.Name = "displayOutsideParentBox";
            this.displayOutsideParentBox.Size = new System.Drawing.Size(162, 33);
            this.displayOutsideParentBox.TabIndex = 14;
            this.displayOutsideParentBox.Text = "Display outside Parent?";
            this.displayOutsideParentBox.UseVisualStyleBackColor = true;
            // 
            // PeekPokerMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(702, 306);
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
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ipAddressTextBox;
        private System.Windows.Forms.Button connectButton;
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
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button peekNpokeButton;
        private System.Windows.Forms.Button pluginInfoButton;
        private System.Windows.Forms.Button converterButton;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Button dumpButton;
        private System.Windows.Forms.CheckBox displayOutsideParentBox;

    }
}

