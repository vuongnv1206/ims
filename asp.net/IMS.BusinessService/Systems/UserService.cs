using AutoMapper;
using AutoMapper.Internal.Mappers;
using Azure.Core;
using IMS.Contract.Systems.Users;
using IMS.Domain.Systems;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BusinessService.Systems
{
	public class UserService : IUserService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<AppRole> _roleManager;
		private readonly IMapper _mapper;
		public UserService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IMapper mapper)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_mapper = mapper;
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
			if ((await _userManager.FindByNameAsync(userDto.UserName)) != null)
			{
				throw new Exception("Username is already exist");
			}

			if ((await _userManager.FindByEmailAsync(userDto.Email)) != null)
			{
				throw new Exception("Email  is already exist");
			}
			var user = _mapper.Map<CreateUserDto, AppUser>(userDto);
			var result = await _userManager.CreateAsync(user, userDto.Password);

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

		public async Task<List<UserDto>> GetListAllAsync(string? keyword)
		{

			var usersQuery = _userManager.Users
			.Where(u => string.IsNullOrWhiteSpace(keyword) ||
						u.FullName.Contains(keyword) ||
						u.UserName.Contains(keyword));

			var users = await usersQuery.ToListAsync();

			var userDtos = new List<UserDto>();

			foreach (var user in users)
			{
				// Retrieve the roles for each user
				var roles = await _userManager.GetRolesAsync(user);

				var userDto = _mapper.Map<UserDto>(user);

				// Set the Roles property in UserDto
				userDto.Roles = roles.ToList();

				userDtos.Add(userDto);
			}
			return userDtos;
		}

		public async Task<UserDto> GetUserByIdAsync(Guid id)
		{
			var user = await _userManager.FindByIdAsync(id.ToString());
			if (user == null)
			{
				throw new Exception("Not found");
			}
			var userDto = _mapper.Map<AppUser, UserDto>(user);
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
			_mapper.Map(userDto, user);
			var result = await _userManager.UpdateAsync(user);
		}
	}
}
