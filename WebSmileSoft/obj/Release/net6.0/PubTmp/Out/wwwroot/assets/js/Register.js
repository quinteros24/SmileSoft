function validarRegistro() {
    let userNameRegister = document.getElementById("userNameRegister").value;
    let userLastNameRegister = document.getElementById("userLastNameRegister").value;
    let option = document.getElementById("options").value;
    let idNumber = document.getElementById("IdNumber").value;
    let birthDate = document.getElementById("birthDate").value;
    let genderMasculino = document.getElementById("genderMasculino");
    let genderFemenino = document.getElementById("genderFemenino");
    let genderOtro = document.getElementById("genderOtro");
    let selectedGender = "";
    if (genderMasculino.checked) {
        selectedGender = genderMasculino.value;
    } else if (genderFemenino.checked) {
        selectedGender = genderFemenino.value;
    } else if (genderOtro.checked) {
        selectedGender = genderOtro.value;
    } else {
        // Ningún género seleccionado
    }
    let mobileNumber = document.getElementById("mobileNumber").value;
    let address = document.getElementById("address").value;
    let email = document.getElementById("email").value;
    let password = document.getElementById("password").value;
    let confirmPassword = document.getElementById("confirmPassword").value;

    if (email === "" || address === "" || mobileNumber === "" || selectedGender === "" || birthDate === "" || idNumber === "" || option === "" || userNameRegister === "" || userLastNameRegister === "" || password === "" || confirmPassword === "") {
        alert("Por favor ingresar todos los campos");
    } else if (password !== confirmPassword) {

        alert("Las contraseñas no coinciden.");

    } else {
        // Lógica de inicio de sesión aquí
        let data = {
            userNameRegister: userNameRegister,
            option: option,
            idNumber: idNumber,
            birthDate: birthDate,
            selectedGender: selectedGender,
            mobileNumber: mobileNumber,
            address: address,
            email: email,
            userLastNameRegister: userLastNameRegister,
            password: password,
            confirmPassword: confirmPassword
        };
        $.ajax({
            type: "POST",
            url: "Login/RegisterNewUser",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.success) {
                    alert(response.message);
                } else {
                    alert("Registro fallido. " + response.message);
                }
            },
            error: function (error) {
                console.log("Error.");
            }
        });

    }
}

document.getElementById("registerButton").addEventListener("click", validarRegistro);