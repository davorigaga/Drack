$(document).ready(function () {
    $("body").on("click", "#sidebarCollapse", function (e) {
        e.preventDefault();
        $("#wrapper").toggleClass("toggled");
    });
});