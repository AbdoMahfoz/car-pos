using System;
using System.Collections.Generic;
using BusinessLogic.Initializers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;

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
        [HttpGet("Categories")]
        public IActionResult GetAllCategories([FromQuery] int? RootCategoryId)
        {
            return Ok(logic.GetRootCategories(RootCategoryId));
        }

        /// <summary>
        ///     Returns all car models in the system
        /// </summary>
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarModelResultDTO>))]
        [HttpGet("CarModels")]
        public IActionResult GetCarModels([FromQuery] DateTime? cacheTime)
        {
            return Ok(logic.GetCarModels(cacheTime));
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
        ///     Returns the full details of an item
        /// </summary>
        [HttpGet("Detail")]
        public IActionResult GetItemDetail([FromQuery] int ItemId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Returns a picture for the item
        /// </summary>
        /// <param name="ItemId">The Id of the item</param>
        /// <param name="PictureIdx">0: Icon, 1: Cover, 2-N: Extra pictures</param>
        [HttpGet("Picture")]
        public IActionResult GetItemPicture([FromQuery] int ItemId, [FromQuery] int PictureIdx)
        {
            throw new NotImplementedException();
        }
    
        /// <summary>
        ///     Buy an item
        /// </summary>
        [HttpPost("Buy")]
        public IActionResult MakeAPurchase([FromQuery] int ItemId)
        {
            throw new NotImplementedException();
        }
    }
}