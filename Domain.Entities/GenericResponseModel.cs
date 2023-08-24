namespace Domain.Entities
{
    public class GenericResponseModel
    {
        public bool Status { get; set; }
        public string? CodeStatus { get; set; } = "-1";
        public string? MessageStatus { get; set; } = string.Empty;
        public int RecordsQuantity { get; set; }
        public DateTime? DateResponse { get; set; }
        public object? ItemJson { get; set; }
    }
}
