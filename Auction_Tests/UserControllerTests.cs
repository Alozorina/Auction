using BLL.Models;
using DAL.Entities;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
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
        private string _token;

        [SetUp]
        public async Task Setup()
        {
            if (_serializerSettings == null)
            {
                _serializerSettings = TestHelper.GetSerializerSettings();
            }
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
            _token = await TestHelper.GenerateToken(_client, "admin");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }


        [Test]
        public async Task Get_ReturnsAllUserPulicInfo_AdminAccess()
        {
            // Arrange
            var expected = expectedResultOfGetMethod;

            // Act
            var httpResponse = await _client.GetAsync(RequestUri);

            // Assert
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
            // Arrange
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");

            // Act
            var httpResponse = await _client.GetAsync(RequestUri);

            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task GetById_ReturnsUserWithDetails()
        {
            // Arrange
            var expected = TestHelper.users[0];

            // Act
            var httpResponse = await _client.GetAsync(RequestUri + "1");

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actualUser = JsonConvert.DeserializeObject<User>(stringResponse);

            actualUser.Should().BeEquivalentTo(expected, options => options
                                                            .Excluding(u => u.Lots)
                                                            .Excluding(u => u.Purchases)
                                                            .Excluding(u => u.Role));
            actualUser.Role.Should().NotBeNull();
        }

        [Test]
        public async Task GetCurrentUserPersonalInfo_ReturnsCorrectData()
        {
            // Arrange
            string expectedJson = JsonConvert.SerializeObject(userPersonalInfo[1], _serializerSettings);

            // Act
            var httpResponse = await _client.GetAsync(RequestUri + "profile");

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            responseJson.Should().BeEquivalentTo(expectedJson);
        }

        [Test]
        public async Task Register_ReturnsNewToken()
        {
            // Arrange
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
            var expectedJson = JsonConvert.SerializeObject(userRegistrationModel[0]);
            var stringContent = new StringContent(expectedJson, Encoding.UTF8, "application/json");

            // Act
            var httpResponse = await _client.PostAsync(RequestUri + "register", stringContent);

            // Assert
            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            httpResponse.EnsureSuccessStatusCode();
            responseJson.Should().NotBeNull();
        }

        [Test]
        public async Task Register_ReturnsBadRequestIfEmailExists()
        {
            // Arrange
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
            var expectedJson = JsonConvert.SerializeObject(userRegistrationModel[1]);
            var stringContent = new StringContent(expectedJson, Encoding.UTF8, "application/json");

            // Act
            var httpResponse = await _client.PostAsync(RequestUri + "register", stringContent);

            // Assert
            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            responseJson.Should().BeEquivalentTo("This email is being used by another user");
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Login_ReturnsNewToken()
        {
            // Arrange
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
            var expectedJson = JsonConvert.SerializeObject(userLoginModel[0]);
            var stringContent = new StringContent(expectedJson, Encoding.UTF8, "application/json");

            // Act
            var httpResponse = await _client.PostAsync(RequestUri + "login", stringContent);

            // Assert
            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            httpResponse.EnsureSuccessStatusCode();
            responseJson.Should().NotBeNull();
        }

        [Test]
        public async Task Login_ReturnsUnauthorizedForInvalidCredentials()
        {
            // Arrange
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
            var expectedJson = JsonConvert.SerializeObject(userLoginModel[1]);
            var stringContent = new StringContent(expectedJson, Encoding.UTF8, "application/json");

            // Act
            var httpResponse = await _client.PostAsync(RequestUri + "login", stringContent);

            // Assert
            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            responseJson.Should().BeEquivalentTo("Invalid credentials");
            httpResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task Logout_InvalidatesAccessToken()
        {
            // Arrange
            var httpPrivateInfo_WithToken = await _client.GetAsync(RequestUri + "profile");
            var stringContent = new StringContent("", Encoding.UTF8, "application/json");

            // Act
            await _client.PostAsync(RequestUri + "logout", stringContent);
            var httpPrivatInfo_WithoutToken = await _client.GetAsync(RequestUri + "profile");

            // Assert
            httpPrivateInfo_WithToken.EnsureSuccessStatusCode();
            httpPrivatInfo_WithoutToken.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            // Tear down
            _token = await TestHelper.GenerateToken(_client, "admin");
        }

        [Test]
        public async Task UpdateCurrentUserPersonalInfo_ReturnsUpdatedUserModel()
        {
            // Arrange
            var expectedJson = JsonConvert.SerializeObject(userPersonalInfo[0], _serializerSettings);
            var stringContent = new StringContent(expectedJson, Encoding.UTF8, "application/json");

            // Act
            var httpResponse = await _client.PutAsync(RequestUri + "profile/edit", stringContent);

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            responseJson.Should().BeEquivalentTo(expectedJson);
        }

        [Test]
        public async Task UpdatePassword_ThrowsExceptionIfOldPasswordDoesntMatch()
        {
            // Arrange
            var incorrectOldPasswordJson = JsonConvert.SerializeObject(userPasswordModel[0]);
            var stringContent = new StringContent(incorrectOldPasswordJson, Encoding.UTF8, "application/json");

            // Act
            var httpResponse = await _client.PutAsync(RequestUri + "profile/password", stringContent);

            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task UpdatePassword_ReturnsSuccessCode()
        {
            // Arrange
            var correctOldPasswordJson = JsonConvert.SerializeObject(userPasswordModel[1]);
            var stringContent = new StringContent(correctOldPasswordJson, Encoding.UTF8, "application/json");

            // Act
            var httpResponse = await _client.PutAsync(RequestUri + "profile/password", stringContent);
            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task UpdatePersonalInfoById_ReturnsForbiddenIfNotAdmin()
        {
            // Arrange
            _token = await TestHelper.GenerateToken(_client, "user");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var newUserDataJson = JsonConvert.SerializeObject(userPersonalInfo[1], _serializerSettings);
            var stringContent = new StringContent(newUserDataJson, Encoding.UTF8, "application/json");

            // Act
            var httpResponse = await _client.PutAsync(RequestUri + "1/edit", stringContent);

            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Test]
        public async Task UpdatePersonalInfoById_AvailableToAdmin()
        {
            // Arrange
            _token = await TestHelper.GenerateToken(_client, "admin");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var newUserDataJson = JsonConvert.SerializeObject(userPersonalInfo[1], _serializerSettings);
            var stringContent = new StringContent(newUserDataJson, Encoding.UTF8, "application/json");

            // Act
            var httpResponse = await _client.PutAsync(RequestUri + "1/edit", stringContent);

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            responseJson.Should().BeEquivalentTo(newUserDataJson);
        }

        [Test]
        public async Task UpdatePasswordById_ReturnsForbiddenIfNotAdmin()
        {
            // Arrange
            _token = await TestHelper.GenerateToken(_client, "user");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var newPasswordJson = JsonConvert.SerializeObject(userPasswordModel[1], _serializerSettings);
            var stringContent = new StringContent(newPasswordJson, Encoding.UTF8, "application/json");

            // Act
            var httpResponse = await _client.PutAsync(RequestUri + "1/creds", stringContent);

            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Test]
        public async Task UpdatePasswordById_AvailableToAdmin()
        {
            // Arrange
            _token = await TestHelper.GenerateToken(_client, "admin");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var newPasswordJson = JsonConvert.SerializeObject(userPasswordModel[1], _serializerSettings);
            var stringContent = new StringContent(newPasswordJson, Encoding.UTF8, "application/json");

            // Act
            var httpResponse = await _client.PutAsync(RequestUri + "2/creds", stringContent);

            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task UpdateRoleById_AvailableToAdmin()
        {
            // Arrange
            const int RoleId = 2;
            _token = await TestHelper.GenerateToken(_client, "admin");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var newPasswordJson = JsonConvert.SerializeObject(RoleId);
            var stringContent = new StringContent(newPasswordJson, Encoding.UTF8, "application/json");

            // Act
            var httpResponse = await _client.PutAsync(RequestUri + "1/role", stringContent);

            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task Delete_AvailableToAdmin()
        {
            // Arrange
            _token = await TestHelper.GenerateToken(_client, "admin");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            // Act
            var getUser_BeforeDelete = await _client.GetAsync(RequestUri + "1");
            var httpResponse = await _client.DeleteAsync(RequestUri + "1");
            var getUser_AfterDelete = await _client.GetAsync(RequestUri + "1");

            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            getUser_BeforeDelete.EnsureSuccessStatusCode();
            getUser_AfterDelete.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        #region TestData
        static readonly List<UserPulicInfo> expectedResultOfGetMethod = new List<UserPulicInfo>() {
            new UserPulicInfo{
                FirstName = TestHelper.users[0].FirstName,
                LastName = TestHelper.users[0].LastName,
                BirthDate = TestHelper.users[0].BirthDate,
                Email = TestHelper.users[0].Email,
                Role = TestHelper.users[0].Role.Name
            },
            new UserPulicInfo{
                FirstName = TestHelper.users[1].FirstName,
                LastName = TestHelper.users[1].LastName,
                BirthDate = TestHelper.users[1].BirthDate,
                Email = TestHelper.users[1].Email,
                Role = TestHelper.users[1].Role.Name
            }
        };

        static readonly List<UserPersonalInfoModel> userPersonalInfo = new List<UserPersonalInfoModel>() {
            new UserPersonalInfoModel{
                FirstName = TestHelper.users[0].FirstName,
                LastName = TestHelper.users[0].LastName,
                BirthDate = "1981-01-21",
                Email = TestHelper.users[0].Email,
            },
            new UserPersonalInfoModel{
                FirstName = TestHelper.users[1].FirstName,
                LastName = TestHelper.users[1].LastName,
                BirthDate = "1992-02-22",
                Email = TestHelper.users[1].Email,
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
                FirstName = TestHelper.users[0].FirstName,
                LastName = TestHelper.users[0].LastName,
                Email = TestHelper.users[0].Email,
                Password = "test_password1"
            }
        };

        static readonly List<UserLoginModel> userLoginModel = new List<UserLoginModel>() {
            new UserLoginModel
            {
                Email = TestHelper.users[1].Email,
                Password = "test_password2"
            },
            new UserLoginModel
            {
                Email = TestHelper.users[0].Email,
                Password = "incorrectPassword"
            }
        };

        static readonly List<UserPassword> userPasswordModel = new List<UserPassword>() {
            new UserPassword
            {
                NewPassword = "someNewPassword",
                OldPassword = "incorrectPassword"
            },
            new UserPassword
            {
                NewPassword = "someNewPassword",
                OldPassword = "test_password2"
            }
        };
        #endregion
    }
}