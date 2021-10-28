using System;
using System.Collections.Generic;
using BusinessLogic.Initializers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using Services.Extensions;

namespace WebAPI.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemLogic logic;

        public ItemController(IItemLogic logic)
        {
            this.logic = logic;
        }

        /// <summary>
        ///     Returns all categories in the system
        /// </summary>
        [ProducesResponseType(200, Type = typeof(IEnumerable<ItemCategoryResultDTO>))]
        [HttpGet("categories")]
        public IActionResult GetAllCategories()
        {
            return Ok(logic.GetRootCategories());
        }

        /// <summary>
        ///     Returns all car models in the system
        /// </summary>
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarModelResultDTO>))]
        [HttpGet("models")]
        public IActionResult GetCarModels()
        {
            return Ok(logic.GetCarModels());
        }

        /// <summary>
        ///     Returns all items that belong to a category
        /// </summary>
        [ProducesResponseType(200, Type = typeof(IEnumerable<ItemResultDTO>))]
        [HttpGet]
        public IActionResult GetItemsInCategory([FromQuery] int? CategoryId, [FromQuery] int? CarModelId)
        {
            var res = logic.GetItemsIn(CategoryId, CarModelId);
            if (res == null) return BadRequest();
            return Ok(res);
        }

        /// <summary>
        ///     Buy an item
        /// </summary>
        [HttpPost("buy")]
        public IActionResult MakePurchase([FromBody] IEnumerable<ItemPurchaseRequest> cart)
        {
            try
            {
                if (logic.MakeAPurchase(User.GetId(), cart)) return Ok();
                return Accepted();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
        }
    }
}