var _currenntMission = [];
var _allMission = [];
var _IsHour = [{ Title:"ماموریت ساعتی", ID:1 }, { Title:"ماموریت روزانه", ID:0 }];
var _select = null;
var _projects = [];
$(document).ready(function () {

    GetProjectForUserName();
    GetAllMissionsForUser();
})

function GetAllMissionsForUser() {
    $.ajax({
        type: "Get",
        url: "/api/MissionsAndLeavesApi/GetAllMissionForUser",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: GetAllMissionsForUserSuccess,
        error: function (e) {

        }
    });
}


function GetAllMissionsForUserSuccess(data) {
   
    _allMission = data;
    InitTable();
    $("#Ishour").kendoDropDownList({
        optionLabel: "انتخاب کنید",
        dataTextField: "Title",
        dataValueField: "ID",
        dataSource: _IsHour,

        change: onDropDownSelect

    });

}

function InitTable() {
    $("#Table").css("display", "block");
    $("#Insert").css("display", "none");
    var table = "";
    for (var i = 0; i < _allMission.length; i++) {
        table += "<tr><th scope='row' >" + (i + 1) + "</th><td>" + _allMission[i].Title + "</td>" +
            " <td>" + _allMission[i].MissionDiscription + "</td>";

        if (!_allMission[i].IsHour) {
            table += " <td>روزانه</td>" +
                " <td>" + _allMission[i].PersianStartDate + "الی" + _allMission[i].PersianFinishDate + "</td><td></td>";
        }

        else {
            table += " <td>ساعتی</td>" +
                " <td>" + _allMission[i].PersianStartDate + "</td><td>" + _allMission[i].HourCount + "</td>";
        }
        table += "  <td><ul style='margin-top:0px;' class='nav nav-tabs nav-justified' role='tablist'> <li class='nav-item'>" +
            "<a class='nav-link active' id='myBtn" + _allMission[i].ID + "' style='width: 100px;background:#ff863a' data-toggle='tab'  role='tab' onClick='LoadModal(\"" + _allMission[i].ID + "\")'  aria-selected='true'>حذف</a></li>" +
            "<li><a class='nav-link active' id='home-tab' style='background:#c6c400;width: 100px;margin-right: 10px;' data-toggle='tab' href='#home' role='tab' aria-controls='home' onClick='EditMission(\"" + _allMission[i].ID + "\")' aria-selected='true'>ویرایش</a>" +
            "</li></ul></td></tr>"
    }
    $("#ShowMissionTable").html(table);
}

function btnMissionClick() {
    $("#Table").css("display", "none");
    $("#Insert").css("display", "block");
    $.ajax({
        type: "Get",
        url: "/api/MissionsAndLeavesApi/GetNewMission",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: BuildNewMissionSuccess,
        error: function (e) {

        }
    });
}


function GetProjectForUserName() {
    $.ajax({
        type: "Get",
        url: "/api/MissionsAndLeavesApi/GetProjectsByUserName",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: GetProjectForUserNameSuccess,
        error: function (e) {

        }
    });
}

function GetProjectForUserNameSuccess(data) {
    _projects = data;
    $("#Projects").kendoDropDownList({
        optionLabel:"انتخاب کنید",
        dataTextField: "Title",
        dataValueField: "ID",
        dataSource: data,

        

    });
}


function BuildNewMissionSuccess(data) {
  
    _currenntMission = data;

}

function onDropDownSelect() {
    _select = $("#Ishour").data("kendoDropDownList").value();
    $("#InsertMission").css("display", "block");
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

    $("#Projects").data("kendoDropDownList").value(_currenntMission.ProjectID);
    if (_currenntMission.IsHour)
        $("#Ishour").data("kendoDropDownList").value(1);
    else
        $("#Ishour").data("kendoDropDownList").value(0);

    $("#Table").css("display", "none");
    $("#InsertMission").css("display", "block");
    $("#Insert").css("display", "block");

    $("#home").css("display", "block");
    $("#Title").val(_currenntMission.Title);
    $("#MissionDiscript").val(_currenntMission.MissionDiscription);
    $("#StartDate0").val(_currenntMission.PersianStartDate);
    $("#StartDate1").val(_currenntMission.PersianStartDate);
    $("#FinishDate1").val(_currenntMission.PersianFinishDate);
    $("#HourCount").val(_currenntMission.HourCount);
 
}

function FillCurrent() {
    if ($("#Title").val() != "" && $("#MissionDiscript").val() != "") {
        var id = $("#Projects").data("kendoDropDownList").value();
        _currenntMission.ProjectID = id;
        _currenntMission.Title = $("#Title").val();
        _currenntMission.MissionDiscription = $("#MissionDiscript").val();
        _currenntMission.IsHour = _select;
        if (!_select) {
            _currenntMission.PersianStartDate = $("#StartDate1").val();
            _currenntMission.PersianFinishDate = $("#FinishDate1").val();
            _currenntMission.HourCount =0;
        }
        else {
            _currenntMission.PersianStartDate = $("#StartDate0").val();
            _currenntMission.PersianFinishDate = "";
            _currenntMission.HourCount = $("#HourCount").val();
        }
        return 1;
    }
    return 0;
}

function Save() {
    if (FillCurrent()) {
        data = JSON.stringify(_currenntMission);
        $.ajax({
            type: "Post",
            url: "/api/MissionsAndLeavesApi/SaveMission",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: data,
            success: function (data) {


                GetAllMissionsForUser();

            },
            error: function (e) {

            }
        });
    }
}

function DiscardAllSetting() {
    $("#Title").val("");
    $("#MissionDiscript").val("");
    $("#StartDate0").val("");
    $("#StartDate1").val("");
    $("#FinishDate1").val("");
    $("#HourCount").val("");
    InitTable();
    $("#Table").css("display", "block");
    $("#Insert").css("display", "none");
}


function LoadModal(id) {

    $("#LoadModal").html("<div style='Display:block' id='myModal' class='modal'><div class='modal-content'><span class='close'>&times;</span><p><h4 style='text-align:center'>می خواهم آیتم را حذف کنم!</h4></p><div class='row' style='text-align:center'><div class='col-md-4'><input type='submit' style='width: 80px;' onClick='DeleteMission(\"" + id + "\")' class='btnRegister' value='بله' /></div><div class='col-md-5'><input style='width: 80px;background:#cc0000' id='close' type='submit' class='btnRegister' value='خیر' /></div></div></div></div>");
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

function DeleteMission(id) {
    $.ajax({
        type: "Get",
        url: "/api/MissionsAndLeavesApi/DeleteMission?id="+id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
       
        success: function (data) {


            GetAllMissionsForUser();
            $("#myModal").css("display", "none");
        },
        error: function (e) {

        }
    });
}


function EditMission(id) {
    for (var i = 0; i < _allMission.length; i++) 

        if (_allMission[i].ID == id)
            _currenntMission = _allMission[i];
        

    
    FillForm();
    if (!_currenntMission.IsHour) {
        $("#tarikh1").css("display", "block");
        $("#tarikh0").css("display", "none");
    }
    else {
        $("#tarikh0").css("display", "block");
        $("#tarikh1").css("display", "none");
    }

}