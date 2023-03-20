using Bookington_FE.Models.Enum;
using Bookington_FE.Models.RequestModel;
using Bookington_FE.Models.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Configuration;
using System.Data;
using System.Numerics;
using System.Text;

namespace Bookington_FE.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            //check session account
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            if (sessAcount == null || sessAcount.result.role == "owner")
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
            if (sessAcount == null || sessAcount.result.role == "owner")
            {
                return RedirectToAction("Login", "Home");
            }
            //
            return View();
        }
        public IActionResult UserManager()
        {

            //check session account
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            if (sessAcount == null || sessAcount.result.role == "owner")
            {
                return RedirectToAction("Login", "Home");
            }
            //
            AccountResponse res = null;
            string resJsonStr = string.Empty;
            try
            {
                string link = ConfigAppSetting.Api_Link + "accounts/query";
                resJsonStr = GlobalFunc.CallAPI(link, null, MethodHttp.GET, sessAcount.result.sysToken);
                //
                res = JsonConvert.DeserializeObject<AccountResponse>(resJsonStr);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            //
            return View(res?.result);
        }
        public IActionResult ManageCourtReport()
        {

            //check session account
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            if (sessAcount == null || sessAcount.result.role == "owner")
            {
                return RedirectToAction("Login", "Home");
            }
            CourtReportResponse res = null;
            string resJsonStr = string.Empty;
            try
            {
                string link = ConfigAppSetting.Api_Link + "bookington/reports/courtreports";
                resJsonStr = GlobalFunc.CallAPI(link, null, MethodHttp.GET, sessAcount.result.sysToken); ;
                //
                res = JsonConvert.DeserializeObject<CourtReportResponse>(resJsonStr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //
            return View(res?.result);
        }
        public IActionResult ManageUserReport()
        {

            //check session account
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            if (sessAcount == null || sessAcount.result.role == "owner")
            {
                return RedirectToAction("Login", "Home");
            }
            UserReportResponse res = null;
            string resJsonStr = string.Empty;
            try
            {
                string link = ConfigAppSetting.Api_Link + "bookington/reports/userreports";
                resJsonStr = GlobalFunc.CallAPI(link, null, MethodHttp.GET, sessAcount.result.sysToken);
                //
                res = JsonConvert.DeserializeObject<UserReportResponse>(resJsonStr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //
            return View(res?.result);
        }
        public IActionResult Profile()
        {

            //check session account
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            if (sessAcount == null || sessAcount.result.role == "owner")
            {
                return RedirectToAction("Login", "Home");
            }
            //
            return View(sessAcount);
        }


        public IActionResult Logout()
        {
            //check session account

            //AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            //sessAcount.message = string.Empty;
            new SessionController(HttpContext).SetSession(KeySession._CURRENACCOUNT, "");
            return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public bool DeleteAccount(string id)
        {
            string resJsonStr;
            try
            {
                //check session account
                AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
                //
                string link = ConfigAppSetting.Api_Link + "accounts/" + id;
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
        public bool DeleteUserReport(string id)
        {
            string resJsonStr;
            try
            {
                //check session account
                AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
                //
                string link = ConfigAppSetting.Api_Link + "bookington/reports/userreports" + id;
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
        public bool DeleteCourtReport(string id)
        {
            string resJsonStr;
            try
            {
                //check session account
                AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
                //
                string link = ConfigAppSetting.Api_Link + "bookington/reports/userreports" + id;
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
        public bool UpdateAccount(string id, string name, string dob)
        {
            string resJsonStr;
            try
            {
                //check session account
                AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
                // 
                string link = ConfigAppSetting.Api_Link + "accounts/" + id;
                UpdateAccountRequest request = new UpdateAccountRequest() { fullName = name, dateOfBirth = dob };
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

        
        
    }
}
