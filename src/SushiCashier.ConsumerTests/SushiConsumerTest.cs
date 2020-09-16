using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using Xunit;

namespace SushiCashier.ConsumerTests
{
	public class SushiConsumerTest : IClassFixture<ConsumerPactClassFixture>
	{
		
		private readonly IMockProviderService _mockProviderService;
		private readonly SushiTrackerApiClient _sushiTrackerApiClient;

		public SushiConsumerTest(ConsumerPactClassFixture fixture)
		{
			_mockProviderService = fixture.MockProviderService;
			_mockProviderService.ClearInteractions(); //NOTE: Clears any previously registered interactions before the test is run
			_sushiTrackerApiClient = new SushiTrackerApiClient(fixture.MockProviderServiceBaseUri);
		}
		
		/// <summary>
		/// Need to install PactNet.Windows to be able to start pact server on windows
		/// Also PactNet.Linux and PactNet.OSX exists
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task ItHandlesInvalidDateParam()
		{
			// Arange
			int MinimalRollsCount = 6;
			var invalidRequestMessage = $"Rolls count is low. Minimal rolls count is {MinimalRollsCount}";
			
			_mockProviderService.Given("There is data")
				.UponReceiving("A invalid POST request for create cashier order")
				.With(new ProviderServiceRequest
				{
					Method = HttpVerb.Post,
					Path = "/api/orders",
					Headers = new Dictionary<string, object>
					{
						{ "Content-Type", "application/json; charset=utf-8" }
					},
					Body = new
					{
						IsMobileApp = false,
						RollsCount = 1,
					}
				})
				.WillRespondWith(new ProviderServiceResponse
				{
					Headers = new Dictionary<string, object>
					{
						{ "Content-Type", "text/plain; charset=utf-8" }
					},
					Status = 400,
					Body = invalidRequestMessage
				});

			// Act
			var result = await _sushiTrackerApiClient.CreateCashierOrder(1);
			var resultBodyText = await result.Content.ReadAsStringAsync();

			// Assert
			Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
			Assert.Contains(invalidRequestMessage, resultBodyText);
		}
	}
}