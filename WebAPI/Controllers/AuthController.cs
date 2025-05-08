using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Dtos.Auth;
using WebAPI.Models;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            // Issues in the provided code:

            /*1.Typo in the error message:
            -The error message in `BadRequest` contains a typo: "usernma" should be "username".

            2.Missing validation for `Fullname`:
               -The `Fullname` property in `RegisterDto` is not validated.If it's required, you should check for null or empty values.

            3.Password handling:
               -The `Password` property is being passed directly to the repository.Ensure that the repository handles password hashing securely.

            4.Error handling:
               -The code assumes that `RegisterAsync` will always succeed.If it fails(e.g., due to a duplicate username), there is no error handling.

            5.Return type inconsistency:
               -The `Register` method returns a success response with user details, but it might expose sensitive information(e.g., `Id`).Consider limiting the returned data.

            // Suggested solutions:

            1.Fix the typo in the error message.
            2.Add validation for `Fullname` if it's required.
            3.Ensure the repository securely hashes passwords.
            4.Add error handling for potential failures in `RegisterAsync`.
            5.Limit the returned data to avoid exposing sensitive information.*/
            if(ModelState.IsValid == false)
                return BadRequest(ModelState);
            if (string.IsNullOrEmpty(dto.Username))
                return BadRequest("username  not allow null");
            if (string.IsNullOrEmpty(dto.Password))
                return BadRequest("password not allow null");
            if (string.IsNullOrEmpty(dto.Fullname))
                return BadRequest("fullname not allow null");
            var user = new User
            {
                Username = dto.Username,
                Fullname = dto.Fullname
            };

            var result = await _authRepository.RegisterAsync(user, dto.Password);
            return Ok(new {result.Username, result.Fullname });
        }

        /*[HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _authRepository.LoginAsync(dto.Username, dto.Password);
            if (user == null)
                return Unauthorized("Invalid username or password");

            return Ok(new { user.Id, user.Username, user.Fullname });
        }*/
        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (string.IsNullOrEmpty(dto.Username) || string.IsNullOrEmpty(dto.Password))
                return BadRequest("Email and password cannot be null");
            var user = await _authRepository.LoginAsync(dto.Username, dto.Password);

            if (user == null)
                return Unauthorized("Invalid Credentials");

            var jwtSection = _configuration.GetSection("Jwt");
            var issuers = jwtSection["Issuer"];
            var audiences = jwtSection["Audience"];
            var keys = jwtSection["Key"];
            var expires = int.Parse(jwtSection["Expire"]);
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, dto.Username),
                };
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keys));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: issuers,
                audience: audiences,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expires),
                signingCredentials: signinCredentials
            );
            //return Ok(new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));
            var jwt = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return Ok(new { token = jwt });
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return BadRequest("Password is required");
            var jwtSection = _configuration.GetSection("Jwt");

            var key = jwtSection["Key"];
            var keyBytes = Encoding.UTF8.GetBytes(key);

            var hmac = new HMACSHA256(keyBytes);
            var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hash = Convert.ToBase64String(hashBytes);

            return Ok(hash);
        }

        /*[HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _authRepository.GetAllUsersAsync();
            return Ok(users);
        }*/
    }
}
