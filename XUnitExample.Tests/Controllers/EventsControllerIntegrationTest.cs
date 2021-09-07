using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using XUnitExample.Persistence;

namespace XUnitExample.Tests.Controllers
{
	public sealed class EventsControllerIntegrationTest
	{
		private static readonly string _connectionString =
			$"server=localhost;user={Environment.GetEnvironmentVariable("MYSQL_USER") ?? "root"};"
			+ $"password={Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? ""};"
			+ $"database={Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? "DotNetCMS_Test"}";

		private readonly TestServer _server;

		private readonly HttpClient _client;

		public EventsControllerIntegrationTest()
		{
			_server = new TestServer(
				new WebHostBuilder()
					.UseSetting("ConnectionStrings:eventDatabase", _connectionString)
					.UseStartup<Startup>()
			);

			_client = _server.CreateClient();

			using (var context = (EventContext) _server.Host.Services.GetService(typeof(EventContext))!)
			{
				context.Database.EnsureDeleted();
				context.Database.Migrate();
			}
		}

		[Fact]
		public async void PostEventAsync()
		{
			var postResponse = await _client.PostAsJsonAsync("/api/Events", new { Title = "Event title"});
			var createdEvent = await GetPageFromResponse(postResponse);
			var createdEventId = GetIdFromEvent(createdEvent);

			Assert.Equal(new Uri($"http://localhost/api/Events/{createdEventId}"), postResponse.Headers.Location);
			Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);
			Assert.Equal("Event title", GetTitleFromEvent(createdEvent));

			var getResponse =  await _client.GetAsync($"/api/Events/{createdEventId}");
			Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
			var loadedEvent = await GetPageFromResponse(getResponse);

			Assert.Equal(createdEventId, GetIdFromEvent(loadedEvent));
			Assert.Equal("Event title", GetTitleFromEvent(loadedEvent));
		}

		private async Task<JsonDocument> GetPageFromResponse(HttpResponseMessage response)
		{
			return (await response.Content.ReadFromJsonAsync<JsonDocument>())!;
		}

		private Guid GetIdFromEvent(JsonDocument page)
		{
			return page.RootElement.GetProperty("id").GetGuid();
		}

		private string GetTitleFromEvent(JsonDocument page)
		{
			return page.RootElement.GetProperty("title").GetString()!;
		}
	}
}

