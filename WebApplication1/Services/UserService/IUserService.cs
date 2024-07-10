using Pagination.EntityFrameworkCore.Extensions;
using WebApplication1.Dtos.GetAll;
using WebApplication1.Dtos.User;
using WebApplication1.Models;
namespace WebApplication1.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<List<GetUserDto>>> GetAllUsers();
        Task<ServiceResponse<GetUserDto>> GetUserById(string id);
        Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserDto updateuser);
        Task<ServiceResponse<GetUserDto>> DeleteUser(string id);
        Task<Pagination<GetUserDto>> GetUsersByAsync(GetAllDto getAllDto);
        //Task<ServiceResponse<List<GetUserDto>>> Search(string name, string username, string email);
        //Task<Pagination<GetUserDto>> GetUsersAsync(int page, int limit);
        //Task<ServiceResponse<List<GetUserDto>>> SortByAsync(string sortby);

    }
}
