using _14_4_CodeFirst_WebApi_LibraryDb.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace _14_4_CodeFirst_WebApi_LibraryDb.Security
{
    public static class TokenHandler
    {
        public static Token CreateToken(User user, IConfiguration configuration)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Role,"Admin")
            };

            Token token = new Token();

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:KEY"]));

            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            token.Expiration = DateTime.Now.AddMinutes(Convert.ToDouble(configuration["JWT:Expiration"]));

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: configuration["JWT:Issuer"], audience: configuration["JWT:Audience"], expires: token.Expiration, claims: claims, notBefore: DateTime.Now, signingCredentials: credentials);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            token.AccessToken = tokenHandler.WriteToken(jwtSecurityToken);

            return token;

        }
    }
}
