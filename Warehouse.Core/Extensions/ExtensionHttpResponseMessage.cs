using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Warehouse.Core.Extensions
{
    public static class ExtensionHttpResponseMessage
    {
        public static async Task<TModel> ToModelAsync<TModel>(this HttpResponseMessage message)
            => message.IsSuccessStatusCode 
                ? JsonConvert.DeserializeObject<TModel>(await message.Content.ReadAsStringAsync()) 
                : default(TModel);
    }
}
