using BookiWeb.Models;
using BookiWeb.Helpers;
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
                HttpResponseMessage Res = await client.GetAsync($"customers/{SessionsHelper.GetId()}/reservations");
                if (Res.IsSuccessStatusCode) {
                    var ReservationResponse = Res.Content.ReadAsStringAsync().Result;
                    ReservationInfo = JsonConvert.DeserializeObject<List<Reservation>>(ReservationResponse);

                }
                return View(ReservationInfo);
            }
        }

        public async Task<ActionResult> Create(int venueId = 0) {
            if (venueId == 0)
                return RedirectToAction("Index", "Venues");
            else
            {
                List<Venue> VenueInfo = new List<Venue>();

                using (var client = base.GetClient())
                {
                    HttpResponseMessage Res = await client.GetAsync("venues/" + venueId);
                    if (Res.IsSuccessStatusCode)
                    {
                        var VenueResponse = Res.Content.ReadAsStringAsync().Result;
                        VenueInfo = JsonConvert.DeserializeObject<List<Venue>>(VenueResponse);
                    }
                }

                ViewBag.VenueId = venueId;
                ViewBag.Venue = VenueInfo[0];
                return View();
            }
            
        }

        [HttpPost]
        public async Task<ActionResult> Create(Reservation res) {
            Debug.WriteLine(res.TableId);
            if (ModelState.IsValid)
            {
                string[] tablePackageIds = res.TablePackageIds.ToArray();
                string reservationId;
                object root = new
                {
                    Reservation = res,
                };
                var json = JsonConvert.SerializeObject(root);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                using (var client = base.GetClient())
                {
                    var response = await client.PostAsync(base.BaseUrl + "/reservations", data);
                    if(!response.IsSuccessStatusCode)
                        return RedirectToAction("Create", new { venueId = res.VenueId, message = "That table is already booked!" });
                    reservationId = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine($"reservationId = {reservationId}");
                }

                string resId = reservationId.Replace(@"\", string.Empty);
                foreach(string tablePackageId in tablePackageIds)
                {
                    json = "{\"ReservationTablePackage\": {\"ReservationId\": " + resId + ", \"TablePackageId\": " + tablePackageId + "}}";
                    Debug.WriteLine(json);
                    Debug.WriteLine(json);
                    data = new StringContent(json, Encoding.UTF8, "application/json");

                    using (var client = base.GetClient())
                    {
                        var response = await client.PostAsync(base.BaseUrl + "/reservationstablepackages", data);
                    }
                }

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Create", new { venueId = res.VenueId, message = "Did you fill out all fields?" });
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