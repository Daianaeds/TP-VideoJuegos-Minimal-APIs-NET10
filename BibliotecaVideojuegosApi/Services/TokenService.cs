using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BibliotecaVideojuegosApi.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace BibliotecaVideojuegosApi.Services;

public class TokenService(IConfiguration configuration, ILogger<TokenService> logger)
{
    public TokenRespuestaDto GenerarToken(IdentityUser usuario)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, usuario.Id),
            new(JwtRegisteredClaimNames.Email, usuario.Email ?? string.Empty),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var clave = configuration["Jwt:Clave"]
            ?? throw new InvalidOperationException("No se configuró la clave JWT (Jwt:Clave).");

        var credenciales = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(clave)),
            SecurityAlgorithms.HmacSha256);

        var expiracion = DateTime.UtcNow.AddHours(2);

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: expiracion,
            signingCredentials: credenciales);

        logger.LogInformation("Token JWT generado para el usuario {UsuarioId}", usuario.Id);

        return new TokenRespuestaDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiracion = expiracion
        };
    }
}
