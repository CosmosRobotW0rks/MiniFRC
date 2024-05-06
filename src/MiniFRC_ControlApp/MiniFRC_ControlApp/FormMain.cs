using MiniFRC_ControlApp.Comms;
using MiniFRC_ControlApp.Comms.Packets;
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Error occured while authenticating\nex: " + ex.Message);
                }
                finally
                {
                    this.Enabled = true;
                }
            });

          
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }
    }
}
