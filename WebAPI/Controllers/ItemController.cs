using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        /// <summary>
        ///     Returns all categories in the system
        /// </summary>
        [HttpGet("Categories")]
        public IActionResult GetAllCategories()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Returns all items that belong to a category
        /// </summary>
        [HttpGet]
        public IActionResult GetItemsInCategory([FromQuery] int CategoryId)
        {
            throw new NotImplementedException();
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
        ///     But an item
        /// </summary>
        [HttpPost("Buy")]
        public IActionResult MakeAPurchase([FromQuery] int ItemId)
        {
            throw new NotImplementedException();
        }
    }
}