
// Agrega un manejador de eventos para la clase "btn-en-desarrollo"
document.addEventListener("DOMContentLoaded", function () {
    const buttons = document.querySelectorAll(".btn-en-desarrollo");

    buttons.forEach(function (button) {
        button.addEventListener("click", function () {
            event.preventDefault();
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Esta Función esta en Desarrollo!',
                confirmButtonColor: '#008dc9'
            })
        });
    });
});

// Obtén el userID del Session Storage

$(document).ready(function () {
    // Adjunta un controlador de eventos de clic al botón
    let uID = sessionStorage.getItem('userID');

    console.log("Buscando al Usuario " + uID);

    $.ajax({
        url: sessionStorage.urlEP + '/api/Users/v1/GetUserDetails',
        type: 'GET',
        data: { 'uID': uID },
        contentType: "application/json",
        dataType: 'json',
        success: function (data) {
            console.log("Cargando Respuesta")
            console.log(data);

            if (data.itemJson === null) {
                console.log("No se cargó correctamente");
            } else {
                // Mostrar los datos en la consola o realizar otras acciones
                let userData = data.itemJson[0]; // Accede al primer elemento del arreglo

                // Llama a una función para actualizar el formulario con los datos recibidos
                console.log("Acutalizando Formulario")
                actualizarFormulario(userData);
            }
        },
        error: function (error) {
            console.log('Error al obtener los detalles del usuario.');
        }
    });

    // Función para actualizar el formulario con los datos del usuario
    function actualizarFormulario(userData) {
        let StatusMapping = {
            0: 'Activo',
            1: 'Inactivo'
        };

        // Rellena los campos del formulario con los datos del usuario
        $("#username").val(userData.uLoginName);
        $("#user_names").val(userData.uName);
        $("#user_lastnames").val(userData.uLastName);
        $("#editCorreo").val(userData.uEmailAddress);
        $("#address").val(userData.uAddress);
        $("#phone_number").val(userData.uCellphone);
        $("#id_type").val(userData.dtID);
        $("#id_number").val(userData.uDocument);
        $("#user_status").val(StatusMapping[userData.uStatus]);
    }
});


$(".update-profile-button").click(function () {

    //let uID = sessionStorage.getItem('userID');
    // Obtener los datos actualizados del formulario
    let updatedUserData = {
        uID: sessionStorage.getItem('userID'),
        //utID: parseInt(sessionStorage.getItem('userRole')),
        uLoginName: $("#username").val(),
        uName: $("#user_names").val(),
        uLastName: $("#user_lastnames").val(),
        uEmailAddress: $("#editCorreo").val(),
        //fechaNacimiento: $("#birthDate").val(),
        //genero: $("#genderSelect").val(),
        uAddress: $("#address").val(),
        dtID: parseInt($("#id_type").val()),
        uCellphone: $("#phone_number").val(),
        uDocument: $("#id_number").val(),
        //uIsBlocked: parseInt(sessionStorage.getItem('userBlock')),
        //uStatus: parseInt(sessionStorage.getItem('userStatus'))
    };


    $.ajax({
        url: sessionStorage.urlEP + '/api/Users/v1/CreateUpdateUsers', // Reemplaza con la URL correcta
        type: 'POST',
        data: JSON.stringify(updatedUserData),
        contentType: "application/json",
        dataType: "json",
        success: function (response) {
            Swal.fire({
                title: 'Datos Actualizados',
                text: 'Los datos del usuario se actualizaron correctamente',
                icon: 'success',
                confirmButtonText: 'Aceptar'
            });

            // Realiza otras acciones después de guardar los cambios si es necesario
        },
        error: function () {
            Swal.fire({
                title: 'Error',
                text: 'Error al guardar los cambios',
                icon: 'error',
                confirmButtonText: 'Aceptar'
            });
        }
    });
});



