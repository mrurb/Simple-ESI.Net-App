using ESI.NET;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionApp5
{
    public class Function(ILogger<Function> logger, IEsiClient esiClient)
    {
        private readonly ILogger<Function> _logger = logger;
        private readonly IEsiClient esiClient = esiClient;

        [Function("Function")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            EsiResponse<List<ESI.NET.Models.Market.Order>> esiResponse = await esiClient.Market.RegionOrders(region_id: 10000002, order_type: ESI.NET.Enumerations.MarketOrderType.Sell, type_id: 44992);
            List<ESI.NET.Models.Market.Order> orders = esiResponse.Data;
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult(orders);
        }
    }
}
