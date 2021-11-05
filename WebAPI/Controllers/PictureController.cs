using System;
using System.Collections.Generic;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PictureController : ControllerBase
    {
        private readonly IItemLogic itemLogic;

        public PictureController(IItemLogic itemLogic)
        {
            this.itemLogic = itemLogic;
        }

        IActionResult getHelper(Func<byte[]> getter)
        {
            byte[] data;
            try
            {
                data = getter();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();   
            }
            catch (IndexOutOfRangeException)
            {
                return BadRequest();
            }
            
            if (data == null) return Accepted();
            return File(data, "image/png");
        }

        /// <summary>
        ///     Get the icon of the item that appears in search results
        /// </summary>
        [HttpGet("item/icon")]
        public IActionResult GetIconOfItem([FromQuery] int ItemId, [FromQuery] DateTime? cacheTime)
        {
            return getHelper(() => itemLogic.GetItemIcon(ItemId, cacheTime));
        }

        /// <summary>
        ///     Get an image of the item specified by the idx.
        /// </summary>
        /// <remarks>
        ///     The count of items is supplied in the result of [GET api/item]
        /// </remarks>
        [HttpGet("item/image")]
        public IActionResult GetImageOfItem([FromQuery] int ItemId, [FromQuery] int Idx, [FromQuery] DateTime? cacheTime)
        {
            return getHelper(() => itemLogic.GetPictureOfItem(ItemId, Idx, cacheTime));
        }

        /// <summary>
        ///     Gets the icon of the model
        /// </summary>
        [HttpGet("model/icon")]
        public IActionResult GetCarModelIcon([FromQuery] int carModelId, [FromQuery] DateTime? cacheTime)
        {
            return getHelper(() => itemLogic.GetCarModelIcon(carModelId, cacheTime));
        }
    }
}