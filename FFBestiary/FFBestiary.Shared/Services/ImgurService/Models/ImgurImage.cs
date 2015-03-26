using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FFBestiary.Services.ImgurService.Models
{
    public class ImgurImage
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "title")]
        public string Title { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "datetime")]
        public uint DateTime { get; set; }
        [DataMember(Name = "type")]
        public string Type { get; set; }
        [DataMember(Name = "animated")]
        public bool Animated { get; set; }
        [DataMember(Name = "width")]
        public int Width { get; set; }
        [DataMember(Name = "height")]
        public int Height { get; set; }
        [DataMember(Name = "size")]
        public int Size { get; set; }
        [DataMember(Name = "views")]
        public int Views { get; set; }
        [DataMember(Name = "bandwidth")]
        public int Bandwidth { get; set; }
        [DataMember(Name = "deletehash")]
        public string DeleteHash { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "section")]
        public string Section { get; set; }
        [DataMember(Name = "link")]
        public string Link { get; set; }
        [DataMember(Name = "gifv")]
        public string Gifv { get; set; }
        [DataMember(Name = "mp4")]
        public string Mp4 { get; set; }
        [DataMember(Name = "webm")]
        public string Webm { get; set; }
        [DataMember(Name = "favorite")]
        public bool Favorite { get; set; }
        [DataMember(Name = "nsfw")]
        public string NSFW { get; set; }
        [DataMember(Name = "vote")]
        public string Vote { get; set; }
        [DataMember(Name = "account_url")]
        public string AccountUrl { get; set; }
        [DataMember(Name = "account_id")]
        public int AccountId { get; set; }
    }
}
