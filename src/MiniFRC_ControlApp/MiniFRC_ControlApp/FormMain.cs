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

        private void buttonConnect_Click(object sender, EventArgs e)
        {
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

                    MessageBox.Show(this, res ? "Authenticated Successfully" : "Failed to authenticate");

                    if (res)
                    {
                        foreach (Control ctr in this.Controls)
                        {
                            if (ctr is GroupBox) ctr.Enabled = true;
                        }

                        groupBoxLogin.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Error occured while authenticating\nex: " + ex.Message);
                }
                finally
                {
                    this.Enabled = true;
                    ServerCommunication.Disconnect();
                }
            });


        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            ServerCommunication.AttachPacketCB<FMSControllerMatchStateUpdatedPacket>(x => { }); // TODO: FILL
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
                            MessageBox.Show("Match loaded successfully");
                            break;
                        case FMSControllerLoadMatchResponsePacket.MatchLoadStatus.IncorrectTeamIDs:
                            MessageBox.Show("Incorrect Team ID/s");
                            break;
                        case FMSControllerLoadMatchResponsePacket.MatchLoadStatus.MatchExists:
                            MessageBox.Show("A match with the given ID already exists");
                            break;
                        case FMSControllerLoadMatchResponsePacket.MatchLoadStatus.NotStandby:
                            MessageBox.Show("Match status has to be standby to load a new match");
                            break;
                        case FMSControllerLoadMatchResponsePacket.MatchLoadStatus.SomethingElseWentWrong:
                            MessageBox.Show("Something went wrong while loading match");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "An error occured while loading match\nex: " + ex.Message);
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
                    MessageBox.Show(this, "An error occured while starting match\nex: " + ex.Message);
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
                    MessageBox.Show(this, "An error occured while aborting match\nex: " + ex.Message);
                }
                finally
                {
                    this.Enabled = true;
                }
            });
        }
    }
}
