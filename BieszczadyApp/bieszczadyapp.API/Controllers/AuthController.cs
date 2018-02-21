using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using bieszczadyapp.API.Data;
using bieszczadyapp.API.Dtos;
using bieszczadyapp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace bieszczadyapp.API.Controllers
{
   
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo ,IConfiguration config )
        {
            _config = config;
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserForRegisterDto userForRegisterDto)
        {
            if(string.IsNullOrEmpty(userForRegisterDto.Username))
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();
           
            if(await _repo.UserExist(userForRegisterDto.Username))
                ModelState.AddModelError("Username" , "Nick jest juz zajety");

            //validation reqeust
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

           
            var userToCreate = new User
            {
                Username = userForRegisterDto.Username
            };
            var createUser = await _repo.Register(userToCreate,userForRegisterDto.Password);

            return StatusCode(201);

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserForLoginDto userForLoginDto)
        {
        var userFromRepo =await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

        if(userFromRepo == null)
        return Unauthorized();

        var tokernHandelr = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value);
        var tokenDescription = new SecurityTokenDescriptor 
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name,userFromRepo.Username)
            }),
            Issuer="",
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha512Signature)

        };
        var token = tokernHandelr.CreateToken(tokenDescription);
        var tokenString = tokernHandelr.WriteToken(token);

        return Ok(new{tokernHandelr});
        }
    }
}