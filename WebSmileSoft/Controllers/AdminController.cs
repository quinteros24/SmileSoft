﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebSmileSoft.Models;
using WebSmileSoft.Interfaces;

namespace WebSmileSoft.Controllers
{
    //[Authorize(Roles = "Administrador")] // Asegura que solo los usuarios autenticados con el rol de "Administrador" puedan acceder a este controlador
    public class AdminController : Controller
    {

        private readonly ISettings _settings;

        public AdminController(ISettings settings)
        {
            _settings = settings;
        }

        public IActionResult Index()
        {
            return View("~/Views/Admin/Dashboard.cshtml");
        }

        public IActionResult UserManagement()
        {
            return View("~/Views/Admin/UserManagement/Index.cshtml");
        }

        public IActionResult SiteManagement()
        {
            return View("~/Views/Admin/SiteManagement/Index.cshtml");
        }

        public IActionResult TableUsers()
        {
            // Obtener datos de la base de datos (reemplaza esto con tu lógica)
            var usuarios = new List<UsuariosModel>
        {
           new UsuariosModel { Id = 1, Nombre = "Juan",  Apellido = "Mora Smile", Rol = "Administrador", Consultorio = "Consultorio 1", Edad = 25, UltimoIngreso = DateTime.Parse("12/12/2020"), Documento = "123456789", TipoDocumento = "Cedula", FechaNacimiento = DateTime.Parse("12/12/1995"), Genero = "Masculino", Direccion = "Calle 1", NumeroCelular = "123456789", CorreoElectronico = "juan@mile.com" },
            new UsuariosModel { Id = 2, Nombre = "Pedro", Apellido = "Mora Smile",Rol = "Usuario", Consultorio = "Consultorio 2", Edad = 25, UltimoIngreso = DateTime.Parse("12/12/2020"), Documento = "123456789", TipoDocumento = "Cedula", FechaNacimiento = DateTime.Parse("12/12/1995"), Genero = "Masculino", Direccion = "Calle 1", NumeroCelular = "123456789", CorreoElectronico = "pedro@mile.com" },
            new UsuariosModel { Id = 3, Nombre = "Maria", Apellido = "Mora Smile",Rol = "Usuario", Consultorio = "Consultorio 3", Edad = 25, UltimoIngreso = DateTime.Parse("12/12/2020"), Documento = "123456789", TipoDocumento = "Cedula", FechaNacimiento = DateTime.Parse("12/12/1995"), Genero = "Femenino", Direccion = "Calle 1", NumeroCelular = "123456789", CorreoElectronico = "maria@mile.com" },
            new UsuariosModel { Id = 4, Nombre = "Carlos",Apellido = "Mora Smile", Rol = "Administrador", Consultorio = "Consultorio 4", Edad = 30, UltimoIngreso = DateTime.Parse("11/11/2020"), Documento = "987654321", TipoDocumento = "Pasaporte", FechaNacimiento = DateTime.Parse("10/10/1990"), Genero = "Masculino", Direccion = "Calle 2", NumeroCelular = "987654321", CorreoElectronico = "carlos@mile.com" },
            new UsuariosModel { Id = 5, Nombre = "Ana", Apellido = "Mora Smile",Rol = "Usuario", Consultorio = "Consultorio 5", Edad = 28, UltimoIngreso = DateTime.Parse("10/10/2020"), Documento = "555555555", TipoDocumento = "Cedula", FechaNacimiento = DateTime.Parse("09/09/1993"), Genero = "Femenino", Direccion = "Calle 3", NumeroCelular = "555555555", CorreoElectronico = "ana@mile.com" },
            new UsuariosModel { Id = 6, Nombre = "Pablo",Apellido = "Mora Smile", Rol = "Usuario", Consultorio = "Consultorio 6", Edad = 35, UltimoIngreso = DateTime.Parse("09/09/2020"), Documento = "777777777", TipoDocumento = "Cedula", FechaNacimiento = DateTime.Parse("08/08/1986"), Genero = "Masculino", Direccion = "Calle 4", NumeroCelular = "777777777", CorreoElectronico = "pablo@mile.com" },
            new UsuariosModel { Id = 7, Nombre = "Sofia",Apellido = "Mora Smile", Rol = "Usuario", Consultorio = "Consultorio 7", Edad = 27, UltimoIngreso = DateTime.Parse("08/08/2020"), Documento = "444444444", TipoDocumento = "Cedula", FechaNacimiento = DateTime.Parse("07/07/1994"), Genero = "Femenino", Direccion = "Calle 5", NumeroCelular = "444444444", CorreoElectronico = "sofia@mile.com" },
            new UsuariosModel { Id = 8, Nombre = "Javier",Apellido = "Mora Smile", Rol = "Administrador", Consultorio = "Consultorio 8", Edad = 40, UltimoIngreso = DateTime.Parse("07/07/2020"), Documento = "666666666", TipoDocumento = "Cedula", FechaNacimiento = DateTime.Parse("06/06/1981"), Genero = "Masculino", Direccion = "Calle 6", NumeroCelular = "666666666", CorreoElectronico = "javier@mile.com" },
            new UsuariosModel { Id = 9, Nombre = "Laura",Apellido = "Mora Smile", Rol = "Usuario", Consultorio = "Consultorio 9", Edad = 29, UltimoIngreso = DateTime.Parse("06/06/2020"), Documento = "888888888", TipoDocumento = "Pasaporte", FechaNacimiento = DateTime.Parse("05/05/1992"), Genero = "Femenino", Direccion = "Calle 7", NumeroCelular = "888888888", CorreoElectronico = "laura@mile.com" },
            new UsuariosModel { Id = 10, Nombre = "Andres",Apellido = "Mora Smile", Rol = "Usuario", Consultorio = "Consultorio 10", Edad = 32, UltimoIngreso = DateTime.Parse("05/05/2020"), Documento = "999999999", TipoDocumento = "Cedula", FechaNacimiento = DateTime.Parse("04/04/1989"), Genero = "Masculino", Direccion = "Calle 8", NumeroCelular = "999999999", CorreoElectronico = "andres@mile.com" },
            new UsuariosModel { Id = 11, Nombre = "Elena",Apellido = "Mora Smile", Rol = "Usuario", Consultorio = "Consultorio 11", Edad = 31, UltimoIngreso = DateTime.Parse("04/04/2020"), Documento = "121212121", TipoDocumento = "Cedula", FechaNacimiento = DateTime.Parse("03/03/1990"), Genero = "Femenino", Direccion = "Calle 9", NumeroCelular = "121212121", CorreoElectronico = "elena@mile.com" },
            new UsuariosModel { Id = 12, Nombre = "Felipe",Apellido = "Mora Smile", Rol = "Administrador", Consultorio = "Consultorio 12", Edad = 45, UltimoIngreso = DateTime.Parse("03/03/2020"), Documento = "131313131", TipoDocumento = "Cedula", FechaNacimiento = DateTime.Parse("02/02/1976"), Genero = "Masculino", Direccion = "Calle 10", NumeroCelular = "131313131", CorreoElectronico = "felipe@mile.com" }


        };

            // Pasa los datos a la vista
            return View("~/Views/Admin/UserManagement/ViewTableUsers.cshtml", usuarios);
        }

