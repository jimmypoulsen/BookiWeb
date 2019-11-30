using BookiWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BookiWeb.Controllers
{
    public class RegistrationsController : Controller
    {
        string BaseUrl = "https://localhost:44314/api/";
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Customer customer)
        {
            var customerInfo = new
            {
                Customer = customer
            };
            var json = JsonConvert.SerializeObject(customerInfo);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = BaseUrl + "/customers";
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(url, data);
                string result = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Create", "Sessions");
                else
                    return RedirectToAction("Create");
            }
        }
    }
}