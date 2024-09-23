using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepterDemo.Models;
using RepterDemo.Repositories;

namespace RepterDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemsRepository _orderItemsRepository;

        public OrderItemsController(IOrderItemsRepository orderItemsRepository)
        {
            _orderItemsRepository = orderItemsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItems>>> GetOrderItems()
        {
            var orderItems = await _orderItemsRepository.GetOrderItemsAsync();
            return Ok(orderItems);
        }

        [HttpGet("{orderId}/{productId}")]
        public async Task<ActionResult<OrderItems>> GetOrderItems(int orderId, int productId)
        {
            var orderItems = await _orderItemsRepository.GetOrderItemsByIdAsync(orderId, productId);
            if (orderItems == null)
            {
                return NotFound();
            }
            return Ok(orderItems);
        }

        [HttpPost]
        public async Task<ActionResult<OrderItems>> PostOrderItems(OrderItems orderItems)
        {
            await _orderItemsRepository.AddOrderItemsAsync(orderItems);
            return CreatedAtAction("GetOrderItems", new { orderId = orderItems.OrderID, productId = orderItems.ProductID }, orderItems);
        }

        [HttpPut("{orderId}/{productId}")]
        public async Task<IActionResult> PutOrderItems(int orderId, int productId, OrderItems orderItems)
        {
            if (orderId != orderItems.OrderID || productId != orderItems.ProductID)
            {
                return BadRequest();
            }

            await _orderItemsRepository.UpdateOrderItemsAsync(orderItems);
            return NoContent();
        }

        [HttpDelete("{orderId}/{productId}")]
        public async Task<IActionResult> DeleteOrderItems(int orderId, int productId)
        {
            await _orderItemsRepository.DeleteOrderItemsAsync(orderId, productId);
            return NoContent();
        }
    }
}