        public IActionResult TableDoctor()
        {
            // Obtener datos de la base de datos (reemplaza esto con tu lógica)
            var doctores = new List<UsuariosModel>
        {
           new UsuariosModel { Id = 1, Nombre = "Juan",  Apellido = "Mora Smile", Rol = "Administrador", Consultorio = "Consultorio 1", Edad = 25, UltimoIngreso = DateTime.Parse("12/12/2020"), Documento = "123456789", TipoDocumento = "Cedula", FechaNacimiento = DateTime.Parse("12/12/1995"), Genero = "Masculino", Direccion = "Calle 1", NumeroCelular = "123456789", CorreoElectronico = "juan@mile.com" },
            new UsuariosModel { Id = 2, Nombre = "Pedro", Apellido = "Mora Smile",Rol = "Usuario", Consultorio = "Consultorio 2", Edad = 25, UltimoIngreso = DateTime.Parse("12/12/2020"), Documento = "123456789", TipoDocumento = "Cedula", FechaNacimiento = DateTime.Parse("12/12/1995"), Genero = "Masculino", Direccion = "Calle 1", NumeroCelular = "123456789", CorreoElectronico = "pedro@mile.com" },
            new UsuariosModel { Id = 3, Nombre = "Maria", Apellido = "Mora Smile",Rol = "Usuario", Consultorio = "Consultorio 3", Edad = 25, UltimoIngreso = DateTime.Parse("12/12/2020"), Documento = "123456789", TipoDocumento = "Cedula", FechaNacimiento = DateTime.Parse("12/12/1995"), Genero = "Femenino", Direccion = "Calle 1", NumeroCelular = "123456789", CorreoElectronico = "maria@mile.com" },
            new UsuariosModel { Id = 4, Nombre = "Carlos",Apellido = "Mora Smile", Rol = "Administrador", Consultorio = "Consultorio 4", Edad = 30, UltimoIngreso = DateTime.Parse("11/11/2020"), Documento = "987654321", TipoDocumento = "Pasaporte", FechaNacimiento = DateTime.Parse("10/10/1990"), Genero = "Masculino", Direccion = "Calle 2", NumeroCelular = "987654321", CorreoElectronico = "carlos@mile.com" },
            new UsuariosModel { Id = 5, Nombre = "Ana", Apellido = "Mora Smile",Rol = "Usuario", Consultorio = "Consultorio 5", Edad = 28, UltimoIngreso = DateTime.Parse("10/10/2020"), Documento = "555555555", TipoDocumento = "Cedula", FechaNacimiento = DateTime.Parse("09/09/1993"), Genero = "Femenino", Direccion = "Calle 3", NumeroCelular = "555555555", CorreoElectronico = "ana@mile.com" },


        };

            // Pasa los datos a la vista
            return View("~/Views/Admin/UserManagement/ViewTableUsers.cshtml", doctores);
        }
        public IActionResult TableAdmin()
        {
            // Obtener datos de la base de datos (reemplaza esto con tu lógica)
            var administradores = new List<UsuariosModel>
        {
            new UsuariosModel { Id = 1, Nombre = "Juan",  Apellido = "Mora Smile", Rol = "Administrador", Consultorio = "Consultorio 1", Edad = 25, UltimoIngreso = DateTime.Parse("12/12/2020"), Documento = "123456789", TipoDocumento = "Cedula", FechaNacimiento = DateTime.Parse("12/12/1995"), Genero = "Masculino", Direccion = "Calle 1", NumeroCelular = "123456789", CorreoElectronico = "juan@mile.com" },
            new UsuariosModel { Id = 2, Nombre = "Pedro", Apellido = "Mora Smile",Rol = "Usuario", Consultorio = "Consultorio 2", Edad = 25, UltimoIngreso = DateTime.Parse("12/12/2020"), Documento = "123456789", TipoDocumento = "Cedula", FechaNacimiento = DateTime.Parse("12/12/1995"), Genero = "Masculino", Direccion = "Calle 1", NumeroCelular = "123456789", CorreoElectronico = "pedro@mile.com" },
            new UsuariosModel { Id = 3, Nombre = "Maria", Apellido = "Mora Smile",Rol = "Usuario", Consultorio = "Consultorio 3", Edad = 25, UltimoIngreso = DateTime.Parse("12/12/2020"), Documento = "123456789", TipoDocumento = "Cedula", FechaNacimiento = DateTime.Parse("12/12/1995"), Genero = "Femenino", Direccion = "Calle 1", NumeroCelular = "123456789", CorreoElectronico = "maria@mile.com" },
            new UsuariosModel { Id = 4, Nombre = "Carlos",Apellido = "Mora Smile", Rol = "Administrador", Consultorio = "Consultorio 4", Edad = 30, UltimoIngreso = DateTime.Parse("11/11/2020"), Documento = "987654321", TipoDocumento = "Pasaporte", FechaNacimiento = DateTime.Parse("10/10/1990"), Genero = "Masculino", Direccion = "Calle 2", NumeroCelular = "987654321", CorreoElectronico = "carlos@mile.com" },
            new UsuariosModel { Id = 5, Nombre = "Ana", Apellido = "Mora Smile",Rol = "Usuario", Consultorio = "Consultorio 5", Edad = 28, UltimoIngreso = DateTime.Parse("10/10/2020"), Documento = "555555555", TipoDocumento = "Cedula", FechaNacimiento = DateTime.Parse("09/09/1993"), Genero = "Femenino", Direccion = "Calle 3", NumeroCelular = "555555555", CorreoElectronico = "ana@mile.com" },
            new UsuariosModel { Id = 6, Nombre = "Pablo",Apellido = "Mora Smile", Rol = "Usuario", Consultorio = "Consultorio 6", Edad = 35, UltimoIngreso = DateTime.Parse("09/09/2020"), Documento = "777777777", TipoDocumento = "Cedula", FechaNacimiento = DateTime.Parse("08/08/1986"), Genero = "Masculino", Direccion = "Calle 4", NumeroCelular = "777777777", CorreoElectronico = "pablo@mile.com" },

        };

            // Pasa los datos a la vista
            return View("~/Views/Admin/UserManagement/ViewTableUsers.cshtml", administradores);
        }

