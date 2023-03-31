using Bookington_FE.Models.Enum;
using Bookington_FE.Models.RequestModel;
using Bookington_FE.Models.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;
using NuGet.Configuration;
using NuGet.Protocol;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;


namespace Bookington_FE.Controllers
{
    public class OwnerController : Controller
    {
        public IActionResult Index()
        {
            //check session account
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            if (sessAcount == null || sessAcount.result.role == "admin")
            {
                return RedirectToAction("Login", "Home");
            }
            //
            return View(sessAcount);
        }
        public IActionResult Dashboard()
        {
            //check session account
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            if (sessAcount == null || sessAcount.result.role == "admin")
            {
                return RedirectToAction("Login", "Home");
            }
            //
            return View(sessAcount);
        }
        public IActionResult History()
        {

            //check session account
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            if (sessAcount == null || sessAcount.result.role == "admin")
            {
                return RedirectToAction("Login", "Home");
            }
            //
            return View(sessAcount);
        }
        public IActionResult ManageYard(string searchText = "", int currentPage = 1, int pageSize = 10)
        {

            //check session account
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            if (sessAcount == null || sessAcount.result.role == "admin")
            {
                return RedirectToAction("Login", "Home");
            }
            //getCourt by query
            CourtResponse res = null;
            string resJsonStr = string.Empty;
            try
            {
                string link = ConfigAppSetting.Api_Link + "courts/query";
                string param = "";
                if (!string.IsNullOrEmpty(searchText))
                {
                    param = "SearchText=" + searchText;
                }
                //
                if(currentPage > 0)
                {
                    if (!string.IsNullOrEmpty(param))
                        param += "&PageNumber=" + currentPage;
                    else
                        param += "PageNumber=" + currentPage;
                }
                //
                if(pageSize > 0)
                {
                    if (!string.IsNullOrEmpty(param))
                        param += "&MaxPageSize=" + pageSize;
                    else
                        param += "MaxPageSize=" + pageSize;
                }
                //
                if (!string.IsNullOrEmpty(param))
                    link += "?" + param;
                resJsonStr = GlobalFunc.CallAPI(link, null, MethodHttp.GET, sessAcount.result.sysToken);
                //
                res = JsonConvert.DeserializeObject<CourtResponse>(resJsonStr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //
            return View(res);
        }
        
        public IActionResult Profile()
        {

            //check session account
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            if (sessAcount == null || sessAcount.result.role == "admin")
            {
                return RedirectToAction("Login", "Home");
            }
            //
            return View(sessAcount);
        }

        public IActionResult Chat()
        {
            //check session account
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            if (sessAcount == null || sessAcount.result.role == "admin")
            {
                return RedirectToAction("Login", "Home");
            }
            //
            return View();
        }
        public IActionResult Competition()
        {
            //check session account
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            if (sessAcount == null || sessAcount.result.role == "admin")
            {
                return RedirectToAction("Login", "Home");
            }
            //
            return View();
        }
        public IActionResult Logout()
        {
            //check session account

            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            new SessionController(HttpContext).SetSession(KeySession._CURRENACCOUNT, "");
            return RedirectToAction("Login", "Home");
        }

        public bool DeleteCourt(string id)
        {
            string resJsonStr;
            try
            {
                //check session account
                AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
                //
                string link = ConfigAppSetting.Api_Link + "courts/" + id;
                resJsonStr = GlobalFunc.CallAPI(link, null, MethodHttp.DELETE, sessAcount.result.sysToken);
                //
                return true;
            }
            catch (Exception ex)
            {
                return false;
                //throw new Exception(ex.Message + "\r\n" + ex.StackTrace);
            }
        }
        public bool UpdateCourt(string id, string coid, string cdid, string cname, string caddress, TimeSpan copen, TimeSpan cclose)
        {
            string resJsonStr;
            try
            {
                //check session account
                AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
                // 
                string link = ConfigAppSetting.Api_Link + "courts/" + id;
                UpdateCourtRequest request = new UpdateCourtRequest() { OwnerId = coid, DistrictId = cdid, Name = cname, Address = caddress, OpenAt = copen, CloseAt = cclose };
                StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                resJsonStr = GlobalFunc.CallAPI(link, content, MethodHttp.PUT, sessAcount.result.sysToken);

                //string link = ConfigAppSetting.Api_Link + "accounts/" + id;
                //resJsonStr = GlobalFunc.CallAPI(link, null, MethodHttp.POST, sessAcount.result.sysToken);
                //

            }
            catch (Exception ex)
            {
                throw ex;
                //throw new Exception(ex.Message + "\r\n" + ex.StackTrace);
            }
            return true;
        }
        public IActionResult Search()
        {
            return View();
        }

    }
}
