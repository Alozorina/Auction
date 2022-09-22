using BLL.Models;
using DAL.Entities;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
        static List<Item> _clonedItems = new List<Item>(TestHelper.items);
        string _publicInfoItems;

        [SetUp]
        public async Task Setup()
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
            _token = await TestHelper.GenerateToken(_client, "admin");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            if (_serializerSettings == null || _publicInfoItems == null)
            {
                //arrange
                _serializerSettings = TestHelper.GetSerializerSettings();
                _publicInfoItems = JsonConvert.SerializeObject(expectedResult_GetPublicSortedByStartDate, _serializerSettings);
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
            TestHelper.EmptyArrayResponseHandler(responseJson).Should().BeEquivalentTo(_publicInfoItems);
        }

        [Test]
        public async Task GetLotsByUserId_ReturnsLotsOfSpecificUser()
        {
            // act
            var httpResponse = await _client.GetAsync(RequestUri + "lots/user=1");

            // assert
            httpResponse.EnsureSuccessStatusCode();
            string responseJson = await httpResponse.Content.ReadAsStringAsync();
            TestHelper.EmptyArrayResponseHandler(responseJson).Should().BeEquivalentTo(_publicInfoItems);
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
            TestHelper.EmptyArrayResponseHandler(responseJson).Should().BeEquivalentTo(_publicInfoItems);
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

        #region TestData
        static readonly List<ItemPublicInfo> expectedResult_GetPublicSortedByStartDate = new List<ItemPublicInfo>() {
            new ItemPublicInfo{
                Id = _clonedItems[0].Id,
                Name = _clonedItems[0].Name,
                CreatedBy = _clonedItems[0].CreatedBy,
                OwnerId = _clonedItems[0].OwnerId,
                BuyerId = _clonedItems[0].BuyerId,
                Description = _clonedItems[0].Description,
                StartingPrice = _clonedItems[0].StartingPrice,
                CurrentBid = _clonedItems[0].CurrentBid,
                StartSaleDate = _clonedItems[0].StartSaleDate,
                EndSaleDate = _clonedItems[0].EndSaleDate,
                ItemPhotos = _clonedItems[0].ItemPhotos,
                Status = _clonedItems[0].Status.Name,
                ItemCategories = _clonedItems[0].ItemCategories
                                .Select(ic =>
                                {
                                    ic.Item = null;
                                    ic.Category.ItemCategories = null;
                                    return ic;
                                })
                                .ToList()
            },
            new ItemPublicInfo{
                Id = _clonedItems[1].Id,
                Name = _clonedItems[1].Name,
                CreatedBy = _clonedItems[1].CreatedBy,
                OwnerId = _clonedItems[1].OwnerId,
                BuyerId = _clonedItems[1].BuyerId,
                Description = _clonedItems[1].Description,
                StartingPrice = _clonedItems[1].StartingPrice,
                CurrentBid = _clonedItems[1].CurrentBid,
                StartSaleDate = _clonedItems[1].StartSaleDate,
                EndSaleDate = _clonedItems[1].EndSaleDate,
                ItemPhotos = _clonedItems[1].ItemPhotos,
                Status = _clonedItems[1].Status.Name,
                ItemCategories = _clonedItems[1].ItemCategories
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