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
            groupBoxLogin = new GroupBox();
            groupBox2 = new GroupBox();
            checkBoxMatchPractice = new CheckBox();
            radioButtonMatchSFinal = new RadioButton();
            radioButtonMatchFinal = new RadioButton();
            label6 = new Label();
            numericUpDownMatchBlueAllienceTeam2 = new NumericUpDown();
            radioButtonMatchQual = new RadioButton();
            numericUpDownMatchBlueAllienceTeam1 = new NumericUpDown();
            label7 = new Label();
            label5 = new Label();
            checkBoxMatchRematch = new CheckBox();
            numericUpDownMatchRedAllienceTeam2 = new NumericUpDown();
            buttonMatchAbort = new Button();
            label10 = new Label();
            numericUpDownMatchRedAllienceTeam1 = new NumericUpDown();
            numericUpDownMatchDuration = new NumericUpDown();
            label4 = new Label();
            numericUpDownMatchID = new NumericUpDown();
            label9 = new Label();
            buttonMatchLoad = new Button();
            buttonMatchStart = new Button();
            label8 = new Label();
            label3 = new Label();
            groupBoxLogin.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMatchBlueAllienceTeam2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMatchBlueAllienceTeam1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMatchRedAllienceTeam2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMatchRedAllienceTeam1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMatchDuration).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMatchID).BeginInit();
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
            // groupBoxLogin
            // 
            groupBoxLogin.Controls.Add(textBoxEndpoint);
            groupBoxLogin.Controls.Add(buttonConnect);
            groupBoxLogin.Controls.Add(label1);
            groupBoxLogin.Controls.Add(label2);
            groupBoxLogin.Controls.Add(textBoxSecuityKey);
            groupBoxLogin.Location = new Point(12, 12);
            groupBoxLogin.Name = "groupBoxLogin";
            groupBoxLogin.Size = new Size(271, 115);
            groupBoxLogin.TabIndex = 5;
            groupBoxLogin.TabStop = false;
            groupBoxLogin.Text = "Login";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(checkBoxMatchPractice);
            groupBox2.Controls.Add(radioButtonMatchSFinal);
            groupBox2.Controls.Add(radioButtonMatchFinal);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(numericUpDownMatchBlueAllienceTeam2);
            groupBox2.Controls.Add(radioButtonMatchQual);
            groupBox2.Controls.Add(numericUpDownMatchBlueAllienceTeam1);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(checkBoxMatchRematch);
            groupBox2.Controls.Add(numericUpDownMatchRedAllienceTeam2);
            groupBox2.Controls.Add(buttonMatchAbort);
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(numericUpDownMatchRedAllienceTeam1);
            groupBox2.Controls.Add(numericUpDownMatchDuration);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(numericUpDownMatchID);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(buttonMatchLoad);
            groupBox2.Controls.Add(buttonMatchStart);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(label3);
            groupBox2.Enabled = false;
            groupBox2.Location = new Point(12, 145);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(397, 304);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "Match Operations";
            // 
            // checkBoxMatchPractice
            // 
            checkBoxMatchPractice.Location = new Point(75, 181);
            checkBoxMatchPractice.Name = "checkBoxMatchPractice";
            checkBoxMatchPractice.Size = new Size(120, 19);
            checkBoxMatchPractice.TabIndex = 27;
            checkBoxMatchPractice.Text = "Practice";
            checkBoxMatchPractice.UseVisualStyleBackColor = true;
            // 
            // radioButtonMatchSFinal
            // 
            radioButtonMatchSFinal.AutoSize = true;
            radioButtonMatchSFinal.Location = new Point(203, 156);
            radioButtonMatchSFinal.Name = "radioButtonMatchSFinal";
            radioButtonMatchSFinal.Size = new Size(62, 19);
            radioButtonMatchSFinal.TabIndex = 26;
            radioButtonMatchSFinal.Text = "S. Final";
            radioButtonMatchSFinal.UseVisualStyleBackColor = true;
            // 
            // radioButtonMatchFinal
            // 
            radioButtonMatchFinal.AutoSize = true;
            radioButtonMatchFinal.Location = new Point(274, 156);
            radioButtonMatchFinal.Name = "radioButtonMatchFinal";
            radioButtonMatchFinal.Size = new Size(50, 19);
            radioButtonMatchFinal.TabIndex = 9;
            radioButtonMatchFinal.TabStop = true;
            radioButtonMatchFinal.Text = "Final";
            radioButtonMatchFinal.UseVisualStyleBackColor = true;
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
            // numericUpDownMatchBlueAllienceTeam2
            // 
            numericUpDownMatchBlueAllienceTeam2.BackColor = Color.Blue;
            numericUpDownMatchBlueAllienceTeam2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            numericUpDownMatchBlueAllienceTeam2.ForeColor = Color.White;
            numericUpDownMatchBlueAllienceTeam2.Location = new Point(203, 75);
            numericUpDownMatchBlueAllienceTeam2.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numericUpDownMatchBlueAllienceTeam2.Name = "numericUpDownMatchBlueAllienceTeam2";
            numericUpDownMatchBlueAllienceTeam2.Size = new Size(121, 29);
            numericUpDownMatchBlueAllienceTeam2.TabIndex = 24;
            numericUpDownMatchBlueAllienceTeam2.TextAlign = HorizontalAlignment.Center;
            // 
            // radioButtonMatchQual
            // 
            radioButtonMatchQual.AutoSize = true;
            radioButtonMatchQual.Checked = true;
            radioButtonMatchQual.Location = new Point(239, 181);
            radioButtonMatchQual.Name = "radioButtonMatchQual";
            radioButtonMatchQual.Size = new Size(50, 19);
            radioButtonMatchQual.TabIndex = 7;
            radioButtonMatchQual.TabStop = true;
            radioButtonMatchQual.Text = "Qual";
            radioButtonMatchQual.UseVisualStyleBackColor = true;
            // 
            // numericUpDownMatchBlueAllienceTeam1
            // 
            numericUpDownMatchBlueAllienceTeam1.BackColor = Color.Blue;
            numericUpDownMatchBlueAllienceTeam1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            numericUpDownMatchBlueAllienceTeam1.ForeColor = Color.White;
            numericUpDownMatchBlueAllienceTeam1.Location = new Point(203, 40);
            numericUpDownMatchBlueAllienceTeam1.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numericUpDownMatchBlueAllienceTeam1.Name = "numericUpDownMatchBlueAllienceTeam1";
            numericUpDownMatchBlueAllienceTeam1.Size = new Size(121, 29);
            numericUpDownMatchBlueAllienceTeam1.TabIndex = 23;
            numericUpDownMatchBlueAllienceTeam1.TextAlign = HorizontalAlignment.Center;
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
            // checkBoxMatchRematch
            // 
            checkBoxMatchRematch.Location = new Point(75, 156);
            checkBoxMatchRematch.Name = "checkBoxMatchRematch";
            checkBoxMatchRematch.Size = new Size(120, 19);
            checkBoxMatchRematch.TabIndex = 19;
            checkBoxMatchRematch.Text = "Rematch";
            checkBoxMatchRematch.UseVisualStyleBackColor = true;
            // 
            // numericUpDownMatchRedAllienceTeam2
            // 
            numericUpDownMatchRedAllienceTeam2.BackColor = Color.Red;
            numericUpDownMatchRedAllienceTeam2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            numericUpDownMatchRedAllienceTeam2.ForeColor = Color.White;
            numericUpDownMatchRedAllienceTeam2.Location = new Point(74, 75);
            numericUpDownMatchRedAllienceTeam2.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numericUpDownMatchRedAllienceTeam2.Name = "numericUpDownMatchRedAllienceTeam2";
            numericUpDownMatchRedAllienceTeam2.Size = new Size(121, 29);
            numericUpDownMatchRedAllienceTeam2.TabIndex = 20;
            numericUpDownMatchRedAllienceTeam2.TextAlign = HorizontalAlignment.Center;
            // 
            // buttonMatchAbort
            // 
            buttonMatchAbort.Location = new Point(74, 264);
            buttonMatchAbort.Name = "buttonMatchAbort";
            buttonMatchAbort.Size = new Size(248, 23);
            buttonMatchAbort.TabIndex = 18;
            buttonMatchAbort.Text = "Abort";
            buttonMatchAbort.UseVisualStyleBackColor = true;
            buttonMatchAbort.Click += buttonMatchAbort_Click;
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
            // numericUpDownMatchRedAllienceTeam1
            // 
            numericUpDownMatchRedAllienceTeam1.BackColor = Color.Red;
            numericUpDownMatchRedAllienceTeam1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            numericUpDownMatchRedAllienceTeam1.ForeColor = Color.White;
            numericUpDownMatchRedAllienceTeam1.Location = new Point(74, 40);
            numericUpDownMatchRedAllienceTeam1.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numericUpDownMatchRedAllienceTeam1.Name = "numericUpDownMatchRedAllienceTeam1";
            numericUpDownMatchRedAllienceTeam1.Size = new Size(121, 29);
            numericUpDownMatchRedAllienceTeam1.TabIndex = 7;
            numericUpDownMatchRedAllienceTeam1.TextAlign = HorizontalAlignment.Center;
            // 
            // numericUpDownMatchDuration
            // 
            numericUpDownMatchDuration.Location = new Point(202, 127);
            numericUpDownMatchDuration.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDownMatchDuration.Name = "numericUpDownMatchDuration";
            numericUpDownMatchDuration.Size = new Size(120, 23);
            numericUpDownMatchDuration.TabIndex = 16;
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
            // numericUpDownMatchID
            // 
            numericUpDownMatchID.Location = new Point(74, 127);
            numericUpDownMatchID.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numericUpDownMatchID.Name = "numericUpDownMatchID";
            numericUpDownMatchID.Size = new Size(122, 23);
            numericUpDownMatchID.TabIndex = 15;
            // 
            // label9
            // 
            label9.Location = new Point(75, 105);
            label9.Name = "label9";
            label9.Size = new Size(121, 19);
            label9.TabIndex = 12;
            label9.Text = "Match ID";
            label9.TextAlign = ContentAlignment.BottomCenter;
            // 
            // buttonMatchLoad
            // 
            buttonMatchLoad.Location = new Point(74, 206);
            buttonMatchLoad.Name = "buttonMatchLoad";
            buttonMatchLoad.Size = new Size(248, 23);
            buttonMatchLoad.TabIndex = 11;
            buttonMatchLoad.Text = "Load";
            buttonMatchLoad.UseVisualStyleBackColor = true;
            buttonMatchLoad.Click += buttonMatchLoad_Click;
            // 
            // buttonMatchStart
            // 
            buttonMatchStart.Location = new Point(74, 235);
            buttonMatchStart.Name = "buttonMatchStart";
            buttonMatchStart.Size = new Size(248, 23);
            buttonMatchStart.TabIndex = 10;
            buttonMatchStart.Text = "Start";
            buttonMatchStart.UseVisualStyleBackColor = true;
            buttonMatchStart.Click += buttonMatchStart_Click;
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
            ClientSize = new Size(756, 516);
            Controls.Add(groupBox2);
            Controls.Add(groupBoxLogin);
            Name = "FormMain";
            Text = "Form1";
            Load += FormMain_Load;
            groupBoxLogin.ResumeLayout(false);
            groupBoxLogin.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMatchBlueAllienceTeam2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMatchBlueAllienceTeam1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMatchRedAllienceTeam2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMatchRedAllienceTeam1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMatchDuration).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMatchID).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TextBox textBoxEndpoint;
        private Label label1;
        private TextBox textBoxSecuityKey;
        private Label label2;
        private Button buttonConnect;
        private GroupBox groupBoxLogin;
        private GroupBox groupBox2;
        private Label label3;
        private Label label4;
        private Label label8;
        private Button buttonMatchStart;
        private Button buttonMatchLoad;
        private Label label9;
        private NumericUpDown numericUpDownMatchID;
        private NumericUpDown numericUpDownMatchDuration;
        private Label label10;
        private Button buttonMatchAbort;
        private CheckBox checkBoxMatchRematch;
        private NumericUpDown numericUpDownMatchRedAllienceTeam1;
        private NumericUpDown numericUpDownMatchRedAllienceTeam2;
        private Label label5;
        private Label label6;
        private NumericUpDown numericUpDownMatchBlueAllienceTeam2;
        private NumericUpDown numericUpDownMatchBlueAllienceTeam1;
        private Label label7;
        private RadioButton radioButtonMatchQual;
        private RadioButton radioButtonMatchFinal;
        private RadioButton radioButtonMatchSFinal;
        private CheckBox checkBoxMatchPractice;
    }
}
