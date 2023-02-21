using BlazorApp1.Client.Models;
using System.Text.Json;

namespace BlazorApp1.Client.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly HttpClient client;

        private readonly JsonSerializerOptions options;
        public CategoryService(HttpClient httpClient)
        {
            this.client = httpClient;
            this.options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<Category>?> Get()
        {
            var reponse = await client.GetAsync("/v1/categories");
            var content = await reponse.Content.ReadAsStringAsync();
            if(!reponse.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            return JsonSerializer.Deserialize<List<Category>>(content, options);
        }
    }
}

public interface ICategoryService
{
    Task<List<Category>> Get();
}
