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

namespace BookiWeb.Controllers {
    public class BeveragesController : Controller {
        string BaseUrl = "https://localhost:44314/api/";
        public async Task<ActionResult> Index() {
            List<Beverage> BeverageInfo = new List<Beverage>();

            using (var client = new HttpClient()) {
                //Passing service base url  
                client.BaseAddress = new Uri(BaseUrl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("beverages");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode) {
                    //Storing the response details recieved from web api   
                    var BeverageResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    BeverageInfo = JsonConvert.DeserializeObject<List<Beverage>>(BeverageResponse);

                }
                //returning the employee list to view  
                return View(BeverageInfo);
            }
        }

        public ActionResult Create() {
            ViewBag.Message = "Create new beverage";

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Beverage res) {
            var root = new {
                Beverage = res
            };
            var json = JsonConvert.SerializeObject(root);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = BaseUrl + "/beverages";
            using (var client = new HttpClient()) {
                var response = await client.PostAsync(url, data);
                string result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(result);
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int id) {
            using (var client = new HttpClient()) {
                var url = BaseUrl + "/beverages/" + id;
                var result = await client.DeleteAsync(url);
                if (result.IsSuccessStatusCode) {

                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }


        // This does not seem to be neccesary
        /*[HttpDelete]
        public ActionResult Delete() {
            ViewBag.Message = "Delete a beverage";

            return View();
        }*/

        /*public ActionResult Delete(int id) {
            using (var client = new HttpClient()) {
                var url = BaseUrl + "/beverages/" + id;

                //HTTP DELETE
                var deleteTask = client.DeleteAsync(url);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode) {

                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }*/

    } 
    

}