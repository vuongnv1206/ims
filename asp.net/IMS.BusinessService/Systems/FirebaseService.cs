using AutoMapper;
using Firebase.Auth;
using Firebase.Storage;
using IMS.BusinessService.Service;
using IMS.Contract.Systems.Firebase;
using IMS.Contract.Systems.Settings;
using IMS.Infrastructure.EnityFrameworkCore;
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

    public async Task<bool> UpdateFileAsync(FileStream stream, string fileName)
    {
		var apiKey = firebaseSetting.ApiKey;
		var bucket = firebaseSetting.Bucket;

		var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));

		try
		{
            var task = new FirebaseStorage
            (
                bucket,
                new FirebaseStorageOptions
                {
                    ThrowOnCancel = true,
                }
            )
            .Child("images")
            .Child(fileName)
            .PutAsync(stream);

			return true;
        }
		catch (Exception ex)
		{
			throw new Exception("Exception"+ ex);
		}
		
    }
}
