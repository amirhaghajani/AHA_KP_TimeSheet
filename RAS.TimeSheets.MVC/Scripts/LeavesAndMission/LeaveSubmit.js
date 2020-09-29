var _currentLeave = [];
var _allLeave = [];

$(document).ready(function () {

    GetUnSubmitAllLeaves();

})

function GetUnSubmitAllLeaves() {

    $.ajax({
        type: "Get",
        url: "/api/MissionsAndLeavesApi/GetUnSubmitAllLeaves",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: GetUnSubmitAllLeavesSuccess,
        error: function (e) {

        }
    });
}

function GetUnSubmitAllLeavesSuccess(data) {
    _allLeave = data;
    var tr = "";
    for (var i = 0; i < data.length; i++) {
        tr += "<tr><th scope='row'>" + (i + 1) + "</th><td>" + data[i].UserName + "</td>";
        if (data[i].IsHour) 
            tr += "<td>" + data[i].PersianStartDate + "</td><td>ساعتی</td><td>" + data[i].LeaveHour + "</td><td>------</td>";
        else

            tr += "<td>" + data[i].PersianStartDate + "الی" + data[i].PersianFinishDate + "</td><td>روزانه</td><td>-------</td><td>" + data[i].Type+"</td>";
        tr += "<td><ul style='margin-top:0px;' class='nav nav-tabs nav-justified' role='tablist'> <li class='nav-item'>" +
            "<a class='nav-link active' id='myBtn" + data[i].ID + "' style='width: 100px;background:#ff863a' data-toggle='tab' role='tab' onClick='LoadModal(\"" + data[i].ID + "\",2)' aria-selected='true'>رد کردن</a></li>" +
            "<li><a class='nav-link active' id='home-tab' style='background:#c6c400;width: 100px;margin-right: 10px;' data-toggle='tab' href='#home' role='tab' aria-controls='home' onClick='LoadModal(\"" + data[i].ID + "\",1)' aria-selected='true'>قبول کردن</a>" +
            "</li></ul></td>";
        document.getElementById("tapels").innerHTML = tr;
    }
}


function LoadModal(id,mode) {

    for (var i = 0; i < _allLeave.length; i++)
        if (_allLeave[i].ID == id)
            _currentLeave = _allLeave[i];
    
    if(mode==1)
        $("#LoadModal").html("<div style='Display:block' id='myModal' class='modal'><div class='modal-content'><span class='close'>&times;</span><p><h4 style='text-align:center'>میخواهم درخواست را تایید کنم!</h4></p><div class='row' style='text-align:center'><div class='col-md-4'><input type='submit' style='width: 80px;' onClick='Save(1);return false' class='btnRegister' value='بله' /></div><div class='col-md-5'><input style='width: 80px;background:#cc0000' id='close' type='submit' class='btnRegister' value='خیر' /></div></div></div></div>");
    else
        $("#LoadModal").html("<div style='Display:block' id='myModal' class='modal'><div class='modal-content'><span class='close'>&times;</span><p><h4 style='text-align:center'>میخواهم درخواست را رد کنم!</h4></p><div class='row' style='text-align:center'><div class='col-md-4'><input type='submit' style='width: 80px;' onClick='Save(2);return false' class='btnRegister' value='بله' /></div><div class='col-md-5'><input style='width: 80px;background:#cc0000' id='close' type='submit' class='btnRegister' value='خیر' /></div></div></div></div>");


    var modal = $("#myModal");
    var span = $(".close");
    var span1 = $("#close");
    span.click(function () {
        modal.css("display", "none");
    });
    span1.click(function () {
        modal.css("display", "none");
    });
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.css("display", "none");
        }
    }

}

function Save(Mode) {
    if (Mode == 1)
        _currentLeave.IsAprove = true;
    else
        _currentLeave.IsDenid = true;
    var data = JSON.stringify(_currentLeave);
    $.ajax({
        type: "Post",
        url: "/api/MissionsAndLeavesApi/SaveLeave",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: data,
        success: function (data) {
            GetUnSubmitAllLeaves();
        },
        error: function (e) {

        }
    });

}


