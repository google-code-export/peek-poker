namespace PeekPoker.PeekNPoke
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
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
            this.NumericFloatTextBox = new System.Windows.Forms.TextBox();
            this.LabelFloat = new System.Windows.Forms.Label();
            this.isSigned = new System.Windows.Forms.CheckBox();
            this.LabelInt8 = new System.Windows.Forms.Label();
            this.LabelInt16 = new System.Windows.Forms.Label();
            this.NumericInt32 = new System.Windows.Forms.NumericUpDown();
            this.NumericInt8 = new System.Windows.Forms.NumericUpDown();
            this.NumericInt16 = new System.Windows.Forms.NumericUpDown();
            this.LabelInt32 = new System.Windows.Forms.Label();
            this.labelSelAddress = new System.Windows.Forms.Label();
            this.SelAddress = new System.Windows.Forms.TextBox();
            this.newPeekButton = new System.Windows.Forms.Button();
            this.pokeButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.peekLengthTextBox = new System.Windows.Forms.TextBox();
            this.peekButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.peekPokeAddressTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.hexBox = new Be.Windows.Forms.HexBox();
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
            this.label27.Location = new System.Drawing.Point(363, 51);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(134, 15);
            this.label27.TabIndex = 54;
            this.label27.Text = "Peek/Poke Feedback:";
            // 
            // peekPokeFeedBackTextBox
            // 
            this.peekPokeFeedBackTextBox.Location = new System.Drawing.Point(500, 44);
            this.peekPokeFeedBackTextBox.Name = "peekPokeFeedBackTextBox";
            this.peekPokeFeedBackTextBox.ReadOnly = true;
            this.peekPokeFeedBackTextBox.Size = new System.Drawing.Size(130, 22);
            this.peekPokeFeedBackTextBox.TabIndex = 53;
            // 
            // debugGroupBox
            // 
            this.debugGroupBox.Controls.Add(this.unfreezeButton);
            this.debugGroupBox.Controls.Add(this.freezeButton);
            this.debugGroupBox.Location = new System.Drawing.Point(504, 97);
            this.debugGroupBox.Name = "debugGroupBox";
            this.debugGroupBox.Size = new System.Drawing.Size(141, 98);
            this.debugGroupBox.TabIndex = 52;
            this.debugGroupBox.TabStop = false;
            this.debugGroupBox.Text = "Debug Commands";
            // 
            // unfreezeButton
            // 
            this.unfreezeButton.Enabled = false;
            this.unfreezeButton.Location = new System.Drawing.Point(30, 65);
            this.unfreezeButton.Name = "unfreezeButton";
            this.unfreezeButton.Size = new System.Drawing.Size(87, 27);
            this.unfreezeButton.TabIndex = 1;
            this.unfreezeButton.Text = "Un-Freeze";
            this.unfreezeButton.UseVisualStyleBackColor = true;
            this.unfreezeButton.Click += new System.EventHandler(this.UnfreezeButtonClick);
            // 
            // freezeButton
            // 
            this.freezeButton.Location = new System.Drawing.Point(30, 31);
            this.freezeButton.Name = "freezeButton";
            this.freezeButton.Size = new System.Drawing.Size(87, 27);
            this.freezeButton.TabIndex = 0;
            this.freezeButton.Text = "Freeze";
            this.freezeButton.UseVisualStyleBackColor = true;
            this.freezeButton.Click += new System.EventHandler(this.FreezeButtonClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.NumericFloatTextBox);
            this.groupBox3.Controls.Add(this.LabelFloat);
            this.groupBox3.Controls.Add(this.isSigned);
            this.groupBox3.Controls.Add(this.LabelInt8);
            this.groupBox3.Controls.Add(this.LabelInt16);
            this.groupBox3.Controls.Add(this.NumericInt32);
            this.groupBox3.Controls.Add(this.NumericInt8);
            this.groupBox3.Controls.Add(this.NumericInt16);
            this.groupBox3.Controls.Add(this.LabelInt32);
            this.groupBox3.Location = new System.Drawing.Point(495, 203);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(182, 196);
            this.groupBox3.TabIndex = 51;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Value";
            // 
            // NumericFloatTextBox
            // 
            this.NumericFloatTextBox.Location = new System.Drawing.Point(63, 155);
            this.NumericFloatTextBox.Name = "NumericFloatTextBox";
            this.NumericFloatTextBox.Size = new System.Drawing.Size(95, 22);
            this.NumericFloatTextBox.TabIndex = 20;
            this.NumericFloatTextBox.Text = "0";
            this.NumericFloatTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LabelFloat
            // 
            this.LabelFloat.AutoSize = true;
            this.LabelFloat.Location = new System.Drawing.Point(20, 158);
            this.LabelFloat.Name = "LabelFloat";
            this.LabelFloat.Size = new System.Drawing.Size(37, 15);
            this.LabelFloat.TabIndex = 19;
            this.LabelFloat.Text = "Float";
            // 
            // isSigned
            // 
            this.isSigned.AutoSize = true;
            this.isSigned.Location = new System.Drawing.Point(39, 21);
            this.isSigned.Name = "isSigned";
            this.isSigned.Size = new System.Drawing.Size(109, 19);
            this.isSigned.TabIndex = 16;
            this.isSigned.Text = "Signed Values";
            this.isSigned.UseVisualStyleBackColor = true;
            this.isSigned.CheckedChanged += new System.EventHandler(this.IsSignedCheckedChanged);
            // 
            // LabelInt8
            // 
            this.LabelInt8.AutoSize = true;
            this.LabelInt8.Location = new System.Drawing.Point(15, 60);
            this.LabelInt8.Name = "LabelInt8";
            this.LabelInt8.Size = new System.Drawing.Size(44, 15);
            this.LabelInt8.TabIndex = 17;
            this.LabelInt8.Text = "(U)Int8";
            // 
            // LabelInt16
            // 
            this.LabelInt16.AutoSize = true;
            this.LabelInt16.Location = new System.Drawing.Point(6, 92);
            this.LabelInt16.Name = "LabelInt16";
            this.LabelInt16.Size = new System.Drawing.Size(52, 15);
            this.LabelInt16.TabIndex = 17;
            this.LabelInt16.Text = "(U)Int16";
            // 
            // NumericInt32
            // 
            this.NumericInt32.Location = new System.Drawing.Point(63, 123);
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
            this.NumericInt32.Size = new System.Drawing.Size(109, 22);
            this.NumericInt32.TabIndex = 18;
            this.NumericInt32.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericInt32.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumericIntKeyPress);
            // 
            // NumericInt8
            // 
            this.NumericInt8.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NumericInt8.Location = new System.Drawing.Point(63, 59);
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
            this.NumericInt8.Size = new System.Drawing.Size(109, 18);
            this.NumericInt8.TabIndex = 18;
            this.NumericInt8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericInt8.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumericIntKeyPress);
            // 
            // NumericInt16
            // 
            this.NumericInt16.Location = new System.Drawing.Point(63, 91);
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
            this.NumericInt16.Size = new System.Drawing.Size(109, 22);
            this.NumericInt16.TabIndex = 18;
            this.NumericInt16.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericInt16.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumericIntKeyPress);
            // 
            // LabelInt32
            // 
            this.LabelInt32.AutoSize = true;
            this.LabelInt32.Location = new System.Drawing.Point(6, 125);
            this.LabelInt32.Name = "LabelInt32";
            this.LabelInt32.Size = new System.Drawing.Size(52, 15);
            this.LabelInt32.TabIndex = 17;
            this.LabelInt32.Text = "(U)Int32";
            // 
            // labelSelAddress
            // 
            this.labelSelAddress.AutoSize = true;
            this.labelSelAddress.Location = new System.Drawing.Point(383, 15);
            this.labelSelAddress.Name = "labelSelAddress";
            this.labelSelAddress.Size = new System.Drawing.Size(114, 15);
            this.labelSelAddress.TabIndex = 50;
            this.labelSelAddress.Text = "Selected Address:";
            // 
            // SelAddress
            // 
            this.SelAddress.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.SelAddress.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.SelAddress.Location = new System.Drawing.Point(500, 13);
            this.SelAddress.Name = "SelAddress";
            this.SelAddress.ReadOnly = true;
            this.SelAddress.Size = new System.Drawing.Size(130, 22);
            this.SelAddress.TabIndex = 49;
            // 
            // newPeekButton
            // 
            this.newPeekButton.Location = new System.Drawing.Point(176, 44);
            this.newPeekButton.Name = "newPeekButton";
            this.newPeekButton.Size = new System.Drawing.Size(61, 25);
            this.newPeekButton.TabIndex = 46;
            this.newPeekButton.Text = "New";
            this.newPeekButton.UseVisualStyleBackColor = true;
            this.newPeekButton.Click += new System.EventHandler(this.NewPeekButtonClick);
            // 
            // pokeButton
            // 
            this.pokeButton.Location = new System.Drawing.Point(243, 9);
            this.pokeButton.Name = "pokeButton";
            this.pokeButton.Size = new System.Drawing.Size(61, 25);
            this.pokeButton.TabIndex = 43;
            this.pokeButton.Text = "Poke";
            this.pokeButton.UseVisualStyleBackColor = true;
            this.pokeButton.Click += new System.EventHandler(this.PokeButtonClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 15);
            this.label3.TabIndex = 45;
            this.label3.Text = "Length 0x:";
            // 
            // peekLengthTextBox
            // 
            this.peekLengthTextBox.Location = new System.Drawing.Point(87, 48);
            this.peekLengthTextBox.Name = "peekLengthTextBox";
            this.peekLengthTextBox.Size = new System.Drawing.Size(83, 22);
            this.peekLengthTextBox.TabIndex = 41;
            this.peekLengthTextBox.Text = "FFFF";
            this.peekLengthTextBox.Leave += new System.EventHandler(this.FixTheAddresses);
            // 
            // peekButton
            // 
            this.peekButton.Location = new System.Drawing.Point(176, 9);
            this.peekButton.Name = "peekButton";
            this.peekButton.Size = new System.Drawing.Size(61, 25);
            this.peekButton.TabIndex = 44;
            this.peekButton.Text = "Peek";
            this.peekButton.UseVisualStyleBackColor = true;
            this.peekButton.Click += new System.EventHandler(this.PeekButtonClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 15);
            this.label1.TabIndex = 42;
            this.label1.Text = "Address 0x:";
            // 
            // peekPokeAddressTextBox
            // 
            this.peekPokeAddressTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.peekPokeAddressTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.peekPokeAddressTextBox.Location = new System.Drawing.Point(87, 12);
            this.peekPokeAddressTextBox.Name = "peekPokeAddressTextBox";
            this.peekPokeAddressTextBox.Size = new System.Drawing.Size(83, 22);
            this.peekPokeAddressTextBox.TabIndex = 40;
            this.peekPokeAddressTextBox.Text = "C0000000";
            this.peekPokeAddressTextBox.Leave += new System.EventHandler(this.FixTheAddresses);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(84, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(392, 16);
            this.label7.TabIndex = 47;
            this.label7.Text = "00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F ";
            // 
            // hexBox
            // 
            // 
            // 
            // 
            this.hexBox.BuiltInContextMenu.CopyMenuItemText = "";
            this.hexBox.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hexBox.LineInfoForeColor = System.Drawing.Color.Empty;
            this.hexBox.LineInfoVisible = true;
            this.hexBox.Location = new System.Drawing.Point(2, 97);
            this.hexBox.Name = "hexBox";
            this.hexBox.SelectionBackColor = System.Drawing.Color.DodgerBlue;
            this.hexBox.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexBox.Size = new System.Drawing.Size(489, 302);
            this.hexBox.TabIndex = 33;
            this.hexBox.UseFixedBytesPerLine = true;
            this.hexBox.VScrollBarVisible = true;
            this.hexBox.SelectionStartChanged += new System.EventHandler(this.HexBoxSelectionStartChanged);
            this.hexBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HexBoxKeyDown);
            this.hexBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.HexBoxMouseUp);
            // 
            // PeekNPoke
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 402);
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
            this.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
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
        private System.Windows.Forms.Button newPeekButton;
        private System.Windows.Forms.Button pokeButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox peekLengthTextBox;
        private System.Windows.Forms.Button peekButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox peekPokeAddressTextBox;
        private System.Windows.Forms.Label LabelFloat;
        private System.Windows.Forms.Label label7;
        private Be.Windows.Forms.HexBox hexBox;
        private System.Windows.Forms.TextBox NumericFloatTextBox;
    }
}