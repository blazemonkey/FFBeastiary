using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFBestiary.Services.ImgurService.Models
{
    public class ImgurKeys
    {
        [JsonProperty("client_id")]
        public string ClientId { get; set; }
        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
