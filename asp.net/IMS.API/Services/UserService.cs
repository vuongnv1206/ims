using AutoMapper;
using IMS.Api.Common.Helpers.Extensions;
using IMS.Api.Common.Helpers.Firebase;
using IMS.Api.EnityFrameworkCore;
using IMS.Api.Interfaces;
using IMS.Api.Models.Dtos.Users;
using IMS.Api.Models.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace IMS.Api.Services
{
    public class UserService : ServiceBase<AppUser>, IUserService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<AppRole> _roleManager;
		private readonly IFirebaseService firebaseService;
        public UserService(
			UserManager<AppUser> userManager,
			RoleManager<AppRole> roleManager,
			IFirebaseService firebaseService,
            IMSDbContext context,
			IMapper mapper)
			: base(context, mapper)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			this.firebaseService = firebaseService;
        }

		public async Task AssignRolesAsync(Guid userId, string[] roleNames)
		{
			var user = await _userManager.FindByIdAsync(userId.ToString());
			if (user == null)
			{
				throw new Exception("User doesn't exist");
			}
			var currentRoles = await _userManager.GetRolesAsync(user);
			var removedResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
			var addedResult = await _userManager.AddToRolesAsync(user, roleNames);
			if (!addedResult.Succeeded || !removedResult.Succeeded)
			{
				List<IdentityError> addedErrorList = addedResult.Errors.ToList();
				List<IdentityError> removedErrorList = removedResult.Errors.ToList();
				var errorList = new List<IdentityError>();
				errorList.AddRange(addedErrorList);
				errorList.AddRange(removedErrorList);
				string errors = "";

				foreach (var error in errorList)
				{
					errors = errors + error.Description.ToString();
				}
				throw new Exception(errors);
			}
		}


		public async Task CreateUser(CreateUserDto userDto)
		{
			var existingUser = await _userManager.FindByNameAsync(userDto.UserName);
			if (existingUser != null)
			{
				// Tên người dùng đã tồn tại
				throw new Exception("Username is already exist");
			}

			existingUser = await _userManager.FindByEmailAsync(userDto.Email);
			if (existingUser != null)
			{
				// Email đã tồn tại
				throw new Exception("Email is already exist");
			}

			var user = mapper.Map<CreateUserDto, AppUser>(userDto);
			var result = await _userManager.CreateAsync(user, userDto.Password);

			if (!result.Succeeded)
			{
				// Xử lý lỗi khi tạo người dùng thất bại
				throw new Exception("Failed to create user. Please check the provided data.");
			}
		}


		public async Task DeleteAsync(Guid id)
		{
			var user = await _userManager.FindByIdAsync(id.ToString());
			if (user == null)
			{
				throw new Exception("User doesn't exist");
			}
			await _userManager.DeleteAsync(user);
		}

		public async Task<UserResponse> GetListAllAsync(UserRequest request)
		{

			var usersQuery = _userManager.Users
				.Where(u => string.IsNullOrWhiteSpace(request.KeyWords)
                        || u.FullName.Contains(request.KeyWords)
						|| u.UserName.Contains(request.KeyWords));

			var users = await usersQuery.Paginate(request).ToDynamicListAsync();

			var userDtos = new List<UserDto>();

			foreach (var user in users)
			{
				// Retrieve the roles for each user
				var roles = await _userManager.GetRolesAsync(user);

				var userDto = mapper.Map<UserDto>(user);

				// Set the Roles property in UserDto
				userDto.Roles = roles;

				userDtos.Add(userDto);
			}

			var response = new UserResponse
			{
				Users = userDtos,
				Page = GetPagingResponse(request, usersQuery.Count()),
			};

			return response;	
		}

		public async Task<UserDto> GetUserByIdAsync(Guid id)
		{
			var user = await _userManager.FindByIdAsync(id.ToString());
			if (user == null)
			{
				throw new Exception("Not found");
			}
			var userDto = mapper.Map<AppUser, UserDto>(user);
			var roles = await _userManager.GetRolesAsync(user);
			userDto.Roles = roles;
			return userDto;
		}

		public async Task UpdateUser(Guid id, UpdateUserDto userDto)
		{
            var user = await _userManager.FindByIdAsync(id.ToString());
			if (user == null)
			{
				throw new Exception("Not found");
			}
			mapper.Map(userDto, user);
			if (userDto.FileImage != null)
			{
				var fileName = await firebaseService.UpLoadFileOnFirebaseAsync(userDto.FileImage);
				user.Avatar = fileName;
            }
			var result = await _userManager.UpdateAsync(user);
		}

		
	}
}
