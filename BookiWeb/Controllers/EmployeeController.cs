using BookiWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BookiWeb.Controllers
{
    public class EmployeeController : Controller
    {

        string BaseUrl = "https://localhost:44314/api/";
        public async Task<ActionResult> List() {
            List<Employee> EmpInfo = new List<Employee>();

            using (var client = new HttpClient()) {
                //Passing service base url  
                client.BaseAddress = new Uri(BaseUrl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("employees");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode) {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    EmpInfo = JsonConvert.DeserializeObject<List<Employee>>(EmpResponse);

                }
                //returning the employee list to view  
                return View(EmpInfo);
            }
        }

        // GET: Employee
        [HttpPost]
        public ActionResult Index(Employee employee)
        {
            return View(employee);
        }

        /*[HttpGet]
        public ActionResult View() {
            return View();
        }*/
    }
}