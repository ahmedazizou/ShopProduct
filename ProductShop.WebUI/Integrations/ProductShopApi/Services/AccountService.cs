using Newtonsoft.Json;
using ProductShop.WebUI.Integrations.ProductShop.Api;
using ProductShop.WebUI.Integrations.ProductShopApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ProductShop.WebUI.Integrations.ProductShopApi.Services
{
    public class AccountService
    {
        public static async Task<List<User>> GetAllUsers()
        {

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using HttpResponseMessage res = await client.GetAsync(ProductShopApiHelper.BaseUrl + "authencation/users");
                if (res.IsSuccessStatusCode)
                {
                    using HttpContent content = res.Content;
                    string data = await content.ReadAsStringAsync();

                    if (data != null)
                    {
                        return JsonConvert.DeserializeObject<List<User>>(data);
                    }
                }


            }
            return null;
        }

        public static async Task<HttpResponseMessage> RegisterUser(User user)
        {

            using (HttpClient client = new HttpClient())
            {

                using HttpResponseMessage res = await client.PostAsJsonAsync(ProductShopApiHelper.BaseUrl + "authencation/register", user);
                if (res.IsSuccessStatusCode)
                {
                    return res;
                }
                else
                {
                    return res;

                }


            }
        
        }
        public static async Task<HttpResponseMessage> RegisterUserAdmin(User user)
        {

            using (HttpClient client = new HttpClient())
            {

                using HttpResponseMessage res = await client.PostAsJsonAsync(ProductShopApiHelper.BaseUrl + "authencation/registerAdmin", user);
                if (res.IsSuccessStatusCode)
                {
                    return res;
                }
                else
                {
                    return res;

                }


            }

        }

        public static async Task<User> Login(LoginModel model)
        {

            using (HttpClient client = new HttpClient())
            {

                using HttpResponseMessage res = await client.PostAsJsonAsync(ProductShopApiHelper.BaseUrl + "authencation/login", model);
                if (res.IsSuccessStatusCode)
                {
                    using HttpContent content = res.Content;
                    string data = await content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<User>(data);

                }
                else
                {
                    return null;

                }


            }

        }

        public static async Task<HttpResponseMessage> UpdateUser(User user, int id)
        {

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using HttpResponseMessage res = await client.PutAsJsonAsync(ProductShopApiHelper.BaseUrl + $"authencation/{id}", user);
                if (res.IsSuccessStatusCode)
                {
                    return res;
                }
                else
                {
                    return res;

                }

            }
        }
        public static async Task<HttpResponseMessage> DeleteUser(int id)
        {

            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using HttpResponseMessage res = await client.DeleteAsync(ProductShopApiHelper.BaseUrl + "authencation/" + id);
            if (res.IsSuccessStatusCode)
            {
                return res;
            }
            else
            {
                return res;

            }
        }
        public static async Task<User> GetUserById(int id)
        {

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using HttpResponseMessage res = await client.GetAsync(ProductShopApiHelper.BaseUrl + "authencation/" + id);
                if (res.IsSuccessStatusCode)
                {
                    using HttpContent content = res.Content;
                    string data = await content.ReadAsStringAsync();

                    if (data != null)
                    {
                        return JsonConvert.DeserializeObject<User>(data);
                    }
                }


            }
            return null;
        }
    }
}
