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
    public class VenuesController : BaseController
    {
        public async Task<ActionResult> Index()
        {
            List<Venue> VenueInfo = new List<Venue>();

            using (var client = base.GetClient())
            {
                HttpResponseMessage Res = await client.GetAsync("venues");

                if (Res.IsSuccessStatusCode)
                {
                    var VenueResponse = Res.Content.ReadAsStringAsync().Result;
                    VenueInfo = JsonConvert.DeserializeObject<List<Venue>>(VenueResponse);
                }
                return View(VenueInfo);
            }
        }

        public ActionResult Create()
        {
            ViewBag.Message = "Create new venue";

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Venue venue)
        {
            var root = new
            {
                Venue = venue
            };
            var json = JsonConvert.SerializeObject(root);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = base.GetClient())
            {
                var response = await client.PostAsync(base.BaseUrl, data);
                string result = response.Content.ReadAsStringAsync().Result;
            }

            return RedirectToAction("Index");
        }
    }
}
