using MapoticPoiExportTool.Model;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;

namespace MapoticPoiExportTool.Service
{
    public class NetworkService
    {
        private readonly HttpClient _client;

        public NetworkService()
        {
            _client = new HttpClient();
        }

        public void Login(string email, string password)
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", GetAccessTokenString(email, password));
        }

        public JObject GetAvailableMaps()
        {
            string mapPoiDataString = GetAvailableMapsAsync().Result;
            JObject jsonObject = JObject.Parse(mapPoiDataString);
            return jsonObject;
        }

        public JObject GetAvailableMapPois(string mapId)
        {
            string mapPoiDataString = GetMapPoisAsync(mapId).Result;
            JObject jsonObject = JObject.Parse(mapPoiDataString);
            return jsonObject;
        }

        public JObject GetPoi(string mapId, string poiId)
        {
            string mapPoiDataString = GetPoiAsync(mapId, poiId).Result;
            JObject jsonObject = JObject.Parse(mapPoiDataString);
            return jsonObject;
        }

        public List<JObject> GetPois (string mapId, List<string> poiIds)
        {
            List<JObject> result = new List<JObject>();

            foreach (string poiId in poiIds)
            {
                var poiData = GetPoi(mapId, poiId);

                result.Add(poiData);
            }

            return result;
        }


        #region Private Methods

        private async System.Threading.Tasks.Task<string> GetAccessTokenAsync(string email, string password)
        {
            var loginCredentials = new Dictionary<string, string>
            {
                { "email", email },
                { "password", password }
            };

            var content = new FormUrlEncodedContent(loginCredentials);

            var response = await _client.PostAsync("https://www.mapotic.com/api/v1/auth/login/", content);

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

        private string GetAccessTokenString(string email, string password)
        {
            string jsonString = GetAccessTokenAsync(email, password).Result;
            JObject jsonObject = JObject.Parse(jsonString);
            return (string)jsonObject["auth_token"];
        }

        private async System.Threading.Tasks.Task<string> GetAvailableMapsAsync()
        {
            var response = await _client.GetAsync("https://www.mapotic.com/api/v1/auth/me/");
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        private async System.Threading.Tasks.Task<string> GetMapPoisAsync(string mapId)
        {
            var response = await _client.GetAsync("https://www.mapotic.com/api/v1/maps/" + mapId + "/pois.geojson/");

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

        private async System.Threading.Tasks.Task<string> GetPoiAsync(string mapId, string poiId)
        {
            var response = await _client.GetAsync("https://www.mapotic.com/api/v1/maps/" + mapId + "/public-pois/" + poiId + "/");

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

        #endregion
    }
}
