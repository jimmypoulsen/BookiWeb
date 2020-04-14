using BookiWeb.Models;
using BookiWeb.Helpers;
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
using System.Diagnostics;

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
            if (ModelState.IsValid)
            {
                customer.Salt = HashingHelper.RandomString(20);
                customer.Password = HashingHelper.GenerateHash(customer.Password, customer.Salt);
                var customerInfo = new
                {
                    Customer = customer
                };
                var json = JsonConvert.SerializeObject(customerInfo);
                Debug.WriteLine(json);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var url = BaseUrl + "/customers";
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync(url, data);
                    var result = response.Content.ReadAsStringAsync().Result;

                    HttpCookie AuthCookies = new HttpCookie("AuthCookies");
                    HttpCookie AuthenticatedWith = new HttpCookie("AuthenticatedWith");

                    if (response.IsSuccessStatusCode)
                    {
                        AuthCookies["email"] = customer.Email;
                        AuthCookies["customerId"] = "" + response.Content.ReadAsStringAsync().Result;
                        if(customer.FacebookUserID != null)
                        {
                            AuthenticatedWith.Value = "Facebook";
                        }
                        if (customer.GoogleUserID != null)
                        {
                            AuthenticatedWith.Value = "Google";
                        }
                        AuthCookies.Expires = DateTime.Now.AddHours(72);
                        AuthenticatedWith.Expires = DateTime.Now.AddHours(72);
                        Response.SetCookie(AuthCookies);
                        Response.SetCookie(AuthenticatedWith);
                        return RedirectToAction("Index", "Home");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                        return RedirectToAction("Create", new { message = "That user already exists!" });
                    else
                        return RedirectToAction("Create", new { message = "Did you fill out all the fields?" });
                }
            }
            else
                return View(customer);
        }
    }
}