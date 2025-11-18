using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Entities;

namespace Backend.Routes
{
    public static class MovieRoutes
    {
        public static RouteGroupBuilder MapMovieRoutes(this WebApplication app)
        {
            var group = app.MapGroup("movies");

            group.MapGet("/", async (MovieContext context) => await context.Movies.Include("Genre").ToListAsync());
            group.MapGet("/{id}", async (MovieContext context, int id) => {
                Movie? movie = await context.Movies.Include("Genre").FirstOrDefaultAsync(x => x.Id == id);
                return movie is null ? Results.NotFound() : Results.Ok(movie);
            });

            group.MapPost("/", async (Movie data, MovieContext ctx) => {
                data.Genre = await ctx.Genres.FirstOrDefaultAsync(x => x.Id == data.GenreId);
                ctx.Movies.Add(data);
                await ctx.SaveChangesAsync();
                return Results.Created($"/movies/{data.Id}", data);
            });


            group.MapPatch("/{id}", async (int id, Movie data, MovieContext ctx) => 
            {
                Movie? movie = await ctx.Movies.FindAsync(id);

                if(movie is null)
                {
                    return Results.NotFound();
                }


                foreach (var prop in data.GetType().GetProperties())
                {
                    var value = prop.GetValue(data);
                    

                    if (value == null || 
                        (prop.PropertyType == typeof(int) && (int)value == 0) ||
                        (prop.PropertyType == typeof(decimal) && (decimal)value == 0) ||
                        (prop.PropertyType == typeof(DateOnly) && (DateOnly)value == default(DateOnly)) ||
                        (value == default) ||
                        (prop.PropertyType == typeof(string) && string.IsNullOrEmpty((string)value)))
                    {
                        continue;
                    }
                    
               
                    var targetProp = movie.GetType().GetProperty(prop.Name);
                    if (targetProp != null && targetProp.CanWrite && targetProp.Name != "Id")
                    {
                        Console.WriteLine("---");
                        Console.WriteLine(targetProp.Name);
                        Console.WriteLine(value);
                        Console.WriteLine("---");
                        targetProp.SetValue(movie, value);
                    }
                }

                ctx.Movies.Update(movie);
                await ctx.SaveChangesAsync();

                return Results.NoContent();

            });
        
            group.MapDelete("/{id}", async (int id, MovieContext ctx) => {
                Movie? movie = await ctx.Movies.FindAsync(id);
                if (movie is null){
                    return Results.NoContent();
                }

                ctx.Movies.Remove(movie);
                await ctx.SaveChangesAsync();

                return Results.NoContent();

            });


            return group;

        }
    }
}
