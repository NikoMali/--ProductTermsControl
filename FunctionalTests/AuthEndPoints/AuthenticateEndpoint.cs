using Microsoft.AspNetCore.TestHost;
using ProductTermsControl.WebAPI;
using ProductTermsControl.WebAPI.Models.Users;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace FunctionalTests.AuthEndPoints
{
    [Collection("Sequential")]
    public class AuthenticateEndpoint : IClassFixture<BaseFixture>
    {
        //JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        protected TestServer Server { get; }
        protected HttpClient Client { get; }
        public AuthenticateEndpoint(BaseFixture fixture)
        {
            //Server = fixture.Server;
            Client = fixture.CreateClient();
        }

        

        [Theory]
        [InlineData("string", "string", true)]
        
        public async Task ReturnsExpectedResultGivenCredentials(string testUsername, string testPassword, bool expectedResult)
        {
            var request = new AuthenticateModel()
            {
                Username = testUsername,
                Password = testPassword
            };
            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("users/authenticate", jsonContent);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            //var model = stringResponse.FromJson<AuthenticateResponse>();

            Assert.Equal(stringResponse, stringResponse);
        }
    }
}
