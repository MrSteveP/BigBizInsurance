using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using BigBizInsurance.Api.Entities;
using BigBizInsurance.Api.Helpers;
using BigBizInsurance.Api.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Services.Abstraction;

namespace BigBizInsurance.Api.Services
{
    public class ApiUserService : IApiUserService
    {
        private readonly AppSettings _appSettings;

        public ApiUserService(IOptions<AppSettings> appSettings, IUsersService usersService)
        {
            _appSettings = appSettings.Value;
            UsersService = usersService;
        }

        public IUsersService UsersService { get; }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var isValidUser = await UsersService.IsValidUser(model.Username, model.Password);
            // return null if user not found
            if (!isValidUser) return null;

            var user = new UserDto() { Username = model.Username, Password = model.Password };
            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        // helper methods

        private string generateJwtToken(UserDto user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
