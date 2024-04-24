using MiniFRC_FMS.Utils;
using SimpleWebServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HttpMethod = SimpleWebServer.HttpMethod;

namespace MiniFRC_FMS.FieldItems
{
    internal abstract class BaseFieldItem
    {
        public abstract string Host { get; protected set; }

        public abstract int Port { get; protected set; }

        protected string RootURL => $"http://{Host}:{Port}";

        public async Task<bool> Ping()
        {
            var resp = await FieldRequestSender.GET(RootURL + "/ping", TimeSpan.FromSeconds(5));

            if (resp == null || resp.StatusCode != HttpStatusCode.OK) return false;

            return true;
        }


        public BaseFieldItem(string host, int port)
        {
            Host = host;
            Port = port;
        }
    }
}
