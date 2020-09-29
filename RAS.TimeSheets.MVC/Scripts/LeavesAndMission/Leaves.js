var _currenntLeave = [];
var _allLeave = [];
var _IsHour = [{ Title: "مرخصی ساعتی", ID: 1 }, { Title: "مرخصی روزانه", ID: 0 }];
var _select = null;
var _Type = [{ Title: "استحقاقی ", ID: 1 }, { Title: "استعلاجی ", ID: 2 }, { Title: "بدون حقوق ", ID: 3 }, { Title: "سایر موارد ", ID: 4 }]

$(document).ready(function () {


    GetAllLeavesForUser();
})

function GetAllLeavesForUser() {
    $.ajax({
        type: "Get",
        url: "/api/MissionsAndLeavesApi/GetAllLeaveForUser",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: GetAllLeavesForUserSuccess,
        error: function (e) {

        }
    });
}


function GetAllLeavesForUserSuccess(data) {

    _allLeave = data;
    InitTable();
    InitDropDowns();

}
function InitDropDowns() {
    $("#Ishour").kendoDropDownList({
        optionLabel: "انتخاب کنید",
        dataTextField: "Title",
        dataValueField: "ID",
        dataSource: _IsHour,
        change: onDropDownSelect

    });

    $("#Type").kendoDropDownList({
        
        dataTextField: "Title",
        dataValueField: "ID",
        dataSource: _Type,
    });
} 

function InitTable() {
    $("#Table").css("display", "block");
    $("#Insert").css("display", "none");
    var table = "";
    for (var i = 0; i < _allLeave.length; i++) {
        table += "<tr><th scope='row' >" + (i + 1) + "</th><td>" + _allLeave[i].Title + "</td>" +
            " <td>" + _allLeave[i].LeaveDiscription + "</td>";

        if (!_allLeave[i].IsHour) {
            table += " <td>روزانه</td>" +
                " <td>" + _allLeave[i].PersianStartDate + "الی" + _allLeave[i].PersianFinishDate + "</td><td></td>";
        }

        else {
            table += " <td>ساعتی</td>" +
                " <td>" + _allLeave[i].PersianStartDate + "</td><td>" + _allLeave[i].LeaveHour + "</td>";
        }
        table += "  <td><ul style='margin-top:0px;' class='nav nav-tabs nav-justified' role='tablist'> <li class='nav-item'>" +
            "<a class='nav-link active' id='myBtn" + _allLeave[i].ID + "' style='width: 100px;background:#ff863a' data-toggle='tab'  role='tab' onClick='LoadModal(\"" + _allLeave[i].ID + "\")'  aria-selected='true'>حذف</a></li>" +
            "<li><a class='nav-link active' id='home-tab' style='background:#c6c400;width: 100px;margin-right: 10px;' data-toggle='tab' href='#home' role='tab' aria-controls='home' onClick='EditLeave(\"" + _allLeave[i].ID + "\")' aria-selected='true'>ویرایش</a>" +
            "</li></ul></td></tr>"
    }
    $("#ShowLeaveTable").html(table);
}

function btnLeaveClick() {
    $("#Table").css("display", "none");
    $("#Insert").css("display", "block");
    $.ajax({
        type: "Get",
        url: "/api/MissionsAndLeavesApi/GetNewLeave",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: BuildNewLeaveSuccess,
        error: function (e) {

        }
    });
}

//function InitTimePicker() {
//    $("#StartTime").kendoTimePicker({
//        dateInput: true
//    });
//    $("#FinishTime").kendoTimePicker({
//        dateInput: true
//    });
//}

function BuildNewLeaveSuccess(data) {
    //InitTimePicker();
    _currenntLeave = data;
    
 
}

function onDropDownSelect() {
    _select = $("#Ishour").data("kendoDropDownList").value();
    $("#InsertLeave").css("display", "block");
    if (!_select) {
        $("#tarikh1").css("display", "block");
        $("#tarikh0").css("display", "none");
    }
    else {
        $("#tarikh0").css("display", "block");
        $("#tarikh1").css("display", "none");
    }
}

