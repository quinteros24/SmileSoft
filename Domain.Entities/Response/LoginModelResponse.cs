namespace Domain.Entities
{
    public class LoginModelResponse
    {
        public int uID {  get; set; }
        public int utID {  get; set; }
        public string? uName {  get; set; }
        public string? uLastName {  get; set; }
        public string? uCellphone {  get; set; }
        public string? uAddress {  get; set; }
        public string? uLoginName {  get; set; }
        public string? uEmailAddress {  get; set; }
        public string? uPassword {  get; set; }
        public int dtID {  get; set; }
        public int? dID {  get; set; }
        public int? oID {  get; set; }
        public string? uDocument {  get; set; }
        public string? uToken {  get; set; }
        public int uStatus {  get; set; }
        public int uIsBlocked {  get; set; }
    }
}
