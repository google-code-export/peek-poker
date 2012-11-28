namespace UFC3___Memory_Editor
{
    partial class Ufc3Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ufc3Form));
            this.ipAddressTextBox = new System.Windows.Forms.TextBox();
            this.pokeMemoryButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.customAttrTextBox = new System.Windows.Forms.TextBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.credNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.credNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // ipAddressTextBox
            // 
            this.ipAddressTextBox.Location = new System.Drawing.Point(157, 12);
            this.ipAddressTextBox.Name = "ipAddressTextBox";
            this.ipAddressTextBox.Size = new System.Drawing.Size(140, 20);
            this.ipAddressTextBox.TabIndex = 0;
            // 
            // pokeMemoryButton
            // 
            this.pokeMemoryButton.Enabled = false;
            this.pokeMemoryButton.Location = new System.Drawing.Point(224, 127);
            this.pokeMemoryButton.Name = "pokeMemoryButton";
            this.pokeMemoryButton.Size = new System.Drawing.Size(75, 45);
            this.pokeMemoryButton.TabIndex = 1;
            this.pokeMemoryButton.Text = "Poke Memory";
            this.pokeMemoryButton.UseVisualStyleBackColor = true;
            this.pokeMemoryButton.Click += new System.EventHandler(this.PokeMemoryButtonClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "Xbox 360\'s IP Address:";
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(224, 38);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 6;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.ConnectButtonClick);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.customAttrTextBox);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.credNumericUpDown);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(18, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 137);
            this.panel1.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(172, 14);
            this.label4.TabIndex = 7;
            this.label4.Text = "Custom Attributes/ Skills - Hex\r\n";
            // 
            // customAttrTextBox
            // 
            this.customAttrTextBox.Location = new System.Drawing.Point(4, 105);
            this.customAttrTextBox.MaxLength = 44;
            this.customAttrTextBox.Name = "customAttrTextBox";
            this.customAttrTextBox.Size = new System.Drawing.Size(191, 20);
            this.customAttrTextBox.TabIndex = 6;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(21, 36);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(89, 18);
            this.radioButton2.TabIndex = 5;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Default Max";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(21, 60);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(159, 18);
            this.radioButton1.TabIndex = 4;
            this.radioButton1.Text = "Custom Attributes/ Skills";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 14);
            this.label3.TabIndex = 3;
            this.label3.Text = "CRED :";
            // 
            // credNumericUpDown
            // 
            this.credNumericUpDown.Location = new System.Drawing.Point(83, 3);
            this.credNumericUpDown.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.credNumericUpDown.Name = "credNumericUpDown";
            this.credNumericUpDown.Size = new System.Drawing.Size(95, 20);
            this.credNumericUpDown.TabIndex = 0;
            // 
            // Ufc3Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 180);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pokeMemoryButton);
            this.Controls.Add(this.ipAddressTextBox);
            this.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Ufc3Form";
            this.Text = "UFC 3 - RealTime Editor";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.credNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ipAddressTextBox;
        private System.Windows.Forms.Button pokeMemoryButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown credNumericUpDown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox customAttrTextBox;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}

