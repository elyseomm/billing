using BlazorApp2.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http.Json;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlazorApp2.API
{
    public class ProductApiClient : HttpClient
    {
        public ProductApiClient() { }
        public async Task<string> IsOnline()
        {
            return await GetStringAsync("/Product/ping");
        }

        public async Task<Product[]> GetAll()
        {
            var response = await GetAsync("/Product");
            if (response != null && response.IsSuccessStatusCode) {
                var stream = await response.Content.ReadAsStringAsync();
                if (stream != null)
                {
                    var list = JsonConvert.DeserializeObject<List<Product>>(stream);
                    return [.. list!];
                }
            }
            return [];
        }

        public async Task<Product> GetById(string id)
        {
            var response = await GetAsync($"/Product/{id}");
            if (response != null && response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStringAsync();
                if (stream != null)
                {
                    var tuple = JsonConvert.DeserializeObject<Product>(stream);
                    return tuple!;
                }
            }
            return new Product();
        }

        public async Task<string> Create(Product tuple)
        {
            var content = new StringContent(JObject.FromObject(tuple).ToString(), UnicodeEncoding.UTF8, "application/json");
            var response = await this.PostAsync("/Product/create", content);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            
            return "Error trying to create an item";
        }

        public async Task<string> Update(Product tuple)
        {
            var data = new Billing.Core.Models.Customer()
            {
                Id = tuple.Id,
                Name = tuple.ProductName,
                Active = tuple.Active
            };

            var content = new StringContent(JObject.FromObject(data).ToString(), UnicodeEncoding.UTF8, "application/json");
            var response = await this.PutAsync("/Product/update", content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return "Error trying to update an item";
        }
    }
}
