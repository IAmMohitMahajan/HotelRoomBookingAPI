using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HotelController : Controller
    {
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61076");
            MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            HttpResponseMessage response = client.GetAsync("/api/Hotel").Result;
            string stringdata = response.Content.ReadAsStringAsync().Result;
            List<Hotel> data = JsonConvert.DeserializeObject<List<Hotel>>(stringdata);
            return View(data);
        }

        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Hotel us)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61076");

            string stringData = JsonConvert.SerializeObject(us);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync("api/Hotel", contentData).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");

        }

        public IActionResult Detail(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61076");
            HttpResponseMessage response = client.GetAsync("/api/Hotel/" + id).Result;
            string stringdata = response.Content.ReadAsStringAsync().Result;
            Hotel data = JsonConvert.DeserializeObject<Hotel>(stringdata);
            return View(data);
        }

        public ActionResult Edit(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61076");
            HttpResponseMessage response = client.GetAsync("/api/Hotel/" + id).Result;
            string stringdata = response.Content.ReadAsStringAsync().Result;
            Hotel data = JsonConvert.DeserializeObject<Hotel>(stringdata);
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(Hotel hotel)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61076");
            string stringData = JsonConvert.SerializeObject(hotel);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync("/api/Hotel/"+hotel.HotelId, contentData).Result;
            ViewBag.Meassage = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61076");

            HttpResponseMessage response = client.GetAsync("/api/Hotel/"+id).Result;
            string stringData = response.Content.ReadAsStringAsync().Result;
            Hotel data = JsonConvert.DeserializeObject<Hotel>(stringData);
            return View(data);
        }
        [HttpPost]
        public ActionResult Delete(int id, Hotel hotel)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61076");
            string stringData = JsonConvert.SerializeObject(hotel);

            HttpResponseMessage response = client.DeleteAsync("/api/Hotel/"+id).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");
        }
    }
}