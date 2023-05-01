using Bookington_FE.Models.Enum;
using Bookington_FE.Models.RequestModel;
using Bookington_FE.Models.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;
using NuGet.Configuration;
using NuGet.Protocol;
using System.Collections.Generic;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using static System.Reflection.Metadata.BlobBuilder;


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
        public void GetNotify()
        {
            //check session account
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            if (sessAcount == null || sessAcount.result.role == "admin")
            {
                return;// RedirectToAction("Login", "Home");
            }
            //call query notify
            try
            {
                string link = ConfigAppSetting.Api_Link + "notifications?UserId=" + sessAcount.result.userId + "&PageNumber=1&MaxPageSize=1000";

                string resJsonStr = GlobalFunc.CallAPI(link, null, MethodHttp.GET, sessAcount.result.sysToken);
            }
            catch (Exception)
            {

            }
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
        public IActionResult History(string searchText = "", int currentPage = 1, int pageSize = 10)
        {

            //check session account
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            if (sessAcount == null || sessAcount.result.role == "admin")
            {
                return RedirectToAction("Login", "Home");
            }
            //getCourt by query
            transactionResponse res = null;
            string resJsonStr = string.Empty;
            try
            {
                string link = ConfigAppSetting.Api_Link + "transaction-history/owner";
                string param = "";
                if (!string.IsNullOrEmpty(searchText))
                {
                    param = "SearchText=" + searchText;
                }
                //
                if (currentPage > 0)
                {
                    if (!string.IsNullOrEmpty(param))
                        param += "&PageNumber=" + currentPage;
                    else
                        param += "PageNumber=" + currentPage;
                }
                //
                if (pageSize > 0)
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
                res = JsonConvert.DeserializeObject<transactionResponse>(resJsonStr);
                //
                //luu lai session
                new SessionController(HttpContext).SetSession(KeySession._COURT, res);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //
            return View(res);
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
                string link = ConfigAppSetting.Api_Link + "courts";
                string param = "";
                if (!string.IsNullOrEmpty(searchText))
                {
                    param = "SearchText=" + searchText;
                }
                //
                if (currentPage > 0)
                {
                    if (!string.IsNullOrEmpty(param))
                        param += "&PageNumber=" + currentPage;
                    else
                        param += "PageNumber=" + currentPage;
                }
                //
                if (pageSize > 0)
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
                //
                //luu lai session
                new SessionController(HttpContext).SetSession(KeySession._COURT, res);
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
        public IActionResult SubCourt(string courtID)
        {

            //check session account
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            if (sessAcount == null || sessAcount.result.role == "admin")
            {
                return RedirectToAction("Login", "Home");
            }
            //
            CourtResponse arrCourt = new SessionController(HttpContext).GetSessionT<CourtResponse>(KeySession._COURT);
            CourtModel parentCourt = (from x in arrCourt.result
                                      where x.Id.ToLower() == courtID.ToLower()
                                      select x).FirstOrDefault();
            //

            //getCourt by query
            SubcourtResponse res = null;
            string resJsonStr = string.Empty;
            try
            {
                string link = ConfigAppSetting.Api_Link + "subcourts/" + courtID;

                //
                resJsonStr = GlobalFunc.CallAPI(link, null, MethodHttp.GET, sessAcount.result.sysToken);
                //
                res = JsonConvert.DeserializeObject<SubcourtResponse>(resJsonStr);
                //
                SubCourtAllModel resAll = new SubCourtAllModel();
                List<SubCourtDetails> subcourtDetails = new List<SubCourtDetails>();
                //
                foreach (SubcourtModel sc in res.result)
                {
                    string subcourtId = sc.Id;
                    //get schedule by query
                    SlotResponse resslot = null;
                    string resslotJsonStr = string.Empty;
                    string linkslot = ConfigAppSetting.Api_Link + "slots/schedule/" + subcourtId;

                    resslotJsonStr = GlobalFunc.CallAPI(linkslot, null, MethodHttp.GET, sessAcount.result.sysToken);
                    //
                    resslot = JsonConvert.DeserializeObject<SlotResponse>(resslotJsonStr);
                    //
                    Dictionary<string, List<SlotModel>> mappingSlotToTime = new Dictionary<string, List<SlotModel>>();
                    foreach (SlotModel slotModel in resslot?.result)
                    {
                        string key = slotModel.startTime.ToString(@"hh\:mm") + "-" + slotModel.endTime.ToString(@"hh\:mm");

                        if (!mappingSlotToTime.Keys.Contains(key))
                        {
                            mappingSlotToTime.Add(key, new List<SlotModel> { slotModel });
                        }
                        else
                        {
                            mappingSlotToTime[key].Add(slotModel);
                        }
                    }
                    //
                    SubCourtDetails dt = new SubCourtDetails();
                    dt.subcourtModel = sc;
                    dt.GroupSlotByTime = mappingSlotToTime;
                    subcourtDetails.Add(dt);
                    //
                }
                //
                resAll.SubCourtDetails = subcourtDetails;
                //
                resAll.courtParent = parentCourt;
                //
                return View(resAll);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        public bool DeleteSubCourt(string id)
        {
            string resJsonStr;
            try
            {
                //check session account
                AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
                //
                string link = ConfigAppSetting.Api_Link + "subcourts/" + id;
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

        public bool UpdateProfile(string name, string dob)
        {
            string resJsonStr;
            try
            {
                //check session account
                AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
                string id = sessAcount.result.userId;
                // 
                string link = ConfigAppSetting.Api_Link + "accounts/" + id;
                UpdateAccountRequest request = new UpdateAccountRequest() { fullName = name, dateOfBirth = dob };
                StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                resJsonStr = GlobalFunc.CallAPI(link, content, MethodHttp.PUT, sessAcount.result.sysToken);
                //
                //if success
                sessAcount.profileRead.DateOfBirth = DateTime.ParseExact(dob, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                sessAcount.profileRead.FullName = name;
                sessAcount.result.fullName = name;
                //
                new SessionController(HttpContext).SetSession(KeySession._CURRENACCOUNT, sessAcount);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
        public IActionResult Schedule(string subcourtId, SubcourtResponse ressub)
        
        {

            //check session account
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            if (sessAcount == null || sessAcount.result.role == "admin")
            {
                return RedirectToAction("Login", "Home");
            }
            //get schedule by query
            SlotResponse res = null;
            string resJsonStr = string.Empty;
            string link = ConfigAppSetting.Api_Link + "slots/schedule/" + subcourtId;

            resJsonStr = GlobalFunc.CallAPI(link, null, MethodHttp.GET, sessAcount.result.sysToken);
            //
            res = JsonConvert.DeserializeObject<SlotResponse>(resJsonStr);
            //
            Dictionary<string, List<SlotModel>> mappingSlotToTime = new Dictionary<string, List<SlotModel>>();
            foreach (SlotModel slotModel in res?.result)
            {
                string key = slotModel.startTime.ToString(@"hh\:mm") + "-" + slotModel.endTime.ToString(@"hh\:mm");

                if (!mappingSlotToTime.Keys.Contains(key))
                {
                    mappingSlotToTime.Add(key, new List<SlotModel> { slotModel });
                }
                else
                {
                    mappingSlotToTime[key].Add(slotModel);
                }
            }
            SubCourtAllModel model = new SubCourtAllModel();
            return RedirectToAction("Subcourt", "Owner", new { courtID = "", resall = model });
            //
            //return View(res);
            //
        }
        public bool UpdateSlot(string id, double price, bool status, string idsubcourt)
        {
            //
            AuthLoginResponse sessAcount = new SessionController(HttpContext).GetSessionT<AuthLoginResponse>(KeySession._CURRENACCOUNT);
            //PUT/ slots​ / updateSlot​ / ref -subCourt
            string link = ConfigAppSetting.Api_Link + "slots​/updateSlot​/ref-subCourt?subCourtId="+idsubcourt;
            //loại bỏ ký tự không gian có độ dài = 0
            link = link.Replace("\u200B", "");
            UpdateSlotRequest request = new UpdateSlotRequest() { Id = id, Price = price, IsActive = status};
            string jscontent = JsonConvert.SerializeObject(request);
            StringContent content = new StringContent(jscontent, Encoding.UTF8, "application/json");
            string resJsonStr = GlobalFunc.CallAPI(link, content, MethodHttp.PUT, sessAcount.result.sysToken);
            return true;
        }
    }
}
