namespace PeekPoker
{
    partial class Search
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.resultGrid = new System.Windows.Forms.DataGridView();
            this.resultRefreshButton = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.stopSearchButton = new System.Windows.Forms.Button();
            this.searchRangeButton = new System.Windows.Forms.Button();
            this.startRangeAddressTextBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.searchRangeValueTextBox = new System.Windows.Forms.TextBox();
            this.endRangeAddressTextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.resultGrid);
            this.groupBox2.Controls.Add(this.resultRefreshButton);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Location = new System.Drawing.Point(12, 96);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(674, 163);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Result/s";
            // 
            // resultGrid
            // 
            this.resultGrid.AllowUserToAddRows = false;
            this.resultGrid.AllowUserToDeleteRows = false;
            this.resultGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.resultGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.resultGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.resultGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultGrid.Location = new System.Drawing.Point(6, 18);
            this.resultGrid.MultiSelect = false;
            this.resultGrid.Name = "resultGrid";
            this.resultGrid.ReadOnly = true;
            this.resultGrid.RowHeadersVisible = false;
            this.resultGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.resultGrid.Size = new System.Drawing.Size(424, 138);
            this.resultGrid.TabIndex = 14;
            this.resultGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.ResultGridCellValueChanged);
            // 
            // resultRefreshButton
            // 
            this.resultRefreshButton.Location = new System.Drawing.Point(522, 120);
            this.resultRefreshButton.Name = "resultRefreshButton";
            this.resultRefreshButton.Size = new System.Drawing.Size(76, 36);
            this.resultRefreshButton.TabIndex = 13;
            this.resultRefreshButton.Text = "Refresh";
            this.resultRefreshButton.UseVisualStyleBackColor = true;
            this.resultRefreshButton.Click += new System.EventHandler(this.ResultRefreshClick);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(451, 18);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(174, 52);
            this.label14.TabIndex = 12;
            this.label14.Text = "NOTE:\r\nThe larger the range the longer \r\nthe search will take.\r\nCannot Stop searc" +
    "h while dumping.";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.stopSearchButton);
            this.groupBox1.Controls.Add(this.searchRangeButton);
            this.groupBox1.Controls.Add(this.startRangeAddressTextBox);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.searchRangeValueTextBox);
            this.groupBox1.Controls.Add(this.endRangeAddressTextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(668, 78);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Range Selection Options";
            // 
            // stopSearchButton
            // 
            this.stopSearchButton.Enabled = false;
            this.stopSearchButton.Location = new System.Drawing.Point(598, 21);
            this.stopSearchButton.Name = "stopSearchButton";
            this.stopSearchButton.Size = new System.Drawing.Size(64, 47);
            this.stopSearchButton.TabIndex = 18;
            this.stopSearchButton.Text = "Stop Search";
            this.stopSearchButton.UseVisualStyleBackColor = true;
            this.stopSearchButton.Click += new System.EventHandler(this.StopSearchButtonClick);
            // 
            // searchRangeButton
            // 
            this.searchRangeButton.Location = new System.Drawing.Point(499, 21);
            this.searchRangeButton.Name = "searchRangeButton";
            this.searchRangeButton.Size = new System.Drawing.Size(93, 47);
            this.searchRangeButton.TabIndex = 9;
            this.searchRangeButton.Text = "Search Hex Value";
            this.searchRangeButton.UseVisualStyleBackColor = true;
            this.searchRangeButton.Click += new System.EventHandler(this.SearchRangeButtonClick);
            // 
            // startRangeAddressTextBox
            // 
            this.startRangeAddressTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.startRangeAddressTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.startRangeAddressTextBox.Location = new System.Drawing.Point(88, 22);
            this.startRangeAddressTextBox.Name = "startRangeAddressTextBox";
            this.startRangeAddressTextBox.Size = new System.Drawing.Size(126, 20);
            this.startRangeAddressTextBox.TabIndex = 5;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 51);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(62, 13);
            this.label13.TabIndex = 10;
            this.label13.Text = "Search for :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Start Offset:";
            // 
            // searchRangeValueTextBox
            // 
            this.searchRangeValueTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.searchRangeValueTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.searchRangeValueTextBox.Location = new System.Drawing.Point(87, 48);
            this.searchRangeValueTextBox.Name = "searchRangeValueTextBox";
            this.searchRangeValueTextBox.Size = new System.Drawing.Size(406, 20);
            this.searchRangeValueTextBox.TabIndex = 8;
            this.searchRangeValueTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchRangeValueTextBoxKeyUp);
            // 
            // endRangeAddressTextBox
            // 
            this.endRangeAddressTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.endRangeAddressTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.endRangeAddressTextBox.Location = new System.Drawing.Point(270, 22);
            this.endRangeAddressTextBox.Name = "endRangeAddressTextBox";
            this.endRangeAddressTextBox.Size = new System.Drawing.Size(163, 20);
            this.endRangeAddressTextBox.TabIndex = 7;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(235, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 13);
            this.label12.TabIndex = 19;
            this.label12.Text = "End:";
            // 
            // Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 260);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Search";
            this.Text = "Search";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView resultGrid;
        private System.Windows.Forms.Button resultRefreshButton;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button stopSearchButton;
        private System.Windows.Forms.Button searchRangeButton;
        private System.Windows.Forms.TextBox startRangeAddressTextBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox searchRangeValueTextBox;
        private System.Windows.Forms.TextBox endRangeAddressTextBox;
        private System.Windows.Forms.Label label12;
    }
}