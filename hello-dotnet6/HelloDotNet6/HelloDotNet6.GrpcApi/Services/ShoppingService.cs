using Dapr.Client;
using Grpc.Core;
using HelloDotNet6.Data;
using HelloDotNet6.Data.Models;
using HelloDotNet6.Data.Models.DaprShopping;
using Microsoft.EntityFrameworkCore;

namespace HelloDotNet6.GrpcApi.Services
{
    public class ShoppingService : Shopping.ShoppingBase
    {
        private readonly ILogger<ShoppingService> _logger;
        private readonly ShoppingDatabaseContext _shoppingDatabaseContext;

        public ShoppingService(ILogger<ShoppingService> logger, ShoppingDatabaseContext shoppingDatabaseContext)
        {
            _logger = logger;
            _shoppingDatabaseContext = shoppingDatabaseContext;
        }

        public override async Task<GetInventoryResponse> GetInventory(GetInventoryRequest request, ServerCallContext context)
        {
            using var client = new DaprClientBuilder().Build();

            var inventory = await client.GetStateAsync<Inventory>("statestore", "inventory_" + request.ProductId);

            if (inventory == null)
            {
                inventory = await _shoppingDatabaseContext.Inventory.Include(i => i.Product)
                    .SingleOrDefaultAsync(s => s.Product.ProductId == request.ProductId);

                var metadata = new Dictionary<string, string>
                {
                    {"ttlInSeconds", "60"}
                };

                await client.SaveStateAsync("statestore", "inventory_" + inventory.Product.ProductId, inventory, null,
                    metadata);
            }

            return new GetInventoryResponse
                {ProductId = inventory.Product.ProductId, ProductName = inventory.Product.Name, StockQuantity = inventory.Quantity};

        }
    }
}