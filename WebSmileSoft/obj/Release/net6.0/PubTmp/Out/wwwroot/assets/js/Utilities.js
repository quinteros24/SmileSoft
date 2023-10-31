﻿// Función para mostrar el loader
window.addEventListener("load", function () {

    var load_screen = document.getElementById("load_screen");
    document.body.removeChild(load_screen);
});

function validarEdadMayorDe18(e) { e = document.getElementById(e), e = new Date(e.value); return !((new Date).getFullYear() - e.getFullYear() < 18 && (Swal.fire({ title: "Error", text: "Debes ser mayor de 18 años", icon: "error", confirmButtonText: "Aceptar" }), 1)) } function onChangeAgeNotification(e) { e.addEventListener("change", function () { var e = new Date(this.value); (new Date).getFullYear() - e.getFullYear() < 18 && Swal.fire({ title: "Error", text: "El usuario debe ser mayor de 18 años", icon: "error", confirmButtonText: "Aceptar" }) }) } function CheckPass(e) { return e.length < 8 ? "La contraseña debe tener al menos 8 caracteres." : /[A-Z]/.test(e) ? /[a-z]/.test(e) ? !/\s/.test(e) && !/[\uD800-\uDFFF]/.test(e) || "La contraseña debe contener al menos un numero." : "La contraseña debe contener al menos una letra minuscula." : "La contraseña debe contener al menos una letra mayuscula." } function allowNumbersOnly(e) { e.value = e.value.replace(/[^0-9]/g, "") } function mostrarCargando() { Swal.fire({ title: "Cargando", text: "Por favor, espere un momento...", showCancelButton: !1, showConfirmButton: !1, allowOutsideClick: !1, onBeforeOpen: () => { Swal.showLoading() } }) } function mostrarMensajeError(e) { Swal.fire({ title: "Error", text: e, icon: "error", confirmButtonText: "Aceptar" }) } function updateNavbarBackground(e) { var t = document.getElementById("nav_bar"); t.classList.remove("bg-primary", "bg-danger", "bg-success"), t.classList.add(e) } function CerrarSesion() { sessionStorage.clear(), window.location.href = '@Url.Action("Login", "Account")' } function mostrarElementosPorRol(e) { var t = $("#divAdmin"), n = $("#divDoctor"), o = $("#divUser"), r = $("#page-top"); t.hide(), n.hide(), o.hide(), r.hide(), 1 == e ? (t.show(), n.hide(), o.hide()) : 2 == e ? n.show() : 3 == e && o.show(), 1 != e && 2 != e && 3 != e || r.show() } function CambiarVinculoLogo(e) { e = 1 == e ? "/Admin/Index" : 2 == e ? "/Doctor/Index" : 3 == e ? "/Patient/Index" : "/Home/Index"; document.getElementById("sidebar-link").href = e } document.addEventListener("DOMContentLoaded", function () { document.querySelectorAll(".btn-en-desarrollo").forEach(function (e) { e.addEventListener("click", function () { event.preventDefault(), Swal.fire({ icon: "error", title: "Oops...", text: "Este Modulo esta en Desarrollo!", confirmButtonColor: "#008dc9" }) }) }) }), document.addEventListener("DOMContentLoaded", function () { var e = sessionStorage.getItem("userFName"), t = sessionStorage.getItem("userLName"), n = document.getElementById("nombreUsuario"), o = document.getElementById("SaludoUsuario"); null !== e && null !== t && n ? n.textContent = e + " " + t : null !== e && null !== t && o ? o.textContent = e + " " + t : console.log("No se encontraron datos en sessionStorage o algunos elementos HTML no existen.") });


//// Set initial values from local storage
//const backgroundColor = localStorage.getItem('backgroundColor');
//if (backgroundColor) {
//    $("#content-wrapper").css("background-color", backgroundColor);
//}

//const sidebarColor = localStorage.getItem('sideColor');
//if (sidebarColor) {
//    $("#sidebar").css("background-color", sidebarColor);
//}

//const navbarColor = localStorage.getItem('topColor');
//if (navbarColor) {
//    $("#nav_bar").removeClass("bg-primary");
//    $("#nav_bar").css("background-color", navbarColor);
//}

//// Update local storage and corresponding elements on change
//$('#logoInput').change(function () {
//    const file = this.files[0];
//    if (file) {
//        const objectURL = URL.createObjectURL(file);
//        document.getElementById('urlImageMenu').src = objectURL;
//        // Save to server and update database here
//    }
//});

//$('#BackgroundColor').change(function () {
//    const color = $(this).val();
//    $("#content-wrapper").css("background-color", color);
//    localStorage.setItem('backgroundColor', color);
//});

//$('#SidebarColor').change(function () {
//    const color = $(this).val();
//    $("#sidebar").css("background-color", color);
//    localStorage.setItem('sidebarColor', color);
//});

//$('#NavbarColor').change(function () {
//    const color = $(this).val();
//    $("#nav_bar").removeClass("bg-primary");
//    $("#nav_bar").css("background-color", color);
//    localStorage.setItem('navbarColor', color);
//});
