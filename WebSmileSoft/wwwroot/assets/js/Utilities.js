//// Función para mostrar el loader
//function showLoader() {
//    var loader = document.querySelector(".loader-container");
//    loader.style.display = "flex"; // Muestra el loader
//}

//// Función para ocultar el loader
//function hideLoader() {
//    var loader = document.querySelector(".loader-container");
//    loader.style.opacity = "0"; // Cambia la opacidad a 0 (completamente transparente)

//    // Después de que se complete la transición, oculta el loader
//    loader.addEventListener("transitionend", function () {
//        loader.style.display = "none"; // Oculta el loader
//    });
//}

// Ejemplo de uso: Mostrar el loader mientras se realiza una petición AJAX
/*showLoader();*/

// Simular una petición AJAX (puedes reemplazar esto con tu propia lógica)
//setTimeout(function () {
//    // Supongamos que la petición AJAX se completó con éxito después de 3 segundos
//    // Puedes realizar tus acciones aquí después de que la petición sea exitosa

//    // Ocultar el loader
//    hideLoader();
//}, 500);

// Espera a que se cargue el contenido de la página
//document.addEventListener("DOMContentLoaded", function () {
//    // Simula una espera de 3 segundos (3000 milisegundos) antes de ocultar el loader
//    setTimeout(function () {
//        // Cambia la opacidad del loader gradualmente para que se desvanezca
//        var loader = document.querySelector(".loader-container");
//        loader.style.opacity = "0"; // Cambia la opacidad a 0 (completamente transparente)

//        // Después de que se complete la transición, oculta el loader
//        loader.addEventListener("transitionend", function () {
//            loader.style.display = "none"; // Oculta el loader
//        });
//    }, 300);
//});

// Variable para rastrear si el loader ya se mostró
//let loaderShown = false;

// Función para mostrar el loader
window.addEventListener("load", function () {
    var load_screen = document.getElementById("load_screen");
    document.body.removeChild(load_screen);
});