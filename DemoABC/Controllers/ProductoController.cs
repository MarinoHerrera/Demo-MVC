using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoABC.Models;
using Azure.Core;
using Azure;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Policy;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DemoABC.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IConfiguration _configuration;

        public ProductoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: Producto
        public async Task<IActionResult> Index()
        {
            var result = new List<Producto>();
            try
            {
                string url = _configuration.GetSection("APIConfig").GetValue<string>("apiURL") + "/Producto";
                var response = await getResponse(url, "Get", null);
                if (!Equals(null, response))
                {
                    var objdeserialized = JsonConvert.DeserializeObject<List<Producto>>(response);
                    if (objdeserialized != null)
                    {
                        result = objdeserialized;
                    }
                }
                else
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");

                return View(result);
            }
            catch (Exception ex)
            {
                return Problem("Server error with API. Please contact administrator.");
            }
        }

		// GET: Producto/AddOrEdit
		public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Producto());
            else
            {
                var result = new Producto();
                try
                {
                    string url = _configuration.GetSection("APIConfig").GetValue<string>("apiURL") +  "/Producto/AddOrEdit?id=" + id.ToString();
                    var response = await getResponse(url, "Get", null);
                    if (!Equals(null, response))
                    {
                        var objdeserialized = JsonConvert.DeserializeObject<Producto>(response);
                        if (objdeserialized != null)
                        {
                            result = objdeserialized;
                        }
                    }
                    else //web api sent error response 
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }

                    return View(result);
                }
                catch (Exception ex)
                {
                    return Problem("Server error with API. Please contact administrator.");
                }

            }
        }

        // POST: Producto/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("Id,SKU,Descripcion,PrecioDetal,PrecioMayor,Estiba,ModDate")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                var result = new List<Producto>();
                try
                {
                    string url = _configuration.GetSection("APIConfig").GetValue<string>("apiURL") +  "/Producto/AddOrEdit";
                    var dataSerialized = JsonConvert.SerializeObject(producto,
                                    new JsonSerializerSettings
                                    {
                                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                                        NullValueHandling = NullValueHandling.Ignore
                                    });

                    var response = await getResponse(url, "POST", dataSerialized);
                    if (!Equals(null, response))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception ex)
                {
                    return Problem("Server error with API. Please contact administrator.");
                }
            }
            return View(producto);
        }

        // POST: producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = new Producto();

            try
            {
                string url = _configuration.GetSection("APIConfig").GetValue<string>("apiURL") +  "/Producto/Delete?id=" + id.ToString();
                var response = await getResponse(url, "POST", null);
                if (!Equals(null, response))
                {
                    return RedirectToAction(nameof(Index));
                }
                else //web api sent error response 
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
                
            }
            catch (Exception ex)
            {
                return Problem("Server error with API. Please contact administrator.");
            }

            return View(result);

        }

        public async Task<string> getResponse(string url, string method, string data)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string usr = _configuration.GetSection("APIConfig").GetValue<string>("usr");
                    string psw = _configuration.GetSection("APIConfig").GetValue<string>("psw");

                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{usr}:{psw}")));

                    using (var request = new HttpRequestMessage())
                    {
                        request.Method = new HttpMethod(method);
                        request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);

                        if (!Equals(data, null))
                        {
                            request.Content = new StringContent(data, Encoding.UTF8, "application/json");
                        }

                        var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                        var responseBody = await response.Content.ReadAsStringAsync();

                        return responseBody;
                    }
                }
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
