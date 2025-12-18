using FoodOrderingSystem.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuItemsController : ControllerBase
    {
        private readonly IMenuItemRepository _menuItemRepository;

        public MenuItemsController(IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
        }

        /// <summary>
        /// Get menu items by restaurant ID
        /// </summary>
        [HttpGet("restaurant/{restaurantId:int}")]
        public async Task<ActionResult> GetMenuItemsByRestaurant(int restaurantId)
        {
            var menuItems = await _menuItemRepository.GetByRestaurantIdAsync(restaurantId);
            return Ok(menuItems);
        }
    }
}