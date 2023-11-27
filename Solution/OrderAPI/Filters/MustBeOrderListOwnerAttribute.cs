using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;

namespace OrderAPI.Filters
{
    public class MustBeOrderListOwnerAttribute : TypeFilterAttribute
    {
        public MustBeOrderListOwnerAttribute() : base(typeof(MustBeOrderListOwnerFilter))
        {
        }
    }

    public class MustBeOrderListOwnerFilter : IAuthorizationFilter
    {
        private readonly IOrderService _orderService;

        public MustBeOrderListOwnerFilter(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userId = context.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            var requestUserId = context.HttpContext.Request.Query["userId"].ToString();

            if (userId != requestUserId)
            {
                context.Result = new ForbidResult();

            }
        }
    }
}
