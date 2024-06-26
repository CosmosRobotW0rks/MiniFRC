﻿using SimpleWebServer;
using SimpleWebServer.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HttpMethod = SimpleWebServer.HttpMethod;

namespace MiniFRC_FMS.Modules.Comms
{
    [ModuleInitPriority(1)]
    internal class WebServerModule : BaseModule
    {
        private WebServer? webServer;

        protected override bool Init()
        {
            webServer = new WebServer(Config.WebSVRootURL);

            webServer.Start();

            return true;
        }

        private bool SecurityCheckPreExecute(HttpListenerContext ctx)
        {
            string? secKeyHeader = ctx.Request.Headers["SecurityKey"];

            if (secKeyHeader == null || secKeyHeader != Config.SecurityKey.ToString())
            {
                ctx.Response.StatusCode = 401;
                ctx.Response.Close();
                return false;
            }

            return true;
        }

        public void AddRoute(string path, WebServer.ControllerMethod controllerMethod, HttpMethod httpmethod)
        {
            webServer?.AddRoute(path, controllerMethod, SecurityCheckPreExecute, httpmethod);
        }
    }
}
