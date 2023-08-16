using Employee_CleanPattern.DTOs;
using Employee_CleanPattern.Helper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Solution.Core.Features.UsersCQ.Commands;
using Solution.Core.Features.UsersCQ.Queries;
using Solution.Core.Features.UsersCQ.ViewModels;
using Solution.Domain.Entities._User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Employee_CleanPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _iconfiguration;

        public UsersController(IMediator mediator, IWebHostEnvironment webHostEnvironment, IConfiguration iconfiguration)
        {
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
            _iconfiguration = iconfiguration;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> PostUser(AddUserVM data)
        {
            var res = await _mediator.Send(new CreateUserCommand(data));
            if (res > 0)
            {
                return Ok(res);
            }
            return BadRequest("Invalid Data!!! Please Enter Valid Data.");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> LoginUser(LoginUserVM data)
        {
            var res = await _mediator.Send(new LoginUserQuery(data));
            if (res != null)
            {
                CreateJwtToken obj = new CreateJwtToken(_iconfiguration);
                var token = obj.CreateToken(res.User_Id, res.User_Name);
                return Ok(new
                {
                    message = "Success",
                    token = token,
                    user = res
                }); ;
            }
            return BadRequest("Invalid Credentials!!! Please Enter valid User_Name And Password");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadUserProfile([FromForm]UploadImageDto data)
        {
            var dbUser = await _mediator.Send(new GetUserByIdQuery() { User_Id = data.Id });
            if (dbUser != null)
            {
                if (data.File != null)
                {
                    UploadUserProfile obj = new UploadUserProfile(_webHostEnvironment);
                    var path = obj.Upload(data.Id, data.File);
                    var res = await _mediator.Send(new AddProfilePathCommand(data.Id, path));
                    if (res > 0)
                    {
                        return Ok(res);
                    }
                    return BadRequest(new
                    {
                        message = "Failed"
                    });
                }
                return BadRequest(new
                {
                    message = "Invalid Request!!! Please Enter Valid Details"
                });
            }
            return BadRequest(new
            {
                message = $"User not found with User Id : {data.Id}"
            });
        }

        /*[NonAction]
        private string CreateJwtToken(ReturnUserVM user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"]);
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.User_Name),
                new Claim("Id",user.User_Id.ToString())
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = credentials
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            return jwtTokenHandler.WriteToken(token);
        }*/
    }
}