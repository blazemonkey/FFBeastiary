using FFBestiary.Services.ImgurService.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FFBestiary.Services.ImgurService
{
    public interface IImgurService
    {
        Task<IEnumerable<ImgurAlbum>> GetAllAlbums();
        Task<IEnumerable<ImgurImage>> GetAlbumImages(string id);     
    }
}
