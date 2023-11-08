
function validarEmail(email) {
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return regex.test(email);
}
////
//function ageValidation(birthDate) {
//    // Obtener la fecha actual
//    const fechaActual = new Date();

//    // Calcular la fecha de nacimiento a partir de la cadena de fecha
//    const fechaNac = new Date(birthDate);

//    // Calcular la diferencia en años
//    const { getFullYear, getMonth, getDate } = fechaActual;
//    const edad = getFullYear() - fechaNac.getFullYear() - (getMonth() < fechaNac.getMonth() || (getMonth() === fechaNac.getMonth() && getDate() < fechaNac.getDate()));

//    // Comprobar si la edad es mayor o igual a 18
//    return edad >= 18;
//}
//function generateUsername() {
//    const firstName = document.getElementById("userFirstNameRegister").value.trim();
//    const lastName = document.getElementById("userLastNameRegister").value.trim();

//    // Combine the first name and last name to create the base username
//    const baseUsername = `${firstName}.${lastName}`.toLowerCase().replace(/\s/g, "");

//    // Generate a random number between 1 and 999 (adjust the range as needed)
//    const randomSuffix = Math.floor(Math.random() * 999) + 1;

//    // Combine the base username with the random number
//    const suggestedUsername = `${baseUsername}${randomSuffix}`;

//    // Update the username field with the suggested username
//    document.getElementById("userNameRegister").value = suggestedUsername;
//}
//// Obtén una referencia al campo de fecha de nacimiento
//const fechaNacimientoInput = document.getElementById('birthDate');
//onChangeAgeNotification(fechaNacimientoInput);

//function registrarUsuario() {
//    let firstName = document.getElementById("userFirstNameRegister").value.trim();
//    let lastName = document.getElementById("userLastNameRegister").value.trim();
//    let documentID = document.getElementById("IdNumber").value.trim();
//    let address = document.getElementById("address").value.trim();
//    let mobileNumber = document.getElementById("mobileNumber").value.trim();
//    let email = document.getElementById("email").value.trim();
//    let userRegister = document.getElementById("userNameRegister").value.trim();
//    let password = document.getElementById("password").value.trim();
//    let confirmPassword = document.getElementById("confirmPassword").value.trim();
//    let birthDate = document.getElementById("birthDate").value.trim();
//    let documentType = parseInt(document.getElementById("tipo_documento").value.trim(), 10);

//    let genderSelect = document.getElementById("genderSelect");
//    let gender = genderSelect.options[genderSelect.selectedIndex].value;



//    const esMayorDe18 = validarEdadMayorDe18('birthDate');

//    if (firstName === "" || lastName === "") {
//        Swal.fire({
//            text: 'Por favor, ingrese su nombre completo.',
//            confirmButtonColor: '#008dc9'
//        });
//    }
//    else if (documentID === "") {
//        Swal.fire({
//            text: 'Por favor, ingrese su número de documento.',
//            confirmButtonColor: '#008dc9'
//        });
//    }
//    else if (birthDate === "") {
//        Swal.fire({
//            text: 'Por favor, ingrese su fecha de nacimiento.',
//            confirmButtonColor: '#008dc9'
//        });
//    }
//    else if (gender === "Seleccionar" || gender === "") {
//        Swal.fire({
//            text: 'Por favor, seleccione su género.',
//            confirmButtonColor: '#008dc9'
//        });
//    }
//    else if (address === "") {
//        Swal.fire({
//            text: 'Por favor, ingrese su dirección.',
//            confirmButtonColor: '#008dc9'
//        });
//    }
//    else if (mobileNumber === "") {
//        Swal.fire({
//            text: 'Por favor, ingrese su número de celular o telefono.',
//            confirmButtonColor: '#008dc9'
//        });
//    } else if (email === "") {
//        Swal.fire({
//            text: 'Por favor, ingrese su correo electrónico.',
//            confirmButtonColor: '#008dc9'
//        });
//    } else if (userRegister === "") {
//        Swal.fire({
//            text: 'Por favor, ingrese su usuario.',
//            confirmButtonColor: '#008dc9'
//        });
//    } else if (password === "") {
//        Swal.fire({
//            text: 'Por favor, ingrese su contraseña.',
//            confirmButtonColor: '#008dc9'
//        });
//    } else if (confirmPassword === "") {
//        Swal.fire({
//            text: 'Por favor, ingrese la confirmación de su contraseña.',
//            confirmButtonColor: '#008dc9'
//        });
//    } else if (!CheckPass(password)) {
//        Swal.fire({
//            text: 'La contraseña no cumple con los requisitos.',
//            confirmButtonColor: '#008dc9'
//        });
//    } else if (confirmPassword != password) {
//        Swal.fire({
//            text: 'Las contraseñas no coinciden.',
//            confirmButtonColor: '#008dc9'
//        });
//    }
//    else if (!esMayorDe18) {
//        Swal.fire({
//            text: 'No tienes la mayoría de edad, ponte en contacto con soporte.',
//            confirmButtonColor: '#008dc9'
//        });
//    }

//    //let formattedBirthDate = formatDateToISO8601(birthDate);
//    else {
//        //let formattedBirthDate = formatToDatetime(birthDate);
//        let data = {
//            uID: 0,
//            utID: 3,
//            uName: firstName,
//            ulastName: lastName,
//            uDocument: documentID,
//            uAddress: address,
//            uCellphone: mobileNumber,
//            uEmailAddress: email,
//            uLoginName: userRegister,
//            uPassword: password,
//            dtID: documentType,
//            uStatus: true,
//            //uIsBLocked: false,
//            gID: parseInt(gender),
//            oID: 1,
//            uBirthDate: birthDate

//        };
//        $.ajax({
//            type: 'POST',
//            url: '@ViewBag.urlEndPoint' + '/api/Users/v1/CreateUpdateUsers', // Ruta para crear o actualizar usuarios
//            data: JSON.stringify(data),
//            contentType: 'application/json',
//            success: function (response) {
//                if (response.codeStatus == 0) {
//                    Swal.fire({
//                        icon: 'success',
//                        title: response.messageStatus,
//                        confirmButtonColor: '#008dc9',
//                        allowOutsideClick: false,
//                        allowEscapeKey: false,
//                    }).then((result) => {
//                        if (result.isConfirmed) {
//                            window.location.href = '@Url.Action("Login", "Account")';
//                        }
//                    });
//                } else {
//                    Swal.fire({
//                        title: 'Error',
//                        text: response.messageStatus,
//                        icon: 'error',
//                        confirmButtonText: 'Aceptar'
//                    });

//                }
//            },

//            error: function (error) {
//                // Manejar errores de la solicitud AJAX
//                Swal.fire({
//                    title: 'Error',
//                    text: 'Ha ocurrido un error al intentar registrar el usuario.',
//                    icon: 'error',
//                    confirmButtonText: 'Aceptar'
//                });
//            }
//        });
//    }

//}