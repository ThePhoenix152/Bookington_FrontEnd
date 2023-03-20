﻿using Bookington_FE.Models;
using Bookington_FE.Models.Enum;
using Bookington_FE.Models.RequestModel;
using Bookington_FE.Models.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

namespace Bookington_FE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Login()
        {
            return View();
        }       
        //public IActionResult Owner()
        //{
        //    //check session account
        //    AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
        //    if (sessAcount == null)
        //    {
        //        return RedirectToAction("Login", "Home");
        //    }
        //    return View();
        //}
        [HttpPost]
        public string AuthAccount(string phone, string password)
        {
            string resJsonStr;
            try
            {
                string link = ConfigAppSetting.Api_Link + "auth/login";
                AuthLoginRequest request = new AuthLoginRequest() { Phone = phone, Password = password };
                StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                resJsonStr = GlobalFunc.CallAPI(link, content, MethodHttp.POST);                
                //
                AuthLoginResponse res = JsonConvert.DeserializeObject<AuthLoginResponse>(resJsonStr);
                //
                //if login sucess, add new session for user
                if(!res.isError)
                {
                    new SessionController(HttpContext).SetSession(KeySession._CURRENACCOUNT, res);
                }
                //
                return resJsonStr;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}