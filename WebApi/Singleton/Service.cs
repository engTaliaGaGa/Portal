using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WebApi.Singleton.Interface;

namespace WebApi.Singleton
{
    public class AuthService : IAuthService
    {
        public static string Issuer { get; set; }
        public static string Audience { get; set; }
        public static string SigningKey { get; set; }

        public bool ValidateLogin(string user, string pass)
        {
            //using var logic = new Logic.Usuario.Usuario();
            //return logic.Validar(user, pass);
            return true;
        }

        public string GenerateToken(DateTime date, string user, int? idApp, TimeSpan validDate)
        {
            //Entity.Usuario usuario = null;
            var expire = date.Add(validDate);
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(
                    JwtRegisteredClaimNames.Iat,
                    new DateTimeOffset(date).ToUniversalTime().ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64
                ),
                //new Claim("roles","Cliente"),
                //new Claim("roles","Administrador"),
                //new Claim("id", usuario.IdUsuario),
                //new Claim("user", usuario.Usuario),
                //new Claim("name", $"{usuario.NombreUsuario} {usuario.ApPatUsuario} {usuario.ApMatUsuario}"),
                //new Claim("data", JsonConvert.SerializeObject(usuario, new JsonSerializerSettings()
                //    {
                //        PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                //        Formatting = Formatting.Indented
                //    })
                //),
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(SigningKey)
                ),
                SecurityAlgorithms.HmacSha256Signature
            );

            var jwt = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                notBefore: date,
                expires: expire,
                signingCredentials: signingCredentials
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }
    }
}
