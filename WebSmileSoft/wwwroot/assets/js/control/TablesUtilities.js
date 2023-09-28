
//<!-- Script para Visualizar Lista De Usuarios en la Tabla -->
//Recibir el parametro de la funcion verusers(1) para filtrar por tipo de usuario
$(document).ready(function () {
    verusers(3);
    $("#loadingScreen").show();
});
//Funcion para visualizar la lista de usuarios en la tabla pasando como parametro el tipo de usuario
function verusers(utID) {
    // Realizar solicitud AJAX para obtener la lista de usuarios
    // var utID = 1;
    mostrarCargando();

    $.ajax({
        url: sessionStorage.urlEP + '/api/Users/v1/ViewUsers',
        type: 'GET',
        data: { 'utID': utID },
        contentType: "application/json",
        dataType: 'json',
        success: function (data) {
            $("#loadingScreen").hide();
            //console.log("Respuesta del Servidor " + data);
            // Procesar los datos recibidos y agregar filas a la tabla
            let dataSet = [];
            let users = data.itemJson;
            console.table(users);
            let rolesMapping = {
                1: 'Administrador',
                2: 'Doctores',
                3: 'Pacientes'
            };
            let BlockMapping = {
                0: 'Desbloqueado',
                1: 'Bloqueado'
            };
            let StatusMapping = {
                true: 'Activo',
                false: 'Inactivo'
            };
            let DocumentType = {
                1: 'CC',
                2: 'CE'
                // 3: 'Tarjeta de Identidad (TI)'
            }
            $.each(data.itemJson, function (index, usuario) {
                let rol = rolesMapping[usuario.utID];
                let Block = BlockMapping[usuario.uIsBlocked];
                let Status = StatusMapping[usuario.uStatus];
                let DocumentT = DocumentType[usuario.dtID];
                let rowData = [
                    usuario.uID,
                    rol,
                    usuario.uLoginName,
                    usuario.uName,
                    usuario.uLastName,
                    usuario.uEmailAddress,
                    usuario.uCellphone,
                    DocumentT,
                    usuario.uDocument,
                    //usuario.uIsBlocked,
                    //usuario.uStatus
                    Block,
                    Status
                ];
                dataSet.push(rowData);
            });

            // Inicializa o actualiza el DataTable con los datos procesados
            table = $('#example').DataTable({
                // new DataTable('#example', {
                columns: [
                    { title: 'ID' },
                    { title: 'Rol' },
                    { title: 'Usuario' },
                    { title: 'Nombre' },
                    { title: 'Apellidos' },
                    { title: 'Correo Electronico' },
                    { title: 'Celular' },
                    { title: 'Tipo Documento' },
                    { title: 'Documento' },
                    { title: 'Bloqueado' },
                    { title: 'Estado' },
                    {
                        title: 'Acciones',
                        orderable: false, // Para desactivar la ordenación en esta columna
                        data: null, // Usaremos la columna "Acciones" solo para botones
                        defaultContent: '<button class="btn btn-primary btn-sm" id="btnEditar" data-toggle="modal" data-target="#editModal" data-toggle="tooltip" title="Editar Usuario"><i class="fas fa-edit"></i></button>' +
                            '<button class="btn btn-success btn-sm deactivate-button" id="btnDesactivar" data-toggle="modal" data-target="#deactivateModal" data-toggle="tooltip" title="Desactivar Usuario" > <i class="fas fa-ban"> </i></button > ' +
                            '<button class="btn btn-danger btn-sm password-button" id="btnPassword" data-toggle="modal" data-target="#passwordModal" data-toggle="tooltip" title="Cambiar Contrasena" > <i class="fa-solid fa-lock"></i></button > '

                    }
                ],
                data: dataSet,
                dom: 'Bfrtip',
                "dom": "<'dt--top-section'<'row'<'col-sm-12 col-md-6 d-flex justify-content-md-start justify-content-center'B><'col-sm-12 col-md-6 d-flex justify-content-md-end justify-content-center mt-md-0 mt-3'f>>>" +
                    "<'table-responsive'tr>" +
                    "<'dt--bottom-section d-sm-flex justify-content-sm-between text-center'<'dt--pages-count  mb-sm-0 mb-3'i><'dt--pagination'p>>",
                buttons: {
                    buttons: [
                        { extend: 'copy', className: 'btn btn-sm' },
                        {
                            extend: 'print', className: 'btn btn-sm',
                            messageTop: 'Tabla de Usuarios SmileSoft',
                        },
                        {
                            extend: 'spacer',
                            style: 'bar',
                            text: 'Exportar a:'
                        },
                        { extend: 'csv', className: 'btn btn-sm' },
                        { extend: 'excel', className: 'btn btn-sm' },
                    ]
                },
                select: true,
                destroy: true,
                language: {
                    url: '//cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json',
                },

            });

            Swal.close();
            table.column(11).nodes().to$().find('#btnEditar').click(function () {
                // Maneja la acción del botón aqui
                let data = table.row($(this).parents('tr')).data();

                let userId = data[0]; // la primera columna contiene el ID del usuario

                // Realiza una solicitud AJAX para obtener los detalles del usuario

                $.ajax({
                    url: sessionStorage.urlEP + '/api/Users/v1/GetUserDetails',
                    type: 'GET',
                    data: { 'uID': userId },
                    contentType: "application/json",
                    dataType: 'json',
                    success: function (data) {
                        console.log("Cargando Respuesta")
                        console.log(data);

                        if (data.itemJson === null) {
                            console.log("No se cargo correctamente");
                        } else {
                            // Mostrar los datos en la consola o realizar otras acciones
                            let userData = data.itemJson[0]; // Accede al primer elemento del arreglo

                            // Llama a una funcion para actualizar el formulario con los datos recibidos
                            console.log("Actualizando Formulario")
                            actualizarFormulario(userData);
                        }
                    },
                    error: function (error) {
                        console.log('Error al obtener los detalles del usuario.');
                    }
                });

                // Funcion para actualizar el formulario con los datos del usuario
                function actualizarFormulario(userData) {
                    let StatusMapping = {
                        0: 'Activo',
                        1: 'Inactivo'
                    };

                    // Rellena los campos del formulario con los datos del usuario
                    //Datos Personales
                    $("#editNombre").val(userData.uName);
                    $("#editApellido").val(userData.uLastName);
                    $("#tipoDocumentoe").val(userData.dtID);
                    $("#editDocumentoe").val(userData.uDocument);
                    $("#tipoGeneroe").val(userData.gID)
                    $("#fechaNacimientoe").val(userData.uBirthDate) 
                    //Datos de Contacto
                    $("#editCorreo").val(userData.uEmailAddress);
                    $("#direccione").val(userData.uAddress);
                    $("#editCelular").val(userData.uCellphone);
                    //Datos de Cuenta
                    $("#tipoUsuarioe").val(userData.utID);
                    $("#username").val(userData.uLoginName);

                    //Datos de Estudios
                    $("#TituloAcademico").val(userData.dDegree);
                    $("#AcademicLevel").val(userData.dAcademicLevel);
                    $("#Speciality").val(userData.dSpeciality);
                    $("#ProfessionalLicense").val(userData.dProfessionalCard);
                    $("#Universityname").val(userData.dUniversityName);

                }

                // Abre el modal de edición
                $("#editModal").modal("show");


                console.log("Usuario a Editar" + userId);
                // Realiza una solicitud AJAX para obtener los detalles del usuario

                $("#saveChangesBtn").click(function () {
                    let status = $("#editStatus").val() == "Activo" ? true : false;

                    // Obtén los datos actualizados del formulario de edición
                    let updatedUserData = {
                        // Recoge los datos actualizados desde los campos del formulario

                        //Datos Personales
                        uName: $("#editNombre").val(),
                        uLastName: $("#editApellido").val(),
                        dtID: parseInt($("#tipoDocumentoe").val()),
                        uDocument: $("#editDocumento").val(),
                        gID: parseInt($("#genderSelect").val()),
                        uBirthDate: $("#birthDate").val(),

                        //Datos de Contacto
                        uEmailAddress: $("#editCorreo").val(),
                        uCellphone: $("#editCelular").val(),
                        uAddress: $("#address").val(),

                        //Datos de Estudios
                        dAcademicLevel: $("#AcademicLevel").val(),
                        dDegree: $("#TituloAcademico").val(),
                        dSpeciality: $("#Speciality").val(),
                        dProfessionalCard: $("#ProfessionalLicense").val(),
                        dUniversityName: $("#Universityname").val(),
                        
                        //Datos de Cuenta
                        uLoginName: $("#username").val(),
                        uID: userId,
                        utID: $("#tipoUsuarioe").val(), 
                        uStatus: status
                    };
                    console.log("Usuario a Editar" + userId)
                    // Realiza una solicitud AJAX para guardar los cambios en el servidor
                    $.ajax({
                        url: sessionStorage.urlEP + '/api/Users/v1/CreateUpdateUsers', // Reemplaza con la URL correcta
                        type: 'POST',
                        data: JSON.stringify(updatedUserData), // Convierte updatedUserData a JSON
                        contentType: "application/json", // Especifica el tipo de contenido
                        dataType: "json",
                        success: function (response) {
                            // Cierra el modal de edición
                            $("#editModal").modal("hide");
                            verusers(updatedUserData.utID);
                            // Puedes realizar otras acciones después de guardar los cambios si es necesario
                            // Por ejemplo, actualizar la tabla con los datos modificados
                            // o mostrar un mensaje de éxito
                        },
                        error: function () {
                            $("#editModal").modal("hide");
                            Swal.fire({
                                title: 'Error',
                                text: 'Error al guardar los cambios',
                                icon: 'error',
                                confirmButtonText: 'Aceptar'
                            });
                        }
                    });
                });
            });

            //Desactivar Estado --> Funcionando Bien
            table.column(11).nodes().to$().find('#btnDesactivar').click(function () {
                let StatusMappingI = {
                    Activo: 1,
                    Inactivo: 0
                };


                let rolesMappingI = {
                    Administrador: 1,
                    Doctores: 2,
                    Pacientes: 3
                };
                let data = table.row($(this).parents('tr')).data();
                let userId = data[0];
                let utID = data[1];
                let loginName = data[2];
                let userName = data[3] + ' ' + data[4];
                let StatusI = StatusMappingI[data[10]]; // Supongo que obtienes el estado del usuario de alguna parte
                let estadoActual = data[10];
                console.log("Documento" + loginName + " User ID: " + userId);
                console.log("Estado" + estadoActual);
                console.log("Estado Mapeado " + StatusI)
                let action = StatusI === 1 ? 'desactivar' : 'activar';
                let uRol = rolesMappingI[utID];
                // Muestra un mensaje de confirmación antes de realizar la acción
                Swal.fire({
                    title: 'Estas seguro de ' + action + ' a:',
                    text: ' ' + userName + ' !',
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Si, ' + action,
                    cancelButtonText: 'Cancelar'
                }).then((result) => {
                    if (result.isConfirmed) {
                        let newStatus = StatusI === 1 ? 0 : 1;

                        // Realiza la solicitud AJAX para cambiar el estado del usuario
                        $.ajax({
                            url: sessionStorage.urlEP + '/api/Users/v1/SetUserStatus/',
                            type: 'GET',
                            data: { 'uID': userId, 'uStatus': newStatus },
                            contentType: "application/json",
                            dataType: 'json',
                            success: function (response) {
                                console.log("Respuesta del Servidor " + response);
                                console.log("Rol del Usuario desactivado " + uRol);
                                Swal.fire({
                                    title: 'Usuario ' + action.charAt(0).toUpperCase() + action.slice(1), // Capitalizar la acción
                                    text: 'El usuario ' + loginName + ' se ' + action + 'ó correctamente',
                                    icon: 'success',
                                    confirmButtonText: 'Aceptar'
                                });
                                verusers(uRol);
                            },
                            error: function () {
                                Swal.fire({
                                    title: 'Error',
                                    text: 'Error al ' + action + ' el usuario',
                                    icon: 'error',
                                    confirmButtonText: 'Aceptar'
                                });
                            }
                        });
                    }
                });



                //// Maneja la acción de desactivación aqui
                //let data = table.row($(this).parents('tr')).data();
                //let userId = data[0];
                //let loginName = data[2];
                //let userName = data[3] + ' ' + data[4];

                //console.log("Documento" + loginName + " User ID: " + userId);
                ////var userId = data[0]; // Obtén el ID del usuario desde el botón
                //let dataUser = {
                //    uID: userId,
                //    uStatus: 0
                //}; //Ajustar para enviar si esta inactivo activar si esta activo desactivar
                //// Muestra un mensaje de confirmación antes de desactivar el usuario

                //Swal.fire({
                //    title: 'Estas seguro de Desactivar? a:',
                //    text: ' ' + userName + ' !',
                //    icon: 'warning',
                //    showCancelButton: true,
                //    confirmButtonColor: '#3085d6',
                //    cancelButtonColor: '#d33',
                //    confirmButtonText: 'Si, desactivar',
                //    cancelButtonText: 'Cancelar'
                //}).then((result) => {
                //    if (result.isConfirmed) {
                //        //console.log("Desactivar 2" + userId);
                //        // Realiza la solicitud AJAX para desactivar el usuario
                //        $.ajax({
                //            url: sessionStorage.urlEP + '/api/Users/v1/SetUserStatus/',
                //            type: 'POST',
                //            data: JSON.stringify(dataUser),
                //            contentType: "application/json",
                //            //async: false,
                //            dataType: "json",
                //            success: function (response) {
                //                console.log("Respuesta del Servidor " + response);

                //                Swal.fire({
                //                    title: 'Usuario Desactivado',
                //                    text: 'El usuario' + data[0] + 'se desactivó correctamente',
                //                    icon: 'success',
                //                    confirmButtonText: 'Aceptar'
                //                });


                //            },
                //            error: function () {
                //                Swal.fire({
                //                    title: 'Error',
                //                    text: 'Error al deshabilitar el usuario',
                //                    icon: 'error',
                //                    confirmButtonText: 'Aceptar'
                //                });
                //            }
                //        });
                //    }
                //});

            });
            //Cambiar Contrasena Estado --> Funcionando Bien
            table.column(11).nodes().to$().find('#btnPassword').click(function () {
                // Maneja la acción del botón aqui
                let data = table.row($(this).parents('tr')).data();

                let userId = data[0]; // Supongamos que la primera columna contiene el ID del usuario
                console.log("Contrasena " + userId)
                let userNameEdit = data[3] + ' ' + data[4];
                let userRol = data[1];
                let rolesMappingI = {
                    Administrador: 1,
                    Doctores: 2,
                    Pacientes: 3
                };
                let uRol = rolesMappingI[userRol];
                //$("#usernamepass").val(userNameEdit);
                var userNamePassSpan = document.getElementById("userNamePass");

                // Establece el contenido del span
                userNamePassSpan.innerHTML = userNameEdit;
                // Abre el modal de edición
                $("#editPassword").modal("show");


                console.log("Usuario a Editar" + userId);
                // Realiza una solicitud AJAX para obtener los detalles del usuario

                $("#changePasswordBtn").click(function () {

                    let password = $("#newPassword").val();
                    if (!CheckPass(password)) {
                        Swal.fire({
                            text: 'La contrasena no cumple con los requisitos.',
                            confirmButtonColor: '#008dc9'
                        });
                    } else {
                        // Obtén los datos actualizados del formulario de edición
                        let updatedUserData = {
                            // Recoge los datos actualizados desde los campos del formulario
                            uID: userId,
                            Password: password
                        };
                        // Realiza una solicitud AJAX para guardar los cambios en el servidor
                        $.ajax({
                            url: sessionStorage.urlEP + '/api/Users/v1/ChangePassword', // Reemplaza con la URL correcta
                            type: 'POST',
                            data: JSON.stringify(updatedUserData), // Convierte updatedUserData a JSON
                            contentType: "application/json", // Especifica el tipo de contenido
                            dataType: "json",
                            success: function (response) {
                                // Cierra el modal de edición
                                if (response.codeStatus == 0) {
                                    $("#editPassword").modal("hide");
                                    //window.location.href = '@Url.Action("Index", "")';
                                    Swal.fire({
                                        text: 'Contrasena actualizada con exito',
                                        confirmButtonColor: '#008dc9'
                                    });
                                    verusers(uRol);
                                } else {

                                    Swal.fire({
                                        text: "Hubo un fallo" + response.messageStatus,
                                        confirmButtonColor: '#008dc9'
                                    });
                                    //alert('Inicio de sesión fallido, ' + response.messageStatus);
                                }


                                // Puedes realizar otras acciones después de guardar los cambios si es necesario
                                // Por ejemplo, actualizar la tabla con los datos modificados
                                // o mostrar un mensaje de éxito
                            },
                            error: function () {
                                // $("#editModal").modal("hide");
                                // Swal.fire({
                                //     title: 'Error',
                                //     text: 'Error al guardar los cambios',
                                //     icon: 'error',
                                //     confirmButtonText: 'Aceptar'
                                // });
                            }
                        });
                    }
                });
            });

        }
    });
};
function CheckPass(password) {
    // Requiere al menos 8 caracteres
    if (password.length < 8) {
        return false;
    }
    // Requiere al menos una letra minuscula
    if (!/[a-z]/.test(password)) {
        return false;
    }
    // Requiere al menos una letra mayuscula
    if (!/[A-Z]/.test(password)) {
        return false;
    }
    // Requiere al menos un digito
    if (!/\d/.test(password)) {
        return false;
    }
    // agregar requisitos adicionales aqui, como caracteres especiales
    // if (!/[!#$%^&*()_+{ }\[\]:;<>,.?~\\-]/.test(password)) {
    //     return false;
    // }

    return true;
}

