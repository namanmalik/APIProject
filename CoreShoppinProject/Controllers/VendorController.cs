﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CoreShoppingProject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoreShoppingProject.Controllers
{
    public class VendorController : Controller
    {
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:50856");
            MediaTypeWithQualityHeaderValue contentType =
                new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
            HttpResponseMessage response = client.GetAsync("/api/vendor").Result;
            string stringData = response.Content.ReadAsStringAsync().Result;
            List<Vendor> data = JsonConvert.DeserializeObject<List<Vendor>>(stringData);

            return View(data);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Vendor vendor)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:50856");

            string stringData = JsonConvert.SerializeObject(vendor);
            var contentData = new StringContent
                (stringData, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync
                ("/api/vendor", contentData).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");

        }

        public IActionResult Details(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:50856");
            HttpResponseMessage response = client.GetAsync("/api/vendor/"+id).Result;
              string stringData= response.Content.ReadAsStringAsync().Result;
            Vendor data = JsonConvert.DeserializeObject<Vendor>(stringData);
            return View(data);
        }

        public IActionResult Edit(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:50856");
            HttpResponseMessage response = client.GetAsync("/api/vendor/" + id).Result;
            string stringData = response.Content.ReadAsStringAsync().Result;
            Vendor data = JsonConvert.DeserializeObject<Vendor>(stringData);
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(Vendor vendor)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:50856");
            string stringData = JsonConvert.SerializeObject(vendor);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync("/api/vendor/" + vendor.VendorId,contentData).Result;
            ViewBag.Message = response.Content.ReadAsStringAsync().Result;
            return RedirectToAction("Index");
           
        }


    }
}