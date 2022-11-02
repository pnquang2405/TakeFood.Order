using Order.Model.Entities.User;
using Order.Model.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Order.Service.Implement;


public class JwtService : IJwtService
{
    private readonly string _secret;
    private readonly string _secret2;
    private readonly string _expDate;
    private readonly string _expMonth;

    /// <summary>
    /// UserRepository
    /// </summary>
    private readonly IMongoRepository<UserRefreshToken> userRefreshTokenRepository;

    public JwtService(IMongoRepository<UserRefreshToken> userRefreshTokenRepository, string secret, string secret2,
        int expDate, int expMonth)
    {
        this.userRefreshTokenRepository = userRefreshTokenRepository;
        _secret = secret;
        _secret2 = secret2;
        _expDate = expDate.ToString();
        _expMonth = expMonth.ToString();
    }

    public string GenerateRefreshToken(string user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_secret2);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                    new Claim("UId", user.ToString())
                }),
            IssuedAt = DateTime.Now,
            Expires = DateTime.UtcNow.AddMonths(Int32.Parse(_expMonth)),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        userRefreshTokenRepository.InsertAsync(new UserRefreshToken()
        {
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            Token = tokenHandler.WriteToken(token),
            UserId = user,
        });
        return tokenHandler.WriteToken(token);
    }

    public string GenerateSecurityToken(string id, IList<String> roles)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("UId", id),
                new Claim("Roles", JsonSerializer.Serialize(roles))
            }),
            IssuedAt = DateTime.Now,
            Expires = DateTime.UtcNow.AddHours(double.Parse(_expDate)),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GetId(string token)
    {
        try
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            return jwtToken.Claims.FirstOrDefault(claim => claim.Type == "UId").Value;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new Exception(e.Message);
        }
    }
    public List<string> GetRoles(string token)
    {
        try
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            return JsonSerializer.Deserialize<List<String>>(jwtToken.Claims.FirstOrDefault(claim => claim.Type == "Roles").Value);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new Exception(e.Message);
        }
    }

    public bool ValidRefreshToken(String _token)
    {
        try
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = tokenHandler.ReadToken(_token) as JwtSecurityToken;
            UserRefreshToken token = new UserRefreshToken
            {
                Token = _token,
                UserId = GetId(_token)
            };
            var key = Encoding.UTF8.GetBytes(_secret2);
            tokenHandler.ValidateToken(token.Token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true
            }, out SecurityToken validatedRefreshToken);
            var t = userRefreshTokenRepository.Find(x => x.Token.Equals(token.Token));
            if (t == null) throw new Exception("Token doesn't exist");
            return true;
        }
        catch (SecurityTokenExpiredException e)
        {
            Console.WriteLine(e.Message);
            throw new Exception(e.Message);
        }
    }

    public bool ValidSecurityToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secret);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true
            }, out SecurityToken validatedToken);
            return true;
        }
        catch (SecurityTokenExpiredException e)
        {
            throw new Exception(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new Exception(e.Message);
        }
    }
}
