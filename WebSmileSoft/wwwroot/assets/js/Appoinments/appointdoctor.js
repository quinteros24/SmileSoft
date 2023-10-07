function abrirCita() {
    window.location.href = '@Url.Action("AdministrarCitas", "Doctor")';
} //SetAppointment
$.ajax({
    url: sessionStorage.urlEP + '/api/Appointments/v1/GetAppointmentsList',
    type: 'GET',
    contentType: "application/json",
    dataType: 'json',
    success: function (data) {
        $("#loadingScreen").hide();
        let dataSet = [];

        $.each(data.itemJson, function (index, cita) {
            let fechaCita = cita.aDate;
            let fechaFormateada = fechaCita.substring(0, 10);
            let rowData = [
                index + 1,
                cita.aID,
                fechaFormateada,
                cita.uName,
                cita.uCellphone,
                cita.uDoctorName,
                cita.aDescription,
                cita.aTime,
                cita.asName
            ];
            dataSet.push(rowData);
        });
        console.log(dataSet);
        // Inicializa o actualiza el DataTable con los datos procesados
        tablecitasd = $('#citasDoctor').DataTable({
            columnDefs: [
                {
                    target: 0,
                    visible: true,
                    searchable: true
                }
            ],
            columns: [
                { title: '#' },
                { title: 'IdCita', visible: false },
                { title: 'Fecha' },
                { title: 'Nombre' },
                { title: 'Celular' },
                { title: 'Doctor' },
                { title: 'Motivo' },
                { title: 'Hora' },
                { title: 'Estado' },
                {
                    title: 'Acciones',
                    orderable: false, // Para desactivar la ordenaci�n en esta columna
                    data: null, // Usaremos la columna "Acciones" solo para botones
                    defaultContent: '<button class="btn btn-primary btn-sm btnDesactivarC" data-toggle="tooltip" title="Cancelar Cita" type="button"> <i class="fas fa-ban"> </i></button> ' +
                        '<button class="btn btn-warning btn-sm btnEditarC" data-toggle="modal" data-toggle="tooltip" title="Cambiar Estado de la Cita"> <i class="fa-regular fa-pen-to-square"></i></button> '
                }
            ],
            data: dataSet,
            dom: 'Bfrtip',
            buttons: {
                buttons: [
                    {
                        extend: 'print', className: 'btn btn-sm',
                        messageTop: 'Tabla de Citas Programadas',
                    },
                ]
            },
            select: true,
            destroy: true,
            language: {
                url: '//cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json',
            },
            createdRow: function (row, data, dataIndex) {
                // Obt�n el valor del estado en la columna "Estado"
                var estado = data[8]; // Aseg�rate de que 8 sea el �ndice correcto para la columna "Estado"

                // Si el estado es "cancelado", oculta los botones en esta fila
                if (estado === 'Cancelado') {
                    $(row).find('.btnDesactivarC, .btnEditarC').hide();
                }
            }
        });

        Swal.close();

        tablecitasd.column(9).nodes().to$().find('.btnDesactivarC').click(function () {
            // Maneja la acci�n del bot�n aqui
            let data = tablecitasd.row($(this).parents('tr')).data();
            let CitaID = data[1]; // la primera columna contiene el ID del usuario
            let CitaEstado = data[8];
            if (CitaEstado == "Realizado") {
                Swal.fire({
                    icon: 'info',
                    title: 'Oops...',
                    text: 'La cita ya fue realizada, no se puede cancelar.',
                    confirmButtonColor: '#008dc9'
                })
            } else if (CitaEstado == "Cancelado") {
                Swal.fire({
                    icon: 'info',
                    title: 'Oops...',
                    text: 'La cita ya fue cancelada.',
                    confirmButtonColor: '#008dc9'
                })
            } else if (CitaEstado == "Confirmado") {
                Swal.fire({
                    icon: 'info',
                    title: 'Oops...',
                    text: 'La cita ya fue confirmada, no se puede cancelar.',
                    confirmButtonColor: '#008dc9'
                })
            } else if (CitaEstado == "Pendiente") {
                Swal.fire({
                    title: 'Cancelar Cita',
                    text: '�Est� seguro que desea cancelar la cita?',
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonText: 'Si, Cancelar Cita',
                    cancelButtonText: 'Atr�s'
                }).then((result) => {
                    if (result.isConfirmed) {
                        $.ajax({
                            url: sessionStorage.urlEP + '/api/Appointments/v1/UpdateAppointmentStatus',
                            type: 'GET',
                            data: { 'aID': CitaID, 'asID': 4 },
                            contentType: "application/json",
                            dataType: 'json',
                            success: function (response) {
                                if (response.codeStatus == 0) {
                                    Swal.fire({
                                        title: 'Cita Cancelada',
                                        text: 'La cita ha sido Cancelada exitosamente.',
                                        icon: 'success',
                                        confirmButtonText: 'Aceptar'
                                    }).then((result) => {
                                        if (result.isConfirmed) {
                                            location.reload();
                                        }
                                    })
                                }
                                else if (response.codeStatus == -1) {
                                    Swal.fire({
                                        title: 'Error',
                                        text: response.messageStatus,
                                        icon: 'error',
                                        confirmButtonText: 'Aceptar'
                                    })
                                }

                            },
                            error: function (error) {
                                console.log('Error al obtener los detalles del usuario.');
                            }

                        }).then((result) => {
                            if (result.isConfirmed) {
                                location.reload();
                            }
                        })
                    }
                })
            }

        });
        tablecitasd.column(9).nodes().to$().find('.btnEditarC').click(async function () {
            // Maneja la acci�n del bot�n aqui
            let data = tablecitasd.row($(this).parents('tr')).data();
            let CitaID = data[1]; // la primera columna contiene el ID del usuario

            const { value: citaState } = await Swal.fire({
                title: 'Seleccionar estado de la cita',
                input: 'select',
                inputOptions: {
                    '1': 'Pendiente',
                    '2': 'Confirmada',
                    '3': 'Completada'
                },
                inputPlaceholder: 'Selecciona el estado de la cita',
                showCancelButton: true,
                inputValidator: (value) => {
                    return new Promise((resolve) => {
                        if (value > 0) {
                            // alert(value);
                            //resolve(value); // Si se selecciona un estado, resolvemos con ese valor
                            changeDateStatus(value, CitaID);
                        } else {
                            resolve('Debes seleccionar un estado de cita.');
                        }
                    });
                }
            });

        });
    },
    error: function (error) {
        console.log('Error al obtener los detalles del usuario.');
    }
});
function changeDateStatus(citaState, CitaID) {
    // Aqu� puedes realizar la solicitud AJAX con el valor de citaState
    //console.log(`Estado de cita seleccionado: ${citaState}`);

    $.ajax({
        url: sessionStorage.urlEP + '/api/Appointments/v1/UpdateAppointmentStatus',
        type: 'GET',
        data: { 'aID': CitaID, 'asID': citaState },
        contentType: "application/json",
        dataType: 'json',
        success: function (response) {
            if (response.codeStatus == 0) {
                Swal.fire({
                    title: response.messageStatus,
                    icon: 'info',
                    confirmButtonText: 'Aceptar'
                }).then((result) => {
                    if (result.isConfirmed) {
                        swal.close();
                        location.reload();
                    }
                })
            }
            else if (response.codeStatus == -1) {
                Swal.fire({
                    title: 'Error',
                    text: response.messageStatus,
                    icon: 'error',
                    confirmButtonText: 'Aceptar'
                })
            }

        },
        error: function (error) {
            console.log('Error al obtener los detalles del usuario.');
        }

    }).then((result) => {
        if (result.isConfirmed) {
            location.reload();
        }
    })
}

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