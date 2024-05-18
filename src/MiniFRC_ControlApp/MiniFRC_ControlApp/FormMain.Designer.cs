﻿namespace MiniFRC_ControlApp
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
            label11 = new Label();
            numericUpDownMatchBlueAllienceTeam3 = new NumericUpDown();
            label12 = new Label();
            numericUpDownMatchRedAllienceTeam3 = new NumericUpDown();
            labelBluePoints = new Label();
            labelRedPoints = new Label();
            labelMatchInfo = new Label();
            labelMatchTime = new Label();
            labelMatchState = new Label();
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
            richTextBoxDevicesLastSeen = new RichTextBox();
            groupBox1 = new GroupBox();
            groupBoxFieldManualControl = new GroupBox();
            buttonShortcut2 = new Button();
            buttonDeviceShortcutRST = new Button();
            buttonDeviceShortcut1 = new Button();
            buttonDisableDevice = new Button();
            buttonEnableDevice = new Button();
            comboBoxDeviceSelection = new ComboBox();
            buttonDisableAll = new Button();
            buttonEnableAll = new Button();
            groupBoxLogin.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMatchBlueAllienceTeam3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMatchRedAllienceTeam3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMatchBlueAllienceTeam2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMatchBlueAllienceTeam1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMatchRedAllienceTeam2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMatchRedAllienceTeam1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMatchDuration).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMatchID).BeginInit();
            groupBox1.SuspendLayout();
            groupBoxFieldManualControl.SuspendLayout();
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
            groupBoxLogin.Size = new Size(486, 115);
            groupBoxLogin.TabIndex = 5;
            groupBoxLogin.TabStop = false;
            groupBoxLogin.Text = "Login";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label11);
            groupBox2.Controls.Add(numericUpDownMatchBlueAllienceTeam3);
            groupBox2.Controls.Add(label12);
            groupBox2.Controls.Add(numericUpDownMatchRedAllienceTeam3);
            groupBox2.Controls.Add(labelBluePoints);
            groupBox2.Controls.Add(labelRedPoints);
            groupBox2.Controls.Add(labelMatchInfo);
            groupBox2.Controls.Add(labelMatchTime);
            groupBox2.Controls.Add(labelMatchState);
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
            groupBox2.Location = new Point(504, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(397, 747);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "Match Operations";
            // 
            // label11
            // 
            label11.Location = new Point(330, 110);
            label11.Name = "label11";
            label11.Size = new Size(58, 29);
            label11.TabIndex = 35;
            label11.Text = "Blue 3";
            label11.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // numericUpDownMatchBlueAllienceTeam3
            // 
            numericUpDownMatchBlueAllienceTeam3.BackColor = Color.Blue;
            numericUpDownMatchBlueAllienceTeam3.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            numericUpDownMatchBlueAllienceTeam3.ForeColor = Color.White;
            numericUpDownMatchBlueAllienceTeam3.Location = new Point(203, 110);
            numericUpDownMatchBlueAllienceTeam3.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numericUpDownMatchBlueAllienceTeam3.Name = "numericUpDownMatchBlueAllienceTeam3";
            numericUpDownMatchBlueAllienceTeam3.Size = new Size(121, 29);
            numericUpDownMatchBlueAllienceTeam3.TabIndex = 34;
            numericUpDownMatchBlueAllienceTeam3.TextAlign = HorizontalAlignment.Center;
            // 
            // label12
            // 
            label12.Location = new Point(10, 110);
            label12.Name = "label12";
            label12.Size = new Size(58, 29);
            label12.TabIndex = 33;
            label12.Text = "Red 3";
            label12.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numericUpDownMatchRedAllienceTeam3
            // 
            numericUpDownMatchRedAllienceTeam3.BackColor = Color.Red;
            numericUpDownMatchRedAllienceTeam3.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            numericUpDownMatchRedAllienceTeam3.ForeColor = Color.White;
            numericUpDownMatchRedAllienceTeam3.Location = new Point(74, 110);
            numericUpDownMatchRedAllienceTeam3.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numericUpDownMatchRedAllienceTeam3.Name = "numericUpDownMatchRedAllienceTeam3";
            numericUpDownMatchRedAllienceTeam3.Size = new Size(121, 29);
            numericUpDownMatchRedAllienceTeam3.TabIndex = 32;
            numericUpDownMatchRedAllienceTeam3.TextAlign = HorizontalAlignment.Center;
            // 
            // labelBluePoints
            // 
            labelBluePoints.Font = new Font("Segoe UI", 30F, FontStyle.Bold);
            labelBluePoints.ForeColor = Color.Blue;
            labelBluePoints.Location = new Point(201, 406);
            labelBluePoints.Name = "labelBluePoints";
            labelBluePoints.Size = new Size(120, 52);
            labelBluePoints.TabIndex = 31;
            labelBluePoints.Text = "0";
            labelBluePoints.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelRedPoints
            // 
            labelRedPoints.Font = new Font("Segoe UI", 30F, FontStyle.Bold);
            labelRedPoints.ForeColor = Color.Red;
            labelRedPoints.Location = new Point(74, 406);
            labelRedPoints.Name = "labelRedPoints";
            labelRedPoints.Size = new Size(120, 52);
            labelRedPoints.TabIndex = 7;
            labelRedPoints.Text = "0";
            labelRedPoints.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelMatchInfo
            // 
            labelMatchInfo.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelMatchInfo.Location = new Point(5, 476);
            labelMatchInfo.Name = "labelMatchInfo";
            labelMatchInfo.Size = new Size(382, 253);
            labelMatchInfo.TabIndex = 30;
            // 
            // labelMatchTime
            // 
            labelMatchTime.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelMatchTime.Location = new Point(8, 377);
            labelMatchTime.Name = "labelMatchTime";
            labelMatchTime.Size = new Size(382, 29);
            labelMatchTime.TabIndex = 29;
            labelMatchTime.Text = "CD / R.T";
            labelMatchTime.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelMatchState
            // 
            labelMatchState.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelMatchState.Location = new Point(8, 348);
            labelMatchState.Name = "labelMatchState";
            labelMatchState.Size = new Size(382, 29);
            labelMatchState.TabIndex = 28;
            labelMatchState.Text = "Match State: ?";
            labelMatchState.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // checkBoxMatchPractice
            // 
            checkBoxMatchPractice.Location = new Point(74, 239);
            checkBoxMatchPractice.Name = "checkBoxMatchPractice";
            checkBoxMatchPractice.Size = new Size(120, 19);
            checkBoxMatchPractice.TabIndex = 27;
            checkBoxMatchPractice.Text = "Practice";
            checkBoxMatchPractice.UseVisualStyleBackColor = true;
            // 
            // radioButtonMatchSFinal
            // 
            radioButtonMatchSFinal.AutoSize = true;
            radioButtonMatchSFinal.Location = new Point(202, 214);
            radioButtonMatchSFinal.Name = "radioButtonMatchSFinal";
            radioButtonMatchSFinal.Size = new Size(62, 19);
            radioButtonMatchSFinal.TabIndex = 26;
            radioButtonMatchSFinal.Text = "S. Final";
            radioButtonMatchSFinal.UseVisualStyleBackColor = true;
            // 
            // radioButtonMatchFinal
            // 
            radioButtonMatchFinal.AutoSize = true;
            radioButtonMatchFinal.Location = new Point(273, 214);
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
            radioButtonMatchQual.Location = new Point(238, 239);
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
            checkBoxMatchRematch.Location = new Point(74, 214);
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
            buttonMatchAbort.Location = new Point(73, 322);
            buttonMatchAbort.Name = "buttonMatchAbort";
            buttonMatchAbort.Size = new Size(248, 23);
            buttonMatchAbort.TabIndex = 18;
            buttonMatchAbort.Text = "Abort";
            buttonMatchAbort.UseVisualStyleBackColor = true;
            buttonMatchAbort.Click += buttonMatchAbort_Click;
            // 
            // label10
            // 
            label10.Location = new Point(202, 163);
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
            numericUpDownMatchDuration.Location = new Point(201, 185);
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
            numericUpDownMatchID.Location = new Point(73, 185);
            numericUpDownMatchID.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numericUpDownMatchID.Name = "numericUpDownMatchID";
            numericUpDownMatchID.Size = new Size(122, 23);
            numericUpDownMatchID.TabIndex = 15;
            // 
            // label9
            // 
            label9.Location = new Point(74, 163);
            label9.Name = "label9";
            label9.Size = new Size(121, 19);
            label9.TabIndex = 12;
            label9.Text = "Match ID";
            label9.TextAlign = ContentAlignment.BottomCenter;
            // 
            // buttonMatchLoad
            // 
            buttonMatchLoad.Location = new Point(73, 264);
            buttonMatchLoad.Name = "buttonMatchLoad";
            buttonMatchLoad.Size = new Size(248, 23);
            buttonMatchLoad.TabIndex = 11;
            buttonMatchLoad.Text = "Load";
            buttonMatchLoad.UseVisualStyleBackColor = true;
            buttonMatchLoad.Click += buttonMatchLoad_Click;
            // 
            // buttonMatchStart
            // 
            buttonMatchStart.Location = new Point(73, 293);
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
            // richTextBoxDevicesLastSeen
            // 
            richTextBoxDevicesLastSeen.BorderStyle = BorderStyle.None;
            richTextBoxDevicesLastSeen.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            richTextBoxDevicesLastSeen.Location = new Point(6, 21);
            richTextBoxDevicesLastSeen.Name = "richTextBoxDevicesLastSeen";
            richTextBoxDevicesLastSeen.ReadOnly = true;
            richTextBoxDevicesLastSeen.Size = new Size(474, 304);
            richTextBoxDevicesLastSeen.TabIndex = 7;
            richTextBoxDevicesLastSeen.Text = "1\n2\n3\n4\n5\n6\n7\n8\n9\n10";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(richTextBoxDevicesLastSeen);
            groupBox1.Location = new Point(12, 133);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(486, 332);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Field Device Last Seen";
            // 
            // groupBoxFieldManualControl
            // 
            groupBoxFieldManualControl.Controls.Add(buttonShortcut2);
            groupBoxFieldManualControl.Controls.Add(buttonDeviceShortcutRST);
            groupBoxFieldManualControl.Controls.Add(buttonDeviceShortcut1);
            groupBoxFieldManualControl.Controls.Add(buttonDisableDevice);
            groupBoxFieldManualControl.Controls.Add(buttonEnableDevice);
            groupBoxFieldManualControl.Controls.Add(comboBoxDeviceSelection);
            groupBoxFieldManualControl.Controls.Add(buttonDisableAll);
            groupBoxFieldManualControl.Controls.Add(buttonEnableAll);
            groupBoxFieldManualControl.Location = new Point(12, 471);
            groupBoxFieldManualControl.Name = "groupBoxFieldManualControl";
            groupBoxFieldManualControl.Size = new Size(486, 184);
            groupBoxFieldManualControl.TabIndex = 9;
            groupBoxFieldManualControl.TabStop = false;
            groupBoxFieldManualControl.Text = "Field Manual Control";
            // 
            // buttonShortcut2
            // 
            buttonShortcut2.Location = new Point(424, 109);
            buttonShortcut2.Name = "buttonShortcut2";
            buttonShortcut2.Size = new Size(56, 69);
            buttonShortcut2.TabIndex = 7;
            buttonShortcut2.Text = "2";
            buttonShortcut2.UseVisualStyleBackColor = true;
            buttonShortcut2.Click += buttonShortcut2_Click;
            // 
            // buttonDeviceShortcutRST
            // 
            buttonDeviceShortcutRST.Location = new Point(362, 109);
            buttonDeviceShortcutRST.Name = "buttonDeviceShortcutRST";
            buttonDeviceShortcutRST.Size = new Size(56, 69);
            buttonDeviceShortcutRST.TabIndex = 6;
            buttonDeviceShortcutRST.Text = "RST";
            buttonDeviceShortcutRST.UseVisualStyleBackColor = true;
            buttonDeviceShortcutRST.Click += buttonDeviceShortcutRST_Click;
            // 
            // buttonDeviceShortcut1
            // 
            buttonDeviceShortcut1.Location = new Point(300, 109);
            buttonDeviceShortcut1.Name = "buttonDeviceShortcut1";
            buttonDeviceShortcut1.Size = new Size(56, 69);
            buttonDeviceShortcut1.TabIndex = 5;
            buttonDeviceShortcut1.Text = "1";
            buttonDeviceShortcut1.UseVisualStyleBackColor = true;
            buttonDeviceShortcut1.Click += buttonDeviceShortcut1_Click;
            // 
            // buttonDisableDevice
            // 
            buttonDisableDevice.Location = new Point(150, 148);
            buttonDisableDevice.Name = "buttonDisableDevice";
            buttonDisableDevice.Size = new Size(130, 30);
            buttonDisableDevice.TabIndex = 4;
            buttonDisableDevice.Text = "Disable";
            buttonDisableDevice.UseVisualStyleBackColor = true;
            buttonDisableDevice.Click += buttonDisableDevice_Click;
            // 
            // buttonEnableDevice
            // 
            buttonEnableDevice.Location = new Point(7, 148);
            buttonEnableDevice.Name = "buttonEnableDevice";
            buttonEnableDevice.Size = new Size(130, 30);
            buttonEnableDevice.TabIndex = 3;
            buttonEnableDevice.Text = "Enable";
            buttonEnableDevice.UseVisualStyleBackColor = true;
            buttonEnableDevice.Click += buttonEnableDevice_Click;
            // 
            // comboBoxDeviceSelection
            // 
            comboBoxDeviceSelection.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBoxDeviceSelection.FormattingEnabled = true;
            comboBoxDeviceSelection.Location = new Point(6, 109);
            comboBoxDeviceSelection.Name = "comboBoxDeviceSelection";
            comboBoxDeviceSelection.Size = new Size(274, 33);
            comboBoxDeviceSelection.TabIndex = 2;
            comboBoxDeviceSelection.SelectedIndexChanged += comboBoxDeviceSelection_SelectedIndexChanged;
            // 
            // buttonDisableAll
            // 
            buttonDisableAll.Location = new Point(255, 22);
            buttonDisableAll.Name = "buttonDisableAll";
            buttonDisableAll.Size = new Size(225, 58);
            buttonDisableAll.TabIndex = 1;
            buttonDisableAll.Text = "Disable All";
            buttonDisableAll.UseVisualStyleBackColor = true;
            buttonDisableAll.Click += buttonDisableAll_Click;
            // 
            // buttonEnableAll
            // 
            buttonEnableAll.Location = new Point(6, 22);
            buttonEnableAll.Name = "buttonEnableAll";
            buttonEnableAll.Size = new Size(225, 58);
            buttonEnableAll.TabIndex = 0;
            buttonEnableAll.Text = "Enable All";
            buttonEnableAll.UseVisualStyleBackColor = true;
            buttonEnableAll.Click += buttonEnableAll_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1116, 766);
            Controls.Add(groupBoxFieldManualControl);
            Controls.Add(groupBox1);
            Controls.Add(groupBox2);
            Controls.Add(groupBoxLogin);
            Name = "FormMain";
            Text = "Form1";
            FormClosing += FormMain_FormClosing;
            Load += FormMain_Load;
            groupBoxLogin.ResumeLayout(false);
            groupBoxLogin.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMatchBlueAllienceTeam3).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMatchRedAllienceTeam3).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMatchBlueAllienceTeam2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMatchBlueAllienceTeam1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMatchRedAllienceTeam2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMatchRedAllienceTeam1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMatchDuration).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMatchID).EndInit();
            groupBox1.ResumeLayout(false);
            groupBoxFieldManualControl.ResumeLayout(false);
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
        private Label labelMatchState;
        private Label labelMatchTime;
        private Label labelMatchInfo;
        private Label labelRedPoints;
        private Label labelBluePoints;
        private RichTextBox richTextBoxDevicesLastSeen;
        private Button buttonReconnect;
        private GroupBox groupBox1;
        private GroupBox groupBoxFieldManualControl;
        private Button buttonEnableAll;
        private Button buttonDisableAll;
        private ComboBox comboBoxDeviceSelection;
        private Button buttonEnableDevice;
        private Button buttonDisableDevice;
        private Button buttonDeviceShortcut1;
        private Button buttonShortcut2;
        private Button buttonDeviceShortcutRST;
        private Label label11;
        private NumericUpDown numericUpDownMatchBlueAllienceTeam3;
        private Label label12;
        private NumericUpDown numericUpDownMatchRedAllienceTeam3;
    }
}
