using Business;
using DataAccess.Models;

namespace MinimalApi
{
    public static  class UserEndpoints
    {
        public static RouteGroupBuilder MapUsers(this WebApplication app, IConfiguration configuration)
        {
            var group = app.MapGroup("Api/Users");
            group.MapPost("/ValidateUser", async (User user, IUnitOfWork unitOfWork) =>
            {
                try
                {
                    ITokenService tokenService = new TokenService(configuration);
                    if (user == null) Results.BadRequest();
                    var credencial = await unitOfWork.UserBusiness.ValidateUser(user);
                    if (credencial != null)
                    {
                        var token = tokenService.BuildToken(user);
                        user.Token = token;
                        return Results.Ok(user);
                    }
                    else
                    {
                        return Results.NotFound();
                    }
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });
            return group;
        }
    }
}
