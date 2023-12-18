using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsOrder_ASP.Net_API.Models;
using ProductsOrder_ASP.Net_API.Models.Dtos;
using ProductsOrder_ASP.Net_API.Services;
using ProductsOrder_ASP.Net_API.Services.IServices;


namespace ProductsOrder_ASP.Net_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUser _userService;
        private readonly IJwt _jwtservice;
        public UserController(IMapper mapper, IUser user, IJwt jwt)
        {
            _mapper = mapper;
            _userService = user;
            _jwtservice = jwt;

        }

        [HttpPost("Register")]
        public async Task<ActionResult<string>> AddUser(AddUserDto addUserDto)
        {
            var user = _mapper.Map<Users>(addUserDto);

            user.Password = BCrypt.Net.BCrypt.HashPassword(addUserDto.Password);
            var checkUser = await _userService.GetUserByEmail(addUserDto.Email);
            if (checkUser != null)
            {
                return BadRequest("Email Already exists");
            }
            var response = await _userService.RegisterUser(user);
            return Ok(response);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<string>> LogInUser(LogUserDto user)
        {
           
            var checkUser = await _userService.GetUserByEmail(user.Email);
            if (checkUser == null)
            {
                return BadRequest("invalid credentials");
            }
            var isCorrect = BCrypt.Net.BCrypt.Verify(user.Password, checkUser.Password);
            if (!isCorrect)
            {
                return BadRequest("Invalid Credentials");
            }
            var token = _jwtservice.GenerateToken(checkUser);
            return Ok(token);
        }


    }
}
