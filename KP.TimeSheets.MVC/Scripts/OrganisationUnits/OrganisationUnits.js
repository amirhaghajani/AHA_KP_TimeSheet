﻿var _OrganUnits = [];
var OrganUnit = {};
var _UsersOrganUnits = [];
var _UnitUsers = [];
var SelectedUser = {};
var SelectedManager = {};


$("document").ready(function () {
    GetOrganUnits();
    WNDEditAndAddOrgan_OnInit();
    GetUsersOrganUnits();
    GRDOrganUsers_OnInit();
    BuildNewOrganUnit();
});

$("#UnitTitle").keyup(function () {
    OrganUnit.Title = $("#UnitTitle").val();
});

function BuildNewOrganUnit() {

    $.ajax({
        type: "Get",
        url: "/api/SettingApi/BuildNewOrganUnit",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            ;
            OrganUnit = response;
        },
        error: function (e) {
        }
    });
}

function GetUsersOrganUnits() {
    $.ajax({
        type: "Get",
        url: "/api/LoadCalendar/GetAllUsers",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            _UsersOrganUnits = response
            DDLUnitManager_OnInit();
            DDLtParentOrgan_OnInit();
            DDLAddUser_OnInit();
        },
        error: function (e) {

        }
    });
}

function RefreshGRDOrganUnits() {
    $.ajax({
        type: "Get",
        url: "/api/SettingApi/GetAllOrganisationUnits",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            _OrganUnits = response;
            $("#GRDOrganisationUnits").data("kendoGrid").dataSource.read();
        },
        error: function (e) {
        }
    });
}

function GetOrganUnits() {
    $.ajax({
        type: "Get",
        url: "/api/SettingApi/GetAllOrganisationUnits",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnGetOrganUnits,
        error: function (e) {
        }
    });
}

function OnGetOrganUnits(response) {
 
    _OrganUnits = response;
    GRDOrganisationUnits_OnInit();
    DDLtParentOrgan_OnInit();
}

function WNDEditAndAddOrgan_OnInit() {
    var kwndaddorganWHs = $("#WNDEditAndAddOrgan");
    kwndaddorganWHs.kendoWindow({
        width: "800px",
        height: "650px",
        scrollable: true,
        visible: false,
       
        modal: true,
        actions: [
            "Pin",
            "Minimize",
            "Maximize",
            "Close"
        ],
        open: adjustSize,
    }).data("kendoWindow").center();
}

function WNDEditAndAddOrgan_OnOpen() {
    $("#WNDEditAndAddOrgan").data("kendoWindow").open();
}

function WNDEditAndAddOrgan_OnClose() {
    $("#WNDEditAndAddOrgan").data("kendoWindow").close();
}

function GRDOrganisationUnits_OnInit() {
 
    $("#GRDOrganisationUnits").kendoGrid({
        dataSource: {
            transport: {
                read: function (e) {
                    e.success(_OrganUnits);
                }
            },
            schema: {

                model: {
                    id: "ID"
                },
                total: function (response) {
                    return response.length;
                }
            }
        },
        height: 600,
        toolbar: "<a class='btn btn-info' id='home-tab'   onClick='AddNewUnit()' >افزودن واحد جدید</a> ",
        filterable: true,
        sortable: true,
        selectable: true,
        columns: [
            { field: "Title", title: "عنوان", width: 50 },
            { field: "ManagerFullName", title: "مدیر", width: 50 },

            {
                title: "ویرایش ",
                template: "<button onclick='EditOrganUnit(this)' type='button' class='btn btn-warning btn-sm edit' name='info' title='ویرایش' > ویرایش</button>",
                headerTemplate: "<label class='text-center'> ویرایش </label>",
                filterable: false,
                sortable: false,
                width: 30
            }
        ],
        pageable: {
            pageSize: 15,
            pageSizes: true
        }
    });
}

function AddNewUnit() {

    $("#DDLtUnitManager").data("kendoDropDownList").select(0);
    $("#DDLAddUser").data("kendoDropDownList").select(0);
    $("#DDLtParentOrgan").data("kendoDropDownList").select(0);


    $.ajax({
        type: "Get",
        url: "/api/SettingApi/BuildNewOrganUnit",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            OrganUnit = response;
            WNDEditAndAddOrgan_OnOpen();
            FillFormAddUnit();

        },
        error: function (e) {
        }
    });


}

function DDLtParentOrgan_OnInit() {

    $("#DDLtParentOrgan").kendoDropDownList({
        dataSource: {
            data: _OrganUnits,
            schema: {
                model: {
                    id: "ID"
                }
            }
        },

        dataTextField: "Title",
        dataValueField: "ID",
        filter: "contains",
        optionLabel: "انتخاب بخش...",
        change: SelectUnitParent
    });
    LoaderHide();

}

function DDLUnitManager_OnInit() {
    $("#DDLtUnitManager").kendoDropDownList({
        dataSource: {
            data: _UsersOrganUnits,
            schema: {
                model: {
                    id: "ID"
                }
            }
        },

        dataTextField: "FullName",
        dataValueField: "ID",
        filter: "contains",
        optionLabel: "انتخاب مدیر...",
        change: SelectUnitManager
    });
}

