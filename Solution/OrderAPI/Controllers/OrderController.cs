using AutoMapper;
using BusinessLogic.Interfaces;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderAPI.Filters;
using System.IdentityModel.Tokens.Jwt;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderService _orderService;
        IMapper _mapper;
        ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, IMapper mapper, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _mapper = mapper;
            _logger = logger;
        }


        /// <summary>
        /// Get list of all orders for user
        /// </summary>
        /// <returns>Returns IEnumerable<CardDto></CardDto></returns>
        [HttpGet(Name = "GetAllOrders")]
        [Authorize("MustBeOrderListOwner")]
        public ActionResult<IEnumerable<OrderDto>> GetAll(int userId)
        {
            var orders = _orderService.GetAll(userId);
            return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
        }

        /// <summary>
        /// Get order by id
        /// </summary>
        /// <param name="id">The id of the order to get</param>
        /// <returns>ActionResult<OrderDto></OrderDto>></returns>
        /// <response code = "200">Returns the requested order</response>
        [HttpGet("{id}", Name = "GetOrder")]
        [MustBeOrderOwner]
        public async Task<ActionResult<OrderDto>> Get(int id)
        {
            try
            {
                var order = await _orderService.Get(id);

                if (order == null)
                    return NotFound($"Order with id: {id} doesn`t exists.");

                return Ok(_mapper.Map<OrderDto>(order));
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogCritical(
                    $"Exception while hetting order by id {id}.");

                return StatusCode(500, $"A problem happened while handling your request.{ex.Message}");
            }
        }

        /// <summary>
        /// Create new order
        /// </summary>
        /// <param name="order">New order</param>
        /// <returns>returns IActionResult</returns>
        [Route("signup")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] OrderCreateDto order)
        {
            try
            {
                if (order == null)
                    return BadRequest();

                var orderToCreate = _mapper.Map<Order>(order);
                await _orderService.Create(orderToCreate);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Update card
        /// </summary>
        /// <param name="id">The id of the order to update</param>
        /// <param name="updatedStatus">status</param>
        /// <param name="updatedProfit">profit</param>
        /// <param name="updatedClosePrice">price</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [MustBeOrderOwner]
        public async Task<IActionResult> Update(int id, [FromBody] OrderUpdateDto updatedOrder)
        {
            if (updatedOrder == null)
                return BadRequest();

            var card = await _orderService.Get(id);

            if (card == null)
                return NotFound($"Order with id: {id} doesn`t exists.");

            var orderToUpdate = _mapper.Map<Order>(updatedOrder);
            await _orderService.Update(id, orderToUpdate);

            return NoContent();
        }

        /// <summary>
        /// Delete order by id
        /// </summary>
        /// <param name="id">The id of order to delete</param>
        /// <returns>Returns deleted order</returns>
        [HttpDelete("{id}")]
        [MustBeOrderOwner]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedOrder = await _orderService.Delete(id);

            if (deletedOrder == null)
                return NotFound();

            _logger.LogInformation($"Order with id {id} was successfully deleted.");
            return new ObjectResult(deletedOrder);
        }
    }
}
