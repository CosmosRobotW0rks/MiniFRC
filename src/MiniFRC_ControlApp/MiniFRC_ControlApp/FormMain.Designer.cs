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
            buttonDisableDevice = new Button();
            buttonEnableDevice = new Button();
            comboBoxDeviceSelection = new ComboBox();
            buttonDisableAll = new Button();
            buttonEnableAll = new Button();
            groupBox4 = new GroupBox();
            buttonApprovePoints = new Button();
            buttonBLUEDeletePoint = new Button();
            buttonREDDeletePoint = new Button();
            listBoxBLUEPoints = new ListBox();
            listBoxREDPoints = new ListBox();
            groupBox3 = new GroupBox();
            labelAuDisPage = new Label();
            button5 = new Button();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            buttonEnableField = new Button();
            buttonDisableField = new Button();
            buttonFieldDeviceControl = new Button();
            groupBox5 = new GroupBox();
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
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox5.SuspendLayout();
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
            groupBox2.Size = new Size(397, 741);
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
            groupBoxFieldManualControl.Text = "Field Device Manual Control";
            // 
            // buttonDisableDevice
            // 
            buttonDisableDevice.Location = new Point(255, 148);
            buttonDisableDevice.Name = "buttonDisableDevice";
            buttonDisableDevice.Size = new Size(225, 30);
            buttonDisableDevice.TabIndex = 4;
            buttonDisableDevice.Text = "Disable";
            buttonDisableDevice.UseVisualStyleBackColor = true;
            buttonDisableDevice.Click += buttonDisableDevice_Click;
            // 
            // buttonEnableDevice
            // 
            buttonEnableDevice.Location = new Point(7, 148);
            buttonEnableDevice.Name = "buttonEnableDevice";
            buttonEnableDevice.Size = new Size(224, 30);
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
            comboBoxDeviceSelection.Size = new Size(474, 33);
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
            // groupBox4
            // 
            groupBox4.Controls.Add(buttonApprovePoints);
            groupBox4.Controls.Add(buttonBLUEDeletePoint);
            groupBox4.Controls.Add(buttonREDDeletePoint);
            groupBox4.Controls.Add(listBoxBLUEPoints);
            groupBox4.Controls.Add(listBoxREDPoints);
            groupBox4.Location = new Point(907, 16);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(489, 806);
            groupBox4.TabIndex = 11;
            groupBox4.TabStop = false;
            groupBox4.Text = "Point Operations";
            // 
            // buttonApprovePoints
            // 
            buttonApprovePoints.Location = new Point(6, 743);
            buttonApprovePoints.Name = "buttonApprovePoints";
            buttonApprovePoints.Size = new Size(477, 52);
            buttonApprovePoints.TabIndex = 36;
            buttonApprovePoints.Text = "Approve Points";
            buttonApprovePoints.UseVisualStyleBackColor = true;
            buttonApprovePoints.Click += buttonApprovePoints_Click;
            // 
            // buttonBLUEDeletePoint
            // 
            buttonBLUEDeletePoint.Location = new Point(247, 683);
            buttonBLUEDeletePoint.Name = "buttonBLUEDeletePoint";
            buttonBLUEDeletePoint.Size = new Size(236, 54);
            buttonBLUEDeletePoint.TabIndex = 3;
            buttonBLUEDeletePoint.Text = "Delete Selected";
            buttonBLUEDeletePoint.UseVisualStyleBackColor = true;
            buttonBLUEDeletePoint.Click += buttonBLUEDeletePoint_Click;
            // 
            // buttonREDDeletePoint
            // 
            buttonREDDeletePoint.Location = new Point(6, 683);
            buttonREDDeletePoint.Name = "buttonREDDeletePoint";
            buttonREDDeletePoint.Size = new Size(236, 54);
            buttonREDDeletePoint.TabIndex = 2;
            buttonREDDeletePoint.Text = "Delete Selected";
            buttonREDDeletePoint.UseVisualStyleBackColor = true;
            buttonREDDeletePoint.Click += buttonREDDeletePoint_Click;
            // 
            // listBoxBLUEPoints
            // 
            listBoxBLUEPoints.BackColor = Color.Blue;
            listBoxBLUEPoints.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            listBoxBLUEPoints.ForeColor = Color.White;
            listBoxBLUEPoints.FormattingEnabled = true;
            listBoxBLUEPoints.ItemHeight = 21;
            listBoxBLUEPoints.Location = new Point(248, 22);
            listBoxBLUEPoints.Name = "listBoxBLUEPoints";
            listBoxBLUEPoints.Size = new Size(236, 655);
            listBoxBLUEPoints.TabIndex = 1;
            // 
            // listBoxREDPoints
            // 
            listBoxREDPoints.BackColor = Color.Red;
            listBoxREDPoints.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            listBoxREDPoints.ForeColor = Color.White;
            listBoxREDPoints.FormattingEnabled = true;
            listBoxREDPoints.ItemHeight = 21;
            listBoxREDPoints.Location = new Point(6, 22);
            listBoxREDPoints.Name = "listBoxREDPoints";
            listBoxREDPoints.Size = new Size(236, 655);
            listBoxREDPoints.TabIndex = 0;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(labelAuDisPage);
            groupBox3.Controls.Add(button5);
            groupBox3.Controls.Add(button4);
            groupBox3.Controls.Add(button3);
            groupBox3.Controls.Add(button2);
            groupBox3.Controls.Add(button1);
            groupBox3.Location = new Point(1402, 16);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(297, 312);
            groupBox3.TabIndex = 12;
            groupBox3.TabStop = false;
            groupBox3.Text = "AuDis Control";
            // 
            // labelAuDisPage
            // 
            labelAuDisPage.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelAuDisPage.Location = new Point(6, 264);
            labelAuDisPage.Name = "labelAuDisPage";
            labelAuDisPage.Size = new Size(285, 38);
            labelAuDisPage.TabIndex = 5;
            labelAuDisPage.Text = "Page: ?";
            labelAuDisPage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button5
            // 
            button5.Location = new Point(6, 218);
            button5.Name = "button5";
            button5.Size = new Size(285, 43);
            button5.TabIndex = 4;
            button5.Tag = "4";
            button5.Text = "Calculating Points";
            button5.UseVisualStyleBackColor = true;
            button5.Click += AuDisUpdatePage;
            // 
            // button4
            // 
            button4.Location = new Point(6, 169);
            button4.Name = "button4";
            button4.Size = new Size(285, 43);
            button4.TabIndex = 3;
            button4.Tag = "3";
            button4.Text = "Leaderboard";
            button4.UseVisualStyleBackColor = true;
            button4.Click += AuDisUpdatePage;
            // 
            // button3
            // 
            button3.Location = new Point(6, 120);
            button3.Name = "button3";
            button3.Size = new Size(285, 43);
            button3.TabIndex = 2;
            button3.Tag = "2";
            button3.Text = "After Match";
            button3.UseVisualStyleBackColor = true;
            button3.Click += AuDisUpdatePage;
            // 
            // button2
            // 
            button2.Location = new Point(6, 71);
            button2.Name = "button2";
            button2.Size = new Size(285, 43);
            button2.TabIndex = 1;
            button2.Tag = "1";
            button2.Text = "Match";
            button2.UseVisualStyleBackColor = true;
            button2.Click += AuDisUpdatePage;
            // 
            // button1
            // 
            button1.Location = new Point(6, 22);
            button1.Name = "button1";
            button1.Size = new Size(285, 43);
            button1.TabIndex = 0;
            button1.Tag = "0";
            button1.Text = "Main Page";
            button1.UseVisualStyleBackColor = true;
            button1.Click += AuDisUpdatePage;
            // 
            // buttonEnableField
            // 
            buttonEnableField.BackColor = Color.Lime;
            buttonEnableField.Font = new Font("Segoe UI Black", 16F, FontStyle.Bold);
            buttonEnableField.ForeColor = Color.White;
            buttonEnableField.Location = new Point(6, 19);
            buttonEnableField.Name = "buttonEnableField";
            buttonEnableField.Size = new Size(208, 123);
            buttonEnableField.TabIndex = 13;
            buttonEnableField.Text = "Enable Field";
            buttonEnableField.UseVisualStyleBackColor = false;
            buttonEnableField.Click += buttonEnableField_Click;
            // 
            // buttonDisableField
            // 
            buttonDisableField.BackColor = Color.Red;
            buttonDisableField.Font = new Font("Segoe UI Black", 16F, FontStyle.Bold);
            buttonDisableField.ForeColor = Color.White;
            buttonDisableField.Location = new Point(272, 19);
            buttonDisableField.Name = "buttonDisableField";
            buttonDisableField.Size = new Size(208, 123);
            buttonDisableField.TabIndex = 14;
            buttonDisableField.Text = "Disable Field";
            buttonDisableField.UseVisualStyleBackColor = false;
            buttonDisableField.Click += buttonDisableField_Click;
            // 
            // buttonFieldDeviceControl
            // 
            buttonFieldDeviceControl.Location = new Point(504, 759);
            buttonFieldDeviceControl.Name = "buttonFieldDeviceControl";
            buttonFieldDeviceControl.Size = new Size(397, 52);
            buttonFieldDeviceControl.TabIndex = 15;
            buttonFieldDeviceControl.Text = "Field Device Control";
            buttonFieldDeviceControl.UseVisualStyleBackColor = true;
            buttonFieldDeviceControl.Click += buttonFieldDeviceControl_Click;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(buttonEnableField);
            groupBox5.Controls.Add(buttonDisableField);
            groupBox5.Location = new Point(12, 661);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(486, 150);
            groupBox5.TabIndex = 16;
            groupBox5.TabStop = false;
            groupBox5.Text = "Field Control";
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1711, 855);
            Controls.Add(groupBox5);
            Controls.Add(buttonFieldDeviceControl);
            Controls.Add(groupBox3);
            Controls.Add(groupBox4);
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
            groupBox4.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
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
        private Label label11;
        private NumericUpDown numericUpDownMatchBlueAllienceTeam3;
        private Label label12;
        private NumericUpDown numericUpDownMatchRedAllienceTeam3;
        private GroupBox groupBox4;
        private ListBox listBoxREDPoints;
        private ListBox listBoxBLUEPoints;
        private Button buttonBLUEDeletePoint;
        private Button buttonREDDeletePoint;
        private Button buttonApprovePoints;
        private GroupBox groupBox3;
        private Button button1;
        private Button button2;
        private Button button5;
        private Button button4;
        private Button button3;
        private Label labelAuDisPage;
        private Button buttonEnableField;
        private Button buttonDisableField;
        private Button buttonFieldDeviceControl;
        private GroupBox groupBox5;
    }
}
