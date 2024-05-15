using MiniFRC_FMS.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;
using Logger = MiniFRC_FMS.Utils.Logger;
using LogLevel = MiniFRC_FMS.Utils.LogLevel;

namespace MiniFRC_FMS.Modules.Comms
{
    [ModuleInitPriority(3)]
    internal class WebSocketModule : BaseModule
    {
        WebSocketServer server;

        List<WSClient> clients = new();

        public WSClient[] Clients => clients.ToArray();


        public int Announce(object data)
        {
            foreach (var client in clients)
            {
                if(client != null)
                client.SendData(data);
            }

            return clients.Count;
        }

        public int Announce(string data)
        {
            foreach (var client in clients)
            {
                client.SendData(data);
            }

            return clients.Count;
        }

        protected override bool Init()
        {
            server = new WebSocketServer(Config.WebSocketRootURL);

            server.AddWebSocketService<WSClient>("/ws");

            server.Start();
            
            return true;
        }

        private void NewClient(WSClient client)
        {
            Logger.Log($"WS Client Connected ({client.GetHashCode()})", LogLevel.DEBUG);
            clients.Add(client);
        }


        public class WSClient : WebSocketBehavior
        {
            public event EventHandler<string>? OnDataReceive;
            public event EventHandler? OnClientClose;

            public void SendData(string data)
            {
                try
                {
                    Send(data);
                }
                catch(Exception ex)
                {
                    Logger.Log($"WSClient SendData errored: {ex.Message}", LogLevel.WARNING);
                }
            }

            public void SendData(object obj)
            {
                string serialized = JsonConvert.SerializeObject(obj);
                SendData(serialized);
            }

            protected override void OnOpen()
            {
                ModulesMain.Instance.GetModule<WebSocketModule>().NewClient(this);
            }

            protected override void OnClose(CloseEventArgs e)
            {
                Logger.Log($"WS Client Closed ({this.GetHashCode()})", LogLevel.DEBUG);
                ModulesMain.Instance.GetModule<WebSocketModule>().clients.Remove(this);
                OnClientClose?.Invoke(this, null);
            }

            protected override void OnError(WebSocketSharp.ErrorEventArgs e)
            {
                Logger.Log($"WSClient errored: {e.Exception?.Message}", LogLevel.WARNING);
            }

            protected override void OnMessage(MessageEventArgs e)
            {
                if (e.IsText)
                {
                    OnDataReceive?.Invoke(this, e.Data);
                }
                else
                {
                    Logger.Log($"WSBehavior OnMessage ({this.GetHashCode()}) - Binary data received", LogLevel.DEBUG);
                }
            }

        }

    }
}
