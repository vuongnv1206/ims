using AutoMapper;
using Firebase.Auth;
using Firebase.Storage;
using IMS.BusinessService.Service;
using IMS.Contract.Systems.Firebase;
using IMS.Contract.Systems.Settings;
using IMS.Infrastructure.EnityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace IMS.BusinessService.Systems;

public class FirebaseService : ServiceBase, IFirebaseService
{
	private readonly FirebaseSetting firebaseSetting;
	public FirebaseService(
		IMSDbContext context,
		IMapper mapper,
		IOptions<FirebaseSetting> firebaseSetting
		) : base(context, mapper)
    {
		this.firebaseSetting = firebaseSetting.Value;
	}

    private async Task<string> UpdateFileAsync(MemoryStream stream, string fileName)
    {
		var apiKey = firebaseSetting.ApiKey;
		var bucket = firebaseSetting.Bucket;

        var ext = fileName.Split(".").Last();
        var nameFile = fileName.Split(".").First();
        var dateTime = DateTime.UtcNow; // unique file name
		fileName = $"{nameFile}_{dateTime.ToString("dd-MM-yyyy HH:mm:ss")}.{ext}";

  //      var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
		//var a = await auth.SignInWithEmailAndPasswordAsync("swdgroup6@gmail.com", "Abcd@1234");

		var cancellation = new CancellationTokenSource();
		try
		{
            var task = await new FirebaseStorage
            (
                bucket,
                new FirebaseStorageOptions
                {
                    ThrowOnCancel = true,
                }
            )
            .Child("images")
            .Child(fileName)
            .PutAsync(stream, cancellation.Token);

			return fileName;
        }
		catch (Exception ex)
		{
			throw new Exception("Exception"+ ex);
		}
		
    }

    public async Task<string> UpLoadFileOnFirebaseAsync(IFormFile file)
    {
        if (file.Length > 0)
        {
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Seek(0, SeekOrigin.Begin);

                try
                {
                    var uploadResult = await UpdateFileAsync(stream, file.FileName);
                    return uploadResult;
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi tải lên ở đây (vd: thông báo lỗi cho người dùng)
                }
            }
        }
        return null;
    }
}
