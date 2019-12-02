using BookiWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BookiWeb.Controllers {
    public class ReservationsController : AuthenticationController {
        public async Task<ActionResult> Index() {
            List<Reservation> ReservationInfo = new List<Reservation>();

            using (var client = base.GetClient())
            {
                HttpResponseMessage Res = await client.GetAsync("reservations");
                if (Res.IsSuccessStatusCode) {
                    var ReservationResponse = Res.Content.ReadAsStringAsync().Result;
                    ReservationInfo = JsonConvert.DeserializeObject<List<Reservation>>(ReservationResponse);

                }
                return View(ReservationInfo);
            }
        }

        public ActionResult Create(int venueId = 0) {
            if (venueId == 0)
                return RedirectToAction("Index", "Venues");
            else
            {
                ViewBag.VenueId = venueId;
                return View();
            }
            
        }

        [HttpPost]
        public async Task<ActionResult> Create(Reservation res) {
            if (ModelState.IsValid)
            {
                var root = new
                {
                    Reservation = res
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
            else
            {
                return View(res);
            }
        }
        public async Task<ActionResult> Delete(int id) {
            using (var client = base.GetClient()) {
                var url = base.BaseUrl + "/reservations/" + id;
                var result = await client.DeleteAsync(url);
                if (result.IsSuccessStatusCode) {

                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
    }
}