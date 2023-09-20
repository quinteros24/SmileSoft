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
//function verifyToken() {
//    // Realiza una solicitud al servidor para verificar el token
//    const token = sessionStorage.getItem('accessToken');
//    fetch('v1/GenerateJWToken', {
//        method: 'GET',
        
//        headers: {
//            'Authorization': 'Bearer ' + token // Agrega el token actual
//        }
//    })
//        .then(response => {
//            if (response.status === 200) {
//                return response.json(); // Parsea la respuesta JSON
//            } else {
//                // Token inválido
//                logoutUser();
//                throw new Error('Token inválido');
//            }
//        })
//        .then(data => {
//            // Almacena los datos del usuario en sessionStorage si el token es válido
//            sessionStorage.getItem("jwtToken", token);
//            sessionStorage.getItem("userFName", data.uName);
//            sessionStorage.getItem("userLName", data.uLastName);
//            sessionStorage.getItem("UserRole", data.uTID);
//        })
//        .catch(error => {
//            console.error('Error al verificar el token:', error);
//        });
//}

//// Función para cerrar la sesión del usuario
//function logoutUser() {
//    // Realiza una solicitud al servidor para cerrar la sesión
//    fetch('v1/Logout', {
//        method: 'POST'
//    })
//        .then(response => {
//            if (response.status === 200) {
//                // Redirecciona a la página de inicio de sesión
//                window.location.href = 'Login';
//            }
//        });
//}

//// Inicializa el temporizador para verificar el token cada 5 minutos
//setInterval(verifyToken, 10000); // 300,000 milisegundos = 5 minutos
