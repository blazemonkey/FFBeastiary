using FFBestiary.Services.FileReaderService;
using FFBestiary.Services.ImgurService.Models;
using FFBestiary.Services.JSONService;
using FFBestiary.Services.MessageDialogService;
using FFBestiary.Services.SQLiteService;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;

namespace FFBestiary.Services.ImgurService
{
    public class ImgurService : IImgurService
    {
        private IJSONService _json;
        private IFileReaderService _fileReader;
        private ISqlLiteService _sql;
        private IMessageDialogService _dialog;

        private const string _tokenUri = "https://api.imgur.com/oauth2/token";
        private const string _albumsUri = "https://api.imgur.com/3/account/me/albums/";
        private const string _albumImageUri = "https://api.imgur.com/3/account/me/album/{0}/images";

        private const string _errorMessage = "There is no internet connection detected. Enemy images will not be displayed.";

        private string _clientId;
        private string _clientSecret;
        private string _accessToken;
        private string _refreshToken;


        public ImgurService(IJSONService json, IFileReaderService fileReader, ISqlLiteService sql, IMessageDialogService dialog)
        {
            _json = json;
            _fileReader = fileReader;
            _sql = sql;
            _dialog = dialog;
        }

        public async Task<bool> Initialize()
        {
            try
            {
                var albums = await GetAllAlbums();

                foreach (var album in albums)
                {
                    await _sql.UpdateGameImgurId(album.Title, album.Id);
                    var images = await GetAlbumImages(album.Id);

                    foreach (var image in images)
                    {
                        await _sql.UpdateEnemyImagePath(image.Name, image.Link);
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private async Task ReadKeys()
        {
            var keyFileExists = await _fileReader.FileExists(ApplicationData.Current.LocalFolder, "key.json");

            if (!keyFileExists)
                await _fileReader.CopyFile(Package.Current.InstalledLocation, ApplicationData.Current.LocalFolder, "key.json");

            var keysJSON = await _fileReader.ReadFile(ApplicationData.Current.LocalFolder, "key.json");
            var keys = _json.Deserialize<ImgurKeys>(keysJSON);

            _clientId = keys.ClientId;
            _clientSecret = keys.ClientSecret;
            _accessToken = keys.AccessToken;
            _refreshToken = keys.RefreshToken;
        }

        private async Task WriteKeys()
        {
            var imgurKeys = new ImgurKeys
            {
                AccessToken = _accessToken,
                RefreshToken = _refreshToken,
                ClientId = _clientId,
                ClientSecret = _clientSecret
            };

            var jsonSerialize = _json.Serialize(imgurKeys);
            await _fileReader.WriteFile(ApplicationData.Current.LocalFolder, jsonSerialize, "key.json");
        }

        private async Task GetNewAccessToken()
        {

            var success = true;
            var error = false;

            using (var webRequest = new HttpClient())
            {
                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("client_id", _clientId));
                postData.Add(new KeyValuePair<string, string>("client_secret", _clientSecret));
                postData.Add(new KeyValuePair<string, string>("refresh_token", _refreshToken));
                postData.Add(new KeyValuePair<string, string>("grant_type", "refresh_token"));

                var content = new FormUrlEncodedContent(postData);
                
                try
                {
                    var response = await webRequest.PostAsync(_tokenUri, content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();

                    var fullResponse = _json.Deserialize<ImgurRefreshToken>(responseBody);
                    _accessToken = fullResponse.AccessToken;

                    await WriteKeys();
                }
                catch (HttpRequestException e)
                {
                    if (e.Message.StartsWith("Response status code does not indicate success: 403 (Permission Denied)."))
                        success = false;
                    else
                        error = true;                    
                }
                catch (Exception)
                {
                    error = true;
                }

                if (error)
                    await _dialog.Show(_errorMessage);
            }
        }

        public async Task<IEnumerable<ImgurAlbum>> GetAllAlbums()
        {
            var success = true;
            var error = false;

            using (var webRequest = new HttpClient())
            {
                await ReadKeys();

                var authHeader = string.Format("{0} {1}", "Bearer", _accessToken);
                webRequest.DefaultRequestHeaders.Add("Authorization", authHeader);

                try
                {
                    var response = await webRequest.GetAsync(_albumsUri);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();

                    var fullResponse = _json.Deserialize<ImgurAllAlbums>(responseBody);
                    return fullResponse.Data;
                }
                catch (HttpRequestException e)
                {
                    if (e.Message.StartsWith("Response status code does not indicate success: 403 (Permission Denied)."))
                        success = false;
                    else
                        error = true;             
                }
                catch (Exception)
                {
                    error = true;
                }
            }

            if (error)
                await _dialog.Show(_errorMessage);
       
            if (!success)
            {
                await GetNewAccessToken();
                return await GetAllAlbums();
            }

            return null;
        }

        public async Task<IEnumerable<ImgurImage>> GetAlbumImages(string id)
        {
            var success = true;
            var error = false;

            using (var webRequest = new HttpClient())
            {
                await ReadKeys();

                var authHeader = string.Format("{0} {1}", "Bearer", _accessToken);
                webRequest.DefaultRequestHeaders.Add("Authorization", authHeader);

                try
                {
                    var response = await webRequest.GetAsync(string.Format(_albumImageUri, id));
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();

                    var fullResponse = _json.Deserialize<ImgurAllAlbumImages>(responseBody);
                    return fullResponse.Data;
                }
                catch (HttpRequestException e)
                {
                    if (e.Message.StartsWith("Response status code does not indicate success: 403 (Permission Denied)."))
                        success = false;
                    else
                        error = true;
                }
                catch (Exception)
                {
                    error = true;
                }
            }

            if (error)
                await _dialog.Show(_errorMessage);

            if (!success)
            {
                await GetNewAccessToken();
                return await GetAlbumImages(id);
            }

            return null;
        }
    }
}
