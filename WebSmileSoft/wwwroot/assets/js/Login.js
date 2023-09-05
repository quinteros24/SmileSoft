//function validarInicioSesion(url) {
//    let username = document.getElementById("UserLogin").value;
//    let password = document.getElementById("Password").value;

//    if (username === "" || password === "") {
//        alert("Por favor ingresar todos los campos");
//    } else {
//        // Lógica de inicio de sesión aquí
//        let data = {
//            UserLogin: username,
//            password: password
//        };
//        $.ajax({
//            type: "POST",
//            url: url + '/api/Session/v1/Login',
//            data: JSON.stringify(data),
//            contentType: "application/json",
//            dataType: "json",
//            success: function (response) {
//                if (response.success) {
//                    alert(response.message);
//                } else {
//                    alert("Inicio de sesión fallido. " + response.message);
//                }
//            },
//            error: function (error) {
//                console.log("Error.");
//            }
//        });
//        //$.ajax({
//        //    type: "POST",
//        //    url: '/Account/Login',
//        //    data: JSON.stringify(data),
//        //    contentType: "application/json",
//        //    dataType: "json",
//        //    success: function (response) {
//        //        if (response.success) {
//        //            alert(response.message);
//        //        } else {
//        //            alert("Inicio de sesión fallido. " + response.message);
//        //        }
//        //    },
//        //    error: function (error) {
//        //        console.log("Error.");
//        //    }
//        //});

//    }
//}

//document.getElementById("loginButton").addEventListener("click", validarInicioSesion);