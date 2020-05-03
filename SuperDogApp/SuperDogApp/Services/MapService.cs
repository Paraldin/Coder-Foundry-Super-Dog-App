using SuperDogApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SuperDogApp.Services
{
    class MapService : IMapService
    {
        static string _googleMapsKey;
        
        public async Task<SearchResultModel> GetTextSearch(SearchModel model)
        {
            var client = Constants.Client;
            SearchResultModel resultModel = null;
            _googleMapsKey = "AIzaSyBgX5VRIZycIRFEZCJ6v1duVMsZfIbC14c";
            try
            {
                var response = await client.GetAsync($"api/place/findplacefromtext/json?" +
                    $"&input={model.Name}" +
                    $"&inputtype={model.InputType}" +
                    $"&fields={model.Fields}" +
                    $"&key={_googleMapsKey}");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    resultModel = SearchResultModel.FromJson(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }
            return resultModel;
        }
    }
}
