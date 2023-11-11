namespace Domain.Entities
{
    public class DoctorsModelResponse
    {
        public int? dID { get; set; }
        public string? dAcademicLevel { get; set; }
        public string? dDegree { get; set; }
        public string? dUniversityName { get; set; }
        public int? spID { get; set; } //Especialidad
        public string? dProfessionalCard { get; set; }

    }
}
