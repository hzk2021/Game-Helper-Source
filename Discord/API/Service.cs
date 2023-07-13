using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Security;
using System.Text;

namespace Discord.API
{
    public static class Service
    {
        public static LiveGameData GetAPIData()
        {
            Uri lolEndPoint = new Uri("https://127.0.0.1:2999/liveclientdata/allgamedata"); //API Endpoint

            ServicePointManager.ServerCertificateValidationCallback = new
            RemoteCertificateValidationCallback
            (
            delegate { return true; }
            );

            var json = new WebClient() { Encoding = Encoding.UTF8 }.DownloadString(lolEndPoint);
            var result = JsonConvert.DeserializeObject<LiveGameData>(json);

            return result;
        }
    }

}
