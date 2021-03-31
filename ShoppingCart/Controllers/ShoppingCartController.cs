using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingCart.Data.Repositories;
using ShoppingCart.Domain.Model.Dtos;
using ShoppingCart.Domain.Model.Entities;
using ShoppingCart.Domain.Model.Enum;
using ShoppingCart.Domain.Services.Services;
using ShoppingCart.FilterAttribute;

namespace ShoppingCart.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingCartController : ControllerBase
    {
       

        private readonly ILogger<ShoppingCartController> _logger;
        private readonly ICartService _service;
        private readonly IProductDummyService _productDummyService;

        public ShoppingCartController(ILogger<ShoppingCartController> logger, IRepository<Basket, int> repository, ICartService service, IProductDummyService productDummyService)
        {
            _logger = logger;
            _service = service;
            _productDummyService = productDummyService;
        }

     
        [HttpPost]
        [Route(nameof(AddToCart))]
        [ExceptionLog(logTypes: new[] { (short)LoggerType.DatabaseLogger, (short)LoggerType.EmailLogger, (short)LoggerType.FileLogger })]
        public async Task<ActionResult> AddToCart(BasketDto user)
        {
            var response = await _service.AddToCartAsync(user);
            return Ok(response);
        }

        [HttpGet]
        [Route(nameof(GetProductById))]
        [ExceptionLog(logTypes: new[] { (short)LoggerType.DatabaseLogger, (short)LoggerType.EmailLogger, (short)LoggerType.FileLogger })]
        public async Task<ActionResult> GetProductById(Guid id)
        {
            var response = await _productDummyService.GetProductById(id);

            return Ok(response);
        }
    }
}
