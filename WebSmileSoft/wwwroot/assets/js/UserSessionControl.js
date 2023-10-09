//// C�digo de usuario para controlar la sesi�n del usuario
//// Se requiere jQuery para realizar las solicitudes AJAX
//// Se requiere SweetAlert2 para mostrar mensajes de alerta
//// Se requiere sessionStorage para almacenar los datos del usuario
//// Se requiere el atributo data-logout-url en el elemento <a> para cerrar la sesi�n

//var inactivityTimeout;
//var logoutUrl = document.getElementById("logout-link").getAttribute("data-logout-url");
////console.log(logoutUrl);
//// Luego, puedes redirigir al usuario utilizando esta URL
////window.location.href = logoutUrl;
//// Funci�n para restablecer el tiempo de inactividad
//function resetInactivityTimer() {
//    clearTimeout(inactivityTimeout);
//    inactivityTimeout = setTimeout(logoutUser, 900000); // 15 minutos (900,000 milisegundos)
//}

//function logoutUser() {
//    // Limpia el sessionStorage
//    sessionStorage.clear();

//    // Realiza una solicitud al servidor para cerrar la sesi�n
//    console.log("logoutUser" + inactivityTimeout);

//    // Redirecciona a la p�gina de inicio de sesi�n
//    window.location.href = logoutUrl; // Reemplaza con la URL correcta
//}

//// Evento para restablecer el temporizador en respuesta a la actividad del usuario
//document.addEventListener('mousemove', resetInactivityTimer);
//document.addEventListener('keydown', resetInactivityTimer);

//// Inicia el temporizador al cargar la p�gina
//resetInactivityTimer();

//function verifyToken() {
//    let data = {
//        token: sessionStorage.getItem('accessToken'),
//        userNameLogin: sessionStorage.getItem('userLogin')

//    };
//    // Realiza una solicitud al servidor para verificar el token
//    console.log("Validando el token");
//    $.ajax({

//        type: "POST",
//        url: sessionStorage.getItem('urlEP') + '/api/Generics/v1/GenerateJWToken',
//        data: JSON.stringify(data),
//        contentType: "application/json",
//        dataType: "json",
//        headers: {
//            'uID': parseInt(sessionStorage.getItem("userID")),
//            //'Authorization': 'Bearer ' + token // Agrega el token actual
//        }
//    }).done(function (response) {
//        if (response.itemJson != null && response.itemJson != undefined && response.itemJson != "") {
//            return sessionStorage.setItem('accessToken', response.itemJson)
//        }
//        Swal.fire({
//            title: 'Ups...',
//            html: 'Se ha iniciado sesi&oacuten en otro dispositivo',
//            icon: 'warning'
//        }).then(function () {
//            logoutUser();
//        });
//    });
//        //.then(response => {
//        //    if (response.status === 200) {
//        //        return response.json(); // Parsea la respuesta JSON
//        //    } else {
//        //        // Token inv�lido
//        //        logoutUser();
//        //        throw new Error('Token inv�lido');
//        //    }
//        //})
//        //.then(data => {
//        //    // Almacena los datos del usuario en sessionStorage si el token es v�lido
//        //    sessionStorage.setItem("jwtToken", token);
//        //    sessionStorage.setItem("userFName", data.uName);
//        //    sessionStorage.setItem("userLName", data.uLastName);
//        //    sessionStorage.setItem("UserRole", data.uTID);
//        //})
//        //.catch(error => {
//        //    console.error('Error al verificar el token:', error);
//        //});
//}

//// Inicializa el temporizador para verificar el token cada 5 minutos
//setInterval(verifyToken, 300000); // 300,000 milisegundos = 5 minutos

const logoutUrl = document.getElementById("logout-link").getAttribute("data-logout-url");

const resetInactivityTimer = () => {
    clearTimeout(inactivityTimeout);
    inactivityTimeout = setTimeout(logoutUser, 900000); // 15 minutos (900,000 milisegundos)
};

const logoutUser = () => {
    // Limpia el sessionStorage
    sessionStorage.clear();

    // Realiza una solicitud al servidor para cerrar la sesi�n
    console.log(`logoutUser ${inactivityTimeout}`);

    // Redirecciona a la p�gina de inicio de sesi�n
    window.location.href = logoutUrl; // Reemplaza con la URL correcta
};

// Evento para restablecer el temporizador en respuesta a la actividad del usuario
document.addEventListener('mousemove', resetInactivityTimer);
document.addEventListener('keydown', resetInactivityTimer);

// Inicia el temporizador al cargar la p�gina
let inactivityTimeout = setTimeout(logoutUser, 900000); // 15 minutos (900,000 milisegundos)

const verifyToken = async () => {
    const data = {
        token: sessionStorage.getItem('accessToken'),
        userNameLogin: sessionStorage.getItem('userLogin')
    };
    // Realiza una solicitud al servidor para verificar el token
    console.log("Validando el token");
    const response = await fetch(`${sessionStorage.getItem('urlEP')}/api/Generics/v1/GenerateJWToken`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'uID': parseInt(sessionStorage.getItem("userID")),
            //'Authorization': 'Bearer ' + token // Agrega el token actual
        },
        body: JSON.stringify(data)
    });
    const json = await response.json();
    if (json.itemJson != null && json.itemJson != undefined && json.itemJson != "") {
        sessionStorage.setItem('accessToken', json.itemJson);
    } else {
        Swal.fire({
            title: 'Ups...',
            html: 'Se ha iniciado sesi&oacuten en otro dispositivo',
            icon: 'warning'
        }).then(() => {
            logoutUser();
        });
    }
};

// Inicializa el temporizador para verificar el token cada 5 minutos
setInterval(() => {
    Promise.all([verifyToken()]); // Add more HTTP requests here if needed
}, 300000); // 300,000 milisegundos = 5 minutos