//function validarInicioSesion() {
//    console.log(1);
//    event.preventDefault();
//    let username = document.getElementById("UserLogin").value;
//    let password = document.getElementById("Password").value;

//    if (username === "") {
//        Swal.fire({
//            text: 'Por favor, ingrese su nombre de usuario',
//            confirmButtonColor: '#008dc9'
//        });
//    } else if (password === "") {
//        Swal.fire({
//            text: 'Por favor, ingrese su contraseña',
//            confirmButtonColor: '#008dc9'
//        });
//    } else {

//        let data = {
//            UserLogin: username,
//            password: password
//        };
//        $.ajax({
//            type: "POST",
//            url: '@ViewBag.urlEndPoint' + '/api/Session/v1/Login',
//            data: JSON.stringify(data),
//            contentType: "application/json",
//            //async: false,
//            dataType: "json",
//            success: function (response) {
//                console.log(response);
//                if (response.codeStatus == 0) {
//                    //ir a la vista
//                    sessionStorage.setItem("jwtToken", response.uToken);
//                    sessionStorage.setItem("userFName", response.uName);
//                    sessionStorage.setItem("userLName", response.uLastName);
//                    sessionStorage.setItem("UserRole", response.uTID);
//                    //Swal.fire(response.messageStatus);
//                    //alert(response.messageStatus);
//                    //window.location.href = '@Url.Action("Index", "Home")';
//                } else {
                    
//                    Swal.fire({
//                        text: "Inicio de sesión fallido. " + response.messageStatus,
//                        confirmButtonColor: '#008dc9'
//                    });
//                    //alert('Inicio de sesión fallido, ' + response.messageStatus);
//                }
//            },
//            error: function (error) {
//                console.log("Error.");
//            }
//        });
//    }
//}
//document.getElementById("loginButton").addEventListener("click", validarInicioSesion);

function mostrarCarga() {
    // Oculta el formulario de inicio de sesión
    document.getElementById("loginForm").style.display = "none";
    // Muestra el elemento de carga
    document.getElementById("loader").style.display = "block";
}
document.getElementById("loginButton").addEventListener("submit", mostrarCarga);