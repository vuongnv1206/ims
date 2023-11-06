using Microsoft.AspNetCore.Http;

namespace IMS.Api.Common.Helpers.Firebase;

public interface IFirebaseService
{
    Task<string> UpLoadFileOnFirebaseAsync(IFormFile file);
}