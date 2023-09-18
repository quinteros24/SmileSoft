﻿function validarInicioSesion() {
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
            //async: false,
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
document.getElementById("loginButton").addEventListener("click", validarInicioSesion);

