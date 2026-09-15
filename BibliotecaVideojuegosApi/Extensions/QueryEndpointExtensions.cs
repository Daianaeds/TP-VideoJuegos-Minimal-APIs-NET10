namespace BibliotecaVideojuegosApi.Extensions;

// .NET 10 no trae un MapQuery propio — solo las primitivas: la constante del método
// (HttpMethods.Query) y MapMethods, que registra un endpoint para cualquier método HTTP.
// Este helper envuelve esas primitivas para que el endpoint se lea igual de limpio
// que un MapGet o un MapPost:  app.MapQuery("/ruta", Handler)
public static class QueryEndpointExtensions
{
    public static RouteHandlerBuilder MapQuery(this IEndpointRouteBuilder app, string pattern, Delegate handler)
        => app.MapMethods(pattern, [HttpMethods.Query], handler);
}
