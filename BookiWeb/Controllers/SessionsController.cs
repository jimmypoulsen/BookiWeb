using BookiWeb.Models;
using BookiWeb.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BookiWeb.Controllers
{
    public class SessionsController : Controller
    {
        string BaseUrl = "https://localhost:44314/api/";
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Customer res)
        {
            if (res.Email != null && res.Password != null)
            {
                var customer = new
                {
                    Customer = res
                };
                var json = JsonConvert.SerializeObject(customer);
                Debug.WriteLine(json);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var url = BaseUrl + "/sessions";
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync(url, data);
                    if (response.IsSuccessStatusCode)
                    {
                        HttpCookie AuthCookies = new HttpCookie("AuthCookies");
                        AuthCookies["email"] = res.Email;
                        AuthCookies["customerId"] = "" + response.Content.ReadAsStringAsync().Result;
                        AuthCookies.Expires = DateTime.Now.AddHours(72);
                        Response.SetCookie(AuthCookies);
                        Response.Redirect("/");
                    }
                    else
                    {
                        return RedirectToAction("Create", new { message = "Wrong email/password" });
                    }
                }
            }
            return RedirectToAction("Create", new { message = "Something went wrong .." });
        }

        [HttpPost]
        public ActionResult Delete()
        {
            HttpCookie AuthCookies = new HttpCookie("AuthCookies");
            AuthCookies.Value = "";
            AuthCookies.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(AuthCookies);

            return RedirectToAction("Create");
        }
    }
}