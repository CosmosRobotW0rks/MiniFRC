using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Utils
{
    internal class FieldRequestResponse
    {
        public HttpResponseMessage? respMessage { get; private set; }

        public string? ResponseContent => respMessage?.Content.ReadAsStringAsync().Result;

        public HttpStatusCode? StatusCode => respMessage?.StatusCode;

        public FieldRequestResponse(HttpResponseMessage? respMessage)
        {
            this.respMessage = respMessage;
        }

        ~FieldRequestResponse()
        {
            respMessage?.Dispose();
        }
    }

    internal static class FieldRequestSender
    {
        public static async Task<FieldRequestResponse?> GET(string url, TimeSpan timeout)
        {

            try
            {
                using HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, url);
                msg.Headers.Add("SecurityKey", Config.SecurityKey.ToString());

                using HttpClient client = new HttpClient();
                
                client.Timeout = timeout;

                var resp = await client.SendAsync(msg);

                if (resp == null) throw new Exception("Null response");

                if (!resp.IsSuccessStatusCode) throw new Exception($"Status code: {resp.StatusCode}");
                
                return new(resp);
                
            }
            catch(Exception ex)
            {
                Logger.Log(LogLevel.ERROR, $"Error occured while sending GET request to {url} (ex: {ex.Message})");
            }
            finally
            {
            }

            return null;
        }
    }
}
