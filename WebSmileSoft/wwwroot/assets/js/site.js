// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.getElementById('resetpasswordbutton').addEventListener('click', function (event) {
    event.preventDefault(); // Evita la recarga de la página
    Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: 'Aun estamos Trabajando!',
        confirmButtonColor: '#008dc9'
    })
});