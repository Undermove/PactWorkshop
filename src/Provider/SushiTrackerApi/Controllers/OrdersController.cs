using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SushiTrackerApiContracts;

namespace SushiTrackerApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class OrdersController : ControllerBase
	{
		const int MinimalRollsCount = 6;
		const int FullRollPrice = 39;
		const int DiscountRollPrice = 9;

		private readonly ILogger<OrdersController> _logger;
		
		public OrdersController(ILogger<OrdersController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public IActionResult Get()
		{
			return Ok();
		}
		
		[HttpPost]
		public IActionResult Create([FromBody] CreateOrderRequest createOrderRequest)
		{
			if (createOrderRequest.RollsCount < MinimalRollsCount)
			{
				return BadRequest($"Rolls count is low. Minimal rolls count is {MinimalRollsCount}");
			}

			if (createOrderRequest.IsMobileApp)
			{
				return new JsonResult(new CreateOrderResponse()
				{
					OrderPrice = createOrderRequest.RollsCount * DiscountRollPrice
				});
			}
			
			return new JsonResult(new CreateOrderResponse()
			{
				OrderPrice = createOrderRequest.RollsCount * FullRollPrice
			});
		}
	}
}