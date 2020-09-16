using System;
using System.Threading.Tasks;

namespace SushiCookingInfoService
{
	// ReSharper disable once ClassNeverInstantiated.Global
	class Program
	{
		static async Task Main(string[] args)
		{
			Console.WriteLine("Wanna sushi bro? How much?");
			int rollsCount = Int32.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
			SushiTrackerApiClient client = new SushiTrackerApiClient("http://localhost:9000");
			var response = await client.CreateCashierOrder(rollsCount);
			if (response.IsSuccessStatusCode)
			{
				var responseText = await response.Content.ReadAsStringAsync();
				Console.WriteLine($"Sushi response: {responseText}");
			}
		}
	}
}