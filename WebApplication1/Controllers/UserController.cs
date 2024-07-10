using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dtos.GetAll;
using WebApplication1.Dtos.User;
using WebApplication1.Services.UserService;


namespace WebApplication1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private readonly IUserService _userservice;


        public UserController(IUserService userService)
        {
            _userservice = userService;
           
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllDto getAllDto)
        {
            try
            {
                return Ok(await _userservice.GetUsersByAsync(getAllDto));
            }
            catch (Exception exp) {
                return BadRequest(exp.Message); 
            }
        }

        //[HttpGet]
        //public async Task<IActionResult> Get(string sortby)
        //{

        //    return Ok(await _userservice.SortByAsync(sortby));
        //}

        [HttpGet("id")]
        public async Task<IActionResult> GetById(string id) {
            try
            {
                return Ok(await _userservice.GetUserById(id));
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserDto updateuser)
        {
            try
            {
                return Ok(await _userservice.UpdateUser(updateuser));
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                return Ok(await _userservice.DeleteUser(id));
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

        //[HttpGet("Search")]

        //public async Task<IActionResult> Search(string name = "", string username = "" , string email = "")
        //{
        //    return Ok(await _userservice.Search(name, username, email));
        //}
        //[HttpGet("pagination")]
        //public async Task<IActionResult> GetUsersAsync(int page, int limit)
        //{
        //    return Ok(await _userservice.GetUsersAsync(page, limit));
        //}
    }
}