function FillForm() {
    for (var i = 0; i < _Type.length; i++)
        if (_Type[i].Title == _currenntLeave.Type)
            $("#Type").data("kendoDropDownList").value(_Type[i].ID);
    $("#Table").css("display", "none");
    $("#InsertLeave").css("display", "block");
    $("#Insert").css("display", "block");
    $("#home").css("display", "block");
    $("#Title").val(_currenntLeave.Title);
    $("#LeaveDiscript").val(_currenntLeave.LeaveDiscription);
    $("#StartDate0").val(_currenntLeave.PersianStartDate);
    $("#StartDate1").val(_currenntLeave.PersianStartDate);
    $("#FinishDate1").val(_currenntLeave.PersianFinishDate);
    $("#LeaveHour").val(_currenntLeave.LeaveHour);
    $("#StartTime").val(_currenntLeave.StartTime);
    $("#FinishTime").val(_currenntLeave.FinishTime);

    if (_currenntLeave.IsHour) 
        $("#Ishour").data("kendoDropDownList").value(1);
    else
        $("#Ishour").data("kendoDropDownList").value(0);
    
  

}

function FormDestory() {
    InitDropDowns();
    $("#Title").val("");
    $("#LeaveDiscript").val("");
    $("#StartDate0").val("");
    $("#StartDate1").val("");
    $("#FinishDate1").val("");
    $("#LeaveHour").val("");
    $("#StartTime").val("");
    $("#FinishTime").val("");
}

function FillCurrent() {
    if ($("#Title").val() != "" && $("#LeaveDiscript").val() != "") {
        _currenntLeave.Title = $("#Title").val();
        _currenntLeave.LeaveDiscription = $("#LeaveDiscript").val();
        _currenntLeave.IsHour = _select;
        if (!_select) {
            _currenntLeave.PersianStartDate = $("#StartDate1").val();
            _currenntLeave.PersianFinishDate = $("#FinishDate1").val();
            _currenntLeave.LeaveHour = 0;
            _currenntLeave.Type = $("#Type").data("kendoDropDownList").text();
        }
        else {
            _currenntLeave.PersianStartDate = $("#StartDate0").val();
            _currenntLeave.PersianFinishDate = "";
            _currenntLeave.LeaveHour = $("#LeaveHour").val();
            //_currenntLeave.StartTime = $("#StartTime").data("kendoTimePicker").value();
            //_currenntLeave.FinishTime = $("#FinishTime").data("kendoTimePicker").value();
            _currenntLeave.Type = "";
        }
        return 1;
    }
    return 0;
}

function Save() {
    if (FillCurrent()) {
        data = JSON.stringify(_currenntLeave);
        $.ajax({
            type: "Post",
            url: "/api/MissionsAndLeavesApi/SaveLeave",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: data,
            success: function (data) {
                GetAllLeavesForUser();
                FormDestory();
            },
            error: function (e) {

            }
        });
    }
}

function DiscardAllSetting() {
    FormDestory();

    InitTable();
    $("#Table").css("display", "block");
    $("#Insert").css("display", "none");
}


function LoadModal(id) {

    $("#LoadModal").html("<div style='Display:block' id='myModal' class='modal'><div class='modal-content'><span class='close'>&times;</span><p><h4 style='text-align:center'>می خواهم آیتم را حذف کنم!</h4></p><div class='row' style='text-align:center'><div class='col-md-8'><input type='submit' style='width: 80px;' onClick='DeleteLeave(\"" + id + "\")' class='btnRegister' value='بله' /></div><div class='col-md-4'><input style='width: 80px;background:#cc0000' id='close' type='submit' class='btnRegister' value='خیر' /></div></div></div></div>");
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

function DeleteLeave(id) {
    $.ajax({
        type: "Get",
        url: "/api/MissionsAndLeavesApi/DeleteLeave?id=" + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (data) {


            GetAllLeavesForUser();
            $("#myModal").css("display", "none");
        },
        error: function (e) {

        }
    });
}


function EditLeave(id) {
    for (var i = 0; i < _allLeave.length; i++)

        if (_allLeave[i].ID == id)
            _currenntLeave = _allLeave[i];



    FillForm();
    if (!_currenntLeave.IsHour) {
        $("#tarikh1").css("display", "block");
        $("#tarikh0").css("display", "none");
    }
    else {
        $("#tarikh0").css("display", "block");
        $("#tarikh1").css("display", "none");
    }

}