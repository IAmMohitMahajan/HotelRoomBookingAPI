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
    public class RoomFacilityController : Controller
    {
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61076");
            MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            HttpResponseMessage response = client.GetAsync("/api/RoomFacility").Result;
            string stringdata = response.Content.ReadAsStringAsync().Result;
            List<RoomFacility> data = JsonConvert.DeserializeObject<List<RoomFacility>>(stringdata);
            return View(data);
        }

        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(RoomFacility us)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61076");

            string stringData = JsonConvert.SerializeObject(us);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync("api/RoomFacility", contentData).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");

        }

        public IActionResult Detail(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61076");
            HttpResponseMessage response = client.GetAsync("/api/RoomFacility/" + id).Result;
            string stringdata = response.Content.ReadAsStringAsync().Result;
            RoomFacility data = JsonConvert.DeserializeObject<RoomFacility>(stringdata);
            return View(data);
        }

        public ActionResult Edit(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61076");
            HttpResponseMessage response = client.GetAsync("/api/RoomFacility/" + id).Result;
            string stringdata = response.Content.ReadAsStringAsync().Result;
            RoomFacility data = JsonConvert.DeserializeObject<RoomFacility>(stringdata);
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(RoomFacility roomFacility)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61076");
            string stringData = JsonConvert.SerializeObject(roomFacility);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync("/api/RoomFacility/" + roomFacility.RoomFacilityId, contentData).Result;
            ViewBag.Meassage = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61076");

            HttpResponseMessage response = client.GetAsync("/api/RoomFacility/" + id).Result;
            string stringData = response.Content.ReadAsStringAsync().Result;
            RoomFacility data = JsonConvert.DeserializeObject<RoomFacility>(stringData);
            return View(data);
        }
        [HttpPost]
        public ActionResult Delete(int id, RoomFacility roomFacility)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61076");
            string stringData = JsonConvert.SerializeObject(roomFacility);

            HttpResponseMessage response = client.DeleteAsync("/api/RoomFacility/" + id).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");
        }
    }
}