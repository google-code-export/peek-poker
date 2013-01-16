namespace PeekPoker
{
    partial class PeekNPoke
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
            this.label27 = new System.Windows.Forms.Label();
            this.peekPokeFeedBackTextBox = new System.Windows.Forms.TextBox();
            this.debugGroupBox = new System.Windows.Forms.GroupBox();
            this.unfreezeButton = new System.Windows.Forms.Button();
            this.freezeButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.isSigned = new System.Windows.Forms.CheckBox();
            this.LabelInt8 = new System.Windows.Forms.Label();
            this.LabelInt16 = new System.Windows.Forms.Label();
            this.NumericInt32 = new System.Windows.Forms.NumericUpDown();
            this.NumericInt8 = new System.Windows.Forms.NumericUpDown();
            this.NumericInt16 = new System.Windows.Forms.NumericUpDown();
            this.LabelInt32 = new System.Windows.Forms.Label();
            this.labelSelAddress = new System.Windows.Forms.Label();
            this.SelAddress = new System.Windows.Forms.TextBox();
            this.hexBox = new Be.Windows.Forms.HexBox();
            this.label7 = new System.Windows.Forms.Label();
            this.newPeekButton = new System.Windows.Forms.Button();
            this.pokeButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.peekLengthTextBox = new System.Windows.Forms.TextBox();
            this.peekButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.peekPokeAddressTextBox = new System.Windows.Forms.TextBox();
            this.debugGroupBox.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericInt32)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericInt8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericInt16)).BeginInit();
            this.SuspendLayout();
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(432, 50);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(116, 13);
            this.label27.TabIndex = 54;
            this.label27.Text = "Peek/Poke Feedback:";
            // 
            // peekPokeFeedBackTextBox
            // 
            this.peekPokeFeedBackTextBox.Location = new System.Drawing.Point(576, 43);
            this.peekPokeFeedBackTextBox.Name = "peekPokeFeedBackTextBox";
            this.peekPokeFeedBackTextBox.ReadOnly = true;
            this.peekPokeFeedBackTextBox.Size = new System.Drawing.Size(112, 20);
            this.peekPokeFeedBackTextBox.TabIndex = 53;
            // 
            // debugGroupBox
            // 
            this.debugGroupBox.Controls.Add(this.unfreezeButton);
            this.debugGroupBox.Controls.Add(this.freezeButton);
            this.debugGroupBox.Location = new System.Drawing.Point(516, 87);
            this.debugGroupBox.Name = "debugGroupBox";
            this.debugGroupBox.Size = new System.Drawing.Size(164, 85);
            this.debugGroupBox.TabIndex = 52;
            this.debugGroupBox.TabStop = false;
            this.debugGroupBox.Text = "Debug Commands";
            // 
            // unfreezeButton
            // 
            this.unfreezeButton.Enabled = false;
            this.unfreezeButton.Location = new System.Drawing.Point(38, 50);
            this.unfreezeButton.Name = "unfreezeButton";
            this.unfreezeButton.Size = new System.Drawing.Size(75, 23);
            this.unfreezeButton.TabIndex = 1;
            this.unfreezeButton.Text = "Un-Freeze";
            this.unfreezeButton.UseVisualStyleBackColor = true;
            this.unfreezeButton.Click += new System.EventHandler(this.UnfreezeButtonClick);
            // 
            // freezeButton
            // 
            this.freezeButton.Location = new System.Drawing.Point(38, 21);
            this.freezeButton.Name = "freezeButton";
            this.freezeButton.Size = new System.Drawing.Size(75, 23);
            this.freezeButton.TabIndex = 0;
            this.freezeButton.Text = "Freeze";
            this.freezeButton.UseVisualStyleBackColor = true;
            this.freezeButton.Click += new System.EventHandler(this.FreezeButtonClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.isSigned);
            this.groupBox3.Controls.Add(this.LabelInt8);
            this.groupBox3.Controls.Add(this.LabelInt16);
            this.groupBox3.Controls.Add(this.NumericInt32);
            this.groupBox3.Controls.Add(this.NumericInt8);
            this.groupBox3.Controls.Add(this.NumericInt16);
            this.groupBox3.Controls.Add(this.LabelInt32);
            this.groupBox3.Location = new System.Drawing.Point(500, 190);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(189, 143);
            this.groupBox3.TabIndex = 51;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Value";
            // 
            // isSigned
            // 
            this.isSigned.AutoSize = true;
            this.isSigned.Location = new System.Drawing.Point(71, 21);
            this.isSigned.Name = "isSigned";
            this.isSigned.Size = new System.Drawing.Size(94, 17);
            this.isSigned.TabIndex = 16;
            this.isSigned.Text = "Signed Values";
            this.isSigned.UseVisualStyleBackColor = true;
            this.isSigned.CheckedChanged += new System.EventHandler(this.IsSignedCheckedChanged);
            // 
            // LabelInt8
            // 
            this.LabelInt8.AutoSize = true;
            this.LabelInt8.Location = new System.Drawing.Point(13, 52);
            this.LabelInt8.Name = "LabelInt8";
            this.LabelInt8.Size = new System.Drawing.Size(39, 13);
            this.LabelInt8.TabIndex = 17;
            this.LabelInt8.Text = "(U)Int8";
            // 
            // LabelInt16
            // 
            this.LabelInt16.AutoSize = true;
            this.LabelInt16.Location = new System.Drawing.Point(5, 80);
            this.LabelInt16.Name = "LabelInt16";
            this.LabelInt16.Size = new System.Drawing.Size(45, 13);
            this.LabelInt16.TabIndex = 17;
            this.LabelInt16.Text = "(U)Int16";
            // 
            // NumericInt32
            // 
            this.NumericInt32.Location = new System.Drawing.Point(74, 106);
            this.NumericInt32.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.NumericInt32.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.NumericInt32.Name = "NumericInt32";
            this.NumericInt32.Size = new System.Drawing.Size(109, 20);
            this.NumericInt32.TabIndex = 18;
            this.NumericInt32.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericInt32.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumericIntKeyPress);
            // 
            // NumericInt8
            // 
            this.NumericInt8.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NumericInt8.Location = new System.Drawing.Point(74, 50);
            this.NumericInt8.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.NumericInt8.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.NumericInt8.Name = "NumericInt8";
            this.NumericInt8.Size = new System.Drawing.Size(109, 16);
            this.NumericInt8.TabIndex = 18;
            this.NumericInt8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericInt8.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumericIntKeyPress);
            // 
            // NumericInt16
            // 
            this.NumericInt16.Location = new System.Drawing.Point(74, 78);
            this.NumericInt16.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.NumericInt16.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.NumericInt16.Name = "NumericInt16";
            this.NumericInt16.Size = new System.Drawing.Size(109, 20);
            this.NumericInt16.TabIndex = 18;
            this.NumericInt16.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericInt16.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumericIntKeyPress);
            // 
            // LabelInt32
            // 
            this.LabelInt32.AutoSize = true;
            this.LabelInt32.Location = new System.Drawing.Point(5, 108);
            this.LabelInt32.Name = "LabelInt32";
            this.LabelInt32.Size = new System.Drawing.Size(45, 13);
            this.LabelInt32.TabIndex = 17;
            this.LabelInt32.Text = "(U)Int32";
            // 
            // labelSelAddress
            // 
            this.labelSelAddress.AutoSize = true;
            this.labelSelAddress.Location = new System.Drawing.Point(452, 19);
            this.labelSelAddress.Name = "labelSelAddress";
            this.labelSelAddress.Size = new System.Drawing.Size(93, 13);
            this.labelSelAddress.TabIndex = 50;
            this.labelSelAddress.Text = "Selected Address:";
            // 
            // SelAddress
            // 
            this.SelAddress.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.SelAddress.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.SelAddress.Location = new System.Drawing.Point(576, 16);
            this.SelAddress.Name = "SelAddress";
            this.SelAddress.ReadOnly = true;
            this.SelAddress.Size = new System.Drawing.Size(112, 20);
            this.SelAddress.TabIndex = 49;
            // 
            // hexBox
            // 
            this.hexBox.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hexBox.LineInfoForeColor = System.Drawing.Color.Empty;
            this.hexBox.LineInfoVisible = true;
            this.hexBox.Location = new System.Drawing.Point(7, 87);
            this.hexBox.Name = "hexBox";
            this.hexBox.SelectionBackColor = System.Drawing.Color.DodgerBlue;
            this.hexBox.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexBox.Size = new System.Drawing.Size(487, 246);
            this.hexBox.TabIndex = 33;
            this.hexBox.UseFixedBytesPerLine = true;
            this.hexBox.VScrollBarVisible = true;
            this.hexBox.SelectionStartChanged += new System.EventHandler(this.HexBoxSelectionStartChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Courier New", 10F);
            this.label7.Location = new System.Drawing.Point(95, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(392, 17);
            this.label7.TabIndex = 47;
            this.label7.Text = "00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F ";
            // 
            // newPeekButton
            // 
            this.newPeekButton.Location = new System.Drawing.Point(213, 40);
            this.newPeekButton.Name = "newPeekButton";
            this.newPeekButton.Size = new System.Drawing.Size(87, 27);
            this.newPeekButton.TabIndex = 46;
            this.newPeekButton.Text = "New";
            this.newPeekButton.UseVisualStyleBackColor = true;
            this.newPeekButton.Click += new System.EventHandler(this.NewPeekButtonClick);
            // 
            // pokeButton
            // 
            this.pokeButton.Location = new System.Drawing.Point(317, 10);
            this.pokeButton.Name = "pokeButton";
            this.pokeButton.Size = new System.Drawing.Size(87, 27);
            this.pokeButton.TabIndex = 43;
            this.pokeButton.Text = "Poke";
            this.pokeButton.UseVisualStyleBackColor = true;
            this.pokeButton.Click += new System.EventHandler(this.PokeButtonClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 45;
            this.label3.Text = "Length:";
            // 
            // peekLengthTextBox
            // 
            this.peekLengthTextBox.Location = new System.Drawing.Point(79, 42);
            this.peekLengthTextBox.Name = "peekLengthTextBox";
            this.peekLengthTextBox.Size = new System.Drawing.Size(116, 20);
            this.peekLengthTextBox.TabIndex = 41;
            this.peekLengthTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FixTheAddresses);
            this.peekLengthTextBox.Leave += new System.EventHandler(this.FixTheAddresses);
            // 
            // peekButton
            // 
            this.peekButton.Location = new System.Drawing.Point(213, 10);
            this.peekButton.Name = "peekButton";
            this.peekButton.Size = new System.Drawing.Size(87, 27);
            this.peekButton.TabIndex = 44;
            this.peekButton.Text = "Peek";
            this.peekButton.UseVisualStyleBackColor = true;
            this.peekButton.Click += new System.EventHandler(this.PeekButtonClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "Address:";
            // 
            // peekPokeAddressTextBox
            // 
            this.peekPokeAddressTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.peekPokeAddressTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.peekPokeAddressTextBox.Location = new System.Drawing.Point(79, 12);
            this.peekPokeAddressTextBox.Name = "peekPokeAddressTextBox";
            this.peekPokeAddressTextBox.Size = new System.Drawing.Size(116, 20);
            this.peekPokeAddressTextBox.TabIndex = 40;
            this.peekPokeAddressTextBox.Leave += new System.EventHandler(this.FixTheAddresses);
            // 
            // PeekNPoke
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 341);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.peekPokeFeedBackTextBox);
            this.Controls.Add(this.debugGroupBox);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.labelSelAddress);
            this.Controls.Add(this.SelAddress);
            this.Controls.Add(this.hexBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.newPeekButton);
            this.Controls.Add(this.pokeButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.peekLengthTextBox);
            this.Controls.Add(this.peekButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.peekPokeAddressTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PeekNPoke";
            this.Text = "PeeKNPoke";
            this.Load += new System.EventHandler(this.PeekNPokeLoad);
            this.debugGroupBox.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericInt32)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericInt8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericInt16)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox peekPokeFeedBackTextBox;
        private System.Windows.Forms.GroupBox debugGroupBox;
        private System.Windows.Forms.Button unfreezeButton;
        private System.Windows.Forms.Button freezeButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox isSigned;
        private System.Windows.Forms.Label LabelInt8;
        private System.Windows.Forms.Label LabelInt16;
        private System.Windows.Forms.NumericUpDown NumericInt32;
        private System.Windows.Forms.NumericUpDown NumericInt8;
        private System.Windows.Forms.NumericUpDown NumericInt16;
        private System.Windows.Forms.Label LabelInt32;
        private System.Windows.Forms.Label labelSelAddress;
        private System.Windows.Forms.TextBox SelAddress;
        private Be.Windows.Forms.HexBox hexBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button newPeekButton;
        private System.Windows.Forms.Button pokeButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox peekLengthTextBox;
        private System.Windows.Forms.Button peekButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox peekPokeAddressTextBox;
    }
}