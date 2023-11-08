const logoutUrl = document.getElementById("logout-link").getAttribute("data-logout-url");

const resetInactivityTimer = () => {
    clearTimeout(inactivityTimeout);
    inactivityTimeout = setTimeout(logoutUser, 900000); // 15 minutos (900,000 milisegundos)
};

const logoutUser = () => {
    // Limpia el sessionStorage
    sessionStorage.clear();

    // Realiza una solicitud al servidor para cerrar la sesión
    //console.log(`logoutUser ${inactivityTimeout}`);

    // Redirecciona a la página de inicio de sesión
    window.location.href = logoutUrl; // Reemplaza con la URL correcta
};

// Evento para restablecer el temporizador en respuesta a la actividad del usuario
document.addEventListener('mousemove', resetInactivityTimer);
document.addEventListener('keydown', resetInactivityTimer);

// Inicia el temporizador al cargar la página
let inactivityTimeout = setTimeout(logoutUser, 9000); // 15 minutos (900,000 milisegundos)

const verifyToken = async () => {
    const data = {
        token: sessionStorage.getItem('accessToken'),
        userNameLogin: sessionStorage.getItem('userLogin')
    };
    // Realiza una solicitud al servidor para verificar el token
    //console.log("Validando el token");
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

