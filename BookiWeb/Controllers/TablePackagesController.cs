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
    public class TablePackagesController : Controller
    {
        string BaseUrl = "https://localhost:44314/api/";
        public async Task<ActionResult> Index() {
            List<TablePackage> TablePackageInfo = new List<TablePackage>();

            using (var client = new HttpClient()) {
                //Passing service base url  
                client.BaseAddress = new Uri(BaseUrl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("tablePackages");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode) {
                    //Storing the response details recieved from web api   
                    var TablePackageResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    TablePackageInfo = JsonConvert.DeserializeObject<List<TablePackage>>(TablePackageResponse);

                }
                //returning the employee list to view  
                return View(TablePackageInfo);
            }
        }

        public ActionResult Create() {
            ViewBag.Message = "Create new tablePackage";

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(TablePackage res) {
            var root = new {
                TablePackage = res
            };
            var json = JsonConvert.SerializeObject(root);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = BaseUrl + "/tablePackages";
            using (var client = new HttpClient()) {
                var response = await client.PostAsync(url, data);
                string result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(result);
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int id) {
            using (var client = new HttpClient()) {
                var url = BaseUrl + "/tablePackages/" + id;
                var result = await client.DeleteAsync(url);
                if (result.IsSuccessStatusCode) {

                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
    }

    }
