using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MS_Authentication.Application.PaginationModel;
using MS_Authentication.Application.Responses;

namespace MS_Authentication.Application.Services;

public class BaseService
{
    private HttpContext? _context;
    protected ILogger<BaseService> logger;
    public BaseService(IHttpContextAccessor contextAccessor, ILogger<BaseService> logger)
    {
        _context = contextAccessor.HttpContext;
        this.logger = logger;
    }

    public static Response ReturnReponse(string status, bool error)
    {
        return new Response
        {
            Status = status,
            Error = error
        };
    }

    public static Response ReturnResponseSuccess()
    {
        return new Response
        {
            Status = "Success",
            Error = false
        };
    }

    protected async Task<Response> ExecuteValidationResponseAsync<T>(IValidator<T> validator, T entity)
    {
        var result = await validator.ValidateAsync(entity);
        return result.IsValid ? ReturnResponseSuccess() : ReturnReponse(result.ToString(), true);
    }

    protected Guid GetUserLogged()
    {
        try
        {
            var identity = _context?.User.Identity as ClaimsIdentity;
            var userId = identity?.FindFirst("UserId")?.Value;
            return userId != null ? Guid.Parse(userId) : Guid.Empty;
        }
        catch (Exception)
        {
            return Guid.Empty;
        }    
    }

    protected static DateTime DateHourNow() => DateTime.UtcNow.AddHours(-3);

    protected static Pagination<T> Page<T>(IEnumerable<T> itens, int page, int pageSize)
    {
        var quantityOfPages = (int)Math.Ceiling((double)itens.Count() / pageSize);

        var list = itens
            .Skip((page -1 ) * pageSize)
            .Take(pageSize)
            .ToList();

        return new Pagination<T>
        {
            QuantityOfPages = quantityOfPages,
            TotalItens = list.Count(),
            CurrentPage = page,
            Itens = list
        };
    }
}
