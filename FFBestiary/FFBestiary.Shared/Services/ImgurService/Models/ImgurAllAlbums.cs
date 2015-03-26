using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FFBestiary.Services.ImgurService.Models
{
    public class ImgurAllAlbums
    {
        public ImgurAllAlbums()
        {
            Data = new List<ImgurAlbum>();
        }

        [DataMember(Name = "data")]
        public List<ImgurAlbum> Data { get; set; }
        [DataMember(Name = "success")]
        public bool Success { get; set; }
        [DataMember(Name = "status")]
        public int Status { get; set; }
    }
}
