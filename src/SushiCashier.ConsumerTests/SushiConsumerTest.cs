using System;
using PactNet.Mocks.MockHttpService;
using Xunit;

namespace SushiCashier.ConsumerTests
{
	public class SushiConsumerTest : IClassFixture<ConsumerPactClassFixture>
	{
		
		private readonly IMockProviderService _mockProviderService;
		private string _mockProviderServiceBaseUri;
		private SushiTrackerApiClient _sushiTrackerApiClient;

		public SushiConsumerTest(ConsumerPactClassFixture fixture)
		{
			_mockProviderService = fixture.MockProviderService;
			_mockProviderService.ClearInteractions(); //NOTE: Clears any previously registered interactions before the test is run
			_mockProviderServiceBaseUri = fixture.MockProviderServiceBaseUri;
			_sushiTrackerApiClient = new SushiTrackerApiClient(_mockProviderServiceBaseUri);
		}
		
		[Fact]
		public void Test1()
		{
			
		}
	}
}