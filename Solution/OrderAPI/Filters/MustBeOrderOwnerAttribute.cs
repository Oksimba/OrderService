using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;

namespace OrderAPI.Filters
{
    public class MustBeOrderOwnerAttribute : TypeFilterAttribute
    {
        public MustBeOrderOwnerAttribute() : base(typeof(MustBeOrderOwnerFilter))
        {
        }
    }

    public class MustBeOrderOwnerFilter : IAuthorizationFilter
    {
        private readonly IOrderService _orderService;

        public MustBeOrderOwnerFilter(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userId = context.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            var orderId = context.HttpContext.Request.RouteValues["id"]?.ToString();

            if (!_orderService.IsUserOrderOwner(int.Parse(userId), int.Parse(orderId))) 
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
