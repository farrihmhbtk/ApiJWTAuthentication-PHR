using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using UseApi.Models;

namespace UseApi.Controllers
{
    public class BrandController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl = "/ApiGateway/Equipment/Brand";
        public BrandController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient("API_OCELOT");
            HttpResponseMessage response = await httpClient.GetAsync(_apiBaseUrl);
            bool showTable = false;

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var _list = JsonConvert.DeserializeObject<List<Brand>>(json);
                showTable = true;
                ViewBag.StatusCode = (int)response.StatusCode;
                ViewBag.ShowTable = showTable;
                return View(_list);
            }
            else
            {
                ViewBag.StatusCode = (int)response.StatusCode;
                ViewBag.ShowTable = showTable;
                return View();
            }
        }
        public async Task<IActionResult> Create()
        {
            var httpClient = _httpClientFactory.CreateClient("API_OCELOT");
            HttpResponseMessage response = await httpClient.GetAsync(_apiBaseUrl);
            bool showTable = false;

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var _list = JsonConvert.DeserializeObject<List<Brand>>(json);
                showTable = true;
                ViewBag.StatusCode = (int)response.StatusCode;
                ViewBag.ShowTable = showTable;
                if (_list.Count > 0)
                {
                    int maxId = _list.Max(brand => brand.Id);
                    ViewBag.LastId = maxId+1;
                }
                else
                {
                    ViewBag.LastId = 1;
                }
                return View();
            }
            else
            {
                ViewBag.StatusCode = (int)response.StatusCode;
                ViewBag.ShowTable = showTable;
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(Brand model)
        {
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient("API_OCELOT");
                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(_apiBaseUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Terjadi kesalahan saat membuat data di API.");
                    return View(model);
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("API_OCELOT");
            var deleteUrl = $"{_apiBaseUrl}/{id}";
            HttpResponseMessage response = await httpClient.DeleteAsync(deleteUrl);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Terjadi kesalahan saat menghapus data di API.");
                return RedirectToAction("Index");
            }
        }
    }
}
