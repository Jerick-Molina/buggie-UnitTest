
using System;
using System.IdentityModel.Tokens.Jwt;
using Buggie.DataProperties;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;
namespace BuggieUnit.UnitTest.Logic {

    public class AccountAuth  {

    
    public string GenerateJwtAccessToken(User user)
    {
        var tokenHanlder = new JwtSecurityTokenHandler();
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("j79n4hfrug5c9jk1u"));
        var credit =  new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var token = new SecurityTokenDescriptor
        {
            Issuer = "",
            Expires = DateTime.UtcNow.AddHours(10),
            SigningCredentials = credit
        };

        var output = tokenHanlder.CreateEncodedJwt(token);

        return output;
    }


    public string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {   

            byte[] value;

            UTF8Encoding encoder = new UTF8Encoding();

            //String hashing
            value = sha256.ComputeHash(encoder.GetBytes(password));


            //putting the encoding into a readble string
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < value.Length; i++)
            {

                builder.Append(value[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }


    public bool PasswordAuthentication(string password, string hashed)
    {

        var hash = HashPassword(password);

        if (hash == hashed)
        {
            return true;
        }
        return false;
    }

    }
}