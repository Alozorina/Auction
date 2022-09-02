using BLL.Models;
using DAL.Entities;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Auction_Tests
{
    public class UserControllerTests
    {
        private CustomWebApplicationFactory _factory;
        private HttpClient _client;
        JsonSerializerSettings _serializerSettings;
        private const string RequestUri = "api/user/";

        [SetUp]
        public async Task Setup()
        {
            if (_factory == null || _client == null || _serializerSettings == null)
            {
                _factory = new CustomWebApplicationFactory();
                _client = _factory.CreateClient();
                _serializerSettings = UnitTestHelper.GetSerializerSettings();
            }
            var token = await GenerateToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }


        [Test]
        public async Task Get_ReturnsAllUserPulicInfo_AdminAccess()
        {
            //arrange
            var expected = expectedResultOfGetMethod;

            // act
            var httpResponse = await _client.GetAsync(RequestUri);

            // assert
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<IEnumerable<UserPulicInfo>>(stringResponse).ToList();

            actual.Should().BeEquivalentTo(expected, options => options
                                                            .Excluding(u => u.Lots)
                                                            .Excluding(u => u.Purchases));
        }

        [Test]
        public async Task Get_ReturnsUnauthorized()
        {
            //arrange
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");

            // act
            var httpResponse = await _client.GetAsync(RequestUri);

            // assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task GetById_ReturnsUserWithDetails()
        {
            //arrange
            string expectedJson = JsonConvert.SerializeObject(UnitTestHelper.users[0], _serializerSettings);

            // act
            var httpResponse = await _client.GetAsync(RequestUri + "1");

            // assert
            httpResponse.EnsureSuccessStatusCode();
            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            string responseWithNull = UnitTestHelper.EmptyArrayResponseHandler(responseJson);

            responseWithNull.Should().BeEquivalentTo(expectedJson);
        }

        [Test]
        public async Task GetCurrentUserPersonalInfo_ReturnsCorrectData()
        {
            //arrange
            string expectedJson = JsonConvert.SerializeObject(userPersonalInfo[1], _serializerSettings);

            // act
            var httpResponse = await _client.GetAsync(RequestUri + "profile");

            // assert
            httpResponse.EnsureSuccessStatusCode();
            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            responseJson.Should().BeEquivalentTo(expectedJson);
        }

        [Test]
        public async Task Register_ReturnsNewToken()
        {
            //arrange
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
            var expectedJson = JsonConvert.SerializeObject(userRegistrationModel[0]);

            // act
            var stringContent = new StringContent(expectedJson, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(RequestUri + "register", stringContent);

            // assert
            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            httpResponse.EnsureSuccessStatusCode();
            responseJson.Should().NotBeNull();
        }

        [Test]
        public async Task Register_ReturnsBadRequestIfEmailExists()
        {
            //arrange
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
            var expectedJson = JsonConvert.SerializeObject(userRegistrationModel[1]);

            // act
            var stringContent = new StringContent(expectedJson, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(RequestUri + "register", stringContent);

            // assert
            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            responseJson.Should().BeEquivalentTo("This email is being used by another user");
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Login_ReturnsNewToken()
        {
            //arrange
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
            var expectedJson = JsonConvert.SerializeObject(userLoginModel[0]);

            // act
            var stringContent = new StringContent(expectedJson, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(RequestUri + "login", stringContent);

            // assert
            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            httpResponse.EnsureSuccessStatusCode();
            responseJson.Should().NotBeNull();
        }

        [Test]
        public async Task Login_ReturnsUnauthorized()
        {
            //arrange
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
            var expectedJson = JsonConvert.SerializeObject(userLoginModel[1]);

            // act
            var stringContent = new StringContent(expectedJson, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(RequestUri + "login", stringContent);

            // assert
            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            responseJson.Should().BeEquivalentTo("Invalid credentials");
            httpResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task Logout_ReturnsEmptyToken_AndNullUser()
        {
            // act
            var httpPrivateInfo_WithToken = await _client.GetAsync(RequestUri + "profile");
            var stringContent = new StringContent("", Encoding.UTF8, "application/json");
            await _client.PostAsync(RequestUri + "logout", stringContent);
            var httpPrivatInfo_WithoutToken = await _client.GetAsync(RequestUri + "profile");

            // assert
            httpPrivateInfo_WithToken.EnsureSuccessStatusCode();
            httpPrivatInfo_WithoutToken.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }


/*        [TearDown]
        public void TearDown()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN_ADMIN);
        }*/

        #region TestData
        static readonly List<UserPulicInfo> expectedResultOfGetMethod = new List<UserPulicInfo>() {
            new UserPulicInfo{
                FirstName = UnitTestHelper.users[0].FirstName,
                LastName = UnitTestHelper.users[0].LastName,
                BirthDate = UnitTestHelper.users[0].BirthDate,
                Email = UnitTestHelper.users[0].Email,
                Role = UnitTestHelper.users[0].Role.Name
            },
            new UserPulicInfo{
                FirstName = UnitTestHelper.users[1].FirstName,
                LastName = UnitTestHelper.users[1].LastName,
                BirthDate = UnitTestHelper.users[1].BirthDate,
                Email = UnitTestHelper.users[1].Email,
                Role = UnitTestHelper.users[1].Role.Name
            }
        };

        static readonly List<UserPersonalInfoModel> userPersonalInfo = new List<UserPersonalInfoModel>() {
            new UserPersonalInfoModel{
                FirstName = UnitTestHelper.users[0].FirstName,
                LastName = UnitTestHelper.users[0].LastName,
                BirthDate = "1981-01-21",
                Email = UnitTestHelper.users[0].Email,
            },
            new UserPersonalInfoModel{
                FirstName = UnitTestHelper.users[1].FirstName,
                LastName = UnitTestHelper.users[1].LastName,
                BirthDate = "1992-02-22",
                Email = UnitTestHelper.users[1].Email,
            }
        };

        static readonly List<UserRegistrationModel> userRegistrationModel = new List<UserRegistrationModel>() {
            new UserRegistrationModel
            {
                FirstName = "RegTestName",
                LastName = "RegTestLastName",
                Email = "regtestemail@m.com",
                Password = "regTestPassword"
            },
            new UserRegistrationModel
            {
                FirstName = UnitTestHelper.users[0].FirstName,
                LastName = UnitTestHelper.users[0].LastName,
                Email = UnitTestHelper.users[0].Email,
                Password = UnitTestHelper.users[0].Password
            }
        };

        static readonly List<UserLoginModel> userLoginModel = new List<UserLoginModel>() {
            new UserLoginModel
            {
                Email = UnitTestHelper.users[1].Email,
                Password = UnitTestHelper.users[1].Password
            },
            new UserLoginModel
            {
                Email = UnitTestHelper.users[0].Email,
                Password = "incorrectPassword"
            }
        };
        #endregion

        public async Task<string> GenerateToken()
        {
            var serializedLoginAdmin = JsonConvert.SerializeObject(userLoginModel[0]);

            var stringContent = new StringContent(serializedLoginAdmin, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(RequestUri + "login", stringContent);

            return await httpResponse.Content.ReadAsStringAsync();
        }
    }
}