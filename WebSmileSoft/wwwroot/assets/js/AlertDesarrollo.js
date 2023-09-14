// Agrega un manejador de eventos para la clase "btn-en-desarrollo"
document.addEventListener("DOMContentLoaded", function () {
    const buttons = document.querySelectorAll(".btn-en-desarrollo");

    buttons.forEach(function (button) {
        button.addEventListener("click", function () {
            event.preventDefault();
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Este Modulo esta en Desarrollo!',
                confirmButtonColor: '#008dc9'
            })
        });
    });
});