using BlazorApp2.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace BlazorApp2.API
{
    public class ServiceApiClient : HttpClient
    {
        public ServiceApiClient() { }
        public async Task<string> IsOnline()
        {
            try
            {
                return await GetStringAsync("/Service/ping");
            }
            catch
            {
            }
            return await Task.FromResult("-- offline --");
        }

        public async Task<T[]> GetAll<T>(string modelName)
        {
            var response = await GetAsync($"/{modelName}");
            if (response != null && response.IsSuccessStatusCode) {
                var stream = await response.Content.ReadAsStringAsync();
                if (stream != null)
                {
                    var list = JsonConvert.DeserializeObject<List<T>>(stream);
                    return [.. list!];
                }
            }
            return [];
        }

        public async Task<T> GetById<T>(string modelName, string id)
        {
            var response = await GetAsync($"/{modelName}/{id}");
            if (response != null && response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStringAsync();
                if (stream != null)
                {
                    var tuple = JsonConvert.DeserializeObject<T>(stream);
                    return tuple!;
                }
            }
            return await Task.FromResult(default(T));
        }

        public async Task<string> Create<T>(T tuple)
        {
            var content = new StringContent(JObject.FromObject(tuple!).ToString(), UnicodeEncoding.UTF8, "application/json");
            var response = await this.PostAsync("/Product/create", content);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            
            return "Error trying to create an item";
        }

        public async Task<string> Update<T>(T tuple)
        {
            var content = new StringContent(JObject.FromObject(tuple!).ToString(), UnicodeEncoding.UTF8, "application/json");
            var response = await this.PutAsync("/Product/update", content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return "Error trying to update an item";
        }
    }
}
