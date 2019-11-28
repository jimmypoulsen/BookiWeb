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
    public class VenuesController : Controller {
            string BaseUrl = "https://localhost:44314/api/";
            public async Task<ActionResult> Index() {
                List<Venue> VenueInfo = new List<Venue>();

                using (var client = new HttpClient()) {
                    //Passing service base url  
                    client.BaseAddress = new Uri(BaseUrl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("venues");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode) {
                        //Storing the response details recieved from web api   
                        var VenueResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        VenueInfo = JsonConvert.DeserializeObject<List<Venue>>(VenueResponse);

                    }
                    //returning the employee list to view  
                    return View(VenueInfo);
                }
            }

            public ActionResult Create() {
                ViewBag.Message = "Create new venue";

                return View();
            }

            [HttpPost]
            public async Task<ActionResult> Create(Venue venue) {
                var root = new {
                    Venue = venue
                };
                var json = JsonConvert.SerializeObject(root);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var url = BaseUrl + "/venues";
                using (var client = new HttpClient()) {
                    var response = await client.PostAsync(url, data);
                    string result = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(result);
                }

                return RedirectToAction("Index");
            }
    }
}
