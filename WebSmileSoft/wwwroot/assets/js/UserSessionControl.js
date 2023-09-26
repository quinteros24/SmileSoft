var inactivityTimeout;

// Función para restablecer el tiempo de inactividad
function resetInactivityTimer() {
    clearTimeout(inactivityTimeout);
    inactivityTimeout = setTimeout(logoutUser, 900000); // 15 minutos (900,000 milisegundos)
}

function logoutUser() {
    // Limpia el sessionStorage
    sessionStorage.clear();

    // Realiza una solicitud al servidor para cerrar la sesión
    console.log("logoutUser" + inactivityTimeout);

    // Redirecciona a la página de inicio de sesión
    window.location.href = '/Account/Login'; // Reemplaza con la URL correcta
}

// Evento para restablecer el temporizador en respuesta a la actividad del usuario
document.addEventListener('mousemove', resetInactivityTimer);
document.addEventListener('keydown', resetInactivityTimer);

// Inicia el temporizador al cargar la página
resetInactivityTimer();

function verifyToken() {
    let data = {
        Password: password,
        UID: uID,
        token: token
    };
    // Realiza una solicitud al servidor para verificar el token
    const token = sessionStorage.getItem('accessToken');
    $.ajax({

        type: "GET",
        url: '@ViewBag.urlEndPoint' + '/api/Users/v1/GenerateJWToken',
        data: JSON.stringify(data),
        contentType: "application/json",
        dataType: "json",
        headers: {
            'userID': parseInt(sessionStorage.getItem("userID")),
            'Authorization': 'Bearer ' + token // Agrega el token actual
        },
        console.log(response);
               if (response.codeStatus == 0) {
                    //ir a la vista
                   sessionStorage.getItem("jwtToken", response.uToken);
                   sessionStorage.getItem("userFName", response.uName);
                   sessionStorage.getItem("userLName", response.uLastName);
                   sessionStorage.getItem("UserRole", response.uTID);
    })
        .then(response => {
            if (response.status === 200) {
                return response.json(); // Parsea la respuesta JSON
            } else {
                // Token inválido
                logoutUser();
                throw new Error('Token inválido');
            }
        })
        .then(data => {
            // Almacena los datos del usuario en sessionStorage si el token es válido
            sessionStorage.setItem("jwtToken", token);
            sessionStorage.setItem("userFName", data.uName);
            sessionStorage.setItem("userLName", data.uLastName);
            sessionStorage.setItem("UserRole", data.uTID);
        })
        .catch(error => {
            console.error('Error al verificar el token:', error);
        });
}

// Función para cerrar la sesión del usuario
function logoutUser() {
    // Realiza una solicitud al servidor para cerrar la sesión
    fetch('v1/Logout', {
        method: 'POST'
    })
        .then(response => {
            if (response.status === 200) {
                // Redirecciona a la página de inicio de sesión
                window.location.href = 'Login';
            }
        });
}

// Inicializa el temporizador para verificar el token cada 5 minutos
setInterval(verifyToken, 10000); // 300,000 milisegundos = 5 minutos
