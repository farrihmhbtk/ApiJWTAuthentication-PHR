using JwtAuthenticationManager.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthenticationManager
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "yPkCqn4kSWLtaJwXvN2jGzpQRyTZ3gdXkt7FeBJP";
        private const int JWT_TOKEN_VALIDITY_MINS = 20;
        private readonly List<UserAccount> _userAccountList;

        public JwtTokenHandler()
        {
            _userAccountList = new List<UserAccount>
            {
                new UserAccount{ UserName = "user1", Password = "user1", Role =  new List<string> 
                {
                   "Well-RW", "Equipment-RW"                } 
                },
                new UserAccount{ UserName = "user2", Password = "user2", Role =  new List<string>
                {
                   "Well-RO"
                }
                },
                new UserAccount{ UserName = "user3", Password = "user3", Role =  new List<string>
                {
                   "Equipment-RO"
                }
                },
                new UserAccount{ UserName = "user4", Password = "user4", Role =  new List<string>
                {
                   "Well-RW"
                }
                },
            };
        }

        public AuthenticationResponse? GenerateJwtToken(AuthenticationRequest authenticationRequest)
        {
            if (string.IsNullOrWhiteSpace(authenticationRequest.UserName) || string.IsNullOrWhiteSpace(authenticationRequest.Password))
                return null;

            /* Validation */
            var userAccount = _userAccountList.Where(x => x.UserName == authenticationRequest.UserName && x.Password == authenticationRequest.Password).FirstOrDefault();
            if (userAccount == null) return null;

            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var _claimsList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, authenticationRequest.UserName)
            };
            foreach (var role in userAccount.Role)
            {
                _claimsList.Add(new Claim("Role", role));
            }
            var claimsIdentity = new ClaimsIdentity(_claimsList);
            /*
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, authenticationRequest.UserName),
                //new Claim("Role", userAccount.Role)
                foreach ( var role in userAccount.Role )
                {
                new Claim("Role", role);
                }
                
            });
            */

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new AuthenticationResponse
            {
                UserName = userAccount.UserName,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                JwtToken = token
            };
        }
    }
}
