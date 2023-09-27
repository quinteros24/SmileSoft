namespace Domain.Entities
{
    public class GenericResponseModel
    {
        //public bool Status { get; set; } = false;
        public bool Status { get; set; }
        public string? CodeStatus { get; set; } = "-1";
        public string? MessageStatus { get; set; } = "Sin mensaje mapeado";
        public int RecordsQuantity { get; set; } = 0;
        public DateTime? DateResponse { get; set; }
        public object? ItemJson { get; set; }

        //pruebas

    }
}
