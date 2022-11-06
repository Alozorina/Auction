using Business.Models;
using Data.Entities;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Auction.Tests
{
    class ItemControllerTests
    {
        private CustomWebApplicationFactory _factory;
        private HttpClient _client;
        JsonSerializerSettings _serializerSettings;
        private const string RequestUri = "api/item/";
        private string _token;
        string _jsonPublicItemInfo;

        [SetUp]
        public async Task Setup()
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
            _token = await TestHelper.GenerateToken(_client, "admin");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            if (_serializerSettings == null || _jsonPublicItemInfo == null)
            {
                // Arrange
                _serializerSettings = TestHelper.GetSerializerSettings();
                _jsonPublicItemInfo = GetItemPublicInfoJsonData(publicItemInfo, _serializerSettings);
            }
        }



        [Test]
        public async Task GetPublicSortedByStartDate_ReturnsItemsWithDetails()
        {
            // Act
            var httpResponse = await _client.GetAsync(RequestUri);

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            TestHelper.EmptyArrayResponseHandler(responseJson).Should().BeEquivalentTo(_jsonPublicItemInfo);
        }

        [Test]
        public async Task GetLotsByUserId_ReturnsLotsOfSpecificUser()
        {
            // Act
            var httpResponse = await _client.GetAsync(RequestUri + "lots/user=1");

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            TestHelper.EmptyArrayResponseHandler(responseJson).Should().BeEquivalentTo(_jsonPublicItemInfo);
        }

        [Test]
        public async Task GetLotsByUserId_ReturnsUnauthorizedIfEmptyToken()
        {
            // Arrange
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");

            // Act
            var httpResponse = await _client.GetAsync(RequestUri + "lots/user=1");

            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task GetPurchasesByUserId_ReturnsLotsOfSpecificUser()
        {
            // Act
            var httpResponse = await _client.GetAsync(RequestUri + "purchases/user=2");

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            TestHelper.EmptyArrayResponseHandler(responseJson).Should().BeEquivalentTo(_jsonPublicItemInfo);
        }

        [Test]
        public async Task GetPurchasesByUserId_ReturnsUnauthorizedIfEmptyToken()
        {
            // Arrange
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");

            // Act
            var httpResponse = await _client.GetAsync(RequestUri + "purchases/user=2");

            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task Search_ReturnsCorrectResult()
        {
            // Arrange
            string searchInput = TestHelper.categories[0].Name;
            var expected = new List<ItemPublicInfo>() { publicItemInfo[0] };

            // Act
            var httpResponse = await _client.GetAsync(RequestUri + $"search={searchInput}");

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<IEnumerable<Item>>(responseJson).ToList();

            actual.Should().BeEquivalentTo(expected, options => options
                                                            .Excluding(i => i.ItemPhotos));
        }

        [Test]
        public async Task GetAll_AvailableForAdmin()
        {
            // Arrange
            var expected = TestHelper.items;

            // Act
            var httpResponse = await _client.GetAsync(RequestUri + "private");

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<IEnumerable<Item>>(stringResponse).ToList();

            actual.Should().BeEquivalentTo(expected, options => options
                                                            .Excluding(i => i.Owner)
                                                            .Excluding(i => i.Buyer)
                                                            .Excluding(i => i.ItemCategories)
                                                            .Excluding(i => i.ItemPhotos)
                                                            .Excluding(i => i.Status.Items));
        }

        [Test]
        public async Task GetAll_ReturnsForbiddenIfNotAdmin()
        {
            // Arrange
            _token = await TestHelper.GenerateToken(_client, "user");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            // Act
            var httpResponse = await _client.GetAsync(RequestUri + "private");

            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Test]
        public async Task GetById_ReturnsCorrectItem()
        {
            // Arrange
            var expected = TestHelper.items[0];
            var ID = 1;

            // Act
            var httpResponse = await _client.GetAsync(RequestUri + $"{ID}");

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<Item>(stringResponse);

            actual.Should().BeEquivalentTo(expected, options => options
                                                            .Excluding(i => i.Owner)
                                                            .Excluding(i => i.Buyer)
                                                            .Excluding(i => i.ItemCategories)
                                                            .Excluding(i => i.ItemPhotos)
                                                            .Excluding(i => i.Status));
        }

        [Test]
        public async Task GetById_ReturnsBadRequest()
        {
            // Arrange
            var ID = "abc";

            // Act
            var httpResponse = await _client.GetAsync(RequestUri + $"{ID}");

            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Add_AddsNewItem()
        {
            // Act
            HttpResponseMessage httpResponse;

            using (var fileStream = File.OpenRead(@"StaticFiles\TestPhotos\test.jpg"))
            using (var content = new StreamContent(fileStream))
            using (var formData = new MultipartFormDataContent())
            {
                //create ItemCreateNewEntity data
                formData.Add(new StringContent("TestItem3"), "Name");
                formData.Add(new StringContent("Test Author3"), "CreatedBy");
                formData.Add(new StringContent("20"), "StartingPrice");
                formData.Add(new StringContent("1"), "OwnerId");
                formData.Add(new StringContent("2023-01-21"), "StartSaleDate");
                formData.Add(new StringContent("2023-02-21"), "EndSaleDate");
                formData.Add(content, "ItemFormFilePhotos", "test.jpg");
                httpResponse = await _client.PostAsync(RequestUri, formData);
            }

            // Assert
            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            httpResponse.EnsureSuccessStatusCode();
            responseJson.Should().Contain("TestItem3");

            // these 2 lines delete the created image
            var staticFileName = TestHelper.GetStringFromStartToEndInclusive(responseJson, "veiling_", "jpg");
            File.Delete(TestHelper.GetDirectory() + @"\StaticFiles\images\" + staticFileName);

            httpResponse.Dispose();
        }

        [Test]
        public async Task UpdateBid_ReturnsSuccessCode()
        {
            // Arrange
            var newBid = new ItemUpdateBid
            {
                BuyerId = TestHelper.users[1].Id,
                CurrentBid = 75
            };
            var newBidDataJson = JsonConvert.SerializeObject(newBid, _serializerSettings);
            var stringContent = new StringContent(newBidDataJson, Encoding.UTF8, "application/json");
            var itemID = "1";

            // Act
            var httpResponse = await _client.PutAsync(RequestUri + itemID, stringContent);

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<Item>(responseJson);

            Assert.AreEqual(newBid.CurrentBid, response.CurrentBid);
            Assert.AreEqual(newBid.BuyerId, response.BuyerId);
        }

        [Test]
        public async Task Update_ReturnsSuccessCode()
        {
            // Arrange
            var inputData = new ItemPublicInfo
            {
                Id = TestHelper.items[1].Id,
                Name = "NewTestName",
                CreatedBy = "NewTestAuthor",
                OwnerId = TestHelper.items[0].OwnerId,
                StartingPrice = 10,
                CurrentBid = 80,
                StartSaleDate = new DateTime(2023, 01, 09, 10, 00, 00),
                EndSaleDate = new DateTime(2023, 02, 09, 10, 00, 00),
                Status = new Status { Id = TestHelper.items[0].Status.Id, Name = TestHelper.items[0].Status.Name },
                BuyerId = TestHelper.items[0].BuyerId,
                Description = TestHelper.items[0].Description,
                ItemPhotos = TestHelper.itemPhotos
            };
            var newItemDataJson = JsonConvert.SerializeObject(inputData, _serializerSettings);
            var stringContent = new StringContent(newItemDataJson, Encoding.UTF8, "application/json");
            var itemID = TestHelper.items[1].Id;

            // Act
            var httpResponse = await _client.PutAsync(RequestUri + itemID + "/edit", stringContent);

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<Item>(responseJson);

            response.Should().BeEquivalentTo(inputData);
        }

        [Test]
        public async Task Delete_AvailableToAdmin()
        {
            // Arrange
            _token = await TestHelper.GenerateToken(_client, "admin");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            // Act
            var getItem_BeforeDelete = await _client.GetAsync(RequestUri + "1");
            var httpResponse = await _client.DeleteAsync(RequestUri + "1");
            var getItem_AfterDelete = await _client.GetAsync(RequestUri + "1");

            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            getItem_BeforeDelete.EnsureSuccessStatusCode();
            getItem_AfterDelete.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }


        #region TestData
        string GetItemPublicInfoJsonData(List<ItemPublicInfo> items, JsonSerializerSettings serializerSettings)
            => JsonConvert.SerializeObject(items, serializerSettings);

        //List with nulled navigation properties maked in an immutable way.
        //This will not change the original list in the TestHepler class
        static List<ItemPublicInfo> publicItemInfo = new List<ItemPublicInfo>() {
            new ItemPublicInfo{
                Id = TestHelper.items[0].Id,
                Name = TestHelper.items[0].Name,
                CreatedBy = TestHelper.items[0].CreatedBy,
                OwnerId = TestHelper.items[0].OwnerId,
                BuyerId = TestHelper.items[0].BuyerId,
                Description = TestHelper.items[0].Description,
                StartingPrice = TestHelper.items[0].StartingPrice,
                CurrentBid = TestHelper.items[0].CurrentBid,
                StartSaleDate = TestHelper.items[0].StartSaleDate,
                EndSaleDate = TestHelper.items[0].EndSaleDate,
                ItemPhotos = TestHelper.items[0].ItemPhotos,
                Status = new Status()
                {
                    Id = TestHelper.items[0].Status.Id,
                    Name = TestHelper.items[0].Status.Name
                },
                ItemCategories =  TestHelper.items[0].ItemCategories
                    .Select(ic => new ItemCategory
                    {
                        Id = ic.Id,
                        CategoryId = ic.CategoryId,
                        ItemId = ic.ItemId,
                        Category = new Category
                        {
                            Id = ic.Id,
                            Name = ic.Category.Name
                        }
                    })
                    .ToList()
            },
            new ItemPublicInfo{
                Id = TestHelper.items[1].Id,
                Name = TestHelper.items[1].Name,
                CreatedBy = TestHelper.items[1].CreatedBy,
                OwnerId = TestHelper.items[1].OwnerId,
                BuyerId = TestHelper.items[1].BuyerId,
                Description = TestHelper.items[1].Description,
                StartingPrice = TestHelper.items[1].StartingPrice,
                CurrentBid = TestHelper.items[1].CurrentBid,
                StartSaleDate = TestHelper.items[1].StartSaleDate,
                EndSaleDate = TestHelper.items[1].EndSaleDate,
                ItemPhotos = TestHelper.items[1].ItemPhotos,
                Status = new Status()
                {
                    Id = TestHelper.items[1].Status.Id,
                    Name = TestHelper.items[1].Status.Name
                },
                ItemCategories =  TestHelper.items[1].ItemCategories
                    .Select(ic => new ItemCategory
                    {
                        Id = ic.Id,
                        CategoryId = ic.CategoryId,
                        ItemId = ic.ItemId,
                        Category = new Category
                        {
                            Id = ic.Id,
                            Name = ic.Category.Name
                        }
                    })
                    .ToList()
            }
            };

        #endregion
    }
}