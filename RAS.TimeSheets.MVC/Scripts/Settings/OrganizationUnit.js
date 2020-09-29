var _CurrentOrgUnit = [];
var _orgUnits = [];

$(document).ready(function () {

    GetOrgUnit();


});


function GetOrgUnit() {

    $.ajax({
        type: "Get",
        url: "/api/OrganizatinUnit/GetOrganizationUnits",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnGetorgUnit,
        error: function (e) {

        }
    });
}

function OnGetorgUnit() {
    

}