function ChangeUserPass() {

    console.log("Cambiar Contrasena");
    event.preventDefault();
    let password = document.getElementById("NewPassword").value;
    let repeat_password = document.getElementById("Repeat_Password").value;

    let uID = sessionStorage.getItem('userID');;

    console.log("ID del Usuario" + uID);
    if (password === "") {
        Swal.fire({
            text: 'Por favor, ingrese su nueva contraseña',
            confirmButtonColor: '#008dc9'
        });
    } else if (repeat_password === "") {
        Swal.fire({
            text: 'Por favor, ingrese la confirmación de la contraseña nueva',
            confirmButtonColor: '#008dc9'
        });
    } else if (password != repeat_password) {
        Swal.fire({
            text: 'Las contraseñas deben ser iguales',
            confirmButtonColor: '#008dc9'
        });
    } else if (!CheckUserPass(password)) {
        Swal.fire({
            text: 'La contraseña no cumple con los requisitos.',
            confirmButtonColor: '#008dc9'
        });
    } else {

        let data = {
            Password: password,
            UID: uID
        };

        $.ajax({

            type: "POST",
            url: sessionStorage.urlEP + '/api/Users/v1/ChangePassword',
            //header: {'Authorization': 'Bearer ' + sessionStorage.getItem('accessToken') },
            data: JSON.stringify(data),
            contentType: "application/json",
            //async: false,
            dataType: "json",
            success: function (response) {

                // console.log("Solicitud exitosa. Datos enviados:", data);
                // console.log("Respuesta del servidor:", response);
                if (response.codeStatus == 0) {

                    //window.location.href = '@Url.Action("Index", "")';
                    Swal.fire({
                        text: 'Contraseña actualizada con exito',
                        confirmButtonColor: '#008dc9'
                    });
                } else {

                    Swal.fire({
                        text: "Hubo un fallo" + response.messageStatus,
                        confirmButtonColor: '#008dc9'
                    });
                    //alert('Inicio de sesión fallido, ' + response.messageStatus);
                }
            },
            error: function (xhr, status, error) {
                console.log("Error.");
            }
        });
    }
}
function CheckUserPass(password) {
    // Verificar la longitud de la contraseña (debe tener al menos 8 caracteres)
    if (password.length < 8) {
        return false;
    }

    // Verificar si la contraseña contiene al menos una letra minúscula y una mayúscula
    if (!/[a-z]/.test(password) || !/[A-Z]/.test(password)) {
        return false;
    }

    // Verificar si la contraseña contiene al menos un número
    if (!/\d/.test(password)) {
        return false;
    }

    // Verificar si la contraseña contiene espacios en blanco o caracteres especiales
    if (/\s/.test(password) || /[\uD800-\uDFFF]/.test(password)) {
        return false;
    }

    // Si la contraseña pasa todas las validaciones, es válida
    return true;
}



let userStatus = sessionStorage.getItem('userStatus');

let StatusMapping = {
    0: 'Activo',
    1: 'Inactivo'
};

let Status = StatusMapping[userStatus];
$("#user_status").val(Status);
$("#desactivarCuentabtn").click(function () {

    // Obtén el userID del Session Storage
    let userIdd = sessionStorage.getItem('userID');
    let userNamed = sessionStorage.getItem('userFName') + " " + sessionStorage.getItem('userLName');
    // Comprueba si se tiene un userID válido

    let dataUser = {
        uID: parseInt(userIdd),
        uStatus: 0
    }; //Ajustar para enviar si esta inactivo activar si esta activo desactivar
    // Muestra un mensaje de confirmación antes de desactivar el usuario

    Swal.fire({
        title: '¿Estás seguro de Desactivar? a:',
        text: '¡ ' + userNamed + ' !',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, desactivar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            //console.log("Desactivar 2" + userId);
            // Realiza la solicitud AJAX para desactivar el usuario
            $.ajax({
                url: sessionStorage.urlEP + '/api/Users/v1/SetUserStatus/',
                type: 'POST',
                data: JSON.stringify(dataUser),
                contentType: "application/json",
                //async: false,
                dataType: "json",
                success: function (response) {
                    console.log("Respuesta del Servidor " + response);

                    Swal.fire({
                        title: 'Usuario Desactivado',
                        text: 'El usuario' + sessionStorage.getItem("userFName") + " " + sessionStorage.getItem("userLName") + 'se desactivó correctamente',
                        icon: 'success',
                        confirmButtonText: 'Aceptar'
                    });


                },
                error: function () {
                    Swal.fire({
                        title: 'Error',
                        text: 'Error al deshabilitar el usuario',
                        icon: 'error',
                        confirmButtonText: 'Aceptar'
                    });
                }
            });
        }
    });
});
