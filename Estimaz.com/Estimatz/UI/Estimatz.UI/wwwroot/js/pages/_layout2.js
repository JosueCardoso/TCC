$(document).ready(function () {
    $('.navbar-nav .nav-link').click(function () {
        $('.navbar-nav .nav-link').removeClass('active');
        $(this).addClass('active');
    });

    $("#dashboardMenuLink").click(function () {
        window.location.href = "/Dashboard/Dashboard";   
    });

    $("#roomsMenuLink").click(function () {
        window.location.href = "/Room/Rooms";   
    });

    $("#logoutItemMenu").click(function () {       
        window.location.href = "/Account/Logout";  
    });
});