function DDLAddUser_OnInit() {
    $("#DDLAddUser").kendoDropDownList({
        dataSource: {
            data: _UsersOrganUnits,
            schema: {
                model: {
                    id: "ID"
                }
            }
        },

        dataTextField: "FullName",
        dataValueField: "ID",
        filter: "contains",
        optionLabel: "انتخاب فرد...",
        change: SelectAddUser
    });
}

function SelectUnitParent() {
    OrganUnit.ParentId = $("#DDLtParentOrgan").data("kendoDropDownList").value();
}

function SelectUnitManager() {
    SelectedManager = FindUserById( $("#DDLtUnitManager").data("kendoDropDownList").value())
    OrganUnit.ManagerID = SelectedManager.ID;
}

function SelectAddUser() {

    id = $("#DDLAddUser").data("kendoDropDownList").value();
    SelectedUser = FindUserById(id);


}

function IsExistInList(entities, id) {
    for (var i = 0; i < entities.length; i++) {
        if (entities[i].ID == id) {
            return true;
        }
    }
    return false;
}

function FindUserById(id) {

    var result = {};
    for (var i = 0; i < _UsersOrganUnits.length; i++) {
        if (_UsersOrganUnits[i].ID == id) {
            result = _UsersOrganUnits[i];
        }
    }
    return result;
}

function EditOrganUnit(e) {

    var grid = $("#GRDOrganisationUnits").data("kendoGrid");
    var dataItem = grid.dataItem($(e).closest("tr"));
    OrganUnit = dataItem;
    FillFormEditUnit();
    WNDEditAndAddOrgan_OnOpen();

}

function AddNewUserToUnit(e) {

    if (IsExistInList(OrganUnit.Users, SelectedUser.ID)) {
        Notify("کاربر در این واحد میباشد", "danger");
    } else {
        OrganUnit.Users.push(SelectedUser);
        $("#GRDOrganUsers").data("kendoGrid").dataSource.read();
        Notify("کاربر به واحد مربوطه افزوده شد", "success");
    }

}

function GRDOrganUsers_OnInit() {

    $("#GRDOrganUsers").kendoGrid({
        dataSource: {
            transport: {
                read: function (e) {
                    e.success(OrganUnit.Users);
                }
            },
            schema: {
                model: {
                    id: "ID"
                },
               
            }
        },
        height: 300,
        filterable: true,
        sortable: true,
        selectable: true,
        columns: [
            { field: "FullName", title: "نام فرد", width: 50 },
            {
                title: "حذف",
                template: "<button onclick='DeleteFromUsers(this)' class='btn btn-warning btn-sm edit' name='info' title='حذف' > حذف</button>",
                headerTemplate: "<label class='text-center'> حذف </label>",
                filterable: false,
                sortable: false,
                width: 30
            }
        ],
        pageable: {
            pageSize: 5,
            pageSizes: false
        }
    });
}

function FillFormAddUnit() {
    $("#UnitTitle").val(OrganUnit.Title);
    GRDOrganUsers_OnInit();
}

function FillFormEditUnit() {

    $("#UnitTitle").val(OrganUnit.Title);
    GRDOrganUsers_OnInit();

    var DDLtUnitManager = $("#DDLtUnitManager").data("kendoDropDownList");
    var DDLtParentOrgan = $("#DDLtParentOrgan").data("kendoDropDownList");

    for (var i = 0; i < _UsersOrganUnits.length; i++) {
        if (_UsersOrganUnits[i].ID == OrganUnit.ManagerID) {
            DDLtUnitManager.select(i + 1);
            OrganUnit.ManagerID = _UsersOrganUnits[i].ID;
            return
        }
    }

    for (var i = 0; i < _OrganUnits.length; i++) {
        if (_OrganUnits[i].ID == OrganUnit.ID) {
            DDLtParentOrgan.select(i + 1);
            OrganUnit.ParentId = _OrganUnits[i].ID;
            return
        }
    }

    $("#GRDOrganUsers").data("kendoGrid").dataSource.read();
}

function SendOrganUnit() {

    var prmData = JSON.stringify(OrganUnit);
    $.ajax({
        type: "Post",
        url: "/api/SettingApi/SaveOrganisationUnit",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: prmData,
        success: function (response) {
            WNDEditAndAddOrgan_OnClose();
            RefreshGRDOrganUnits();
           
            
        },
        error: function (e) {

        }
    });
}

function DeleteFromUsers(e) {
    ;
    var grid = $("#GRDOrganUsers").data("kendoGrid");
    var dataItem = grid.dataItem($(e).closest("tr"));

    for (var i = 0; i < OrganUnit.Users.length; i++) {
        if (dataItem.ID == OrganUnit.Users[i].ID) {
            OrganUnit.Users.splice(i, 1);
        }
        $("#GRDOrganUsers").data("kendoGrid").dataSource.read();
        
       
    }
}
