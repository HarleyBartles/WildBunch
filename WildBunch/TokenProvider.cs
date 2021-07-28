using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WildBunch.Business.Entities;

namespace WildBunch
{
    public class WildBunchTokenProvider : IWildBunchTokenProvider
    {
        public string GenerateToken(WildBunchUser user)
        {
            if (user == null)
                return null;

            byte[] key;

            using (SHA256 sha256Hash = SHA256.Create())
            {
                key = sha256Hash.ComputeHash(Encoding.ASCII.GetBytes("WildBunchSecretKey"));
            };

            //Generate Token for user 
            var JWToken = new JwtSecurityToken(
                issuer: "http://localhost:44392/",
                audience: "http://localhost:44392/",
                claims: GetUserClaims(user),
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                //Using HS256 Algorithm to encrypt Token
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key),
                                    SecurityAlgorithms.HmacSha256Signature)
            );

            var token = new JwtSecurityTokenHandler().WriteToken(JWToken);
            
            return token;

        }

        private IEnumerable<Claim> GetUserClaims(WildBunchUser user)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("UserId", user.Id),
                new Claim("Email", user.Email),
                new Claim("ActiveGameId", user.ActiveGameId)
            };
            return claims;
        }
    }

    public interface IWildBunchTokenProvider
    {
        string GenerateToken(WildBunchUser user);
    }
}
