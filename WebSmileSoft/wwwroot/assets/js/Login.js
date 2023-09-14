function validarInicioSesion() {
    console.log(1);
    event.preventDefault();
    let username = document.getElementById("UserLogin").value;
    let password = document.getElementById("Password").value;

    if (username === "") {
        Swal.fire({
            text: 'Por favor, ingrese su nombre de usuario',
            confirmButtonColor: '#008dc9'
        });
    } else if (password === "") {
        Swal.fire({
            text: 'Por favor, ingrese su contraseña',
            confirmButtonColor: '#008dc9'
        });
    } else {

        let data = {
            UserLogin: username,
            password: password
        };
        $.ajax({
            type: "POST",
            url: '@ViewBag.urlEndPoint' + '/api/Session/v1/Login',
            data: JSON.stringify(data),
            contentType: "application/json",
            async: false,
            dataType: "json",
            success: function (response) {
                if (response.codeStatus == 0) {
                    //ir a la vista
                    //Swal.fire(response.messageStatus);
                    //alert(response.messageStatus);
                    window.location.href = '@Url.Action("Index", "Home")';
                } else {
                    
                    Swal.fire({
                        text: "Inicio de sesión fallido. " + response.messageStatus,
                        confirmButtonColor: '#008dc9'
                    });
                    //alert('Inicio de sesión fallido, ' + response.messageStatus);
                }
            },
            error: function (error) {
                console.log("Error.");
            }
        });
    }
}
//function recordarUsuario() {
//    // Obtén el elemento del checkbox
//    var rememberMeCheckbox = document.getElementById("rememberMe");

//    // Verifica si el usuario ya había seleccionado "Recordar usuario" anteriormente
//    if (localStorage.getItem("rememberUser") === "true") {
//        rememberMeCheckbox.checked = true;
//    }

//    // Agrega un controlador de eventos para guardar el estado del checkbox cuando cambie
//    rememberMeCheckbox.addEventListener("change", function () {
//        if (rememberMeCheckbox.checked) {
//            // Si el usuario selecciona "Recordar usuario", guarda la elección en el almacenamiento local
//            localStorage.setItem("rememberUser", "true");
//        } else {
//            // Si el usuario desmarca "Recordar usuario", elimina la elección del almacenamiento local
//            localStorage.removeItem("rememberUser");
//        }
//    });
//}

////document.getElementById("remember_me").addEventListener("click", recordarUsuario);
//document.addEventListener("DOMContentLoaded", function () {
//    var recordarUsuarioCheckbox = document.getElementById("remember_me");
//    if (recordarUsuarioCheckbox) {
//        recordarUsuarioCheckbox.addEventListener("change", function () {
//            if (recordarUsuarioCheckbox.checked) {
//                // Si el usuario selecciona "Recordar usuario", guarda la elección en el almacenamiento local
//                localStorage.setItem("remember_me", "true");
//            } else {
//                // Si el usuario desmarca "Recordar usuario", elimina la elección del almacenamiento local
//                localStorage.removeItem("remember_me");
//            }
//        });
//    }
//});
document.getElementById("loginButton").addEventListener("click", validarInicioSesion);

