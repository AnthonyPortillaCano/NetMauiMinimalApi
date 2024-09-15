using Business;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using MinimalApi.Constants;
namespace MinimalApi
{
    public static class ProductEndpoints
    {
        public static RouteGroupBuilder MapProducts(this WebApplication app, IConfiguration configuration)
        {
            var group = app.MapGroup("Api/Product");
            group.MapGet("/List", [Authorize] async (IUnitOfWork unitOfWork) =>
            {
                try
                {
                    var result = await unitOfWork.ProductBusiness.ListProduct();
                    if (result == null) { return Results.NotFound(); }
                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });
            group.MapGet("/Detail/{id}", [Authorize]  async (int id,IUnitOfWork unitOfWork) =>
            {
                try
                {
                    var result = await unitOfWork.ProductBusiness.GetProductById(id);
                    if (result == null) { return Results.NotFound(); };
                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });
            group.MapPost("/Save", [Authorize] async (Product product,IUnitOfWork unitOfWork) =>
            {
                var respuestaTransaccion = new RespuestaTransaccionDto();
                try
                {
                    await unitOfWork.ProductBusiness.Insert(product);
                    var result = await unitOfWork.CommitAsync();
                    if (result <= 0)
                    {
                        respuestaTransaccion.Resultado = Mensaje.CODE_ERROR;
                        respuestaTransaccion.Descripcion = Mensaje.ERROR_TRANSACTION;
                        return Results.BadRequest(respuestaTransaccion);
                    }
                    respuestaTransaccion.Resultado = Mensaje.CODE_SUCCESS;
                    respuestaTransaccion.Descripcion = Mensaje.TRANSACTION_SUCCESS;

                    return Results.Ok(respuestaTransaccion);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            group.MapPut("/Update", [Authorize] async (Product product, IUnitOfWork unitOfWork) =>
            {
                var respuestaTransaccion = new RespuestaTransaccionDto();
                try
                {
                    if (product == null) return Results.BadRequest();
                    unitOfWork.ProductBusiness.Update(product);
                    var result = await unitOfWork.CommitAsync();
                    if (result <= 0)
                    {
                        respuestaTransaccion.Resultado = Mensaje.CODE_ERROR;
                        respuestaTransaccion.Descripcion = Mensaje.ERROR_TRANSACTION;
                        return Results.BadRequest(respuestaTransaccion);
                    }
                    respuestaTransaccion.Resultado = Mensaje.CODE_SUCCESS;
                    respuestaTransaccion.Descripcion = Mensaje.TRANSACTION_SUCCESS;

                    return Results.Ok(respuestaTransaccion);
                }
                catch (Exception ex)
                {
                    respuestaTransaccion.Resultado = Mensaje.CODE_ERROR;
                    respuestaTransaccion.Descripcion = Mensaje.ERROR_TRANSACTION + " " + ex.Message + ex.StackTrace;
                    return Results.BadRequest(respuestaTransaccion);
                }

            });

            group.MapDelete("/Delete/{id}", [Authorize] async (int id, IUnitOfWork unitOfWork) =>
            {
                var respuestaTransaccion = new RespuestaTransaccionDto();
                try
                {
                    await unitOfWork.ProductBusiness.Delete(id);

                    respuestaTransaccion.Resultado = Mensaje.CODE_SUCCESS;
                    respuestaTransaccion.Descripcion = Mensaje.TRANSACTION_SUCCESS;

                    return Results.Ok(respuestaTransaccion);
                }
                catch (Exception ex)
                {
                    respuestaTransaccion.Resultado = Mensaje.CODE_ERROR;
                    respuestaTransaccion.Descripcion = Mensaje.ERROR_TRANSACTION + " " + ex.Message + ex.StackTrace;
                    return Results.BadRequest(respuestaTransaccion);
                }

            });
            return group;
        }
    }
}
