using MiniFRC_ControlApp.Comms;
using System.Net;

namespace MiniFRC_ControlApp
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        void DisplayDeviceLastSeen(FMSControllerDeviceLastseenUpdatedPacket packet)
        {
            var devices = packet.GetDevices();
            Font boxFont = richTextBoxDevicesLastSeen.Font;

            DateTime now = DateTime.Now;

            richTextBoxDevicesLastSeen.Clear();
            foreach (var kvp in devices)
            {
                int diffSecs = (int)(kvp.Value - now).TotalSeconds;

                richTextBoxDevicesLastSeen.AppendText($"{kvp.Key.Item2} {kvp.Key.Item1}: ", Color.Black, new Font(boxFont, FontStyle.Bold));

                string text = $"[{kvp.Value.ToLongTimeString()}]{(kvp.Value == DateTime.MinValue ? "" : $"({diffSecs})")}\n";
                TimeSpan diff = now - kvp.Value;
                if (diff < TimeSpan.FromSeconds(5)) richTextBoxDevicesLastSeen.AppendText(text, Color.Green, new Font(boxFont, FontStyle.Italic | FontStyle.Bold));
                else if (diff < TimeSpan.FromSeconds(10)) richTextBoxDevicesLastSeen.AppendText(text, Color.DarkOrange, new Font(boxFont, FontStyle.Italic | FontStyle.Bold | FontStyle.Underline));
                else richTextBoxDevicesLastSeen.AppendText(text, Color.Red, new Font(boxFont, FontStyle.Italic | FontStyle.Bold | FontStyle.Strikeout));
            }
        }

        void AttachPacketCallbacks()
        {
            ServerCommunication.AttachPacketCB<FMSControllerMatchStateUpdatedPacket>(HandleMatchUpdate);
            ServerCommunication.AttachPacketCB<FMSControllerDeviceLastseenUpdatedPacket>(DisplayDeviceLastSeen);
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if(ServerCommunication.client != null && ServerCommunication.client.Connected)
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



        void HandleMatchUpdate(FMSControllerMatchStateUpdatedPacket p)
        {
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
                $"TB1: {p.ID_BLUE1}\n" +
                $"TB2: {p.ID_BLUE2}\n" +
                $"Rematch: {p.Rematch == 1}\n" +
                $"Practice: {p.Practice == 1}\n";

            labelRedPoints.Text = p.REDPoints.ToString();
            labelBluePoints.Text = p.BLUEPoints.ToString();

        }


        private void FormMain_Load(object sender, EventArgs e)
        {

        }


        private void buttonMatchLoad_Click(object sender, EventArgs e)
        {
            byte matchid = (byte)numericUpDownMatchID.Value;

            UInt16 matchDuration = (UInt16)numericUpDownMatchDuration.Value;

            byte red1 = (byte)numericUpDownMatchRedAllienceTeam1.Value;
            byte red2 = (byte)numericUpDownMatchRedAllienceTeam2.Value;
            byte blue1 = (byte)numericUpDownMatchBlueAllienceTeam1.Value;
            byte blue2 = (byte)numericUpDownMatchBlueAllienceTeam2.Value;
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
                        ID_RED1 = red1,
                        ID_RED2 = red2,
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

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }




}
