namespace Domain.Entities
{
    public class LogsModel
    {
        public int uID { get; set; }
        public string? uName { get; set; }
        public string? uLoginName { get; set; }
        public int utID { get; set; }
        public string? logAction { get; set; }
        public string? logDescription { get; set; }
        public string? logJSON { get; set; }
        public DateTime logCreationDate { get; set; }
        public int TotalRecords { get; set; }
        public int RecordsLeft { get; set; }
    }
}
