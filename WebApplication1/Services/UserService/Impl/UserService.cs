using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Pagination.EntityFrameworkCore.Extensions;
using WebApplication1.Dtos.User;
using WebApplication1.Models;
using DataLibrary.Models;
using System.Linq.Dynamic.Core;
using WebApplication1.Strings;
using WebApplication1.Dtos.GetAll;
namespace WebApplication1.Services.UserService.Impl
{
    public class UserService : IUserService
    {

        private readonly IMapper _mapper;
        private readonly UserManager<Users> _userManager;

        public UserService(IMapper mapper, UserManager<Users> userManager)
        {

            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> GetAllUsers()
        {
            ServiceResponse<List<GetUserDto>> newserviceResponse = new ServiceResponse<List<GetUserDto>>();
            List<Users> users = _userManager.Users.ToList();
            newserviceResponse.data = users.Select(c => _mapper.Map<GetUserDto>(c)).ToList();
            return newserviceResponse;
        }
        public async Task<ServiceResponse<GetUserDto>> GetUserById(string id)
        {
            ServiceResponse<GetUserDto> newserviceResponse = new ServiceResponse<GetUserDto>();
            Users user = await _userManager.FindByIdAsync(id);
            newserviceResponse.data = _mapper.Map<GetUserDto>(user);
            return newserviceResponse;
        }


        public async Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserDto updateuser)
        {
            ServiceResponse<GetUserDto> newserviceResponse = new ServiceResponse<GetUserDto>();
            try
            {
                Users user = await _userManager.FindByIdAsync(_mapper.Map<Users>(updateuser).Id);
                if (user != null)
                {
                    user.Name = updateuser.Name;
                    await _userManager.UpdateAsync(user);
                    newserviceResponse.data = _mapper.Map<GetUserDto>(user);
                    newserviceResponse.message = ErrorStrings.UserUpdatedSuccess;
               }
                else
                {
                    newserviceResponse.success = false;
                    newserviceResponse.message = ErrorStrings.UserNotFound;
                }
                
            }
            catch (Exception exp)
            {
                newserviceResponse.success = false;
                newserviceResponse.message = exp.Message;
            }
            return newserviceResponse;
        }
        public async Task<ServiceResponse<GetUserDto>> DeleteUser(string id)
        {
            ServiceResponse<GetUserDto> newserviceResponse = new ServiceResponse<GetUserDto>();
            try
            {
                Users user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    await _userManager.DeleteAsync(user);
                    GetUserDto getUser = _mapper.Map<GetUserDto>(user);
                    newserviceResponse.data = getUser;
                    newserviceResponse.message= ErrorStrings.UserDeletedSuccess;  
                   // List<Users> users = _userManager.Users.ToList();
                    //newserviceResponse.data = users.Select(c => _mapper.Map<GetUserDto>(c)).ToList();
                }
                else
                {
                    newserviceResponse.success = false;
                    newserviceResponse.message = ErrorStrings.UserNotFound;
                }
            }
            catch (Exception exp)
            {
                newserviceResponse.success = false;
                newserviceResponse.message = exp.Message;
            }
            return newserviceResponse;
        }

        public async Task<Pagination<GetUserDto>> GetUsersByAsync(GetAllDto getAllDto)
        {
            IQueryable<Users> query = _userManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(getAllDto.sortBy))
            {
                query = query.OrderBy(getAllDto.sortBy);
            }

            if (!string.IsNullOrEmpty(getAllDto.searchTerm))
            {
                query = query.Where(getAllDto.searchTerm);
            }
            int totalItems = _userManager.Users.Count();
            IEnumerable<Users> paginatedUsers = query.Skip((getAllDto.page - 1) * getAllDto.limit).Take(getAllDto.limit).ToList();
            List<GetUserDto> mappedUsers = paginatedUsers.Select(u => _mapper.Map<GetUserDto>(u)).ToList();

