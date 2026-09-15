using BibliotecaVideojuegosApi.DTOs;
using BibliotecaVideojuegosApi.Services;
using Microsoft.AspNetCore.Identity;

namespace BibliotecaVideojuegosApi.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var groupAuth = app.MapGroup("/auth");

        groupAuth.MapPost("/registro", Registro);
        groupAuth.MapPost("/login", Login);

        static async Task<IResult> Registro(RegistroDto request, UserManager<IdentityUser> userManager)
        {
            var usuario = new IdentityUser
            {
                UserName = request.Email,
                Email = request.Email
            };

            var resultado = await userManager.CreateAsync(usuario, request.Password);

            if (!resultado.Succeeded)
            {
                return TypedResults.BadRequest(resultado.Errors.Select(e => e.Description));
            }

            return TypedResults.Created($"/auth/{usuario.Id}", new { usuario.Id, usuario.Email });
        }

        static async Task<IResult> Login(LoginDto request, UserManager<IdentityUser> userManager, TokenService tokenService)
        {
            var usuario = await userManager.FindByEmailAsync(request.Email);

            if (usuario is null || !await userManager.CheckPasswordAsync(usuario, request.Password))
            {
                return TypedResults.Unauthorized();
            }

            var token = tokenService.GenerarToken(usuario);
            return TypedResults.Ok(token);
        }
    }
}
