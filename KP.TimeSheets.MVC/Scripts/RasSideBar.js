﻿function openNav() {
   
    if ($("#mySidebar").css('marginRight') == "-250px") {
        $(".panel-collapse").collapse("hide");
        $(".ras-sidebar-left-logo").fadeOut(100);
        $("#mySidebar").css({ "margin-right": "0px" });
        var width = $(window).width();
        $(".content-body").css({ "margin-right": "300px" });
        $('.content-body').css({ 'width': width-300+"px" });
       
    } else {
        $(".ras-sidebar-left-logo").fadeIn(100);
        $('.panel-collapse').collapse('hide');
        var width = $(window).width();
        $("#mySidebar").css({ "margin-right": "-250px" });
        $(".content-body").css({ "margin-right": "50px" });
        $('.content-body').css({ 'width': width - 50 + "px" });
      
    }
}


function ShowAndHideUserMenu() {
    if ($('.ras-dropdown-content').is(':visible')) {
        $(".ras-dropdown-content").fadeOut(300);
    } else {
        $(".ras-dropdown-content").fadeIn(300);
    }
    }
   



