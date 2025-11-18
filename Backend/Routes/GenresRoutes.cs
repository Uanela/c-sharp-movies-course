using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Entities;

namespace Backend.Routes
{
    public static class GenresRoutes
    {
        public static RouteGroupBuilder MapGenreRoutes(this WebApplication app)
        {
            var group = app.MapGroup("genres").WithParameterValidation();

            group.MapGet("/", async (MovieContext ctx) => await ctx.Genres.ToListAsync());

            return group;
        }
    }
}
