document.addEventListener("DOMContentLoaded", function () {
    // Set initial values from local storage
    console.log("Funcion Cambiar tema cargada")
    const backgroundColor = localStorage.getItem('backgroundColor');
    if (backgroundColor) {
        $("#content-wrapper").css("background-color", backgroundColor);
    }

    const sidebarColor = localStorage.getItem('sideColor');
    if (sidebarColor) {
        $("#sidebar").css("background-color", sidebarColor);
    }

    const navbarColor = localStorage.getItem('topColor');
    if (navbarColor) {
        $("#nav_bar").removeClass("bg-primary");
        $("#nav_bar").css("background-color", navbarColor);
    }

});