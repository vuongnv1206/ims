using Microsoft.AspNetCore.Http;

namespace IMS.Api.Helpers.Firebase;

public interface IFirebaseService
{
    Task<string> UpLoadFileOnFirebaseAsync(IFormFile file);
}