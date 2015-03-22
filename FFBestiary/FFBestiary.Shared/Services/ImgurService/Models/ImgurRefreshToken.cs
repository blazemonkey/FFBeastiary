using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFBestiary.Services.ImgurService.Models
{
    public class ImgurRefreshToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonProperty("bearer")]
        public string Bearer { get; set; }
        [JsonProperty("account_username")]
        public string AccountUsername { get; set; }
    }
}
