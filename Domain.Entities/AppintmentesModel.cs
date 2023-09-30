namespace Domain.Entities
{
    public class AppintmentesModel
    {
        public int? aID {  get; set; }
        public int? oID { get; set; }
        public int? dtID { get; set; }
        public int? gID { get; set; }
        public string? uDocument {  get; set; }
        public string? uEmailAddress {  get; set; }
        public string? uName {  get; set; }
        public string? uLastName {  get; set; }
        public string? uCellphone {  get; set; }
        public int? uID { get; set; }
        public int? dID { get; set; }
        public int? asID { get; set; } = 1;
        public string? aDescription { get; set; }
        public DateOnly? aDate { get; set; }
        public DateOnly? uBirthDate { get; set; }
        public TimeOnly? aTime { get; set; }
    }
}
