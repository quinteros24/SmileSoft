namespace Domain.Entities
{
    public class AppointmentesModel
    {
        public int? aID { get; set; }
        public int? oID { get; set; }
        public int? dtID { get; set; }
        public int? gID { get; set; }
        public string? uDocument { get; set; }
        public string? uEmailAddress { get; set; }
        public string? uName { get; set; }
        public string? uDoctorName { get; set; }
        public string? uLastName { get; set; }
        public string? uCellphone { get; set; }
        public int? uID { get; set; }
        public int? dID { get; set; }
        public int? asID { get; set; } = 1;
        public string? asName { get; set; }
        public string? aDescription { get; set; }
        public DateTime? aDate { get; set; }
        public DateTime? uBirthDate { get; set; }
        public TimeSpan? aTime { get; set; }
    }
}
