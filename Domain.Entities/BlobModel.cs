namespace Domain.Entities
{
    public class BlobModel
    {
        public Stream? Content { get; set; }
        public string? ContentType { get; set; }
    }
}