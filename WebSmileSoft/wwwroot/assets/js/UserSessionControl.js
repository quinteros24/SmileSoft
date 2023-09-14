var inactivityTimeout;

// Funci�n para restablecer el tiempo de inactividad
function resetInactivityTimer() {
    clearTimeout(inactivityTimeout);
    inactivityTimeout = setTimeout(logoutUser, 10000); // 10 segundos (10000 milisegundos)
}

function logoutUser() {
    // Realiza una solicitud al servidor para cerrar la sesi�n
    console.log("logoutUser" + inactivityTimeout);
    // Redirecciona a la p�gina de inicio de sesi�n
    window.location.href = '/Account/Login'; // Reemplaza con la URL correcta
}

// Evento para restablecer el temporizador en respuesta a la actividad del usuario
document.addEventListener('mousemove', resetInactivityTimer);
document.addEventListener('keydown', resetInactivityTimer);

// Inicia el temporizador al cargar la p�gina
resetInactivityTimer();

