
let userRole = sessionStorage.getItem('userRole');
if (userRole !== '1') {
    logoutUser();
}
//document.getElementById('updateLogo').addEventListener('click', function () {
//    // Obtener el archivo seleccionado
//    const fileInput = document.getElementById('logoInput');
//    const file = fileInput.files[0];

//    if (file) {
//        // Crear una URL para la imagen seleccionada
//        const objectURL = URL.createObjectURL(file);

//        // Actualizar la fuente de la imagen en la barra de navegación
//        document.getElementById('navLogo').src = objectURL;

//        // Aquí puedes enviar la imagen al servidor para guardarla
//        // y asignarle un nombre único en tu directorio de imágenes.
//        // Luego, actualiza la fuente de la imagen en el servidor.

//        // También puedes considerar la posibilidad de almacenar la
//        // ruta de la imagen en tu base de datos para recuperarla
//        // cuando sea necesario.
//    }
//});
// Cambiar el color de fondo
$("#BackgroundColor").on("input", function () {
    const color = $(this).val();
    //console.log("Aplicando color a Fondo");
    // $("#SiteContainerRender").css("background-color", color);
    $("#content-wrapper").css("background-color", color);
});
// Cambiar el color de la barra lateral
$("#SidebarColor").on("input", function () {
    const color = $(this).val();
    //console.log("Aplicando color a SideBar");
    $("#sidebar").css("background-color", color);
});
// Cambiar el color de la barra lateral
$("#NavbarColor").on("input", function () {
    const color = $(this).val();
    //console.log("Aplicando color a Barra Superior");
    $("#nav_bar").removeClass("bg-primary");
    $("#nav_bar").css("background-color", color);
});

// Agrega un manejador de eventos para la clase "btn-en-desarrollo"
document.addEventListener("DOMContentLoaded", function () {
    const buttons = document.querySelectorAll(".btn-en-desarrollo");
    event.preventDefault();
    buttons.forEach(function (button) {
        button.addEventListener("click", function () {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Esta Función esta en Desarrollo!',
                confirmButtonColor: '#008dc9'
            })
        });
    });
});
const wpNumberInput = document.getElementById("wpNumber"), urlAPIset = `${sessionStorage.urlEP}/api/Generics/v1/SetContactNumber?uIDPetition=${uIDPetition}&oID=1&number=`; updateWP.addEventListener("click", (function () { const e = wpNumberInput.value; fetch(sessionStorage.urlEP + `/api/Generics/v1/SetContactNumber?uIDPetition=${uIDPetition}&oID=1`, { method: "PUT", headers: { "Content-Type": "application/json" }, body: JSON.stringify(e) }).then((e => { if (e.ok) return e.json(); throw new Error("Algo salió mal!") })).then((e => { Swal.fire({ icon: "success", title: "Actualizado!", text: "El número de Whatsapp ha sido actualizado", confirmButtonColor: "#008dc9" }) })).catch((e => { Swal.fire({ icon: "error", title: "Oops...", text: e.message, confirmButtonColor: "#008dc9" }) })) }));
//mostrar numero actual en el input
fetch(sessionStorage.urlEP + "/api/Generics/v1/GetContactNumber?oID=1", { method: "PUT" }).then((e => { if (e.ok) return e.json(); throw new Error("Algo salió mal!") })).then((e => { wpNumberInput.value = e.itemJson })).catch((e => { Swal.fire({ icon: "error", title: "Oops...", text: e.message, confirmButtonColor: "#008dc9" }) }));
