using BLL.Models;
using DAL.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
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
using System.Threading;
using System.Threading.Tasks;

namespace Auction_Tests
{
    class ItemControllerTests
    {
        private CustomWebApplicationFactory _factory;
        private HttpClient _client;
        JsonSerializerSettings _serializerSettings;
        private const string RequestUri = "api/item/";
        private string _token;

        [SetUp]
        public async Task Setup()
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
            _token = await TestHelper.GenerateToken(_client, "admin");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            
            if (_serializerSettings == null)
            {
                //arrange
                _serializerSettings = TestHelper.GetSerializerSettings();
            }
        }



        [Test]
        public async Task GetPublicSortedByStartDate_ReturnsItemsWithDetails()
        {
            // act
            var httpResponse = await _client.GetAsync(RequestUri);

            // assert
            httpResponse.EnsureSuccessStatusCode();
            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            TestHelper.EmptyArrayResponseHandler(responseJson).Should().BeEquivalentTo(jsonDataGetPublicSortedByStartDate);
        }

        [Test]
        public async Task GetLotsByUserId_ReturnsLotsOfSpecificUser()
        {
            // act
            var httpResponse = await _client.GetAsync(RequestUri + "lots/user=1");

            // assert
            httpResponse.EnsureSuccessStatusCode();
            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            TestHelper.EmptyArrayResponseHandler(responseJson).Should().BeEquivalentTo(jsonDataGetPublicSortedByStartDate);
        }

