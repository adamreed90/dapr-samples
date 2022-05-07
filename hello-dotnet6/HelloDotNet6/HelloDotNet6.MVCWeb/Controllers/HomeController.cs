using HelloDotNet6.MVCWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Grpc.Core;
using Grpc.Net.Client;
using HelloDotNet6.Data;

namespace HelloDotNet6.MVCWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            //We will communicate with the gRPC API via a Dapr Sidecar, so our target will be the Dapr Sidecard running by default at port 50001.
            using var channel = GrpcChannel.ForAddress("https://localhost:50001");

            //For the Dapr Sidecar to locate our gRPC API, we neet to let it know the services app-id and provide it in the metadata of the request.
            var metadata = new Metadata
            {
                { "dapr-app-id", "HelloDotNet6.GrpcApi" }
            };

            var client = new Greeter.GreeterClient(channel);

            var requestTimer = Stopwatch.StartNew();

            var reply = await client.SayHelloAsync(
                new HelloRequest
                {
                    Name = "HelloDotNet6"
                }, metadata);

            requestTimer.Stop();

            ViewData["gRPCResponse"] = reply.Message;
            ViewData["gRPCResponseTime"] = requestTimer.ElapsedMilliseconds;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}