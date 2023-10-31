using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BlobModel
    {
        public Stream? Content { get; set; }
        public string? ContentType { get; set; }
    }
}