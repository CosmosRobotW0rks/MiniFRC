namespace MiniFRC_ControlApp
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBoxEndpoint = new TextBox();
            label1 = new Label();
            textBoxSecuityKey = new TextBox();
            label2 = new Label();
            buttonConnect = new Button();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            radioButton2 = new RadioButton();
            radioButton3 = new RadioButton();
            label6 = new Label();
            numericUpDown5 = new NumericUpDown();
            radioButton1 = new RadioButton();
            numericUpDown6 = new NumericUpDown();
            label7 = new Label();
            label5 = new Label();
            checkBox1 = new CheckBox();
            numericUpDown4 = new NumericUpDown();
            button4 = new Button();
            label10 = new Label();
            numericUpDown3 = new NumericUpDown();
            numericUpDown2 = new NumericUpDown();
            label4 = new Label();
            numericUpDown1 = new NumericUpDown();
            label9 = new Label();
            button3 = new Button();
            button2 = new Button();
            label8 = new Label();
            label3 = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // textBoxEndpoint
            // 
            textBoxEndpoint.Location = new Point(98, 22);
            textBoxEndpoint.Name = "textBoxEndpoint";
            textBoxEndpoint.PlaceholderText = "1.1.1.1:1111";
            textBoxEndpoint.Size = new Size(161, 23);
            textBoxEndpoint.TabIndex = 0;
            // 
            // label1
            // 
            label1.Location = new Point(13, 22);
            label1.Name = "label1";
            label1.Size = new Size(79, 23);
            label1.TabIndex = 1;
            label1.Text = "Endpoint:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textBoxSecuityKey
            // 
            textBoxSecuityKey.Location = new Point(98, 51);
            textBoxSecuityKey.Name = "textBoxSecuityKey";
            textBoxSecuityKey.PasswordChar = '*';
            textBoxSecuityKey.Size = new Size(161, 23);
            textBoxSecuityKey.TabIndex = 2;
            textBoxSecuityKey.TextAlign = HorizontalAlignment.Center;
            // 
            // label2
            // 
            label2.Location = new Point(13, 51);
            label2.Name = "label2";
            label2.Size = new Size(79, 23);
            label2.TabIndex = 3;
            label2.Text = "Security Key:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // buttonConnect
            // 
            buttonConnect.Location = new Point(14, 80);
            buttonConnect.Name = "buttonConnect";
            buttonConnect.Size = new Size(245, 23);
            buttonConnect.TabIndex = 4;
            buttonConnect.Text = "Connect";
            buttonConnect.UseVisualStyleBackColor = true;
            buttonConnect.Click += buttonConnect_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textBoxEndpoint);
            groupBox1.Controls.Add(buttonConnect);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(textBoxSecuityKey);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(271, 115);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Login";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(radioButton2);
            groupBox2.Controls.Add(radioButton3);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(numericUpDown5);
            groupBox2.Controls.Add(radioButton1);
            groupBox2.Controls.Add(numericUpDown6);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(checkBox1);
            groupBox2.Controls.Add(numericUpDown4);
            groupBox2.Controls.Add(button4);
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(numericUpDown3);
            groupBox2.Controls.Add(numericUpDown2);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(numericUpDown1);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(button3);
            groupBox2.Controls.Add(button2);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(label3);
            groupBox2.Location = new Point(12, 145);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(397, 278);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "Match Operations";
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(203, 156);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(62, 19);
            radioButton2.TabIndex = 26;
            radioButton2.Text = "S. Final";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(274, 156);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(50, 19);
            radioButton3.TabIndex = 9;
            radioButton3.TabStop = true;
            radioButton3.Text = "Final";
            radioButton3.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.Location = new Point(330, 75);
            label6.Name = "label6";
            label6.Size = new Size(58, 29);
            label6.TabIndex = 25;
            label6.Text = "Blue 2";
            label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // numericUpDown5
            // 
            numericUpDown5.BackColor = Color.Blue;
            numericUpDown5.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            numericUpDown5.ForeColor = Color.White;
            numericUpDown5.Location = new Point(203, 75);
            numericUpDown5.Name = "numericUpDown5";
            numericUpDown5.Size = new Size(121, 29);
            numericUpDown5.TabIndex = 24;
            numericUpDown5.TextAlign = HorizontalAlignment.Center;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Checked = true;
            radioButton1.Location = new Point(145, 156);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(50, 19);
            radioButton1.TabIndex = 7;
            radioButton1.TabStop = true;
            radioButton1.Text = "Qual";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // numericUpDown6
            // 
            numericUpDown6.BackColor = Color.Blue;
            numericUpDown6.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            numericUpDown6.ForeColor = Color.White;
            numericUpDown6.Location = new Point(203, 40);
            numericUpDown6.Name = "numericUpDown6";
            numericUpDown6.Size = new Size(121, 29);
            numericUpDown6.TabIndex = 23;
            numericUpDown6.TextAlign = HorizontalAlignment.Center;
            // 
            // label7
            // 
            label7.Location = new Point(330, 40);
            label7.Name = "label7";
            label7.Size = new Size(58, 29);
            label7.TabIndex = 22;
            label7.Text = "Blue 1";
            label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            label5.Location = new Point(10, 75);
            label5.Name = "label5";
            label5.Size = new Size(58, 29);
            label5.TabIndex = 21;
            label5.Text = "Red 2";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(74, 156);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(73, 19);
            checkBox1.TabIndex = 19;
            checkBox1.Text = "Rematch";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // numericUpDown4
            // 
            numericUpDown4.BackColor = Color.Red;
            numericUpDown4.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            numericUpDown4.ForeColor = Color.White;
            numericUpDown4.Location = new Point(74, 75);
            numericUpDown4.Name = "numericUpDown4";
            numericUpDown4.Size = new Size(121, 29);
            numericUpDown4.TabIndex = 20;
            numericUpDown4.TextAlign = HorizontalAlignment.Center;
            // 
            // button4
            // 
            button4.Location = new Point(74, 238);
            button4.Name = "button4";
            button4.Size = new Size(248, 23);
            button4.TabIndex = 18;
            button4.Text = "Abort";
            button4.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            label10.Location = new Point(203, 105);
            label10.Name = "label10";
            label10.Size = new Size(121, 19);
            label10.TabIndex = 17;
            label10.Text = "Match Duration (S)";
            label10.TextAlign = ContentAlignment.BottomCenter;
            // 
            // numericUpDown3
            // 
            numericUpDown3.BackColor = Color.Red;
            numericUpDown3.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            numericUpDown3.ForeColor = Color.White;
            numericUpDown3.Location = new Point(74, 40);
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(121, 29);
            numericUpDown3.TabIndex = 7;
            numericUpDown3.TextAlign = HorizontalAlignment.Center;
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(202, 127);
            numericUpDown2.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(120, 23);
            numericUpDown2.TabIndex = 16;
            // 
            // label4
            // 
            label4.Location = new Point(10, 40);
            label4.Name = "label4";
            label4.Size = new Size(58, 29);
            label4.TabIndex = 3;
            label4.Text = "Red 1";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(74, 127);
            numericUpDown1.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(122, 23);
            numericUpDown1.TabIndex = 15;
            // 
            // label9
            // 
            label9.Location = new Point(75, 105);
            label9.Name = "label9";
            label9.Size = new Size(121, 19);
            label9.TabIndex = 12;
            label9.Text = "Match No";
            label9.TextAlign = ContentAlignment.BottomCenter;
            // 
            // button3
            // 
            button3.Location = new Point(74, 180);
            button3.Name = "button3";
            button3.Size = new Size(248, 23);
            button3.TabIndex = 11;
            button3.Text = "Load";
            button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(74, 209);
            button2.Name = "button2";
            button2.Size = new Size(248, 23);
            button2.TabIndex = 10;
            button2.Text = "Start";
            button2.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.Location = new Point(203, 19);
            label8.Name = "label8";
            label8.Size = new Size(121, 18);
            label8.TabIndex = 6;
            label8.Text = "Blue Allience";
            label8.TextAlign = ContentAlignment.BottomCenter;
            // 
            // label3
            // 
            label3.Location = new Point(74, 19);
            label3.Name = "label3";
            label3.Size = new Size(121, 18);
            label3.TabIndex = 1;
            label3.Text = "Red Allience";
            label3.TextAlign = ContentAlignment.BottomCenter;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(756, 450);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "FormMain";
            Text = "Form1";
            Load += FormMain_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown5).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown6).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TextBox textBoxEndpoint;
        private Label label1;
        private TextBox textBoxSecuityKey;
        private Label label2;
        private Button buttonConnect;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label3;
        private Label label4;
        private Label label8;
        private Button button2;
        private Button button3;
        private Label label9;
        private NumericUpDown numericUpDown1;
        private NumericUpDown numericUpDown2;
        private Label label10;
        private Button button4;
        private CheckBox checkBox1;
        private NumericUpDown numericUpDown3;
        private NumericUpDown numericUpDown4;
        private Label label5;
        private Label label6;
        private NumericUpDown numericUpDown5;
        private NumericUpDown numericUpDown6;
        private Label label7;
        private RadioButton radioButton1;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
    }
}
