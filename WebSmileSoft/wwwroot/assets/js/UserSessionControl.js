var inactivityTimeout;
var logoutUrl = document.getElementById("logout-link").getAttribute("data-logout-url");
console.log(logoutUrl);
// Luego, puedes redirigir al usuario utilizando esta URL
//window.location.href = logoutUrl;
// Funci�n para restablecer el tiempo de inactividad
function resetInactivityTimer() {
    clearTimeout(inactivityTimeout);
    inactivityTimeout = setTimeout(logoutUser, 10000); // 15 minutos (900,000 milisegundos)
}

function logoutUser() {
    // Limpia el sessionStorage
    sessionStorage.clear();

    // Realiza una solicitud al servidor para cerrar la sesi�n
    console.log("logoutUser" + inactivityTimeout);

    // Redirecciona a la p�gina de inicio de sesi�n
   window.location.href = logoutUrl; // Reemplaza con la URL correcta
}

// Evento para restablecer el temporizador en respuesta a la actividad del usuario
document.addEventListener('mousemove', resetInactivityTimer);
document.addEventListener('keydown', resetInactivityTimer);

// Inicia el temporizador al cargar la p�gina
resetInactivityTimer();

function verifyToken() {
    let data = {
        token: sessionStorage.getItem('accessToken'),
        userNameLogin: sessionStorage.getItem('userLogin')

    };
    // Realiza una solicitud al servidor para verificar el token
    
    $.ajax({

        type: "POST",
        url: sessionStorage.getItem('urlEP') + '/api/Generics/v1/GenerateJWToken',
        data: JSON.stringify(data),
        contentType: "application/json",
        dataType: "json",
        headers: {
            'userID': parseInt(sessionStorage.getItem("userID")),
            //'Authorization': 'Bearer ' + token // Agrega el token actual
        }
    }).done(function (response) {
        console.log(response)
        if (response.itemJson != null && response.itemJson != undefined && response.itemJson != "") {
            return sessionStorage.setItem('acessToken', response.itemJson)
        }
        logoutUser();
    });
        //.then(response => {
        //    if (response.status === 200) {
        //        return response.json(); // Parsea la respuesta JSON
        //    } else {
        //        // Token inv�lido
        //        logoutUser();
        //        throw new Error('Token inv�lido');
        //    }
        //})
        //.then(data => {
        //    // Almacena los datos del usuario en sessionStorage si el token es v�lido
        //    sessionStorage.setItem("jwtToken", token);
        //    sessionStorage.setItem("userFName", data.uName);
        //    sessionStorage.setItem("userLName", data.uLastName);
        //    sessionStorage.setItem("UserRole", data.uTID);
        //})
        //.catch(error => {
        //    console.error('Error al verificar el token:', error);
        //});
}

// Funci�n para cerrar la sesi�n del usuario
//function logoutUser() {
//    // Realiza una solicitud al servidor para cerrar la sesi�n
//    fetch('v1/Logout', {
//        method: 'POST'
//    })
//        .then(response => {
//            if (response.status === 200) {
//                // Redirecciona a la p�gina de inicio de sesi�n
//                window.location.href = 'Login';
//            }
//        });
//}

// Inicializa el temporizador para verificar el token cada 5 minutos
setInterval(verifyToken, 300000); // 300,000 milisegundos = 5 minutos
