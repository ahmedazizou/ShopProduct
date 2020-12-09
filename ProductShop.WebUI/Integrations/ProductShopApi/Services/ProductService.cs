using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ProductShop.WebUI.Integrations.ProductShop.Api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;

using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ProductShop.WebUI.Integrations.ProductShop.Api.Services
{
    public class ProductService
    {

       
        public ProductService()
        {

        }
        public static async Task<List<Product>> Get
            
            
            ()
        {

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using HttpResponseMessage res = await client.GetAsync(ProductShopApiHelper.BaseUrl + "products");
                if (res.IsSuccessStatusCode)
                {
                    using HttpContent content = res.Content;
                    string data = await content.ReadAsStringAsync();

                    if (data != null)
                    {
                        return JsonConvert.DeserializeObject<List<Product>>(data);
                    }
                }


            }
            return null;
        }
        public static async Task<Product> GetProductById(int id)
        {

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using HttpResponseMessage res = await client.GetAsync(ProductShopApiHelper.BaseUrl + "products/" + id);
                if (res.IsSuccessStatusCode)
                {
                    using HttpContent content = res.Content;
                    string data = await content.ReadAsStringAsync();

                    if (data != null)
                    {
                        return JsonConvert.DeserializeObject<Product>(data);
                    }
                }


            }
            return null;
        }
        public static async Task<HttpResponseMessage> CreateProduct(Product product,IFormFile formFile)
        {

            using (HttpClient client = new HttpClient())
            {
                using (var formData = new MultipartFormDataContent())
                {
                    
                    byte[] imgData;
                    using (var memoryStream = new MemoryStream())
                    {
                        formFile.OpenReadStream().CopyTo(memoryStream);
                        imgData = memoryStream.ToArray();
                        
                    }
                    
                    formData.Add(new StreamContent(new MemoryStream(imgData)), "Image", formFile.FileName);
                    formData.Add(new StringContent(product.Name), "Name");
                    formData.Add(new StringContent(product.Detail), "Detail");
                    formData.Add(new StringContent(product.Amount.ToString()), "Amount");
                    formData.Add(new StringContent(product.Stock.ToString()), "Stock");
                    using HttpResponseMessage res = await client.PostAsync(ProductShopApiHelper.BaseUrl + "products/", formData);
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
        }

        public static async Task<HttpResponseMessage> UpdateProduct(Product product, IFormFile formFile,int id)
        {

            using (HttpClient client = new HttpClient())
            {
                using (var formData = new MultipartFormDataContent())
                {
                    byte[] imgData;
                    using (var memoryStream = new MemoryStream())
                    {
                        formFile.OpenReadStream().CopyTo(memoryStream);
                        imgData = memoryStream.ToArray();

                    }

                    formData.Add(new StreamContent(new MemoryStream(imgData)), "Image", formFile.FileName);
                    formData.Add(new StringContent(product.Name), "Name");
                    formData.Add(new StringContent(product.Detail), "Detail");
                    formData.Add(new StringContent(product.Amount.ToString()), "Amount");
                    formData.Add(new StringContent(product.Stock.ToString()), "Stock");
                    using HttpResponseMessage res = await client.PutAsync(ProductShopApiHelper.BaseUrl + $"products/{id}", formData);
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
        }
        public static async Task<HttpResponseMessage> DeleteProduct(int id)
        {

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using HttpResponseMessage res = await client.DeleteAsync(ProductShopApiHelper.BaseUrl + "products/" + id);
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
    }
}
