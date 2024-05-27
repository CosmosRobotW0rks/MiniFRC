using MiniFRC_ControlApp.Comms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniFRC_ControlApp
{

    public partial class FormFieldControl : Form
    {

        Dictionary<object, (DeviceType, TeamColor, bool)> buttons = new();

        public FormFieldControl()
        {
            InitializeComponent();


            foreach(GroupBox groupbox in this.Controls)
            {
                string tag = groupbox.Tag.ToString();

                DeviceType device = (DeviceType)int.Parse(tag.Split('/')[0]);
                TeamColor color = (TeamColor)int.Parse(tag.Split('/')[1]);

                foreach (Button button in groupbox.Controls)
                {
                    bool enDis = (string)button.Tag == "1";

                    button.Click += (s, e) =>
                    {
                        Task.Run(async () =>
                        {
                            await ServerCommunication.client.SendPacketAsync(new FMSControllerEnableDisableDevicePacket(new byte[] { Utils.GetDeviceIDByDeviceInfo(device, color)},enDis));
                        });
                    };
                }
            }
        }
    }
}