        public IActionResult TableUsersTest()
        {
            ViewBag.urlEndPoint = _settings.urlEndPoint;
            // Obtener datos de la base de datos (reemplaza esto con tu lógica)
        //    var usuarios = new List<UsuariosModel>
        //{
        //   new UsuariosModel { Id = 1, Nombre = "Juan",  Apellido = "Mora Smile", Rol = "Administrador", Consultorio = "Consultorio 1", Edad = 25, UltimoIngreso = DateTime.Parse("12/12/2020"), Documento = "123456789", TipoDocumento = "Cedula", FechaNacimiento = DateTime.Parse("12/12/1995"), Genero = "Masculino", Direccion = "Calle 1", NumeroCelular = "123456789", CorreoElectronico = "juan@mile.com" },
        //    new UsuariosModel { Id = 2, Nombre = "Pedro", Apellido = "Mora Smile",Rol = "Usuario", Consultorio = "Consultorio 2", Edad = 25, UltimoIngreso = DateTime.Parse("12/12/2020"), Documento = "123456789", TipoDocumento = "Cedula", FechaNacimiento = DateTime.Parse("12/12/1995"), Genero = "Masculino", Direccion = "Calle 1", NumeroCelular = "123456789", CorreoElectronico = "pedro@mile.com" },
        //    new UsuariosModel { Id = 3, Nombre = "Maria", Apellido = "Mora Smile",Rol = "Usuario", Consultorio = "Consultorio 3", Edad = 25, UltimoIngreso = DateTime.Parse("12/12/2020"), Documento = "123456789", TipoDocumento = "Cedula", FechaNacimiento = DateTime.Parse("12/12/1995"), Genero = "Femenino", Direccion = "Calle 1", NumeroCelular = "123456789", CorreoElectronico = "maria@mile.com" },
        //    new UsuariosModel { Id = 4, Nombre = "Carlos",Apellido = "Mora Smile", Rol = "Administrador", Consultorio = "Consultorio 4", Edad = 30, UltimoIngreso = DateTime.Parse("11/11/2020"), Documento = "987654321", TipoDocumento = "Pasaporte", FechaNacimiento = DateTime.Parse("10/10/1990"), Genero = "Masculino", Direccion = "Calle 2", NumeroCelular = "987654321", CorreoElectronico = "carlos@mile.com" },
        //    new UsuariosModel { Id = 5, Nombre = "Ana", Apellido = "Mora Smile",Rol = "Usuario", Consultorio = "Consultorio 5", Edad = 28, UltimoIngreso = DateTime.Parse("10/10/2020"), Documento = "555555555", TipoDocumento = "Cedula", FechaNacimiento = DateTime.Parse("09/09/1993"), Genero = "Femenino", Direccion = "Calle 3", NumeroCelular = "555555555", CorreoElectronico = "ana@mile.com" },
        //    new UsuariosModel { Id = 6, Nombre = "Pablo",Apellido = "Mora Smile", Rol = "Usuario", Consultorio = "Consultorio 6", Edad = 35, UltimoIngreso = DateTime.Parse("09/09/2020"), Documento = "777777777", TipoDocumento = "Cedula", FechaNacimiento = DateTime.Parse("08/08/1986"), Genero = "Masculino", Direccion = "Calle 4", NumeroCelular = "777777777", CorreoElectronico = "pablo@mile.com" },
        //    new UsuariosModel { Id = 7, Nombre = "Sofia",Apellido = "Mora Smile", Rol = "Usuario", Consultorio = "Consultorio 7", Edad = 27, UltimoIngreso = DateTime.Parse("08/08/2020"), Documento = "444444444", TipoDocumento = "Cedula", FechaNacimiento = DateTime.Parse("07/07/1994"), Genero = "Femenino", Direccion = "Calle 5", NumeroCelular = "444444444", CorreoElectronico = "sofia@mile.com" },
        //    new UsuariosModel { Id = 8, Nombre = "Javier",Apellido = "Mora Smile", Rol = "Administrador", Consultorio = "Consultorio 8", Edad = 40, UltimoIngreso = DateTime.Parse("07/07/2020"), Documento = "666666666", TipoDocumento = "Cedula", FechaNacimiento = DateTime.Parse("06/06/1981"), Genero = "Masculino", Direccion = "Calle 6", NumeroCelular = "666666666", CorreoElectronico = "javier@mile.com" },
        //    new UsuariosModel { Id = 9, Nombre = "Laura",Apellido = "Mora Smile", Rol = "Usuario", Consultorio = "Consultorio 9", Edad = 29, UltimoIngreso = DateTime.Parse("06/06/2020"), Documento = "888888888", TipoDocumento = "Pasaporte", FechaNacimiento = DateTime.Parse("05/05/1992"), Genero = "Femenino", Direccion = "Calle 7", NumeroCelular = "888888888", CorreoElectronico = "laura@mile.com" },
        //    new UsuariosModel { Id = 10, Nombre = "Andres",Apellido = "Mora Smile", Rol = "Usuario", Consultorio = "Consultorio 10", Edad = 32, UltimoIngreso = DateTime.Parse("05/05/2020"), Documento = "999999999", TipoDocumento = "Cedula", FechaNacimiento = DateTime.Parse("04/04/1989"), Genero = "Masculino", Direccion = "Calle 8", NumeroCelular = "999999999", CorreoElectronico = "andres@mile.com" },
        //    new UsuariosModel { Id = 11, Nombre = "Elena",Apellido = "Mora Smile", Rol = "Usuario", Consultorio = "Consultorio 11", Edad = 31, UltimoIngreso = DateTime.Parse("04/04/2020"), Documento = "121212121", TipoDocumento = "Cedula", FechaNacimiento = DateTime.Parse("03/03/1990"), Genero = "Femenino", Direccion = "Calle 9", NumeroCelular = "121212121", CorreoElectronico = "elena@mile.com" },
        //    new UsuariosModel { Id = 12, Nombre = "Felipe",Apellido = "Mora Smile", Rol = "Administrador", Consultorio = "Consultorio 12", Edad = 45, UltimoIngreso = DateTime.Parse("03/03/2020"), Documento = "131313131", TipoDocumento = "Cedula", FechaNacimiento = DateTime.Parse("02/02/1976"), Genero = "Masculino", Direccion = "Calle 10", NumeroCelular = "131313131", CorreoElectronico = "felipe@mile.com" }


        //};

            // Pasa los datos a la vista
            return View("~/Views/Admin/UserManagement/ViewTableUsersTest.cshtml");
        }



    }
}
