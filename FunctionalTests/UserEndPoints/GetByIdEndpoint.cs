using ProductTermsControl.WebAPI.Models.Users;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace FunctionalTests.UserEndPoints
{
    [Collection("Sequential")]
    public class GetByIdEndpoint : IClassFixture<BaseFixture>
    {
        JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public GetByIdEndpoint(BaseFixture factory)
        {
            Client = factory.CreateClient();
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task ReturnsItemGivenValidId()
        {
            var token = ApiTokenHelper.GetNormalUserToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await Client.GetAsync("Users/1");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = JsonSerializer.Deserialize<UserModel>(stringResponse, _jsonOptions);

            Assert.Equal(1, model.Id);
            Assert.Equal("string", model.FirstName);
        }

       /* [Fact]
        public async Task ReturnsNotFoundGivenInvalidId()
        {
            var response = await Client.GetAsync("api/catalog-items/0");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }*/
    }
}
