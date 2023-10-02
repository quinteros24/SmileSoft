using Domain.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Response;

namespace Domain.Entities
{
    public class DoctorsModelRequest: UsersModelResponse
    {
        public string? uPassword { get; set; }
    }
}
