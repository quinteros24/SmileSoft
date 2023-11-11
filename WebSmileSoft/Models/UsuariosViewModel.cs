namespace WebSmileSoft.Models
{
    public class UsuariosModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Rol { get; set; }
        public string Consultorio { get; set; }
        public int Edad { get; set; }
        public DateTime UltimoIngreso { get; set; }
        public string Documento { get; set; }
        public string TipoDocumento { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public string Direccion { get; set; }
        public string NumeroCelular { get; set; }
        public string CorreoElectronico { get; set; }
        // Puedes agregar más propiedades según sea necesario.
    }
}
