using MiniFRC_ControlApp.Comms;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace MiniFRC_ControlApp
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }
        Dictionary<byte, string> Devices = new Dictionary<byte, string>();

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // CREDS
            if (!File.Exists("creds.txt")) return;

            string[] lines = File.ReadAllLines("creds.txt");

            if (lines.Length != 2) return;

            textBoxEndpoint.Text = lines[0];
            textBoxSecuityKey.Text = lines[1];

            // DEVICES
            foreach (DeviceType device in Enum.GetValues<DeviceType>())
            {
                if (device == DeviceType.NONE) continue;

                if (device == DeviceType.Fan) Devices.Add(Utils.GetDeviceIDByDeviceInfo(device, TeamColor.NONE), device.ToString());
                else
                {
                    Devices.Add(Utils.GetDeviceIDByDeviceInfo(device, TeamColor.RED), Utils.GetDeviceNameByDeviceInfo(device, TeamColor.RED));
                    Devices.Add(Utils.GetDeviceIDByDeviceInfo(device, TeamColor.BLUE), Utils.GetDeviceNameByDeviceInfo(device, TeamColor.BLUE));
                }
            }

            foreach (var kvp in Devices)
            {
                comboBoxDeviceSelection.Items.Add(kvp.Value);
            }

        }

        void AttachPacketCallbacks()
        {
            ServerCommunication.AttachPacketCB<FMSControllerMatchStateUpdatedPacket>(HandleMatchUpdate);
            ServerCommunication.AttachPacketCB<FMSControllerDeviceLastseenUpdatedPacket>(DisplayDeviceLastSeen);

            ServerCommunication.AttachPacketCB<FMSControllerPointAddedPacket>(HandlePointAdd);
            ServerCommunication.AttachPacketCB<FMSControllerPointRemovedPacket>(HandlePointRemove);

            ServerCommunication.AttachPacketCB<FMSControllerAuDisPageUpdatedPacket>(HandleAuDisUpdated);
        }

        private void buttonFieldDeviceControl_Click(object sender, EventArgs e)
        {
            this.Hide();
            new FormFieldControl().ShowDialog();
            this.Show();
        }

        #region Device Last Seen
        void DisplayDeviceLastSeen(FMSControllerDeviceLastseenUpdatedPacket packet)
        {
            var devices = packet.GetDevices();
            Font boxFont = richTextBoxDevicesLastSeen.Font;

            DateTime now = DateTime.Now;

            richTextBoxDevicesLastSeen.Clear();
            foreach (var kvp in devices)
            {
                int diffSecs = (int)(kvp.Value - now).TotalSeconds;

                richTextBoxDevicesLastSeen.AppendText(Utils.GetDeviceNameByDeviceInfo(kvp.Key.Item1, kvp.Key.Item2), Color.Black, new Font(boxFont, FontStyle.Bold));

                string text = $"[{kvp.Value.ToLongTimeString()}]{(kvp.Value == DateTime.MinValue ? "" : $"({diffSecs})")}\n";
                TimeSpan diff = now - kvp.Value;
                if (diff < TimeSpan.FromSeconds(5)) richTextBoxDevicesLastSeen.AppendText(text, Color.Green, new Font(boxFont, FontStyle.Italic | FontStyle.Bold));
                else if (diff < TimeSpan.FromSeconds(10)) richTextBoxDevicesLastSeen.AppendText(text, Color.DarkOrange, new Font(boxFont, FontStyle.Italic | FontStyle.Bold | FontStyle.Underline));
                else richTextBoxDevicesLastSeen.AppendText(text, Color.Red, new Font(boxFont, FontStyle.Italic | FontStyle.Bold | FontStyle.Strikeout));
            }
        }
        #endregion

        #region Login
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (ServerCommunication.client != null && ServerCommunication.client.Connected)
            {
                ServerCommunication.Disconnect();
            }

            try
            {
                ServerCommunication.Connect(IPEndPoint.Parse(textBoxEndpoint.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to connect to FMS\nex: " + ex.Message);
                return;
            }

            this.Enabled = false;
            Task.Run(async delegate ()
            {
                try
                {
                    bool res = await ServerCommunication.Authenticate(ulong.Parse(textBoxSecuityKey.Text));


                    if (res)
                    {
                        AttachPacketCallbacks();
                        foreach (Control ctr in this.Controls)
                        {
                            if (ctr is GroupBox) ctr.Enabled = true;
                        }
                        buttonConnect.Text = "Reconnect";
                        return;
                    }

                    MessageBox.Show("Error occured while authenticating");

                    ServerCommunication.Disconnect();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured while authenticating\nex: " + ex.Message);
                }
                finally
                {
                    this.Enabled = true;
                }
            });


        }
        #endregion

        #region Match Control
        void HandleMatchUpdate(FMSControllerMatchStateUpdatedPacket p)
        {
            if (p.matchState == FMSControllerMatchStateUpdatedPacket.MatchState.Standby)
            {
                REDPoints.Clear();
                BLUEPoints.Clear();

                ResetPointListboxes();
            }

            labelMatchState.Text = $"Match State: {p.matchState}";

            switch (p.matchState)
            {
                case FMSControllerMatchStateUpdatedPacket.MatchState.Countdown:
                    labelMatchTime.Text = $"CD: {p.Countdown}";
                    break;
                case FMSControllerMatchStateUpdatedPacket.MatchState.Running:
                    labelMatchTime.Text = $"R.T: {p.RemainingTime}";
                    break;
                default:
                    labelMatchTime.Text = "CD / R.T";
                    break;
            }

            labelMatchInfo.Text = p.ID_RED1 == 0 && p.ID_RED2 == 0 && p.ID_BLUE1 == 0 && p.ID_BLUE2 == 0 ? "" :
                $"Match Type: {p.matchType}\n" +
                $"TR1: {p.ID_RED1}\n" +
                $"TR2: {p.ID_RED2}\n" +
                $"TR3: {p.ID_RED3}\n" +
                $"TB1: {p.ID_BLUE1}\n" +
                $"TB2: {p.ID_BLUE2}\n" +
                $"TB3: {p.ID_BLUE3}\n" +
                $"Rematch: {p.Rematch == 1}\n" +
                $"Practice: {p.Practice == 1}\n";

            labelRedPoints.Text = p.REDPoints.ToString();
            labelBluePoints.Text = p.BLUEPoints.ToString();

        }



        private void buttonMatchLoad_Click(object sender, EventArgs e)
        {
            byte matchid = (byte)numericUpDownMatchID.Value;

            UInt16 matchDuration = (UInt16)numericUpDownMatchDuration.Value;

            byte red1 = (byte)numericUpDownMatchRedAllienceTeam1.Value;
            byte red2 = (byte)numericUpDownMatchRedAllienceTeam2.Value;
            byte red3 = (byte)numericUpDownMatchRedAllienceTeam3.Value;
            byte blue1 = (byte)numericUpDownMatchBlueAllienceTeam1.Value;
            byte blue2 = (byte)numericUpDownMatchBlueAllienceTeam2.Value;
            byte blue3 = (byte)numericUpDownMatchBlueAllienceTeam3.Value;
            FMSControllerLoadMatchPacket.MatchType matchType = radioButtonMatchQual.Checked ? FMSControllerLoadMatchPacket.MatchType.Qualification : radioButtonMatchSFinal.Checked ? FMSControllerLoadMatchPacket.MatchType.Semifinal : FMSControllerLoadMatchPacket.MatchType.Final;





            this.Enabled = false;
            Task.Run(async delegate ()
            {
                try
                {
                    FMSControllerLoadMatchPacket packet = new FMSControllerLoadMatchPacket()
                    {
                        ID_BLUE1 = blue1,
                        ID_BLUE2 = blue2,
                        ID_BLUE3 = blue3,
                        ID_RED1 = red1,
                        ID_RED2 = red2,
                        ID_RED3 = red3,
                        MatchDuration = matchDuration,
                        MatchID = matchid,
                        Rematch = checkBoxMatchRematch.Checked ? (byte)1 : (byte)0,
                        Practice = checkBoxMatchPractice.Checked ? (byte)1 : (byte)0,
                        matchType = matchType
                    };

                    var resp = await ServerCommunication.client.SendPacketAndWaitForResponseAsync<FMSControllerLoadMatchPacket, FMSControllerLoadMatchResponsePacket>(packet, TimeSpan.FromSeconds(5));
                    if (resp.TimedOut)
                    {
                        MessageBox.Show("Server response timed out");
                        return;
                    }
                    var status = resp.Packet.matchLoadStatus;

                    switch (status)
                    {
                        case FMSControllerLoadMatchResponsePacket.MatchLoadStatus.Success:
                            //MessageBox.Show("Match loaded successfully");
                            break;
                        case FMSControllerLoadMatchResponsePacket.MatchLoadStatus.IncorrectTeamIDs:
                            MessageBox.Show("Incorrect Team ID/s");
                            break;
                        case FMSControllerLoadMatchResponsePacket.MatchLoadStatus.MatchExists:
                            MessageBox.Show("A match with the given ID already exists");
                            break;
                        case FMSControllerLoadMatchResponsePacket.MatchLoadStatus.IncorrectMatchState:
                            MessageBox.Show("Match status has to be standby or loaded to load a new match");
                            break;
                        case FMSControllerLoadMatchResponsePacket.MatchLoadStatus.SomethingElseWentWrong:
                            MessageBox.Show("Something went wrong while loading match");
                            break;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured while loading match\nex: " + ex.Message);
                }
                finally
                {
                    this.Enabled = true;
                }
            });
        }

        private void buttonMatchStart_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Task.Run(async delegate ()
            {
                try
                {
                    var packet = new FMSControllerStartStopMatchPacket() { State = 1 };

                    var resp = await ServerCommunication.client.SendPacketAndWaitForResponseAsync<FMSControllerStartStopMatchPacket, FMSControllerStartStopMatchResponsePacket>(packet, TimeSpan.FromSeconds(5));
                    if (resp.TimedOut)
                    {
                        MessageBox.Show("Server response timed out");
                        return;
                    }

                    bool status = resp.Packet.Success == 1;

                    if (!status)
                    {
                        MessageBox.Show("Failed to start the match");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured while starting match\nex: " + ex.Message);
                }
                finally
                {
                    this.Enabled = true;
                }
            });
        }

        private void buttonMatchAbort_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Task.Run(async delegate ()
            {
                try
                {
                    var packet = new FMSControllerStartStopMatchPacket() { State = 0 };

                    var resp = await ServerCommunication.client.SendPacketAndWaitForResponseAsync<FMSControllerStartStopMatchPacket, FMSControllerStartStopMatchResponsePacket>(packet, TimeSpan.FromSeconds(5));
                    if (resp.TimedOut)
                    {
                        MessageBox.Show("Server response timed out");
                        return;
                    }

                    bool status = resp.Packet.Success == 1;

                    if (!status)
                    {
                        MessageBox.Show("Failed to abort the match");
                        return;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured while aborting match\nex: " + ex.Message);
                }
                finally
                {
                    this.Enabled = true;
                }
            });
        }
        #endregion

        #region Field Device Control
        private void buttonEnableAll_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Task.Run(async delegate ()
            {
                try
                {
                    var packet = new FMSControllerEnableDisableDevicePacket(new byte[0], true);

                    var resp = await ServerCommunication.client.SendPacketAndWaitForResponseAsync<FMSControllerEnableDisableDevicePacket, FMSControllerEnableDisableDeviceResponsePacket>(packet, TimeSpan.FromSeconds(5));
                    if (resp.TimedOut)
                    {
                        MessageBox.Show("Server response timed out");
                        return;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured while enabling all devices\nex: " + ex.Message);
                }
                finally
                {
                    this.Enabled = true;
                }
            });
        }

        private void buttonDisableAll_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Task.Run(async delegate ()
            {
                try
                {
                    var packet = new FMSControllerEnableDisableDevicePacket(new byte[0], false);

                    var resp = await ServerCommunication.client.SendPacketAndWaitForResponseAsync<FMSControllerEnableDisableDevicePacket, FMSControllerEnableDisableDeviceResponsePacket>(packet, TimeSpan.FromSeconds(5));
                    if (resp.TimedOut)
                    {
                        MessageBox.Show("Server response timed out");
                        return;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured while disabling all devices\nex: " + ex.Message);
                }
                finally
                {
                    this.Enabled = true;
                }
            });
        }

        private void buttonEnableDevice_Click(object sender, EventArgs e)
        {
            if (comboBoxDeviceSelection.SelectedIndex == -1) return;

            byte deviceID = Devices.ToList()[comboBoxDeviceSelection.SelectedIndex].Key;

            this.Enabled = false;
            Task.Run(async delegate ()
            {
                try
                {
                    var packet = new FMSControllerEnableDisableDevicePacket(new byte[] { deviceID }, true);

                    var resp = await ServerCommunication.client.SendPacketAndWaitForResponseAsync<FMSControllerEnableDisableDevicePacket, FMSControllerEnableDisableDeviceResponsePacket>(packet, TimeSpan.FromSeconds(5));
                    if (resp.TimedOut)
                    {
                        MessageBox.Show("Server response timed out");
                        return;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured while enabling device\nex: " + ex.Message);
                }
                finally
                {
                    this.Enabled = true;
                }
            });
        }

        private void buttonDisableDevice_Click(object sender, EventArgs e)
        {

            if (comboBoxDeviceSelection.SelectedIndex == -1) return;

            byte deviceID = Devices.ToList()[comboBoxDeviceSelection.SelectedIndex].Key;

            this.Enabled = false;
            Task.Run(async delegate ()
            {
                try
                {
                    var packet = new FMSControllerEnableDisableDevicePacket(new byte[] { deviceID }, false);

                    var resp = await ServerCommunication.client.SendPacketAndWaitForResponseAsync<FMSControllerEnableDisableDevicePacket, FMSControllerEnableDisableDeviceResponsePacket>(packet, TimeSpan.FromSeconds(5));
                    if (resp.TimedOut)
                    {
                        MessageBox.Show("Server response timed out");
                        return;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured while disabling device\nex: " + ex.Message);
                }
                finally
                {
                    this.Enabled = true;
                }
            });
        }

        private void buttonDeviceShortcut1_Click(object sender, EventArgs e)
        {

        }

        private void buttonDeviceShortcutRST_Click(object sender, EventArgs e)
        {

        }

        private void buttonShortcut2_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxDeviceSelection_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region Point Control

        Dictionary<int, (PointSource, int)> REDPoints = new();
        Dictionary<int, (PointSource, int)> BLUEPoints = new();

        void ResetPointListboxes()
        {
            listBoxBLUEPoints.Items.Clear();
            listBoxREDPoints.Items.Clear();


            foreach (var kvp in REDPoints)
            {
                string text = $"{kvp.Value.Item1} - {kvp.Value.Item2}p";
                listBoxREDPoints.Items.Add(text);
            }

            foreach (var kvp in BLUEPoints)
            {
                string text = $"{kvp.Value.Item1} - {kvp.Value.Item2}p";
                listBoxBLUEPoints.Items.Add(text);
            }
        }

        private void HandlePointRemove(FMSControllerPointRemovedPacket packet)
        {
            if (packet.Alliance == TeamColor.RED) REDPoints.Remove(packet.PointID);
            else if (packet.Alliance == TeamColor.BLUE) BLUEPoints.Remove(packet.PointID);

            ResetPointListboxes();
        }

        private void HandlePointAdd(FMSControllerPointAddedPacket packet)
        {
            if (packet.Alliance == TeamColor.RED) REDPoints.Add(packet.PointID, (packet.PointSource, packet.PointValue));
            else if (packet.Alliance == TeamColor.BLUE) BLUEPoints.Add(packet.PointID, (packet.PointSource, packet.PointValue));

            ResetPointListboxes();

        }

        private void buttonREDDeletePoint_Click(object sender, EventArgs e)
        {
            if (listBoxREDPoints.SelectedIndex == -1) return;

            int pointID = REDPoints.ElementAt(listBoxREDPoints.SelectedIndex).Key;

            this.Enabled = false;
            Task.Run(async delegate ()
            {
                try
                {
                    FMSControllerRemovePointPacket packet = new FMSControllerRemovePointPacket(pointID, TeamColor.RED);

                    await ServerCommunication.client.SendPacketAsync(packet);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured while deleting red point\nex: " + ex.Message);
                }
                finally
                {
                    this.Enabled = true;
                }
            });
        }

        private void buttonBLUEDeletePoint_Click(object sender, EventArgs e)
        {

            if (listBoxBLUEPoints.SelectedIndex == -1) return;

            int pointID = BLUEPoints.ElementAt(listBoxBLUEPoints.SelectedIndex).Key;

            this.Enabled = false;
            Task.Run(async delegate ()
            {
                try
                {
                    FMSControllerRemovePointPacket packet = new FMSControllerRemovePointPacket(pointID, TeamColor.BLUE);

                    await ServerCommunication.client.SendPacketAsync(packet);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured while deleting blue point\nex: " + ex.Message);
                }
                finally
                {
                    this.Enabled = true;
                }
            });
        }

        private void buttonApprovePoints_Click(object sender, EventArgs e)
        {

            this.Enabled = false;
            Task.Run(async delegate ()
            {
                try
                {
                    var packet = new FMSControllerApprovePointsPacket();

                    await ServerCommunication.client.SendPacketAsync(packet);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured while approving points\nex: " + ex.Message);
                }
                finally
                {
                    this.Enabled = true;
                }
            });
        }

        #endregion

        #region AuDisControl

        private void HandleAuDisUpdated(FMSControllerAuDisPageUpdatedPacket packet)
        {
            labelAuDisPage.Text = $"Page: {packet.auDisPage}";
        }

        private void AuDisUpdatePage(object sender, EventArgs e)
        {
            int index = int.Parse((string)((Control)sender).Tag);

            FMSControllerSwitchAuDisPagePacket packet = new() { auDisPage = (AuDisPage)index };

            this.Enabled = false;
            Task.Run(async delegate ()
            {
                try
                {
                    await ServerCommunication.client.SendPacketAsync(packet);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured while sending audis update\nex: " + ex.Message);
                }
                finally
                {
                    this.Enabled = true;
                }
            });
        }

        #endregion

        #region Field Control
        private void buttonEnableField_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Task.Run(async delegate ()
            {
                try
                {
                    await ServerCommunication.client.SendPacketAsync(new FMSControllerToggleElectricityPacket(true));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured while toggling field electricity\nex: " + ex.Message);
                }
                finally
                {
                    this.Enabled = true;
                }
            });
        }

        private void buttonDisableField_Click(object sender, EventArgs e)
        {

            this.Enabled = false;
            Task.Run(async delegate ()
            {
                try
                {
                    await ServerCommunication.client.SendPacketAsync(new FMSControllerToggleElectricityPacket(false));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured while toggling field electricity\nex: " + ex.Message);
                }
                finally
                {
                    this.Enabled = true;
                }
            });
        }
        #endregion


    }




}
