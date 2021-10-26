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
        public IActionResult GetAllCategories([FromQuery] int? RootCategoryId)
        {
            return Ok(logic.GetRootCategories(RootCategoryId));
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
            if (logic.MakeAPurchase(User.GetId(), cart)) return Ok();
            return BadRequest();
        }
    }
}