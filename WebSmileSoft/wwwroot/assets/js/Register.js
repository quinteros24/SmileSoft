function validarRegistro() {
    event.preventDefault();
    let userNameRegister = document.getElementById("userNameRegister").value;
    let userFirstNameRegister = document.getElementById("userFirstNameRegister").value;
    let userLastNameRegister = document.getElementById("userLastNameRegister").value;
    let option = document.getElementById("tipo_documento").value;
    let idNumber = document.getElementById("IdNumber").value;
    //let birthDate = ""; 
    //let selectedGender = "";
    let mobileNumber = document.getElementById("mobileNumber").value;
    let address = document.getElementById("address").value;
    let email = document.getElementById("email").value;
    let password = document.getElementById("password").value;
    let confirmPassword = document.getElementById("confirmPassword").value;

    if (
        email === "" ||
        address === "" ||
        mobileNumber === "" ||
        //selectedGender === "" ||
        //birthDate === "" ||
        idNumber === "" ||
        option === "" ||
        userNameRegister === "" ||
        userLastNameRegister === "" ||
        password === "" ||
        confirmPassword === ""
    ) {
        //alert("Por favor ingresa todos los campos.");
        Swal.fire({
            text: "Por favor ingresa todos los campos. ",
            confirmButtonColor: '#008dc9'
        });
    } else if (password !== confirmPassword) {

        //alert("Las contraseñas no coinciden.");
        Swal.fire({
            text: "Las contraseñas no coinciden.",
            confirmButtonColor: '#008dc9'
        });
    } else {
        // Lógica de registro aquí
        let data = {
            userNameRegister: userNameRegister,
            userFirstNameRegister: userFirstNameRegister,
            userLastNameRegister: userLastNameRegister,
            option: option,
            idNumber: idNumber,
            //birthDate: birthDate,
            //selectedGender: selectedGender,
            mobileNumber: mobileNumber,
            address: address,
            email: email,
            password: password,
            confirmPassword: confirmPassword
        };
        $.ajax({
            type: "POST",
            url: '@ViewBag.urlEndPoint' + '/api/Session/v1/Create',
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.success) {
                    //alert(response.message);
                    Swal.fire({
                        text: "Usuario Registrado " + response.message,
                        confirmButtonColor: '#008dc9'
                    });
                } else {
                    //alert("Registro fallido. " + response.message);
                    Swal.fire({
                        text: "Error en el registro " + response.message,
                        confirmButtonColor: '#008dc9'
                    });
                }
            },
            error: function (error) {
                console.log("Error.");
            }
        });
    }
}
document.getElementById("registerButton").addEventListener("click", validarRegistro);
