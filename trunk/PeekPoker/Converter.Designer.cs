namespace PeekPoker
{
    partial class Converter
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
            this.endianTypeGroupBox = new System.Windows.Forms.GroupBox();
            this.littleEndianRadioButton = new System.Windows.Forms.RadioButton();
            this.BigEndianRadioButton = new System.Windows.Forms.RadioButton();
            this.converterClearButton = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.floatCalculatorTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.integer16CalculatorTextBox = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.integer32CalculatorTextBox = new System.Windows.Forms.TextBox();
            this.hexCalculatorTextBox = new System.Windows.Forms.TextBox();
            this.integer8CalculatorTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.endianTypeGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // endianTypeGroupBox
            // 
            this.endianTypeGroupBox.Controls.Add(this.littleEndianRadioButton);
            this.endianTypeGroupBox.Controls.Add(this.BigEndianRadioButton);
            this.endianTypeGroupBox.Location = new System.Drawing.Point(332, 10);
            this.endianTypeGroupBox.Name = "endianTypeGroupBox";
            this.endianTypeGroupBox.Size = new System.Drawing.Size(172, 47);
            this.endianTypeGroupBox.TabIndex = 15;
            this.endianTypeGroupBox.TabStop = false;
            this.endianTypeGroupBox.Text = "Endian Type";
            // 
            // littleEndianRadioButton
            // 
            this.littleEndianRadioButton.AutoSize = true;
            this.littleEndianRadioButton.Location = new System.Drawing.Point(85, 21);
            this.littleEndianRadioButton.Name = "littleEndianRadioButton";
            this.littleEndianRadioButton.Size = new System.Drawing.Size(47, 17);
            this.littleEndianRadioButton.TabIndex = 1;
            this.littleEndianRadioButton.Text = "Little";
            this.littleEndianRadioButton.UseVisualStyleBackColor = true;
            // 
            // BigEndianRadioButton
            // 
            this.BigEndianRadioButton.AutoSize = true;
            this.BigEndianRadioButton.Checked = true;
            this.BigEndianRadioButton.Location = new System.Drawing.Point(6, 21);
            this.BigEndianRadioButton.Name = "BigEndianRadioButton";
            this.BigEndianRadioButton.Size = new System.Drawing.Size(40, 17);
            this.BigEndianRadioButton.TabIndex = 0;
            this.BigEndianRadioButton.TabStop = true;
            this.BigEndianRadioButton.Text = "Big";
            this.BigEndianRadioButton.UseVisualStyleBackColor = true;
            // 
            // converterClearButton
            // 
            this.converterClearButton.Location = new System.Drawing.Point(417, 104);
            this.converterClearButton.Name = "converterClearButton";
            this.converterClearButton.Size = new System.Drawing.Size(75, 23);
            this.converterClearButton.TabIndex = 14;
            this.converterClearButton.Text = "Clear";
            this.converterClearButton.UseVisualStyleBackColor = true;
            this.converterClearButton.Click += new System.EventHandler(this.ConverterClearButtonClick);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(46, 108);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(33, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "Float:";
            // 
            // floatCalculatorTextBox
            // 
            this.floatCalculatorTextBox.Location = new System.Drawing.Point(93, 105);
            this.floatCalculatorTextBox.MaxLength = 3276700;
            this.floatCalculatorTextBox.Name = "floatCalculatorTextBox";
            this.floatCalculatorTextBox.Size = new System.Drawing.Size(145, 20);
            this.floatCalculatorTextBox.TabIndex = 12;
            this.floatCalculatorTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.floatCalculatorTextBox.TextChanged += new System.EventHandler(this.FloatToHex);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "Int16/Short:";
            // 
            // integer16CalculatorTextBox
            // 
            this.integer16CalculatorTextBox.Location = new System.Drawing.Point(93, 47);
            this.integer16CalculatorTextBox.MaxLength = 3276700;
            this.integer16CalculatorTextBox.Name = "integer16CalculatorTextBox";
            this.integer16CalculatorTextBox.Size = new System.Drawing.Size(145, 20);
            this.integer16CalculatorTextBox.TabIndex = 10;
            this.integer16CalculatorTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.integer16CalculatorTextBox.TextChanged += new System.EventHandler(this.Int16ToHex);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(244, 65);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(37, 13);
            this.label20.TabIndex = 6;
            this.label20.Text = "<<  >>";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Int32/Int:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(21, 22);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(54, 13);
            this.label22.TabIndex = 8;
            this.label22.Text = "Int8/Byte:";
            // 
            // integer32CalculatorTextBox
            // 
            this.integer32CalculatorTextBox.Location = new System.Drawing.Point(93, 77);
            this.integer32CalculatorTextBox.MaxLength = 3276700;
            this.integer32CalculatorTextBox.Name = "integer32CalculatorTextBox";
            this.integer32CalculatorTextBox.Size = new System.Drawing.Size(145, 20);
            this.integer32CalculatorTextBox.TabIndex = 1;
            this.integer32CalculatorTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.integer32CalculatorTextBox.TextChanged += new System.EventHandler(this.Int32ToHex);
            // 
            // hexCalculatorTextBox
            // 
            this.hexCalculatorTextBox.Location = new System.Drawing.Point(347, 62);
            this.hexCalculatorTextBox.Name = "hexCalculatorTextBox";
            this.hexCalculatorTextBox.Size = new System.Drawing.Size(145, 20);
            this.hexCalculatorTextBox.TabIndex = 4;
            this.hexCalculatorTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.hexCalculatorTextBox.TextChanged += new System.EventHandler(this.HexToInt);
            // 
            // integer8CalculatorTextBox
            // 
            this.integer8CalculatorTextBox.Location = new System.Drawing.Point(93, 19);
            this.integer8CalculatorTextBox.MaxLength = 3276700;
            this.integer8CalculatorTextBox.Name = "integer8CalculatorTextBox";
            this.integer8CalculatorTextBox.Size = new System.Drawing.Size(145, 20);
            this.integer8CalculatorTextBox.TabIndex = 7;
            this.integer8CalculatorTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.integer8CalculatorTextBox.TextChanged += new System.EventHandler(this.Int8ToHex);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(297, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Hex:";
            // 
            // Converter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 135);
            this.Controls.Add(this.endianTypeGroupBox);
            this.Controls.Add(this.converterClearButton);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.floatCalculatorTextBox);
            this.Controls.Add(this.integer8CalculatorTextBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.hexCalculatorTextBox);
            this.Controls.Add(this.integer16CalculatorTextBox);
            this.Controls.Add(this.integer32CalculatorTextBox);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Converter";
            this.Text = "Converter";
            this.endianTypeGroupBox.ResumeLayout(false);
            this.endianTypeGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox endianTypeGroupBox;
        private System.Windows.Forms.RadioButton littleEndianRadioButton;
        private System.Windows.Forms.RadioButton BigEndianRadioButton;
        private System.Windows.Forms.Button converterClearButton;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox floatCalculatorTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox integer16CalculatorTextBox;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox integer32CalculatorTextBox;
        private System.Windows.Forms.TextBox hexCalculatorTextBox;
        private System.Windows.Forms.TextBox integer8CalculatorTextBox;
        private System.Windows.Forms.Label label5;
    }
}