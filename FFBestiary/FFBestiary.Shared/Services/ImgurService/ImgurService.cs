using FFBestiary.Services.FileReaderService;
using FFBestiary.Services.ImgurService.Models;
using FFBestiary.Services.JSONService;
using FFBestiary.Services.MessageDialogService;
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
        private IMessageDialogService _dialog;

        private const string _tokenUri = "https://api.imgur.com/oauth2/token";
        private const string _albumsUri = "https://api.imgur.com/3/account/me/albums/";

        private const string _errorMessage = "There is no internet connection detected. Enemy images will not be displayed.";

        private string _clientId;
        private string _clientSecret;
        private string _accessToken;
        private string _refreshToken;

        public ImgurService(IJSONService json, IFileReaderService fileReader, IMessageDialogService dialog)
        {
            _json = json;
            _fileReader = fileReader;
            _dialog = dialog;

            ReadKeys();
            GetAllAlbums();
        }

        private async void ReadKeys()
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

        private void WriteKeys()
        {
            var imgurKeys = new ImgurKeys
            {
                AccessToken = _accessToken,
                RefreshToken = _refreshToken,
                ClientId = _clientId,
                ClientSecret = _clientSecret
            };

            var jsonSerialize = _json.Serialize(imgurKeys);
            _fileReader.WriteFile(ApplicationData.Current.LocalFolder, jsonSerialize, "key.json");
        }

        private async Task GetNewAccessToken()
        {
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

                    WriteKeys();
                }
                catch (HttpRequestException e)
                {

                }
            }
        }

        public async Task<IEnumerable<ImgurAlbum>> GetAllAlbums()
        {
            var success = true;

            using (var webRequest = new HttpClient())
            {
                
                var authHeader = string.Format("{0} {1}", "Bearer", _accessToken);
                webRequest.DefaultRequestHeaders.Add("Authorization", authHeader);

                try
                {
                    var response = await webRequest.GetAsync(_albumsUri);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();

                    var fullResponse = _json.Deserialize<ImgurAllAlbumsJson>(responseBody);
                    return fullResponse.Data;
                }
                catch (HttpRequestException e)
                {
                    if (e.Message.StartsWith("Response status code does not indicate success: 403 (Permission Denied)."))
                        success = false;
                    else                    
                        _dialog.Show(_errorMessage);                    
                }
                catch (Exception)
                {
                    _dialog.Show(_errorMessage);
                }
            }    
       
            if (!success)
            {
                await GetNewAccessToken();
                await GetAllAlbums();
            }

            return null;
        }

        public string GetAlbum(string game)
        {

            return "";
        }
    }
}
