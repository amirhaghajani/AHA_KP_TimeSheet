﻿var _YesterdayData = [];
var _ThisMonthData = [];
LoaderShow();

$(document).ready(function () {
    LoaderShow();
    GetHomeData();
});
function adjustSize() {
    // For small screens, maximize the window when it is shown.
    // You can also make the check again in $(window).resize if you want to
    // but you will have to change the way to reference the widget and then
    // to use $("#theWindow").data("kendoWindow").
    // Alternatively, you may want to .center() the window.

    if ($(window).width() < 800 || $(window).height() < 600) {
        this.maximize();
    }
}

 

$(".sidebar-dropdown > a").click(function () {

    $(".sidebar-submenu").slideUp(200);
    if (
        $(this)
            .parent()
            .hasClass("active")
    ) {
        $(".sidebar-dropdown").removeClass("active");
        $(this)
            .parent()
            .removeClass("active");
    } else {
        $(".sidebar-dropdown").removeClass("active");
        $(this)
            .next(".sidebar-submenu")
            .slideDown(200);
        $(this)
            .parent()
            .addClass("active");
    }
});

$("#close-sidebar").click(function () {
    $(".page-wrapper").removeClass("toggled");
});
$("#show-sidebar").click(function () {
    $(".page-wrapper").addClass("toggled");
});

function GetHomeData() {

    $.ajax({
        type: "Get",
        url: "/api/TimeSheetsAPI/GetYesterdayData",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: Page_OnInitYesterday,
        error: function (e) {

        }
    });


}

function Page_OnInitYesterday(response) {
    $.ajax({
        type: "Get",
        url: "/api/TimeSheetsAPI/GetThisMonthDataForHomePage",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: Page_OnInitThisMonth,
        error: function (e) {

        }
    });


    _YesterdayData = response;
    
    $("#UsenNameSidebar").text(response.CurrentUser );
    $("#currentUser").text(response.CurrentUser);
    $("#PresenceYesterday").text(response.Presence);
    $("#WorkYesterday").text(response.Work);
    $("#differenceYesterday").text(response.Defference);
    $("#PresenceYesterdaypercent").width(response.Presencepercent*10);
    $("#workYesterdaypercent").width(response.Workpercent*10);
    $("#differentYesterdaypercent").width(response.Defferencepercent*10);
    
}

function Page_OnInitThisMonth(response) {
    _ThisMonthData = response

    $("#PresenceThisMonth").text(response.Presence);
    $("#WorkThisMonth").text(response.Work);
    $("#differenceThisMonth").text(response.Defference);
    $("#PresenceThisMonthpercent").width(response.Presencepercent );
    $("#workThisMonthpercent").width(response.Workpercent );
    $("#differentThisMonthpercent").width(response.Defferencepercent );
    LoaderHide()

}

function LoaderShow() {
    
    $("#Loader").fadeIn(500)
}

function LoaderHide() {
    $("#Loader").fadeOut(500)
}

//info error success
function ShowNotification(id, message, color) {
    
    //Initial kendoNotification
    $("#" + id).kendoNotification({
        position: {
            top: 150,
            left: 20
        },
        autoHideAfter: 10000,
        stacking: "down"
    });
    $("#" + id).getKendoNotification().show(message, color);
}

function Notify(messege, type) {
   
    
    $.notify({
        //icon: 'glyphicon glyphicon-warning-sign',
        //title: 'Bootstrap notify',
        message:"<strong >"+ messege +"</strong>",
        //url: 'https://github.com/mouse0270/bootstrap-notify',
        //target: '_blank'
    }, {
            // settings
            //element: 'body',
            //position: null,
            type: type,
            allow_dismiss: false,
            //newest_on_top: false,
            //showProgressbar: true,
            placement: {
                from: "top",
                align: "left"
            },
            offset: 20,
            spacing: 10,
            z_index: 10100,
            delay: 1000,
            timer: 1000,
            //url_target: '_blank',
            //mouse_over: null,
            animate: {
                enter: 'animated fadeInDown',
               exit: 'animated fadeOutUp'
           },
            //onShow: null,
            //onShown: null,
            //onClose: null,
            //onClosed: null,
            //icon_type: 'class',
           // template: "<div style='height:15px;width:20%' class='shadow' >" + messege + "</div>"
        });
}
/* When the user clicks on the button,
           toggle between hiding and showing the dropdown content */
function DDLUserAccount() {
    document.getElementById("DDLUserAccount").classList.toggle("show");
}





function doExport(selector, params) {
    var options = {
        //ignoreRow: [1,11,12,-2],
        //ignoreColumn: [0,-1],
        tableName: 'Countries',
        worksheetName: 'Countries by population'
    };

    $.extend(true, options, params);

    $(selector).tableExport(options);
}

function DoOnCellHtmlData(cell, row, col, data) {
    var result = "";
    if (data != "") {
        var html = $.parseHTML(data)

        $.each(html, function () {
            if (typeof $(this).html() === 'undefined')
                result += $(this).text();
            else if ($(this).is("input"))
                result += $('#' + $(this).attr('id')).val();
            else if (!$(this).hasClass('no_export'))
                result += $(this).html();
        });
    }
    return result;
}



//$( ".fancytime" ).timeDropper({

//  // custom time format
////  format: 'h:mm a',
//format: 'HH:mm',
//  // auto changes hour-minute or minute-hour on mouseup/touchend.
//  autoswitch: false,

//  // sets time in 12-hour clock in which the 24 hours of the day are divided into two periods. 
//  meridians: false,

//  // enable mouse wheel
//  mousewheel: false,

//  // auto set current time
//  setCurrentTime: true,

//  // fadeIn(default), dropDown
//  init_animation: "fadein",

//  // custom CSS styles
//  primaryColor: "#1977CC",
//  borderColor: "#1977CC",
//  backgroundColor: "#FFF",
//  textColor: '#555'
  
//});







