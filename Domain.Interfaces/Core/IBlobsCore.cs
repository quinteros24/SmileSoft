﻿using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Domain.Interfaces.Core
{
    public interface IBlobsCore
    {
        Task<string> CreateBlobStorage(IFormFile file, string blobName);
        Task<BlobModel> GetBlobFile(string url);
    }
}