        [Test]
        public async Task GetLotsByUserId_ReturnsUnauthorizedIfEmptyToken()
        {
            //arrange
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");

            // act
            var httpResponse = await _client.GetAsync(RequestUri + "lots/user=1");

            // assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task GetPurchasesByUserId_ReturnsLotsOfSpecificUser()
        {
            // act
            var httpResponse = await _client.GetAsync(RequestUri + "purchases/user=2");

            // assert
            httpResponse.EnsureSuccessStatusCode();
            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            TestHelper.EmptyArrayResponseHandler(responseJson).Should().BeEquivalentTo(jsonDataGetPublicSortedByStartDate);
        }

        [Test]
        public async Task GetPurchasesByUserId_ReturnsUnauthorizedIfEmptyToken()
        {
            //arrange
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");

            // act
            var httpResponse = await _client.GetAsync(RequestUri + "purchases/user=2");

            // assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task Search_ReturnsCorrectResult()
        {
            //arrange
            string searchInput = "test";

            // act
            var httpResponse = await _client.GetAsync(RequestUri + $"search={searchInput}");

            // assert
            httpResponse.EnsureSuccessStatusCode();
            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            TestHelper.EmptyArrayResponseHandler(responseJson).Should().BeEquivalentTo(jsonDataGetPublicSortedByStartDate);
        }

        [Test]
        public async Task GetAll_AvailableForAdmin()
        {
            //arrange
            var expected = TestHelper.items;

            // act
            var httpResponse = await _client.GetAsync(RequestUri + "private");

            // assert
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
            //arrange
            _token = await TestHelper.GenerateToken(_client, "user");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            // act
            var httpResponse = await _client.GetAsync(RequestUri + "private");

            // assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Test]
        public async Task GetById_ReturnsCorrectItem()
        {
            //arrange
            var expected = TestHelper.items[0];
            var ID = 1;

            // act
            var httpResponse = await _client.GetAsync(RequestUri + $"{ID}");

            // assert
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
        public async Task Add_AddsNewItem()
        {
            //arrange
            var stream = File.OpenRead(@"TestPhotos\test.jpg");
            FormFile file = new FormFile(stream, 0, stream.Length, "ItemFormFilePhotos", "test.jpg");
            file.ContentDisposition = "form-data; name = \"ItemFormFilePhotos\"; filename = \"test.jpg\"";

            ItemCreateNewEntity newItemData = new ItemCreateNewEntity()
            {
                Name = "TestItem3",
                CreatedBy = "Test Author3",
                StartingPrice = 20m,
                OwnerId = 1,
                StartSaleDate = new DateTime(2023, 10, 09, 10, 00, 00),
                EndSaleDate = new DateTime(2023, 12, 10, 12, 00, 00),
                ItemFormFilePhotos = new List<IFormFile>() { file }
            };
            var expectedJson = JsonConvert.SerializeObject(newItemData);
            var stringContent = new StringContent(expectedJson, Encoding.UTF8, "application/json");

            // act
            var httpResponse = await _client.PostAsync(RequestUri, stringContent);

            // assert
            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            httpResponse.EnsureSuccessStatusCode();
            responseJson.Should().NotBeNull();
        }

        #region TestData
        readonly string jsonDataGetPublicSortedByStartDate = "[{\"$id\":\"1\",\"id\":1,\"name\":\"TestItem1\",\"createdBy\":\"Test Author\"," +
            "\"ownerId\":1,\"buyerId\":2,\"description\":null,\"startingPrice\":60.0,\"currentBid\":70.0,\"startSaleDate\":\"2022-07-09T10:00:00\"," +
            "\"endSaleDate\":\"2022-08-10T12:00:00\",\"itemCategories\":[{\"$id\":\"2\",\"itemId\":1,\"item\":null,\"categoryId\":1," +
            "\"category\":{\"$id\":\"3\",\"name\":\"Category1\",\"itemCategories\":null,\"id\":1},\"id\":1}],\"itemPhotos\":null," +
            "\"status\":\"test_Status2\"},{\"$id\":\"4\",\"id\":2,\"name\":\"TestItem2\",\"createdBy\":\"Test Author2\",\"ownerId\":1," +
            "\"buyerId\":2,\"description\":null,\"startingPrice\":60.0,\"currentBid\":70.0,\"startSaleDate\":\"2022-10-09T10:00:00\"," +
            "\"endSaleDate\":\"2022-12-10T12:00:00\",\"itemCategories\":[{\"$id\":\"5\",\"itemId\":2,\"item\":null,\"categoryId\":2," +
            "\"category\":{\"$id\":\"6\",\"name\":\"Category2\",\"itemCategories\":null,\"id\":2},\"id\":2}],\"itemPhotos\":null,\"status\":\"test_Status1\"}]";

        string GetItemPublicInfoJsonData(JsonSerializerSettings serializerSettings)
            => JsonConvert.SerializeObject(GetPublicSortedByStartDate(TestHelper.items), serializerSettings);

        List<ItemPublicInfo> GetPublicSortedByStartDate(List<Item> items) => new List<ItemPublicInfo>() {
            new ItemPublicInfo{
                Id = items[0].Id,
                Name = items[0].Name,
                CreatedBy = items[0].CreatedBy,
                OwnerId = items[0].OwnerId,
                BuyerId = items[0].BuyerId,
                Description = items[0].Description,
                StartingPrice = items[0].StartingPrice,
                CurrentBid = items[0].CurrentBid,
                StartSaleDate = items[0].StartSaleDate,
                EndSaleDate = items[0].EndSaleDate,
                ItemPhotos = items[0].ItemPhotos,
                Status = items[0].Status.Name,
                ItemCategories = items[0].ItemCategories
                                .Select(ic =>
                                {
                                    ic.Item = null;
                                    ic.Category.ItemCategories = null;
                                    return ic;
                                })
                                .ToList()
            },
            new ItemPublicInfo{
                Id = items[1].Id,
                Name = items[1].Name,
                CreatedBy = items[1].CreatedBy,
                OwnerId = items[1].OwnerId,
                BuyerId = items[1].BuyerId,
                Description = items[1].Description,
                StartingPrice = items[1].StartingPrice,
                CurrentBid = items[1].CurrentBid,
                StartSaleDate = items[1].StartSaleDate,
                EndSaleDate = items[1].EndSaleDate,
                ItemPhotos = items[1].ItemPhotos,
                Status = items[1].Status.Name,
                ItemCategories = items[1].ItemCategories
                                .Select(ic =>
                                {
                                    ic.Item = null;
                                    ic.Category.ItemCategories = null;
                                    return ic;
                                })
                                .ToList()
            }
        };

        #endregion
    }
}