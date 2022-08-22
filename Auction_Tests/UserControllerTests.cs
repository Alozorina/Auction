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
        JsonSerializerSettings _settings;
        private const string RequestUri = "api/user/";
        const string ACCESS_TOKEN_ADMIN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9." +
            "eyJzdWIiOiJKV1RTZXJ2aWNlQWNjZXNzVG9rZW4iLCJqdGkiOiJkN2I5MjMxOS03M2JhL" +
            "TQwOTktYWY3NC01ZTYxMTM3ZDUwMWQiLCJpYXQiOiIxOC4wOC4yMDIyIDEwOjU5OjExIi" +
            "wiSWQiOiIxIiwiRW1haWwiOiJqYW5lbWFpbEBtYWlsLmNvbSIsImh0dHA6Ly9zY2hlbWF" +
            "zLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFk" +
            "bWluIiwiUm9sZSI6IkFkbWluIiwiaXNzIjoiSldUQXV0aGVudGljYXRpb25TZXJ2ZXIiL" +
            "CJhdWQiOiJKV1RTZXJ2aWNlUG9zdG1hbkNsaWVudCJ9.0aY13-4iOR-sZ2Wl3AXoIPnWLbDv_Ze0Uubf0m1ZhII";
        const string ACCESS_TOKEN_USER = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJKV1RTZXJ2aWNlQWNjZXNzVG9r" +
            "ZW4iLCJqdGkiOiI3ZTc4OGFiMS1hZjhkLTRiZDYtOGQwMS02MjY4MGRiMzZhYzkiLCJpYXQiOiIxOC4wOC4yMDIyIDExOjAyOjQzIi" +
            "wiSWQiOiIxMSIsIkVtYWlsIjoiZGFuYUBtYWlsLmNvbSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9p" +
            "ZGVudGl0eS9jbGFpbXMvcm9sZSI6IlVzZXIiLCJSb2xlIjoiVXNlciIsImlzcyI6IkpXVEF1dGhlbnRpY2F0aW9uU2VydmVyIiwiYX" +
            "VkIjoiSldUU2VydmljZVBvc3RtYW5DbGllbnQifQ.kggkYsfNF0Gvo8TOagFQACk789wTNqIM1LOUbkY8GAY";
        [SetUp]
        public void Setup()
        {
            if (_factory == null || _client == null || _settings == null)
            {
                _factory = new CustomWebApplicationFactory();
                _client = _factory.CreateClient();
                _settings = UnitTestHelper.GetSerializeSettings();
            }
        }


        [Test]
        public async Task Get_ReturnsAllUserPulicInfo_AdminAccess()
        {
            //arrange
            var expected = expectedResultOfGetMethod;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN_ADMIN);

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
            // act
            var httpResponse = await _client.GetAsync(RequestUri);

            // assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task GetById_ReturnsUserWithDetails()
        {
            //arrange
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN_ADMIN);
            string expectedJson = JsonConvert.SerializeObject(UnitTestHelper.users[0], _settings);

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
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN_ADMIN);
            string expectedJson = JsonConvert.SerializeObject(userPersonalInfo[0], _settings);

            // act
            var httpResponse = await _client.GetAsync(RequestUri + "profile");

            // assert
            httpResponse.EnsureSuccessStatusCode();
            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            responseJson.Should().BeEquivalentTo(expectedJson);
        }

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
                BirthDate = "1952-02-22",
                Email = UnitTestHelper.users[1].Email,
            }
        };

        [TearDown]
        public void TearDown()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
        }
    }
}