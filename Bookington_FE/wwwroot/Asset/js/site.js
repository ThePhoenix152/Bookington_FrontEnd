﻿var _idUser_del = "";
var _idCourt_del = "";
var _idUser_up = "";
var _idCourt_up = "";
var _idCourt_detail = "";
var _idSubCourt_del = "";
//for search count
var _currentPage = parseInt(0);
var _pageSize = parseInt(10);

function UserLogin() {

    var phone = $('#phoneinput').val();
    var pass = $('#passinput').val();
    var loginData = { phone: phone, password: pass };
    //
    jQuery.ajax({
        url: "https://localhost:7216/Home/AuthAccount",
        type: "POST",
        cache: false,
        data: loginData,
        //contentType: 'application/json',
        success: function Redirect(dataOut) {
            var data = JSON.parse(dataOut);
            if (data.statusCode == 200) {
                if (data.result.role == "admin") {
                    //var surl = document.location.href;
                    //surl = surl.substr(0, surl.lastIndexOf("/")) + "/Index";
                    RedirectToLink("https://localhost:7216/Admin/Index");
                }
                else if (data.result.role == "owner") {
                    //var surl = document.location.href;
                    //surl = surl.substr(0, surl.lastIndexOf("/")) + "/Privacy";
                    RedirectToLink("https://localhost:7216/Owner/Index");

                }
            }
            else {
                alert("User Phone Or Password Incorrect");
            }
        }
    })
}
function RedirectToLink(link) {
    window.location.replace(link);
}
function OpenModal(id) {
    $('#' + id).modal("show");
    //$('#' + id).appendTo("body");
}
function DeleteUser(uid, uname) {
    _idUser_del = uid;
    _nameUser_del = uname;
    $('#nameUserDel span').text(uname);
    $('#delusermodal').modal("show");
}
function ConfirmDel() {

    //
    jQuery.ajax({
        url: "https://localhost:7216/Admin/DeleteAccount?id=" + _idUser_del,
        type: "GET",
        cache: false,
        success: function Redirect(dataOut) {
            if (dataOut == true) {
                RedirectToLink("https://localhost:7216/Admin/UserManager");
                /*alert("delete user success!");*/
            }
            else {
                alert("delete user failed!");
            }
        }
    })
}


function UpdateUser(uid, uname, udob) {
    debugger
    _idUser_up = uid;
    //
    ('#inputUpUserName').val(uname);
    ('#inputUpUserDOB').val(udob);
  ('#upusermodal').modal("show");
}
function Update() {
    var nameUser_up = $('#inputUpUserName').val();
    var dobUser_up = $('#inputUpUserDOB').val();
    jQuery.ajax({
        url: "https://localhost:7216/Admin/UpdateAccount",
        type: "POST",
        cache: false,
        data: { id: _idUser_up, name: nameUser_up, dob: dobUser_up },
        success: function Redirect(dataOut) {
            if (dataOut == true) {
                /*RedirectToLink("https://localhost:7216/Admin/UserManager");*/
                alert("Update profile successfully");
                /*alert("delete user success!");*/
            }
            else {
                alert("Update user failed!");
            }
        }
    })
}
function UpdateProfile() {
    var nameUser_up = $('#inputUpUserName').val();
    var dobUser_up = $('#inputUpUserDOB').val();
    jQuery.ajax({
        url: "https://localhost:7216/Admin/UpdateProfile",
        type: "POST",
        cache: false,
        data: {name: nameUser_up, dob: dobUser_up },
        success: function Redirect(dataOut) {
            if (dataOut == true) {
                /*RedirectToLink("https://localhost:7216/Admin/UserManager");*/
                alert("Update profile successfully");
                /*alert("delete user success!");*/
            }
            else {
                alert("Update user failed!");
            }
        }
    })
}

