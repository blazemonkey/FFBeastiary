using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FFBestiary.Services.ImgurService.Models
{
    public class ImgurAllAlbumImages
    {
        public ImgurAllAlbumImages()
        {
            Data = new List<ImgurImage>();
        }

        [DataMember(Name = "data")]
        public List<ImgurImage> Data { get; set; }
        [DataMember(Name = "success")]
        public bool Success { get; set; }
        [DataMember(Name = "status")]
        public int Status { get; set; }
    }
}
