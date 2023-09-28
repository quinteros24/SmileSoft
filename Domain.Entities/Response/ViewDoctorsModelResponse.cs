using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ViewDoctorsModelResponse
    {
        public int dID { get; set; }
        public string? dAcademicLevel { get; set; }
        public string? dDegree { get; set; }
        public string? dUniversityName { get; set; }
        public string? dSpeciality { get; set; }
        public string? dProfessionalCard { get; set; }

    }
}
