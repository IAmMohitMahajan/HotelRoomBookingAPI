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
    public class HotelTypeController : Controller
    {
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61076");
            MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            HttpResponseMessage response = client.GetAsync("/api/HotelType").Result;
            string stringdata = response.Content.ReadAsStringAsync().Result;
            List<HotelType> data = JsonConvert.DeserializeObject<List<HotelType>>(stringdata);
            return View(data);
        }

        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(HotelType us)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61076");

            string stringData = JsonConvert.SerializeObject(us);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync("api/HotelType", contentData).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");

        }

        public IActionResult Detail(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61076"); 
            HttpResponseMessage response = client.GetAsync("/api/HotelType/"+id).Result;
            string stringdata = response.Content.ReadAsStringAsync().Result;
            HotelType data = JsonConvert.DeserializeObject<HotelType>(stringdata);
            return View(data);
        }

        public ActionResult Edit(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61076");

            HttpResponseMessage response = client.GetAsync("/api/HotelType/"+id).Result;
            string stringData = response.Content.ReadAsStringAsync().Result;
            HotelType data = JsonConvert.DeserializeObject<HotelType>(stringData);
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(HotelType hotelType)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61076");
            string stringData = JsonConvert.SerializeObject(hotelType);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync("/api/HotelType/"+hotelType.HotelTypeId, contentData).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61076");

            HttpResponseMessage response = client.GetAsync("/api/HotelType/"+id).Result;
            string stringData = response.Content.ReadAsStringAsync().Result;
            HotelType data = JsonConvert.DeserializeObject<HotelType>(stringData);
            return View(data);
        }
        [HttpPost]
        public ActionResult Delete(int id, HotelType hotelType)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61076");
            string stringData = JsonConvert.SerializeObject(hotelType);

            HttpResponseMessage response = client.DeleteAsync("/api/HotelType/" + id).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");
        }
    }
}