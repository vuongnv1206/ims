namespace IMS.Contract.Systems.Firebase;

public interface IFirebaseService
{
    public Task<bool> UpdateFileAsync(FileStream stream, string fileName);
}