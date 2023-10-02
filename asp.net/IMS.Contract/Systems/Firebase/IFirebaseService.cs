using Microsoft.AspNetCore.Http;

namespace IMS.Contract.Systems.Firebase;

public interface IFirebaseService
{
    Task<string> UpLoadFileOnFirebaseAsync(IFormFile file);
}