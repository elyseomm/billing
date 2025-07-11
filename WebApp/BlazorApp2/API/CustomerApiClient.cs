using BlazorApp2.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;
using System.Text;

namespace BlazorApp2.API
{
    public class CustomerApiClient : HttpClient
    {
        public CustomerApiClient() { }
        public async Task<string> IsOnline()
        {
            return await GetStringAsync("/Customer/ping");
        }

        public async Task<Customer[]> GetAll()
        {
            var response = await GetAsync("/Customer");
            if (response != null && response.IsSuccessStatusCode) {
                var stream = await response.Content.ReadAsStringAsync();
                if (stream != null)
                {
                    var list = JsonConvert.DeserializeObject<List<Customer>>(stream);
                    return [.. list!.OrderBy(x => x.Name).ToList()];
                }
            }
            return [];
        }

        public async Task<Customer> GetById(string id)
        {
            var response = await GetAsync($"/Customer/{id}");
            if (response != null && response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStringAsync();
                if (stream != null)
                {
                    var tuple = JsonConvert.DeserializeObject<Customer>(stream);
                    return tuple!;
                }
            }
            return new Customer();
        }

        public async Task<string> Create(string jsonStringFromObject)
        {
            JObject jObj = JObject.Parse(jsonStringFromObject);

            var response = await this.PostAsJsonAsync("/Customer/create", jObj, new CancellationToken());

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            
            return "Error trying to create an item";
        }

        public async Task<string> Update(Customer tuple)
        {
            var data = new Billing.Core.Models.Customer()
            {
                Id = tuple.Id,
                Name = tuple.Name,
                Email = tuple.Email,
                Address = tuple.Address,
                Active = tuple.Active
            };

            var content = new StringContent(JObject.FromObject(data).ToString(), UnicodeEncoding.UTF8, "application/json");
            var response = await this.PutAsync("/Customer/update", content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return "Error trying to update an item";
        }
    }
}
