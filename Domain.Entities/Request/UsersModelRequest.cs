using Domain.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UsersModelRequest: UsersModelResponse
    {
        public string? uPassword { get; set; }
    }
}
