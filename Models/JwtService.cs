using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoginRegistration.Models
{
    public class JwtService
    {
        private readonly IConfiguration configuration;

        public string SecretKey { get; set; }
        public int TokenDuration { get; set; }

        public JwtService(IConfiguration configuration)
        {

            this.configuration = configuration;
            this.SecretKey = configuration.GetSection("jwtConfig").GetSection("Key").Value;
            this.TokenDuration = Int32.Parse(configuration.GetSection("jwtConfig").GetSection("Duration").Value);
        }

        public string GenerateToken(string id,string firstname,string lastname,string email,string gender)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.SecretKey));
            var signature = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var payload = new[]
            {
                new Claim("id",id),
                new Claim("firstname",firstname),
                new Claim("lastname",lastname),
                new Claim("email",email),
                new Claim("gender",gender),
            };

            var jwtToken = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: payload,
                expires: DateTime.Now.AddMinutes(TokenDuration),
                signingCredentials: signature
            );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
        
}