function DeleteCourt(uid, uname) {
    debugger
    _idCourt_del = uid;
    $('#nameCourtDel span').text(uname);
    $('#delcourtmodal').modal("show");
}
function ConfirmCDel() {

    //
    jQuery.ajax({
        url: "https://localhost:7216/Owner/DeleteCourt?id=" + _idCourt_del,
        type: "GET",
        cache: false,
        success: function Redirect(dataOut) {
            if (dataOut == true) {
                RedirectToLink("https://localhost:7216/Owner/ManageYard");
                /*alert("delete user success!");*/
            }
            else {
                alert("delete court failed!");
            }
        }
    })
}
function UpdateCourt(cid, coid, cdid, cname, caddress, copen, cclose) {
    debugger
    _idCourt_up = cid;
    //
    $('#inputUpCourtOwnerID').val(coid)
    $('#inputUpCourtName').val(cname)
    $('inputUpCourtDistrictID').val(cdid)
    $('inputUpCourtAddress').val(caddress)
    $('#inputUpCourtOpen').val(copen)
    $('#inputUpCourtClose').val(cclose)
    $('#upcourtmodal').modal("show");
}
function UpdateC() {
    var ownerIDCourt_up = $('#inputUpCourtOwnerID').val();
    var districtIDCourt_up = $('inputUpCourtDistrictID').val()
    var nameCourt_up = $('#inputUpCourtName').val()
    var addressCourt_up = $('inputUpCourtAddress').val();
    var openCourt_up = $('inputUpCourtOpen').val();
    var closeCourt_up = $('inputUpCourtClose').val();
    jQuery.ajax({
        url: "https://localhost:7216/Owner/UpdateCourt",
        type: "POST",
        cache: false,
        data: { id: _idCourt_up, coid: ownerIDCourt_up, district: districtIDCourt_up, name: nameCourt_up, address: addressCourt_up, open: openCourt_up, close: closeCourt_up },
        success: function Redirect(dataOut) {
            if (dataOut == true) {
                RedirectToLink("https://localhost:7216/Owner/ManageYard");

                /*alert("delete user success!");*/
            }
            else {
                alert("Update court failed!");
            }
        }
    })
}
function SetPageSize(size) {
    _pageSize = parseInt(size);
    $('#pageSizeLab span').text(size);
    SearchCourt();
}
function btnPreviousClick() {
    if (_currentPage > 1) {
        _currentPage = _currentPage - 1;
    }
    SearchCourt();
}
function btnNextClick() {
    _currentPage = _currentPage + 1;
    SearchCourt();
}
function btnNumbpageClick(pageNum) {
    if (_currentPage != pageNum) {
        _currentPage = parseInt(pageNum);
        SearchCourt();
    }
}
function SetPageNumAndSize(currPage, pageSize) {
    _currentPage = parseInt(currPage);
    _pageSize = parseInt(pageSize);
}
function btnSearchCourtClick() {
    _currentPage = parseInt(1);
    SearchCourt();
}
function SearchCourt() {
    var searchText = $("#searchCourtTxt").val();
    window.location.href = 'https://localhost:7216/Owner/ManageYard?searchText=' + searchText + '&currentPage=' + _currentPage + '&pageSize=' + _pageSize;
    //jQuery.ajax({
    //    url: '@Url.Action("ManageYard", "Owner")',
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    data: { searchText: searchText, currentPage: _currentPage, pageSize: _pageSize },
    //    success: function () { alert('Success'); }
    //})
}

function DetailCourt(id) {
    _idCourt_detail = id;
    window.location.href = 'https://localhost:7216/Owner/SubCourt?courtID=' + _idCourt_detail;
    /*$('#subcourtmodal').modal("show");*/

}

function SetPageSizeU(size) {
    _pageSize = parseInt(size);
    $('#pageSizeLabU span').text(size);
    SearchUser();
}
function btnPreviousClickU() {
    if (_currentPage > 1) {
        _currentPage = _currentPage - 1;
    }
    SearchUser();
}
function btnNextClickU() {
    _currentPage = _currentPage + 1;
    SearchUser();
}
function btnNumbpageClickU(pageNum) {
    if (_currentPage != pageNum) {
        _currentPage = parseInt(pageNum);
        SearchUser();
    }
}
function SetPageNumAndSizeU(currPage, pageSize) {
    _currentPage = parseInt(currPage);
    _pageSize = parseInt(pageSize);
}
function btnSearchUserClick() {
    _currentPage = parseInt(1);
    SearchUser();
}
function SearchUser() {
    var searchText = $("#searchUserTxt").val();
    window.location.href = 'https://localhost:7216/Admin/UserManager?searchText=' + searchText + '&currentPage=' + _currentPage + '&pageSize=' + _pageSize;
}

function DetailCompe(id) {
    _idCourt_detail = id;
    $('#detailcompe').modal("show");
}

function DetailSubCourt(id) {
    _idSubCourt_detail = id;
    $('#' + id).modal("show");
}

function DeleteSubCourt(sid, uname) {
    debugger
    _idSubCourt_del = sid;
    $('#nameSubCourtDel span').text(uname);
    $('#delsubcourtmodal').modal("show");
}
function ConfirmSCDel() {

    //
    jQuery.ajax({
        url: "https://localhost:7216/Owner/DeleteSubCourt?id=" + _idSubCourt_del,
        type: "GET",
        cache: false,
        success: function Redirect(dataOut) {
            if (dataOut == true) {
                RedirectToLink("https://localhost:7216/Owner/Subcourt");
                /*alert("delete user success!");*/
            }
            else {
                alert("delete court failed!");
            }
        }
    })
}