using System.Diagnostics;
using inSync.Models;
using inSync.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace inSync.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize()]
    public class ItemListController: ControllerBase
    {
        private readonly ItemListService _service;
        private readonly IConfiguration _configuration;

        private readonly string NotFoundResponse = "No list found";

        public ItemListController(ItemListService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }

        [HttpGet("{token:guid}")]
        public async Task<ActionResult<List<MinecraftItem>>> GetItemList(Guid token)
        {
            var list = await _service.Get(token);

            if (IsListInvalid(list))
            {
                return NotFound(NotFoundResponse);
            }

            Debug.Assert(list != null, nameof(list) + " != null");
            return list.IsExpired ? Ok("List expired") : Ok(list);
        }
        
        [HttpPost]
        public async Task<ActionResult<List<MinecraftItem>>> CreateItemList(ItemListDTO request)
        {
            ItemList itemList = new()
            {
                IsExpired = false,
                Items = request.Items,
                Username = request.Username
            };

            await _service.AddItemList(itemList);
            return CreatedAtAction(nameof(GetItemList), new {token = itemList}, itemList);
        }

        [HttpPut("{token:guid}")]
        public async Task<ActionResult> UpdateItemList(Guid token, ItemListDTO request)
        {
            var itemList = await _service.Get(token);
            if (IsListInvalid(itemList))
            {
                return NotFound(NotFoundResponse);
            }

            Debug.Assert(itemList != null, nameof(itemList) + " != null");
            itemList.Items.AddRange(request.Items);
            await _service.UpdateItemList(token, itemList);

            return NoContent();
        }

        [HttpDelete("{token:guid}")]
        public async Task<ActionResult> DeleteItemList(Guid token)
        {
            var itemList = await _service.Get(token);
            if (IsListInvalid(itemList)) return NotFound(NotFoundResponse);
            
            await _service.DeleteItemList(token);

            return NoContent();
        }


        private bool IsListInvalid(ItemList? list)
        {
            return list == null || !User.HasClaim("Username", list.Username);
        }
    }
}