            return new Pagination<GetUserDto>(mappedUsers, totalItems, getAllDto.page, getAllDto.limit);
          
        }


        //public async Task<Pagination<GetUserDto>> GetUsersByAsync(string searchTerm, string sortBy, int page, int limit)
        //{

        //    List<Users> users = _userManager.Users.ToList();

        //    if (searchTerm != null)
        //    {
        //        users = users.Where(u =>
        //            u.Id.ToLower().Contains(searchTerm.ToLower()) ||
        //            u.Name.ToLower().Contains(searchTerm.ToLower()) ||
        //            u.UserName.ToLower().Contains(searchTerm.ToLower()) ||
        //            u.Email.ToLower().Contains(searchTerm.ToLower())
        //        ).ToList();
        //    }
        //    switch (sortBy)
        //    {
        //        case "id":
        //            users = users.OrderBy(u => u.Id).ToList();
        //            break;
        //        case "name":
        //            users = users.OrderBy(u => u.Name).ToList();
        //            break;
        //        case "username":
        //            users = users.OrderBy(u => u.UserName).ToList();
        //            break;
        //        default:

        //            users = users.OrderBy(u => u.Id).ToList();
        //            break;
        //    }

        //    var totalItems = users.Count;
        //    var paginatedUsers = users.Skip((page - 1) * limit).Take(limit).ToList();
        //    var mappedUsers = paginatedUsers.Select(u => _mapper.Map<GetUserDto>(u)).ToList();

        //    return new Pagination<GetUserDto>(mappedUsers, totalItems, page, limit);
        //}


        //public async Task<ServiceResponse<List<GetUserDto>>> Search(string name, string username, string email)
        //{
        //    ServiceResponse<List<GetUserDto>> newserviceResponse = new ServiceResponse<List<GetUserDto>>();
        //    try
        //    {

        //        List<Users> users = _userManager.Users.ToList();
        //        List<Users> searchList = new List<Users>();
        //        if (!name.IsNullOrEmpty())
        //        {
        //            searchList = users.Where(c => c.Name.ToLower().Contains(name.ToLower())).ToList();
        //        } if (!username.IsNullOrEmpty())
        //        {
        //            searchList = users.Where(c => c.UserName.Contains(username)).ToList();
        //        } if (!email.IsNullOrEmpty())
        //        {
        //            searchList = users.Where(c => c.Email.ToLower().Contains(email.ToLower())).ToList();
        //        }
        //        if (searchList.Count > 0)
        //        {
        //            newserviceResponse.data = (searchList.Select(c => _mapper.Map<GetUserDto>(c))).ToList();

        //        }
        //        else
        //        {

        //            newserviceResponse.message = Message.NothingFound;
        //        }


        //    }
        //    catch (Exception exp)
        //    {
        //        newserviceResponse.success = false;
        //        newserviceResponse.message = exp.Message;
        //    }
        //    return newserviceResponse;
        //}
        //public async Task<ServiceResponse<List<GetUserDto>>> SortByAsync(string sortby)
        //{
        //    ServiceResponse<List<GetUserDto>> newserviceResponse = new ServiceResponse<List<GetUserDto>>();
        //    List<Users> users = _userManager.Users.ToList();
        //    switch (sortby)
        //    {
        //        case "id":
        //            users = users.OrderBy(c => c.Id).ToList();
        //            newserviceResponse.data = (users.Select(c => _mapper.Map<GetUserDto>(c))).ToList();
        //            break;
        //        case "name":
        //            users = users.OrderBy(c => c.Name).ToList();
        //            newserviceResponse.data = (users.Select(c => _mapper.Map<GetUserDto>(c))).ToList();
        //            break;
        //        case "username":
        //            users = users.OrderBy(c => c.UserName).ToList();
        //            newserviceResponse.data = (users.Select(c => _mapper.Map<GetUserDto>(c))).ToList();
        //            break;
        //        default:
        //            newserviceResponse.data = (users.Select(c => _mapper.Map<GetUserDto>(c))).ToList();
        //            break;
        //    }

        //    return newserviceResponse;
        //}

        //public async Task<Pagination<GetUserDto>> GetUsersAsync(int page, int limit) {
        //    List<Users> users = _userManager.Users.ToList();

        //    var list = users.Skip((page - 1) * limit).Take(limit).ToList();
        //    var totalItems = users.Count();

        //    return new Pagination<GetUserDto>((list.Select(c => _mapper.Map<GetUserDto>(c))).ToList(), totalItems, page, limit);

        //}


    }
}
