using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace MapoticPoiExportTool
{
    class NetworkManager
    {
        private readonly HttpClient _client;
        private string _mapId;

        public NetworkManager(string mapId, string email, string password)
        {
            _client = new HttpClient();
            _mapId = mapId;
            login(email, password);
        }

        private async System.Threading.Tasks.Task<string> getAccessTokenAsync(string email, string password)
        {
            var values = new Dictionary<string, string>
            {
                { "email", email },
                { "password", password }
            };

            var content = new FormUrlEncodedContent(values);

            var response = await _client.PostAsync("https://www.mapotic.com/api/v1/auth/login/", content);

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

        private string getAccessTokenString(string email, string password)
        {
            string jsonString = getAccessTokenAsync(email, password).Result;
            JObject jsonObject = JObject.Parse(jsonString);
            return (string)jsonObject["auth_token"];
        }

        public void login(string email, string password)
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", getAccessTokenString(email, password));
        }

        private async System.Threading.Tasks.Task<string> getMapPoiDataAsync()
        {
            var response = await _client.GetAsync("https://www.mapotic.com/api/v1/maps/" + _mapId + "/pois.geojson/");

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

        public JObject getMapPoiDataObject()
        {
            string mapPoiDataString = getMapPoiDataAsync().Result;
            JObject jsonObject = JObject.Parse(mapPoiDataString);
            return jsonObject;
        }

        private async System.Threading.Tasks.Task<string> getPoiAdvancedDataAsync(string poiId)
        {
            var response = await _client.GetAsync("https://www.mapotic.com/api/v1/maps/" + _mapId + "/public-pois/" + poiId + "/");

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

        public JObject getPoiAdvancedDataObject(string poiId)
        {
            string mapPoiDataString = getPoiAdvancedDataAsync(poiId).Result;
            JObject jsonObject = JObject.Parse(mapPoiDataString);
            return jsonObject;
        }



    }
}
