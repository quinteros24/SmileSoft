
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
                1: 'Activo',
                0: 'Inactivo'
            };
            let DocumentType = {
                1: 'CC',
                2: 'CE'
                // 3: 'Tarjeta de Identidad (TI)'
            }
            $.each(data.itemJson, function (index, usuario) {
                let rol = rolesMapping[usuario.utID];
                //let Block = BlockMapping[usuario.uIsBlocked];
                //let Status = StatusMapping[usuario.uStatus];
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
                    usuario.uIsBlocked,
                    usuario.uStatus
                    //Block,
                    //Status
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
                        orderable: false, // Para desactivar la ordenaci�n en esta columna
                        data: null, // Usaremos la columna "Acciones" solo para botones
                        defaultContent: '<button class="btn btn-primary btn-sm" id="btnEditar" data-toggle="modal" data-target="#editModal" data-toggle="tooltip" title="Editar Usuario"><i class="fas fa-edit"></i></button>' +
                            '<button class="btn btn-success btn-sm deactivate-button" id="btnDesactivar" data-toggle="modal" data-target="#deactivateModal" data-toggle="tooltip" title="Desactivar Usuario" > <i class="fas fa-ban"> </i></button > ' +
                            '<button class="btn btn-danger btn-sm password-button" id="btnPassword" data-toggle="modal" data-target="#passwordModal" data-toggle="tooltip" title="Cambiar Contrase�a" > <i class="fa-solid fa-lock"></i></button > '

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
                // Maneja la acci�n del bot�n aqu�
                let data = table.row($(this).parents('tr')).data();

                let userId = data[0]; // la primera columna contiene el ID del usuario
                let rolesMappingI = {
                    Administrador: 1,
                    Doctores: 2,
                    Pacientes: 3
                };
                let BlockMappingI = {
                    Desbloqueado: 0,
                    Bloqueado: 1
                };
                let StatusMappingI = {
                    Activo: 1,
                    Inactivo: 0
                };
                let DocumentTypeI = {
                    CC: 1,
                    CE: 2
                    // 3: 'Tarjeta de Identidad (TI)'
                }
                let rolI = rolesMappingI[data[1]];
                let BlockI = BlockMappingI[data[9]];
                let StatusI = StatusMappingI[data[10]];
                let DocumentTI = DocumentTypeI[data[7]];
                let userData = {
                    uID: userId,
                    utID: rolI,
                    uLoginName: data[2],
                    uName: data[3],
                    uLastName: data[4],
                    uEmailAddress: data[5],
                    uCellphone: data[6],
                    dtID: DocumentTI,
                    uDocument: data[8],
                    uIsBlocked: BlockI,
                    uStatus: StatusI
                };

                console.log("Usuario a Editar" + userData);
                //$("#editModal #userName").val(userData.uLoginName);
                // Llena otros campos del modal con datos del usuario
                // Llena los campos del formulario de edici�n con los datos del usuario
                $("#username").val(userData.uLoginName);
                $("#tipoUsuarioe").val(userData.utID);
                $("#editNombre").val(userData.uName);
                $("#editApellido").val(userData.uLastName);
                $("#editCelular").val(userData.uCellphone);
                $("#editCorreo").val(userData.uEmailAddress);
                $("#editDireccion").val(userData.uAddress);
                $("#tipoDocumentoe").val(userData.dtID);
                $("#editDocumento").val(userData.uDocument);
                //$("#editIsBlocked").val(userData.uIsBlocked);
                $("#editStatus").val(userData.uStatus);

                // Abre el modal de edici�n
                $("#editModal").modal("show");


                console.log("Usuario a Editar" + userId);
                // Realiza una solicitud AJAX para obtener los detalles del usuario

                $("#saveChangesBtn").click(function () {
                    let status = $("#editStatus").val() == "Activo" ? 1 : 0;
                    let utID2 = $("#tipoUsuarioe").val();
                    console.log("Dato enviado " + utID2);
                    console.log("Tipo Documento " + parseInt($("#tipoDocumentoe").val()));
                    // Obt�n los datos actualizados del formulario de edici�n
                    let updatedUserData = {
                        // Recoge los datos actualizados desde los campos del formulario
                        uID: userId,
                        utID: parseInt($("#tipoUsuarioe").val()), // Supongamos que el campo de edici�n del rol tiene el id "editRol"
                        uLoginName: $("#username").val(),
                        uName: $("#editNombre").val(),
                        uLastName: $("#editApellido").val(),
                        uEmailAddress: $("#editCorreo").val(),
                        uCellphone: $("#editCelular").val(),
                        dtID: parseInt($("#tipoDocumentoe").val()),
                        uDocument: $("#editDocumento").val(),
                        //uIsBlocked: $("#editIsBlocked").val(),
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
                            // Cierra el modal de edici�n
                            $("#editModal").modal("hide");

                            // Puedes realizar otras acciones despu�s de guardar los cambios si es necesario
                            // Por ejemplo, actualizar la tabla con los datos modificados
                            // o mostrar un mensaje de �xito
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
            table.column(11).nodes().to$().find('#btnDesactivar').click(function () {
                // Maneja la acci�n de desactivaci�n aqu�
                let data = table.row($(this).parents('tr')).data();
                let userId = data[0];
                let loginName = data[2];
                let userName = data[3] + ' ' + data[4];

                console.log("Documento" + loginName + " User ID: " + userId);
                //var userId = data[0]; // Obt�n el ID del usuario desde el bot�n
                let dataUser = {
                    uID: userId,
                    uStatus: 0
                }; //Ajustar para enviar si esta inactivo activar si esta activo desactivar
                // Muestra un mensaje de confirmaci�n antes de desactivar el usuario

                Swal.fire({
                    title: '�Est�s seguro de Desactivar? a:',
                    text: '� ' + userName + ' !',
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'S�, desactivar',
                    cancelButtonText: 'Cancelar'
                }).then((result) => {
                    if (result.isConfirmed) {
                        //console.log("Desactivar 2" + userId);
                        // Realiza la solicitud AJAX para desactivar el usuario
                        $.ajax({
                            url: sessionStorage.urlEP + '/api/Users/v1/CreateUpdateUsers/',
                            type: 'POST',
                            data: JSON.stringify(dataUser),
                            contentType: "application/json",
                            //async: false,
                            dataType: "json",
                            success: function (response) {
                                console.log("Respuesta del Servidor " + response);

                                Swal.fire({
                                    title: 'Usuario Desactivado',
                                    text: 'El usuario' + data[0] + 'se desactiv� correctamente',
                                    icon: 'success',
                                    confirmButtonText: 'Aceptar'
                                });


                            },
                            error: function () {
                                Swal.fire({
                                    title: 'Error',
                                    text: 'Error al deshabilitar el usuario',
                                    icon: 'error',
                                    confirmButtonText: 'Aceptar'
                                });
                            }
                        });
                    }
                });

            });
            table.column(11).nodes().to$().find('#btnPassword').click(function () {
                // Maneja la acci�n del bot�n aqu�
                let data = table.row($(this).parents('tr')).data();

                let userId = data[0]; // Supongamos que la primera columna contiene el ID del usuario
                console.log("Contrase�a " + userId)
                let userNameEdit = data[3] + ' ' + data[4];

                //$("#usernamepass").val(userNameEdit);
                var userNamePassSpan = document.getElementById("userNamePass");

                // Establece el contenido del span
                userNamePassSpan.innerHTML = userNameEdit;
                // Abre el modal de edici�n
                $("#editPassword").modal("show");


                console.log("Usuario a Editar" + userId);
                // Realiza una solicitud AJAX para obtener los detalles del usuario

                $("#changePasswordBtn").click(function () {

                    let password = $("#newPassword").val();
                    if (!CheckPass(password)) {
                        Swal.fire({
                            text: 'La contrase�a no cumple con los requisitos.',
                            confirmButtonColor: '#008dc9'
                        });
                    } else {
                        // Obt�n los datos actualizados del formulario de edici�n
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
                                // Cierra el modal de edici�n
                                if (response.codeStatus == 0) {
                                    $("#editPassword").modal("hide");
                                    //window.location.href = '@Url.Action("Index", "")';
                                    Swal.fire({
                                        text: 'Contrase�a actualizada con exito',
                                        confirmButtonColor: '#008dc9'
                                    });
                                } else {

                                    Swal.fire({
                                        text: "Hubo un fallo" + response.messageStatus,
                                        confirmButtonColor: '#008dc9'
                                    });
                                    //alert('Inicio de sesi�n fallido, ' + response.messageStatus);
                                }


                                // Puedes realizar otras acciones despu�s de guardar los cambios si es necesario
                                // Por ejemplo, actualizar la tabla con los datos modificados
                                // o mostrar un mensaje de �xito
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
    // Requiere al menos una letra min�scula
    if (!/[a-z]/.test(password)) {
        return false;
    }
    // Requiere al menos una letra may�scula
    if (!/[A-Z]/.test(password)) {
        return false;
    }
    // Requiere al menos un d�gito
    if (!/\d/.test(password)) {
        return false;
    }
    // agregar requisitos adicionales aqu�, como caracteres especiales
    // if (!/[!#$%^&*()_+{ }\[\]:;<>,.?~\\-]/.test(password)) {
    //     return false;
    // }

    return true;
}

$(".add-user-btn").click(function () {
    // Abre el modal de agregar usuario
    $("#addUserModal").modal("show");
});

// Funci�n para permitir solo n�meros en un campo de entrada
function allowNumbersOnly(inputField) {
    inputField.value = inputField.value.replace(/[^0-9]/g, '');
}

// Aplica la funci�n a los campos de celular y n�mero de documento
$("#celular").on("input", function () {
    allowNumbersOnly(this);
});

$("#numeroDocumento").on("input", function () {
    allowNumbersOnly(this);
});

// Controlador de clic para el bot�n "Agregar"
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



    // Validaci�n de la contrase�a
    let password = userData.uPassword;
    let passwordError = CheckPass(password);

    if (passwordError) {
        mostrarMensajeError(passwordError);
        return;
    }
    function CheckPass(password) {
        if (password.length < 8) {
            return "La contrase�a debe tener al menos 8 caracteres.";
        }

        if (!/[A-Z]/.test(password)) {
            return "La contrase�a debe contener al menos una letra may�scula.";
        }

        if (!/[a-z]/.test(password)) {
            return "La contrase�a debe contener al menos una letra min�scula.";
        }

        if (!/\d/.test(password)) {
            return "La contrase�a debe contener al menos un n�mero.";
        }

        return null; // Si la contrase�a cumple con los requisitos, retorna null
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
            // Actualiza la vista o realiza alguna otra acci�n
            $("#addUserModal").modal("hide");
            //alert("Usuario agregado correctamente");
            // Cierra el modal de agregar usuario
            Swal.fire({
                title: 'Usuario Agregado',
                text: 'El usuario se agreg� correctamente',
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
//             // Aqu� puedes realizar acciones adicionales despu�s de que se cierre la ventana de carga, si es necesario.
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
