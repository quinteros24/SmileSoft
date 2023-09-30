using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Response
{


    public class ViewUsersModelResponse : ViewDoctorsModelResponse
    {
        public int uID { get; set; }
        public int utID { get; set; }
        public string? uName { get; set; }
        public string? uLastName { get; set; }
        public string? uCellphone { get; set; }
        public string? uAddress { get; set; }
        public string? uLoginName { get; set; }
        public string? uEmailAddress { get; set; }
        public int dtID { get; set; }
        public string? uDocument { get; set; }
        public bool uStatus { get; set; }
        public bool uIsBlocked { get; set; }
        public int? oID { get; set; }
        public int? gID { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateOnly? uBirthDate { get; set; }

        //public ViewDoctorsModelResponse? Doctors { get; set; }
    }
}