$(".add-user-btn").click(function () {
    // Abre el modal de agregar usuario
    $("#addUserModal").modal("show");
});

// Función para permitir solo numeros en un campo de entrada
function allowNumbersOnly(inputField) {
    inputField.value = inputField.value.replace(/[^0-9]/g, '');
}

// Aplica la función a los campos de celular y numero de documento
$("#celular").on("input", function () {
    allowNumbersOnly(this);
});

$("#numeroDocumento").on("input", function () {
    allowNumbersOnly(this);
});

// Controlador de clic para el botón "Agregar"
$("#add-user").click(function () {
    // Obtiene los datos del formulario

    var userData = {
        utID: parseInt($("#tipoUsuario").val()),
        uName: $("#nombres").val(),
        uLastName: $("#apellidos").val(),
        uCellphone: $("#celular").val(),
        uEmailAddress: $("#correo").val(),
        uAddress: $("#direccion").val(),
        uLoginName: $("#usuario").val(),
        uPassword: $("#password").val(),
        dtID: parseInt($("#tipoDocumento").val()),
        uDocument: $("#numeroDocumento").val()
        //genero: $("input[name='genero']:checked").val(), // Obtiene el valor del radio button seleccionado
        //fechaNacimiento: $("#fechaNacimiento").val(),

    };
    console.log("Datos del Usuario " + userData);
    //Validar campos obligatorios

    if (!userData.nombres || !userData.apellidos || !userData.tipoDocumento || !userData.numeroDocumento || !userData.usuario || !userData.password || !userData.tipoUsuario) {
        mostrarMensajeError('Por favor, complete todos los campos obligatorios.');
        return;
    }



    // Validación de la contrasena
    let password = userData.uPassword;
    let passwordError = CheckPass(password);

    if (passwordError) {
        mostrarMensajeError(passwordError);
        return;
    }
    function CheckPass(password) {
        if (password.length < 8) {
            return "La contrasena debe tener al menos 8 caracteres.";
        }

        if (!/[A-Z]/.test(password)) {
            return "La contrasena debe contener al menos una letra mayuscula.";
        }

        if (!/[a-z]/.test(password)) {
            return "La contrasena debe contener al menos una letra minuscula.";
        }

        if (!/\d/.test(password)) {
            return "La contrasena debe contener al menos un numero.";
        }

        return null; // Si la contrasena cumple con los requisitos, retorna null
    }

    function mostrarMensajeError(mensaje) {
        Swal.fire({
            title: 'Error',
            text: mensaje,
            icon: 'error',
            confirmButtonText: 'Aceptar'
        });
    }

    // Realiza una solicitud AJAX para agregar el usuario en el servidor
    $.ajax({
        type: "POST",
        url: sessionStorage.urlEP + '/api/Users/v1/CreateUpdateUsers', // Ruta para agregar el usuario
        data: JSON.stringify(userData), // Convierte userData a JSON
        contentType: "application/json", // Especifica el tipo de contenido

        dataType: "json",
        success: function (result) {
            // Actualiza la vista o realiza alguna otra acción
            $("#addUserModal").modal("hide");
            //alert("Usuario agregado correctamente");
            // Cierra el modal de agregar usuario
            Swal.fire({
                title: 'Usuario Agregado',
                text: 'El usuario se agregó correctamente',
                icon: 'success',
                confirmButtonText: 'Aceptar'
            });
        },
        error: function () {
            //alert("Error al agregar el usuario");
            Swal.fire({
                title: 'Error',
                text: 'Error al agregar el usuario',
                icon: 'error',
                confirmButtonText: 'Aceptar'
            });
        }
    });
});

// function mostrarCarga() {
//     // Ocultar el contenido suavemente
//     $('#content-container').addClass('hide-content');

//     Swal.fire({
//         title: 'Cargando',
//         html: 'Por favor, espere un momento',
//         timer: 1000,
//         timerProgressBar: true,
//         didOpen: () => {
//             Swal.showLoading();
//         },
//         willClose: () => {
//             // Aqui puedes realizar acciones adicionales después de que se cierre la ventana de carga, si es necesario.
//             // Por ejemplo, mostrar el contenido nuevamente.
//             $('#content-container').removeClass('hide-content');
//         }
//     });
// }
function mostrarCargando() {
    Swal.fire({
        title: 'Cargando',
        text: 'Por favor, espere un momento...',
        showCancelButton: false,
        showConfirmButton: false,
        allowOutsideClick: false,
        onBeforeOpen: () => {
            Swal.showLoading();
        }
    });
}


$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();

});
